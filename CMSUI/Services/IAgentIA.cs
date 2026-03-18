namespace CMSUI.Services;

public interface IAgentIA
{
    string Provedor { get; }
    Task<string> GerarAsync(string prompt);
    Task<string> GerarComImagemAsync(byte[] imageBytes, string mimeType, string prompt);
}
