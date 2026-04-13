using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CMSXData.Models;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GruposController : Controller
    {
        private readonly CmsxDbContext _context;
        public GruposController(CmsxDbContext context) { _context = context; }

        private bool IsAdmin() => User.FindFirstValue("acessoTotal") == "True";

        // Grupos são globais — somente admin gerencia
        [HttpGet]
        public IActionResult Get()
        {
            if (!IsAdmin()) return Forbid();
            return Ok(_context.Grupos.OrderBy(g => g.Nome).ToArray());
        }

        [HttpGet("{id}/usuarios")]
        public IActionResult GetUsuarios(string id)
        {
            if (!IsAdmin()) return Forbid();
            var usuarios = _context.Relusuariogrupos
                .Where(r => r.Grupoid == id)
                .AsEnumerable()
                .Join(_context.Usuarios.AsEnumerable(), r => r.Usuarioid, u => u.Userid,
                    (r, u) => new { r.Relacaoid, u.Userid, u.Nome, u.Sobrenome, u.Apelido, u.Ativo })
                .ToList();
            return Ok(usuarios);
        }

        public class NovoGrupoDto
        {
            public string Nome { get; set; } = "";
            public string? Descricao { get; set; }
            public bool Acessototal { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] NovoGrupoDto dto)
        {
            if (!IsAdmin()) return Forbid();
            var item = new Grupo
            {
                Grupoid     = Guid.NewGuid().ToString(),
                Nome        = dto.Nome,
                Descricao   = dto.Descricao,
                Acessototal = dto.Acessototal
            };
            _context.Grupos.Add(item);
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] NovoGrupoDto dto)
        {
            if (!IsAdmin()) return Forbid();
            var item = _context.Grupos.Find(id);
            if (item == null) return NotFound();
            item.Nome        = dto.Nome;
            item.Descricao   = dto.Descricao;
            item.Acessototal = dto.Acessototal;
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!IsAdmin()) return Forbid();
            var item = _context.Grupos.Find(id);
            if (item == null) return NotFound();
            var vinculos = _context.Relusuariogrupos.Where(r => r.Grupoid == id).ToList();
            _context.Relusuariogrupos.RemoveRange(vinculos);
            _context.Grupos.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("{id}/usuarios")]
        public IActionResult AddUsuario(string id, [FromBody] string usuarioid)
        {
            if (!IsAdmin()) return Forbid();
            if (_context.Relusuariogrupos.Any(r => r.Grupoid == id && r.Usuarioid == usuarioid))
                return BadRequest(new { message = "Usuário já pertence a este grupo." });

            _context.Relusuariogrupos.Add(new Relusuariogrupo
            {
                Relacaoid = Guid.NewGuid().ToString(),
                Grupoid   = id,
                Usuarioid = usuarioid
            });
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}/usuarios/{relacaoid}")]
        public IActionResult RemoveUsuario(string id, string relacaoid)
        {
            if (!IsAdmin()) return Forbid();
            var rel = _context.Relusuariogrupos.Find(relacaoid);
            if (rel == null) return NotFound();
            _context.Relusuariogrupos.Remove(rel);
            _context.SaveChanges();
            return Ok();
        }
    }
}
