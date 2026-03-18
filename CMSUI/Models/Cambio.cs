using System;
using System.Collections.Generic;

namespace CMSUI.Models;

public partial class Cambio
{
    public string Cambiogroupid { get; set; } = null!;

    public DateTime? Datacotacao { get; set; }

    public string? Moedasxml { get; set; }

    public short? Tipo { get; set; }
}
