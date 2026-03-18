using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Tipocotacao
{
    public string Tipocotacaoid { get; set; } = null!;

    public string? Nome { get; set; }

    public string? Descricao { get; set; }
}
