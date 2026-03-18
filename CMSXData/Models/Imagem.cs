using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Imagem
{
    public string Imagemid { get; set; } = null!;

    public string? Url { get; set; }

    public int? Largura { get; set; }

    public int? Altura { get; set; }

    public string? Areaid { get; set; }

    public string? Conteudoid { get; set; }

    public string? Descricao { get; set; }

    public string Parentid { get; set; } = null!;

    public string Tipoid { get; set; } = null!;
}
