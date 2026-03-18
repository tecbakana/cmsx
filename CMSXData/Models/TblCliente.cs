using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class TblCliente
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public int? Idade { get; set; }
}
