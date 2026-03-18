using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Formularionew
{
    public int Idform { get; set; }

    public string? Nome { get; set; }

    public int? Tipo { get; set; }

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public string? Texto { get; set; }

    public int? Ativo { get; set; }

    public string? Formularioid { get; set; }
}
