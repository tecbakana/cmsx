using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Usuario
{
    public string Nome { get; set; } = null!;

    public string Sobrenome { get; set; } = null!;

    public string? Apelido { get; set; }

    public string? Senha { get; set; }

    public short? Ativo { get; set; }

    public DateTime? Datainclusao { get; set; }

    public Guid? Userid { get; set; }
}
