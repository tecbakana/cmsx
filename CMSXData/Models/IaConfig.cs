namespace CMSXData.Models;

public class IaConfig
{
    public string Aplicacaoid { get; set; } = null!;
    public string? Provedor { get; set; }
    public string? Apikey { get; set; }
    public string? Modelo { get; set; }
    public int? LimiteDiario { get; set; }
}
