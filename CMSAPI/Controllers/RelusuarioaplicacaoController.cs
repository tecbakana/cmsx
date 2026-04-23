using Microsoft.AspNetCore.Mvc;
using CMSXData.Models;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("vinculos")]
    public class RelusuarioaplicacaoController : Controller
    {
        private readonly CmsxDbContext _context;
        public RelusuarioaplicacaoController(CmsxDbContext context) { _context = context; }

        // GET /vinculos?aplicacaoid=X  — usuários de uma aplicação
        [HttpGet]
        public IActionResult Get([FromQuery] string? aplicacaoid = null, [FromQuery] string? usuarioid = null)
        {
            var q = _context.Relusuarioaplicacaos.AsQueryable();

            if (!string.IsNullOrEmpty(aplicacaoid))
            {
                q = q.Where(r => r.Aplicacaoid == aplicacaoid);
            }
            if (!string.IsNullOrEmpty(usuarioid))
            {
                q = q.Where(r => r.Usuarioid == usuarioid);
            }

            var resultado = q
                .Join(_context.Usuarios, r => r.Usuarioid, u => u.Userid,
                    (r, u) => new { r.Relacaoid, r.Aplicacaoid, r.Usuarioid, u.Nome, u.Sobrenome, u.Apelido, u.Ativo })
                .AsEnumerable()
                .Join(_context.Aplicacaos.AsEnumerable(), r => r.Aplicacaoid, a => a.Aplicacaoid,
                    (r, a) => new { r.Relacaoid, r.Usuarioid, r.Nome, r.Sobrenome, r.Apelido, r.Ativo, r.Aplicacaoid, AppNome = a.Nome })
                .ToList();

            return Ok(resultado);
        }

        public class NovoVinculoDto
        {
            public string Usuarioid { get; set; } = "";
            public string Aplicacaoid { get; set; } = "";
        }

        // POST /vinculos
        [HttpPost]
        public IActionResult Post([FromBody] NovoVinculoDto dto)
        {
            if (_context.Relusuarioaplicacaos.Any(r => r.Usuarioid == dto.Usuarioid && r.Aplicacaoid == dto.Aplicacaoid))
                return BadRequest(new { message = "Vínculo já existe." });

            var rel = new Relusuarioaplicacao
            {
                Relacaoid   = Guid.NewGuid().ToString(),
                Usuarioid   = dto.Usuarioid,
                Aplicacaoid = dto.Aplicacaoid
            };
            _context.Relusuarioaplicacaos.Add(rel);
            _context.SaveChanges();
            return Ok(rel);
        }

        // DELETE /vinculos/{relacaoid}
        [HttpDelete("{relacaoid}")]
        public IActionResult Delete(string relacaoid)
        {
            var rel = _context.Relusuarioaplicacaos.Find(relacaoid);
            if (rel == null) return NotFound();
            _context.Relusuarioaplicacaos.Remove(rel);
            _context.SaveChanges();
            return Ok();
        }
    }
}
