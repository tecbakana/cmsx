using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Caterium
{
    public string Cateriaid { get; set; } = null!;

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public int? Tipocateria { get; set; }

    public string? Cateriaidpai { get; set; }

    public string? Aplicacaoid { get; set; }
}
