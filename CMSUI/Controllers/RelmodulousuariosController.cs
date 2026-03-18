using Microsoft.AspNetCore.Mvc;
using CMSUI.Models;

namespace CMSUI.Controllers
{
    [ApiController]
    [Route("vinculosmodulo")]
    public class RelmodulousuariosController : Controller
    {
        private readonly CmsxDbContext _context;
        public RelmodulousuariosController(CmsxDbContext context) { _context = context; }

        [HttpGet]
        public IEnumerable<object> Get([FromQuery] string? aplicacaoid = null, [FromQuery] string? usuarioid = null)
        {
            // Usuários elegíveis: se aplicacaoid fornecido, filtra pelo vínculo usuário x app
            IQueryable<string> usuarioIds = aplicacaoid != null
                ? _context.Relusuarioaplicacaos
                    .Where(r => r.Aplicacaoid == aplicacaoid)
                    .Select(r => r.Usuarioid!)
                : _context.Usuarios.Select(u => u.Userid);

            if (!string.IsNullOrEmpty(usuarioid))
                usuarioIds = usuarioIds.Where(id => id == usuarioid);

            return _context.Relmodulousuarios
                .Where(r => usuarioIds.Contains(r.Usuarioid))
                .Join(_context.Usuarios, r => r.Usuarioid, u => u.Userid,
                    (r, u) => new { r.Relacaoid, r.Moduloid, r.Usuarioid, nomeUsuario = u.Nome + " " + u.Sobrenome, apelido = u.Apelido })
                .Join(_context.Modulos, x => x.Moduloid, m => m.Moduloid,
                    (x, m) => new
                    {
                        x.Relacaoid, x.Moduloid, x.Usuarioid,
                        x.nomeUsuario, x.apelido,
                        nomeModulo = m.Nome, urlModulo = m.Url
                    })
                .ToArray();
        }

        public class VincularModuloDto
        {
            public string? Usuarioid { get; set; }
            public string? Moduloid { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] VincularModuloDto dto)
        {
            if (string.IsNullOrEmpty(dto.Usuarioid) || string.IsNullOrEmpty(dto.Moduloid))
                return BadRequest(new { message = "Selecione usuário e módulo." });

            bool existe = _context.Relmodulousuarios
                .Any(r => r.Usuarioid == dto.Usuarioid && r.Moduloid == dto.Moduloid);
            if (existe)
                return BadRequest(new { message = "Vínculo já existe." });

            var rel = new Relmodulousuario
            {
                Relacaoid = Guid.NewGuid().ToString(),
                Usuarioid = dto.Usuarioid,
                Moduloid = dto.Moduloid
            };
            _context.Relmodulousuarios.Add(rel);
            _context.SaveChanges();
            return Ok(rel);
        }

        [HttpDelete("{relacaoid}")]
        public IActionResult Delete(string relacaoid)
        {
            var item = _context.Relmodulousuarios.FirstOrDefault(r => r.Relacaoid == relacaoid);
            if (item == null) return NotFound();
            _context.Relmodulousuarios.Remove(item);
            _context.SaveChanges();
            return Ok();
        }
    }
}
