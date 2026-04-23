namespace CMSXData.Models;

public class IaCache
{
    public string Cacheid { get; set; } = null!;
    public string Hash { get; set; } = null!;
    public string Resultado { get; set; } = null!;
    public DateTime Datainclusao { get; set; }
    public DateTime Datavencimento { get; set; }
}
