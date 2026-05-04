using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

const string SQL_CONN = "Server=localhost\\SQLEXPRESS;Database=cmsxDB;Trusted_Connection=true;TrustServerCertificate=true";
const string PG_CONN  = "Host=localhost;Port=15432;Database=cmsxdb;Username=multiplai_us;Password=P190c@#2026**";

Console.WriteLine("=== DataMigration: SQL Server → PostgreSQL ===\n");

// 1. Listar tabelas do SQL Server
var sqlTables = new List<string>();
using (var sqlConn = new SqlConnection(SQL_CONN))
{
    sqlConn.Open();
    using var cmd = new SqlCommand(
        "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME",
        sqlConn);
    using var reader = cmd.ExecuteReader();
    while (reader.Read()) sqlTables.Add(reader.GetString(0));
}
Console.WriteLine($"Tabelas encontradas no SQL Server: {sqlTables.Count}");
foreach (var t in sqlTables) Console.WriteLine($"  - {t}");
Console.WriteLine();

// 2. Listar tabelas do PostgreSQL
var pgTables = new List<string>();
using (var pgConn = new NpgsqlConnection(PG_CONN))
{
    pgConn.Open();
    using var cmd = new NpgsqlCommand(
        "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE' ORDER BY table_name",
        pgConn);
    using var reader = cmd.ExecuteReader();
    while (reader.Read()) pgTables.Add(reader.GetString(0));
}
Console.WriteLine($"Tabelas encontradas no PostgreSQL: {pgTables.Count}");
Console.WriteLine();

// 3. Mapear tabelas (case-insensitive)
var mapeamento = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
foreach (var sqlTable in sqlTables)
{
    var pgMatch = pgTables.FirstOrDefault(pg => pg.Equals(sqlTable, StringComparison.OrdinalIgnoreCase));
    if (pgMatch != null) mapeamento[sqlTable] = pgMatch;
}

Console.WriteLine($"Tabelas mapeadas (SQL Server → PostgreSQL): {mapeamento.Count}");
var naoMapeadas = sqlTables.Except(mapeamento.Keys, StringComparer.OrdinalIgnoreCase).ToList();
if (naoMapeadas.Any())
{
    Console.WriteLine("Tabelas sem correspondência (serão ignoradas):");
    foreach (var t in naoMapeadas) Console.WriteLine($"  ! {t}");
}
Console.WriteLine();

// 4. Migrar cada tabela
int totalTabelas = 0, totalRegistros = 0;

using var pgConnMain = new NpgsqlConnection(PG_CONN);
pgConnMain.Open();

// Desabilitar FK checks no PostgreSQL
using (var cmd = new NpgsqlCommand("SET session_replication_role = 'replica'", pgConnMain))
    cmd.ExecuteNonQuery();

Console.WriteLine("FK constraints desabilitados temporariamente.\n");

foreach (var (sqlTable, pgTable) in mapeamento)
{
    DataTable dt = new();
    using (var sqlConn = new SqlConnection(SQL_CONN))
    {
        sqlConn.Open();
        using var adapter = new SqlDataAdapter($"SELECT * FROM [{sqlTable}]", sqlConn);
        adapter.Fill(dt);
    }

    if (dt.Rows.Count == 0)
    {
        Console.WriteLine($"  {sqlTable} → vazia, pulando.");
        continue;
    }

    // Obter colunas do PostgreSQL para mapear corretamente
    var pgColumns = new List<string>();
    using (var cmd = new NpgsqlCommand(
        $"SELECT column_name FROM information_schema.columns WHERE table_name = '{pgTable}' AND table_schema = 'public' ORDER BY ordinal_position",
        pgConnMain))
    using (var reader = cmd.ExecuteReader())
        while (reader.Read()) pgColumns.Add(reader.GetString(0));

    // Mapear colunas SQL Server → PostgreSQL (case-insensitive)
    var colMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    foreach (DataColumn col in dt.Columns)
    {
        var pgCol = pgColumns.FirstOrDefault(c => c.Equals(col.ColumnName, StringComparison.OrdinalIgnoreCase));
        if (pgCol != null) colMap[col.ColumnName] = pgCol;
    }

    if (!colMap.Any())
    {
        Console.WriteLine($"  {sqlTable} → nenhuma coluna mapeada, pulando.");
        continue;
    }

    var sqlCols = colMap.Keys.ToList();
    var pgCols  = colMap.Values.ToList();
    var colNames = string.Join(", ", pgCols.Select(c => $"\"{c}\""));
    var paramNames = string.Join(", ", pgCols.Select((_, i) => $"@p{i}"));
    var insertSql = $"INSERT INTO \"{pgTable}\" ({colNames}) VALUES ({paramNames}) ON CONFLICT DO NOTHING";

    int inseridos = 0;
    using var tx = pgConnMain.BeginTransaction();
    try
    {
        foreach (DataRow row in dt.Rows)
        {
            using var cmd = new NpgsqlCommand(insertSql, pgConnMain, tx);
            for (int i = 0; i < sqlCols.Count; i++)
            {
                var val = row[sqlCols[i]];
                cmd.Parameters.AddWithValue($"@p{i}", val == DBNull.Value ? DBNull.Value : ConvertValue(val));
            }
            cmd.ExecuteNonQuery();
            inseridos++;
        }
        tx.Commit();
        Console.WriteLine($"  ✓ {sqlTable} → {pgTable}: {inseridos} registros");
        totalRegistros += inseridos;
        totalTabelas++;
    }
    catch (Exception ex)
    {
        tx.Rollback();
        Console.WriteLine($"  ✗ {sqlTable} → ERRO: {ex.Message}");
    }
}

// Reabilitar FK checks
using (var cmd = new NpgsqlCommand("SET session_replication_role = 'origin'", pgConnMain))
    cmd.ExecuteNonQuery();

Console.WriteLine($"\n=== Concluído: {totalTabelas} tabelas, {totalRegistros} registros migrados ===");

static object ConvertValue(object val)
{
    return val switch
    {
        // SQL Server uniqueidentifier → Guid (Npgsql aceita nativamente)
        Guid g => g,
        // SQL Server bit → bool
        bool b => b,
        // Decimais
        decimal d => d,
        // Datas — garantir UTC para PostgreSQL timestamptz
        DateTime dt => DateTime.SpecifyKind(dt, DateTimeKind.Utc),
        DateTimeOffset dto => dto,
        // Demais tipos passam direto
        _ => val
    };
}
