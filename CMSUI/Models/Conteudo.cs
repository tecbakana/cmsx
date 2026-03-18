using System;
using System.Collections.Generic;

namespace CMSUI.Models;

public partial class Conteudo
{
    public string Conteudoid { get; set; } = null!;

    public string? Areaid { get; set; }

    public string? Autor { get; set; }

    public string? Titulo { get; set; }

    public string? Texto { get; set; }

    public DateTime? Datainclusao { get; set; }

    public DateTime? Datafinal { get; set; }

    public string? Cateriaid { get; set; }
}
