using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICMSX;
using System.Dynamic;

namespace CMSXBLL.Repositorio
{
    public class RoteiroRepositorio : BaseRepositorio,IRoteiroRepositorio
    {
        private IRoteiroDAL dal;

        public void MakeConnection(dynamic prop)
        {
            dal = container.Resolve<IRoteiroDAL>();
            string bc = prop.banco;
            int parm = prop.parms;
            dal.MakeConnection((ExpandoObject)prop);
        }

        public List<RoteiroBLL> Helper(DataTable roteiroData)
        {
            if (roteiroData == null) return null;
            List<RoteiroBLL> rotLista = new List<RoteiroBLL>();
             
            foreach (DataRow dr in roteiroData.Rows)
            {
                RoteiroBLL _rot = RoteiroBLL.ObtemRoteiro();
                _rot.idRoteiro = int.Parse(dr["id_roteiro"].ToString());
                _rot.textoRoteiro = dr["roteiro"].ToString();
                _rot.tipoRoteiro = dr["tipo_roteiro"].ToString();
                _rot.fornecedor = dr["forn"].ToString();
                rotLista.Add(_rot);
            }
            return rotLista;
        }

        public List<RoteiroBLL> ListaRoteiroPorCidade()
        {

            List<RoteiroBLL> rotLista = new List<RoteiroBLL>();

            foreach (DataRow dr in dal.ListaRoteirosPorCidade().Rows)
            {
                RoteiroBLL _rot = RoteiroBLL.ObtemRoteiro();
                _rot.cidade = dr["cidade"].ToString();
                _rot.idCidade = int.Parse(dr["id_cidade"].ToString());
                _rot.imagem = dr["img1a"].ToString();
                rotLista.Add(_rot);
            }
            return rotLista;
        }

        public List<RoteiroBLL> ListaRoteiroPorFornecedor(int fornecedor)
        {
            List<RoteiroBLL> rotLista = new List<RoteiroBLL>();

            foreach (DataRow dr in dal.ListaRoteirosPorFornecedor().Rows)
            {
                RoteiroBLL _rot = RoteiroBLL.ObtemRoteiro();
                _rot.fornecedor = dr["fornecedor"].ToString();
                _rot.idFornecedor = int.Parse(dr["idfornecedor"].ToString());
                rotLista.Add(_rot);
            }
            return rotLista;
        }

        public List<RoteiroBLL> ListaDetalheRoteiro()
        {
            List<RoteiroBLL> rotLista = new List<RoteiroBLL>();

            foreach (DataRow dr in dal.ListaDetalheRoteiro().Rows)
            {
                RoteiroBLL _rot = RoteiroBLL.ObtemRoteiro();
                //roteiro,id_tabrot,cidorigem,id_cidorig
                _rot.idTabrot = int.Parse(dr["id_tabrot"].ToString());
                _rot.textoRoteiro = dr["roteiro"].ToString();
                _rot.cidadeorigem = dr["cidorigem"].ToString();
                _rot.idCidOrig = int.Parse(dr["id_cidorig"].ToString());
                _rot.chaveId = dr["chave"].ToString();
                rotLista.Add(_rot);
            }
            return rotLista;
        }

        public List<RoteiroBLL> ListaRoteiros(int Aplicacao)
        {
            return Helper(dal.ListaRoteiros());
        }
    }
}
