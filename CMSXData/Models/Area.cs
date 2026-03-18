using System;
using System.Collections.Generic;

namespace CMSXData.Models;

public partial class Area
{
    public string Areaid { get; set; } = null!;

    public string? Aplicacaoid { get; set; }

    public string? Nome { get; set; }

    public string? Url { get; set; }

    public string? Descricao { get; set; }

    public string? Areaidpai { get; set; }

    public DateTime? Datainicial { get; set; }

    public DateTime? Datafinal { get; set; }

    public short? Imagem { get; set; }

    public short? Menulateral { get; set; }

    public short? Menusplash { get; set; }

    public short? Menucentral { get; set; }

    public int? Posicao { get; set; }

    public short? Menufixo { get; set; }

    public short? Listasimples { get; set; }

    public short? Listasplash { get; set; }

    public short? Listabanner { get; set; }

    public int? Tipoarea { get; set; }
    public string? Layout { get; set; }
}
