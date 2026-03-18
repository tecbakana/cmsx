using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMSXBLL
{
    public class RoteiroBLL
    {
        #region PROPERTIES
        public int idRoteiro { get; set; }
        public int idCidade { get; set; }
        public int idFornecedor { get; set; }
        public int idTabrot { get; set; }
        public int idCidOrig { get; set; }
        public string chaveId { get; set; }
        public string textoRoteiro { get; set; }
        public string tipoRoteiro { get; set; }
        public string fornecedor { get; set; }
        public string cidade { get; set; }
        public string imagem { get; set; }
        public string cidadeorigem { get; set; }

        #endregion

        public static RoteiroBLL ObtemRoteiro()
        {
            return new RoteiroBLL();
        }
    }
}
