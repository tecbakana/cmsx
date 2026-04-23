using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CMSXData.Models;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FaqController : Controller
    {
        private readonly CmsxDbContext _context;
        public FaqController(CmsxDbContext context) { _context = context; }

        private (bool acessoTotal, string? aplicacaoid) UserContext() =>
            (User.FindFirstValue("acessoTotal") == "True", User.FindFirstValue("aplicacaoid"));

        // ── Wiki pública ─────────────────────────────────────────────────────

        [AllowAnonymous]
        [HttpGet("wiki/{aplicacaoid}")]
        public IActionResult GetWiki(string aplicacaoid)
        {
            var categorias = _context.Cateria
                .Where(c => c.Aplicacaoid == aplicacaoid)
                .Select(c => new { c.Cateriaid, c.Nome, c.Cateriaidpai })
                .AsEnumerable()
                .Select(c => (c.Cateriaid, c.Nome, c.Cateriaidpai))
                .ToList();

            var formularios = _context.Formularios
                .Where(f => f.Categoriaid != null &&
                            _context.Areas
                                .Where(a => a.Aplicacaoid == aplicacaoid)
                                .Select(a => a.Areaid)
                                .Contains(f.Areaid))
                .Select(f => new { f.Formularioid, f.Nome, f.Categoriaid })
                .AsEnumerable()
                .Select(f => (f.Formularioid, f.Nome, f.Categoriaid))
                .ToList();

            var formularioIds = formularios.Select(f => f.Formularioid).ToHashSet();

            var faqs = _context.Faqs
                .Where(faq => faq.Ativo && formularioIds.Contains(faq.Formularioid))
                .OrderBy(faq => faq.Ordem)
                .AsEnumerable()
                .Select(faq => new FaqWiki(faq.Faqid, faq.Formularioid, faq.Pergunta, faq.Resposta, faq.Ordem))
                .ToList();

            var arvore = categorias
                .Where(c => c.Cateriaidpai == null)
                .Select(c => MontarNo(c.Cateriaid, c.Nome, categorias, formularios, faqs))
                .ToList();

            return Ok(arvore);
        }

        private record CategoriaWiki(string Cateriaid, string? Nome, List<CategoriaWiki> Subcategorias, List<FormularioWiki> Formularios);
        private record FormularioWiki(string Formularioid, string? Nome, List<FaqWiki> Items);
        private record FaqWiki(string Faqid, string Formularioid, string Pergunta, string Resposta, int Ordem);

        private CategoriaWiki MontarNo(
            string cateriaid, string? nome,
            IReadOnlyList<(string Cateriaid, string? Nome, string? Cateriaidpai)> categorias,
            IReadOnlyList<(string Formularioid, string? Nome, string? Categoriaid)> formularios,
            IReadOnlyList<FaqWiki> faqs)
        {
            var subcats = categorias
                .Where(c => c.Cateriaidpai == cateriaid)
                .Select(c => MontarNo(c.Cateriaid, c.Nome, categorias, formularios, faqs))
                .ToList();

            var forms = formularios
                .Where(f => f.Categoriaid == cateriaid)
                .Select(f => new FormularioWiki(
                    f.Formularioid, f.Nome,
                    faqs.Where(faq => faq.Formularioid == f.Formularioid).ToList()))
                .ToList();

            return new CategoriaWiki(cateriaid, nome, subcats, forms);
        }

        // ── CRUD autenticado ─────────────────────────────────────────────────

        [Authorize]
        [HttpGet("{formularioid}")]
        public IActionResult GetByFormulario(string formularioid)
        {
            var (acessoTotal, claimAppId) = UserContext();
            if (!acessoTotal && !TemAcesso(formularioid, claimAppId)) return Forbid();

            var items = _context.Faqs
                .Where(f => f.Formularioid == formularioid)
                .OrderBy(f => f.Ordem)
                .ToArray();
            return Ok(items);
        }

        public class FaqDto
        {
            public string? Pergunta { get; set; }
            public string? Resposta { get; set; }
            public int Ordem { get; set; }
        }

        [Authorize]
        [HttpPost("{formularioid}")]
        public IActionResult Post(string formularioid, [FromBody] FaqDto dto)
        {
            var (acessoTotal, claimAppId) = UserContext();
            if (!acessoTotal && !TemAcesso(formularioid, claimAppId)) return Forbid();

            var item = new Faq
            {
                Faqid        = Guid.NewGuid().ToString(),
                Formularioid = formularioid,
                Pergunta     = dto.Pergunta ?? "",
                Resposta     = dto.Resposta ?? "",
                Ordem        = dto.Ordem,
                Ativo        = true,
                Datainclusao = DateTime.Now
            };
            _context.Faqs.Add(item);
            _context.SaveChanges();
            return Ok(item);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] FaqDto dto)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Faqs.FirstOrDefault(f => f.Faqid == id);
            if (item == null) return NotFound();
            if (!acessoTotal && !TemAcesso(item.Formularioid, claimAppId)) return Forbid();

            item.Pergunta = dto.Pergunta ?? item.Pergunta;
            item.Resposta = dto.Resposta ?? item.Resposta;
            item.Ordem    = dto.Ordem;
            _context.SaveChanges();
            return Ok(item);
        }

        [Authorize]
        [HttpPatch("{id}/ativo")]
        public IActionResult PatchAtivo(string id, [FromBody] bool ativo)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Faqs.FirstOrDefault(f => f.Faqid == id);
            if (item == null) return NotFound();
            if (!acessoTotal && !TemAcesso(item.Formularioid, claimAppId)) return Forbid();

            item.Ativo = ativo;
            _context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var item = _context.Faqs.FirstOrDefault(f => f.Faqid == id);
            if (item == null) return NotFound();
            if (!acessoTotal && !TemAcesso(item.Formularioid, claimAppId)) return Forbid();

            _context.Faqs.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        // ── Promoção de resposta do inbox para FAQ ───────────────────────────

        [Authorize]
        [HttpPost("promover/{idform}")]
        public IActionResult Promover(int idform, [FromBody] FaqDto dto)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var resposta = _context.Formularionews.Find(idform);
            if (resposta == null) return NotFound();
            if (!acessoTotal && !TemAcesso(resposta.Formularioid, claimAppId)) return Forbid();

            var item = new Faq
            {
                Faqid        = Guid.NewGuid().ToString(),
                Formularioid = resposta.Formularioid!,
                Pergunta     = dto.Pergunta ?? "",
                Resposta     = dto.Resposta ?? resposta.Texto ?? "",
                Ordem        = dto.Ordem,
                Ativo        = true,
                Datainclusao = DateTime.Now
            };
            _context.Faqs.Add(item);
            _context.SaveChanges();
            return Ok(item);
        }

        // ── Helper ───────────────────────────────────────────────────────────

        private bool TemAcesso(string? formularioid, string? claimAppId)
        {
            if (string.IsNullOrEmpty(formularioid)) return false;
            return _context.Formularios
                .Where(f => f.Formularioid == formularioid)
                .Join(_context.Areas, f => f.Areaid, a => a.Areaid, (f, a) => a)
                .Any(a => a.Aplicacaoid == claimAppId);
        }
    }
}
