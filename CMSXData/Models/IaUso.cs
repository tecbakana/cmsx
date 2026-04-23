namespace CMSXData.Models;

public class IaUso
{
    public string Usoid { get; set; } = null!;
    public string Aplicacaoid { get; set; } = null!;
    public DateOnly Data { get; set; }
    public int Contador { get; set; }
}
