using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroGenerico
{
	public class FormCadastroModulo : IForm<FormCadastroModulo>
	{
		public string FormularioId { get; set; }
		public List<FormCampo> campos { get; set; }=new List<FormCampo>();
		public bool Ativo { get; set; }
		public DateTime DataInclusao { get; set; }
		public string Areaid { get; set; }
	}

}
