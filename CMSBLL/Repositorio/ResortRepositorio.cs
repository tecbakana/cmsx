using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICMS;
using System.Dynamic;

namespace CMSBLL.Repositorio
{
    public class ResortRepositorio : BaseRepositorio,IResortRepositorio
    {
        private IResortDAL dal;

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IResortDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public List<ResortBLL> Helper(DataTable resortData)
        {
            if (resortData == null) return null;
            List<ResortBLL> resLista = new List<ResortBLL>();

            foreach (DataRow dr in resortData.Rows)
            {
                ResortBLL _res = ResortBLL.ObterNovoResort();
                _res.Titulo = dr["titulo"].ToString();
                _res.Texto = dr["subtitulo"].ToString();
                //_res.idMkt = int.Parse(dr["idMkt"].ToString());
                _res.Acomodacoes = dr["acomodacoes"].ToString();
                _res.Regime = dr["regime"].ToString();
                resLista.Add(_res);
            }
            return resLista;
        }

        public List<ResortBLL> ListaResorts()
        {
            return Helper(dal.DetalheResort());
        }

    }
}
