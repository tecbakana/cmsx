using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Informativo
{
    public string Infoid { get; set; } = null!;

    public string? Titulo { get; set; }

    public DateTime? Data { get; set; }

    public string? Texto { get; set; }

    public string? Foto { get; set; }

    public short? Ativo { get; set; }

    public string? Tipoenvio { get; set; }
}
