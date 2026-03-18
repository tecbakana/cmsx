using System;
using System.Collections.Generic;

namespace CMSUI.Models;

public partial class Infofoto
{
    public string Fotoid { get; set; } = null!;

    public string? Fotourl { get; set; }

    public string? Descricao { get; set; }

    public string Cateriaid { get; set; } = null!;
}
