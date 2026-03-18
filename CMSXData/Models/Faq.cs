using System;

namespace CMSXData.Models;

public partial class Faq
{
    public string Faqid { get; set; } = null!;

    public string Formularioid { get; set; } = null!;

    public string Pergunta { get; set; } = null!;

    public string Resposta { get; set; } = null!;

    public int Ordem { get; set; }

    public bool Ativo { get; set; }

    public DateTime Datainclusao { get; set; }
}
