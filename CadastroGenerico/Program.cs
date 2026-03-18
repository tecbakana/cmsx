using System;

namespace CadastroGenerico
{
    public class Program
    {
        public static void Main()
        {
            FormCadastroModulo frm = new FormCadastroModulo();
            frm.Ativo = true;
            frm.FormularioId = new Guid().ToString();
            frm.campos.Add(new FormCampo() { Nome = "formulario1", Valor = "string" });
        }
    }
}