using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CMSUI.Models;

namespace CMSUI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PedidosController : Controller
{
    private readonly CmsxDbContext _context;

    public PedidosController(CmsxDbContext context)
    {
        _context = context;
    }

    private (bool acessoTotal, string? aplicacaoid) UserContext() =>
        (User.FindFirstValue("acessoTotal") == "True", User.FindFirstValue("aplicacaoid"));

    [HttpGet]
    public IActionResult Get([FromQuery] string? aplicacaoid = null, [FromQuery] string? status = null)
    {
        var (acessoTotal, claimAppId) = UserContext();
        var q = _context.Pedidos.AsQueryable();

        if (acessoTotal)
        {
            if (!string.IsNullOrEmpty(aplicacaoid))
                q = q.Where(p => p.Aplicacaoid == aplicacaoid);
        }
        else
        {
            q = q.Where(p => p.Aplicacaoid == claimAppId);
        }

        if (!string.IsNullOrEmpty(status))
            q = q.Where(p => p.Statusatual == status);

        var resultado = q
            .OrderByDescending(p => p.Datainclusao)
            .Select(p => new
            {
                p.Pedidoid,
                p.Aplicacaoid,
                p.Numeropedido,
                p.Clientenome,
                p.Clienteemail,
                p.Valorpedido,
                p.Statusatual,
                p.Datainclusao
            })
            .ToArray();

        return Ok(resultado);
    }

    [HttpGet("{id}/timeline")]
    public IActionResult GetTimeline(Guid id)
    {
        var (acessoTotal, claimAppId) = UserContext();
        var pedido = _context.Pedidos.FirstOrDefault(p => p.Pedidoid == id);
        if (pedido is null) return NotFound();
        if (!acessoTotal && pedido.Aplicacaoid != claimAppId) return Forbid();

        var timeline = _context.Statuspedidos
            .Where(s => s.Pedidoid == id)
            .OrderBy(s => s.Datahora)
            .Select(s => new
            {
                s.Statuspedidoid,
                s.Status,
                s.Descricao,
                s.Datahora
            })
            .ToArray();

        return Ok(timeline);
    }
}
