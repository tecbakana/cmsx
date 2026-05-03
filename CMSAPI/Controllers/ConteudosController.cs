using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CMSXData.Models;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ConteudosController : Controller
    {
        private readonly CmsxDbContext _context;
        public ConteudosController(CmsxDbContext context) { _context = context; }

        private (bool acessoTotal, string? aplicacaoid) UserContext() =>
            (User.FindFirstValue("acessoTotal") == "True", User.FindFirstValue("aplicacaoid"));

        [HttpGet]
        public IEnumerable<Conteudo> Get([FromQuery] string? areaid = null, [FromQuery] string? cateriaid = null, [FromQuery] string? aplicacaoid = null)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var q = _context.Conteudos.AsQueryable();

            if (!acessoTotal)
            {
                var areasIds = _context.Areas
                    .Where(a => a.Aplicacaoid == claimAppId)
                    .Select(a => a.Areaid)
                    .ToHashSet();
                q = q.Where(c => c.Areaid != null && areasIds.Contains(c.Areaid));
            }
            else if (!string.IsNullOrEmpty(aplicacaoid))
            {
                var areasIds = _context.Areas
                    .Where(a => a.Aplicacaoid == aplicacaoid)
                    .Select(a => a.Areaid)
                    .ToHashSet();
                q = q.Where(c => c.Areaid != null && areasIds.Contains(c.Areaid));
            }

            if (!string.IsNullOrEmpty(areaid))
                q = q.Where(c => c.Areaid == areaid);
            if (!string.IsNullOrEmpty(cateriaid))
                q = q.Where(c => c.Cateriaid == cateriaid);

            return q.OrderByDescending(c => c.Datainclusao).ToArray();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Conteudos.Find(id);
            if (item == null) return NotFound();
            if (!acessoTotal)
            {
                var area = _context.Areas.Find(item.Areaid);
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }
            return Ok(item);
        }

        public class NovoConteudoDto
        {
            public string? Titulo { get; set; }
            public string? Texto { get; set; }
            public string? Autor { get; set; }
            public string? Areaid { get; set; }
            public string? Cateriaid { get; set; }
            public DateTime? Datafinal { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] NovoConteudoDto dto)
        {
            var (acessoTotal, claimAppId) = UserContext();
            if (!acessoTotal && !string.IsNullOrEmpty(dto.Areaid))
            {
                var area = _context.Areas.Find(dto.Areaid);
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }

            var item = new Conteudo
            {
                Conteudoid   = Guid.NewGuid().ToString(),
                Titulo       = dto.Titulo,
                Texto        = dto.Texto,
                Autor        = dto.Autor,
                Areaid       = dto.Areaid,
                Cateriaid    = dto.Cateriaid,
                Datafinal    = dto.Datafinal,
                Datainclusao = DateTime.UtcNow
            };
            _context.Conteudos.Add(item);
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Conteudo item)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var existing = _context.Conteudos.Find(id);
            if (existing == null) return NotFound();
            if (!acessoTotal)
            {
                var area = _context.Areas.Find(existing.Areaid);
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }
            existing.Titulo    = item.Titulo;
            existing.Texto     = item.Texto;
            existing.Autor     = item.Autor;
            existing.Cateriaid = item.Cateriaid;
            existing.Areaid    = item.Areaid;
            existing.Datafinal = item.Datafinal;
            _context.SaveChanges();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Conteudos.Find(id);
            if (item == null) return NotFound();
            if (!acessoTotal)
            {
                var area = _context.Areas.Find(item.Areaid);
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }
            _context.Conteudos.Remove(item);
            _context.SaveChanges();
            return Ok();
        }
    }
}
