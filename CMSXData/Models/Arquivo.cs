using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Arquivo
{
    public string Arquivoid { get; set; } = null!;

    public string? Areaid { get; set; }

    public string? Conteudoid { get; set; }

    public string? Nome { get; set; }

    public string? Tipoid { get; set; }
}
