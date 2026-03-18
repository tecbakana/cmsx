using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Ciaaerea
{
    public string Ciaaereaid { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Lotipo { get; set; }

    public string? DescricaoLonga { get; set; }

    public short? Ativo { get; set; }

    public short? Tiponac { get; set; }

    public short? Tipoint { get; set; }

    public string? WebticketStr { get; set; }
}
