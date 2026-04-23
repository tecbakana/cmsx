using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CMSXData.Models;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ModulosController : Controller
    {
        private readonly CmsxDbContext _context;
        public ModulosController(CmsxDbContext context) { _context = context; }

        private (bool acessoTotal, string? aplicacaoid) UserContext() =>
            (User.FindFirstValue("acessoTotal") == "True", User.FindFirstValue("aplicacaoid"));

        [HttpGet]
        public IEnumerable<Modulo> Get([FromQuery] string? usuarioid = null)
        {
            var (acessoTotal, claimAppId) = UserContext();

            // Filtro por usuário específico (para montar menu)
            if (!string.IsNullOrEmpty(usuarioid))
            {
                return _context.Relmodulousuarios
                    .Where(r => r.Usuarioid == usuarioid)
                    .Join(_context.Modulos, r => r.Moduloid, m => m.Moduloid, (r, m) => m)
                    .OrderBy(m => m.Posicao)
                    .ToArray();
            }

            // Admin: todos os módulos
            if (acessoTotal)
                return _context.Modulos.OrderBy(m => m.Posicao).ToArray();

            // Tenant: apenas módulos vinculados à sua aplicação
            return _context.Relmoduloaplicacaos
                .Where(r => r.Aplicacaoid == claimAppId)
                .AsEnumerable()
                .Join(_context.Modulos.AsEnumerable(), r => r.Moduloid, m => m.Moduloid, (r, m) => m)
                .OrderBy(m => m.Posicao)
                .ToArray();
        }
    }
}
