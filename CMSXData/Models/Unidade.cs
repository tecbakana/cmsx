using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Unidade
{
    public Guid Unidadeid { get; set; }

    public string? Nome { get; set; }

    public string? Sigla { get; set; }
}
