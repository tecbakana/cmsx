using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Formulario
{
    public string Formularioid { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? Valor { get; set; }

    public bool? Ativo { get; set; }

    public DateTime? Datainclusao { get; set; }

    public string? Areaid { get; set; }

    public string? Categoriaid { get; set; }
}
