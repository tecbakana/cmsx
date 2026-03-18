using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using CMSUI.Models;

namespace CMSUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LayoutTemplatesController : ControllerBase
    {
        private readonly CmsxDbContext _context;
        public LayoutTemplatesController(CmsxDbContext context) => _context = context;

        private bool IsAdmin() => User.FindFirstValue("acessoTotal") == "True";

        [HttpGet]
        public IActionResult Get() =>
            Ok(_context.LayoutTemplates.OrderBy(t => t.Nome).ToList());

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var t = _context.LayoutTemplates.Find(id);
            return t == null ? NotFound() : Ok(t);
        }

        [HttpGet("padrao")]
        public IActionResult GetPadrao([FromQuery] string tipo = "home")
        {
            var t = _context.LayoutTemplates.FirstOrDefault(x => x.Tipo == tipo && x.Padrao);
            return t == null ? NotFound() : Ok(t);
        }

        public class SalvarTemplateDto
        {
            public string Nome { get; set; } = "";
            public string? Descricao { get; set; }
            public string Tipo { get; set; } = "home";
            public string Layout { get; set; } = "";
            public bool Padrao { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] SalvarTemplateDto dto)
        {
            if (!IsAdmin()) return Forbid();

            // Se for padrão, remove o padrão anterior do mesmo tipo
            if (dto.Padrao)
                foreach (var t in _context.LayoutTemplates.Where(x => x.Tipo == dto.Tipo && x.Padrao))
                    t.Padrao = false;

            // Valida o JSON do layout
            try { JsonDocument.Parse(dto.Layout); }
            catch { return BadRequest(new { erro = "Layout JSON inválido." }); }

            var item = new LayoutTemplate
            {
                Templateid   = Guid.NewGuid().ToString(),
                Nome         = dto.Nome,
                Descricao    = dto.Descricao,
                Tipo         = dto.Tipo,
                Layout       = dto.Layout,
                Padrao       = dto.Padrao,
                Datainclusao = DateTime.Now
            };
            _context.LayoutTemplates.Add(item);
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] SalvarTemplateDto dto)
        {
            if (!IsAdmin()) return Forbid();

            var item = _context.LayoutTemplates.Find(id);
            if (item == null) return NotFound();

            if (dto.Padrao)
                foreach (var t in _context.LayoutTemplates.Where(x => x.Tipo == dto.Tipo && x.Padrao && x.Templateid != id))
                    t.Padrao = false;

            item.Nome      = dto.Nome;
            item.Descricao = dto.Descricao;
            item.Tipo      = dto.Tipo;
            item.Layout    = dto.Layout;
            item.Padrao    = dto.Padrao;
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!IsAdmin()) return Forbid();
            var item = _context.LayoutTemplates.Find(id);
            if (item == null) return NotFound();
            _context.LayoutTemplates.Remove(item);
            _context.SaveChanges();
            return Ok();
        }
    }
}
