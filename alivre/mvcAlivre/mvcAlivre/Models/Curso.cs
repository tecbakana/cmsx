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
    
    public partial class Curso
    {
        public Curso()
        {
            this.Turma = new HashSet<Turma>();
        }
    
        public short NumInstituicao { get; set; }
        public short NumCampus { get; set; }
        public short NumCurso { get; set; }
        public string NomeCurso { get; set; }
    
        public virtual Campus Campus { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }
    }
}