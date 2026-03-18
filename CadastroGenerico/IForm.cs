using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroGenerico
{
	public class FormCampo
	{ 
		public string Nome { get; set; }
		public string Valor { get; set; }
	}
	public interface IForm<T> where T : IForm<T>
	{
		public string FormularioId { get; set; }
		public List<FormCampo> campos { get; set; }
		public bool Ativo { get; set; }
		public DateTime DataInclusao { get; set; }
		public string Areaid { get; set; }
	}
}
