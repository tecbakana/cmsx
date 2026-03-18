using System;
using System.Collections.Generic;

namespace CMSUI.Models;

public partial class Atributo
{
    public Guid Atributoid { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public string? Produtoid { get; set; }
}
