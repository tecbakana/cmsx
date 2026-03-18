using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMSXBLL
{
    public class UsuarioBLL
    {
        public Guid? UserId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Apelido { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Aplicacao { get; set; }
        public string Template { get; set; }
        public Guid? AplicacaoId { get; set; }
        public DateTime? DataInclusao { get; set; }

        public static UsuarioBLL ObterNovoUsuario()
        {
            return new UsuarioBLL();
        }
    }
}
