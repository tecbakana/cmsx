using CMSAPI.Services.Agents;
using ICMSX;

namespace CMSAPI.Services;

public class AgentIAFactory : IAgentIAFactory
{
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpFactory;

    public AgentIAFactory(IConfiguration config, IHttpClientFactory httpFactory)
    {
        _config = config;
        _httpFactory = httpFactory;
    }

    public IAgentIA Criar(string? provedor = null, string? tenantApiKey = null, string? tenantModelo = null)
    {
        var alvo = (provedor ?? _config["AgentIA:Default"] ?? "gemini").ToLowerInvariant();

        return alvo switch
        {
            "gemini" => new GeminiAgent(
                tenantApiKey ?? ChaveOuErro("AgentIA:Gemini:ApiKey", "Gemini"),
                tenantModelo ?? _config["AgentIA:Gemini:Model"] ?? "gemini-2.0-flash",
                _config["AgentIA:Gemini:BaseUrl"] ?? "https://generativelanguage.googleapis.com/v1beta/models",
                _httpFactory),
            "anthropic" => new AnthropicAgent(
                tenantApiKey ?? ChaveOuErro("AgentIA:Anthropic:ApiKey", "Anthropic"),
                tenantModelo ?? _config["AgentIA:Anthropic:Model"] ?? "claude-haiku-4-5-20251001",
                _config["AgentIA:Anthropic:Url"] ?? "https://api.anthropic.com/v1/messages",
                _httpFactory),
            _ => throw new NotSupportedException($"Provedor de IA desconhecido: '{alvo}'. Use 'anthropic' ou 'gemini'.")
        };
    }

    private string ChaveOuErro(string path, string nome)
    {
        var key = _config[path];
        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException($"ApiKey do {nome} não configurada em appsettings.json (caminho: {path}).");
        return key;
    }
}
