namespace CMSUI.Models;

public partial class Usuario
{
    public string Userid { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string Sobrenome { get; set; } = null!;
    public string? Apelido { get; set; }
    public string? Senha { get; set; }
    public byte? Ativo { get; set; }
    public DateTime? Datainclusao { get; set; }
}
