namespace ICMSX;

public interface IAgentIAFactory
{
    IAgentIA Criar(string? provedor = null, string? tenantApiKey = null, string? tenantModelo = null);
}
