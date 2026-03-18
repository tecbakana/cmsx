using System;
using System.Collections.Generic;

namespace CMSUI.Models;

public partial class DictTemplate
{
    public string Idtemplate { get; set; } = null!;

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public string? Viewrelacionada { get; set; }
}
