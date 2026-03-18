using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CMSUI.Models;

namespace CMSUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly CmsxDbContext _context;
        public DashboardController(CmsxDbContext context) { _context = context; }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var acessoTotal = User.FindFirstValue("acessoTotal") == "True";
            var aplicacaoid = User.FindFirstValue("aplicacaoid");

            if (acessoTotal)
            {
                return Ok(new
                {
                    usuarios   = _context.Usuarios.Count(),
                    aplicacoes = _context.Aplicacaos.Count(),
                    conteudos  = _context.Conteudos.Count(),
                    areas      = _context.Areas.Count(),
                    categorias = _context.Cateria.Count(),
                    modulos    = _context.Modulos.Count()
                });
            }

            var areasIds = _context.Areas
                .Where(a => a.Aplicacaoid == aplicacaoid)
                .Select(a => a.Areaid)
                .ToHashSet();

            return Ok(new
            {
                usuarios   = _context.Relusuarioaplicacaos.Count(r => r.Aplicacaoid == aplicacaoid),
                aplicacoes = _context.Aplicacaos.Count(a => a.Aplicacaoid == aplicacaoid),
                conteudos  = _context.Conteudos.Count(c => c.Areaid != null && areasIds.Contains(c.Areaid)),
                areas      = areasIds.Count,
                categorias = _context.Cateria.Count(c => c.Aplicacaoid == aplicacaoid),
                modulos    = _context.Relmoduloaplicacaos.Count(r => r.Aplicacaoid == aplicacaoid)
            });
        }
    }
}
