﻿using System;
using System.Collections.Generic;

namespace CMSUI.Models;

public partial class Moedum
{
    public string Moedaid { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? Sigla { get; set; }
}