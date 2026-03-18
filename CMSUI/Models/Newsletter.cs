using System;
using System.Collections.Generic;

namespace CMSUI.Models;

public partial class Newsletter
{
    public string Newsid { get; set; } = null!;

    public string? Titulo { get; set; }

    public string? Autor { get; set; }

    public DateTime? Data { get; set; }

    public short? Frente { get; set; }

    public string? Texto { get; set; }

    public string? Foto { get; set; }

    public string? Cateriaid { get; set; }

    public short? Ativo { get; set; }
}
