//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mvcAlivre.Models
{
    using System;
    
    public partial class Filmes_Semana_Result
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Chamada { get; set; }
        public string Genero { get; set; }
        public string Diretor { get; set; }
        public Nullable<int> Ano { get; set; }
        public Nullable<int> Duracao { get; set; }
        public string Sinopse { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> DtCad { get; set; }
        public Nullable<int> QtdeExib { get; set; }
        public Nullable<System.DateTime> DtExibAte { get; set; }
        public Nullable<System.DateTime> DtExibIni { get; set; }
        public Nullable<int> Semana { get; set; }
        public string Imagem { get; set; }
        public Nullable<int> Largura { get; set; }
        public Nullable<int> Altura { get; set; }
        public string FilmeUrl { get; set; }
        public Nullable<int> TipoUrl { get; set; }
    }
}