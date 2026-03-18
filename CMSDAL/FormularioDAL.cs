using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICMS;

namespace CMSDAL
{
    public class FormularioDAL : IFormularioDAL
    {
        #region IFormularioDAL Members

        private IDbConnection conn;
        private IDbCommand cmd;
        private IDataParameter[] parms;
        private IDataFactory _factory;
        public int NumParms { get; set; }
        private dynamic _localProps;

        public FormularioDAL(IDataFactory factory)
        {
            _factory = factory;
        }

        //formularioid,nome,valor,ativo,datainclusao
        public void CriaFormulario()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO formulario(formularioid,areaid,nome,valor,ativo,datainclusao) VALUES(@formularioid,@areaid,@nome,@valor,1,@data)";

            parms[0].ParameterName = "@formularioid";
            parms[0].Value = _localProps.formularioid;
            parms[1].ParameterName = "@areaid";
            parms[1].Value = _localProps.areaid;
            parms[2].ParameterName = "@nome";
            parms[2].Value = _localProps.nome;
            parms[3].ParameterName = "@valor";
            parms[3].Value = _localProps.valor;
            parms[4].ParameterName = "@data";
            parms[4].Value = _localProps.data;

            cmd.Parameters.Add(parms[0]);
            cmd.Parameters.Add(parms[1]);
            cmd.Parameters.Add(parms[2]);
            cmd.Parameters.Add(parms[3]);
            cmd.Parameters.Add(parms[4]);

            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (DataException ex)
            {
                throw new DataException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable ListaFormulario()
        {

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT formulario.Formularioid,formulario.nome,formulario.valor,formulario.ativo,formulario.datainclusao,areas.nome AS 'Area'" +
                              "  FROM formulario INNER JOIN areas ON areas.AreaId = formulario.AreaId" +
                              " WHERE formulario.Ativo = 1 AND areas.AplicacaoId = @aplicacaoid";
            parms[0].ParameterName = "@aplicacaoid";
            parms[0].Value = _localProps.aplicacaoid;

            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;

            IDataReader _dr;
            DataTable _dt = new DataTable();

            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("Formularioid"));
            _dt.Columns.Add(new DataColumn("nome"));
            _dt.Columns.Add(new DataColumn("Area"));
            _dt.Columns.Add(new DataColumn("valor"));
            _dt.Columns.Add(new DataColumn("ativo"));
            _dt.Columns.Add(new DataColumn("datainclusao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["Formularioid"] = _dr["Formularioid"].ToString();
                    dr["nome"] = _dr["nome"].ToString();
                    dr["Area"] = _dr["Area"].ToString();
                    dr["valor"] = _dr["valor"].ToString();
                    dr["ativo"] = _dr["ativo"].ToString();
                    dr["datainclusao"] = _dr["datainclusao"].ToString();
                    _dt.Rows.Add(dr);
                }

                _dt.AcceptChanges();
            }
            catch (DataException ex)
            {
                throw new DataException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return _dt;
        }

        public DataTable ListaFormularioPorAreaId()
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Formularioid,nome,valor,ativo,datainclusao" +
                              "  FROM formulario " +
                              " WHERE Ativo = 1 AND areaid = @areaid";

            parms[0].ParameterName = "@areaid";
            parms[0].Value = _localProps.areaid;

            cmd.Parameters.Add(parms[0]);
            cmd.Connection = conn;
            
            IDataReader _dr;
            DataTable _dt = new DataTable();

            //ConteudoId,AreaId,Autor,Titulo,Texto,DataInclusao
            _dt.Columns.Add(new DataColumn("Formularioid"));
            _dt.Columns.Add(new DataColumn("nome"));
            _dt.Columns.Add(new DataColumn("valor"));
            _dt.Columns.Add(new DataColumn("ativo"));
            _dt.Columns.Add(new DataColumn("datainclusao"));

            try
            {
                conn.Open();
                _dr = cmd.ExecuteReader();
                while (_dr.Read())
                {
                    DataRow dr = _dt.NewRow();
                    dr["Formularioid"] = _dr["Formularioid"].ToString();
                    dr["nome"] = _dr["nome"].ToString();
                    dr["valor"] = _dr["valor"].ToString();
                    dr["ativo"] = _dr["ativo"].ToString();
                    dr["datainclusao"] = _dr["datainclusao"].ToString();
                    _dt.Rows.Add(dr);
                }

                _dt.AcceptChanges();
            }
            catch (DataException ex)
            {
                throw new DataException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return _dt;
        }

        public void MakeConnection(dynamic prop)
        {
            var data = _factory.GetConnection(prop.banco, prop.parms);
            conn = data.Key;
            cmd = data.Value.Key;
            parms = data.Value.Value;
            _localProps = prop;
        }

        #endregion
    }
}
