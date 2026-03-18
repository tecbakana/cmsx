using System;
using System.Collections;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Template
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public string? Url { get; set; }

    public BitArray? Ativo { get; set; }
}
