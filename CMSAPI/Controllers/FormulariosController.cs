using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CMSXData.Models;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FormulariosController : Controller
    {
        private readonly CmsxDbContext _context;
        public FormulariosController(CmsxDbContext context) { _context = context; }

        private (bool acessoTotal, string? aplicacaoid) UserContext() =>
            (User.FindFirstValue("acessoTotal") == "True", User.FindFirstValue("aplicacaoid"));

        // ── Definições de formulário ─────────────────────────────────────────

        [HttpGet("defs")]
        public IEnumerable<object> GetDefs([FromQuery] string? areaid = null, [FromQuery] string? aplicacaoid = null)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var q = _context.Formularios.AsQueryable();

            if (!acessoTotal)
            {
                var areasIds = _context.Areas
                    .Where(a => a.Aplicacaoid == claimAppId)
                    .Select(a => a.Areaid)
                    .ToHashSet();
                q = q.Where(f => f.Areaid != null && areasIds.Contains(f.Areaid));
            }
            else if (!string.IsNullOrEmpty(aplicacaoid))
            {
                var areasIds = _context.Areas
                    .Where(a => a.Aplicacaoid == aplicacaoid)
                    .Select(a => a.Areaid)
                    .ToHashSet();
                q = q.Where(f => f.Areaid != null && areasIds.Contains(f.Areaid));
            }

            if (!string.IsNullOrEmpty(areaid))
                q = q.Where(f => f.Areaid == areaid);

            return q.OrderBy(f => f.Nome)
                    .Select(f => new { f.Formularioid, f.Nome, f.Valor, f.Ativo, f.Datainclusao, f.Areaid, f.Categoriaid })
                    .ToArray();
        }

        public class FormularioDefDto
        {
            public string? Nome { get; set; }
            public string? Valor { get; set; }
            public string? Areaid { get; set; }
            public string? Categoriaid { get; set; }
        }

        [HttpPost("defs")]
        public IActionResult PostDef([FromBody] FormularioDefDto dto)
        {
            var (acessoTotal, claimAppId) = UserContext();
            if (!acessoTotal && !string.IsNullOrEmpty(dto.Areaid))
            {
                var area = _context.Areas.Find(dto.Areaid);
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }

            var item = new Formulario
            {
                Formularioid = Guid.NewGuid().ToString(),
                Nome         = dto.Nome ?? "",
                Valor        = dto.Valor,
                Areaid       = dto.Areaid,
                Categoriaid  = dto.Categoriaid,
                Ativo        = true,
                Datainclusao = DateTime.Now
            };
            _context.Formularios.Add(item);
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpPut("defs/{id}")]
        public IActionResult PutDef(string id, [FromBody] FormularioDefDto dto)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Formularios.FirstOrDefault(f => f.Formularioid == id);
            if (item == null) return NotFound();
            if (!acessoTotal)
            {
                var area = _context.Areas.Find(item.Areaid);
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }
            item.Nome       = dto.Nome ?? item.Nome;
            item.Valor      = dto.Valor;
            item.Areaid     = dto.Areaid;
            item.Categoriaid = dto.Categoriaid;
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("defs/{id}")]
        public IActionResult DeleteDef(string id)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Formularios.FirstOrDefault(f => f.Formularioid == id);
            if (item == null) return NotFound();
            if (!acessoTotal)
            {
                var area = _context.Areas.Find(item.Areaid);
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }
            _context.Formularios.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        // ── Submissão pública (sem autenticação) ─────────────────────────────

        [AllowAnonymous]
        [HttpPost("{formularioid}/submit")]
        public IActionResult Submit(string formularioid, [FromBody] Dictionary<string, string> campos)
        {
            var formulario = _context.Formularios.FirstOrDefault(f => f.Formularioid == formularioid);
            if (formulario == null) return NotFound();

            var item = new Formularionew
            {
                Formularioid = formularioid,
                Texto        = System.Text.Json.JsonSerializer.Serialize(campos),
                Nome         = campos.GetValueOrDefault("nome") ?? campos.GetValueOrDefault("Nome"),
                Email        = campos.GetValueOrDefault("email") ?? campos.GetValueOrDefault("Email"),
                Telefone     = campos.GetValueOrDefault("telefone") ?? campos.GetValueOrDefault("Telefone"),
                Ativo        = 1
            };
            _context.Formularionews.Add(item);
            _context.SaveChanges();
            return Ok();
        }

        // ── Respostas ────────────────────────────────────────────────────────

        [HttpGet("respostas")]
        public IEnumerable<Formularionew> GetRespostas([FromQuery] string? aplicacaoid = null)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var q = _context.Formularionews.AsQueryable();

            var filtroAppId = !acessoTotal ? claimAppId : (acessoTotal && !string.IsNullOrEmpty(aplicacaoid) ? aplicacaoid : null);
            if (filtroAppId != null)
            {
                var formularioIds = _context.Formularios
                    .Where(f => _context.Areas
                        .Where(a => a.Aplicacaoid == filtroAppId)
                        .Select(a => a.Areaid)
                        .Contains(f.Areaid))
                    .Select(f => f.Formularioid)
                    .ToHashSet();
                q = q.Where(r => r.Formularioid != null && formularioIds.Contains(r.Formularioid));
            }

            return q.OrderByDescending(f => f.Idform).ToArray();
        }

        [HttpPatch("respostas/{id}/ativo")]
        public IActionResult PatchAtivo(int id, [FromBody] int ativo)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Formularionews.Find(id);
            if (item == null) return NotFound();
            if (!acessoTotal)
            {
                var area = _context.Formularios
                    .Where(f => f.Formularioid == item.Formularioid)
                    .Join(_context.Areas, f => f.Areaid, a => a.Areaid, (f, a) => a)
                    .FirstOrDefault();
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }
            item.Ativo = ativo;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("respostas/{id}")]
        public IActionResult DeleteResposta(int id)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Formularionews.Find(id);
            if (item == null) return NotFound();
            if (!acessoTotal)
            {
                var area = _context.Formularios
                    .Where(f => f.Formularioid == item.Formularioid)
                    .Join(_context.Areas, f => f.Areaid, a => a.Areaid, (f, a) => a)
                    .FirstOrDefault();
                if (area?.Aplicacaoid != claimAppId) return Forbid();
            }
            _context.Formularionews.Remove(item);
            _context.SaveChanges();
            return Ok();
        }
    }
}
