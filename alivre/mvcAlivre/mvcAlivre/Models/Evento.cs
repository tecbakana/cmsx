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
    using System.Collections.Generic;
    
    public partial class Evento
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public short Tipo { get; set; }
        public System.DateTime DtInicio { get; set; }
        public System.DateTime DtFim { get; set; }
        public string Local { get; set; }
        public string Endereco { get; set; }
        public decimal Valor1 { get; set; }
        public string Condicao1 { get; set; }
        public Nullable<decimal> Valor2 { get; set; }
        public string Condicao2 { get; set; }
        public Nullable<decimal> Valor3 { get; set; }
        public string Condicao3 { get; set; }
    }
}