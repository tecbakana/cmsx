using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CMSXData.Models;

namespace CMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly CmsxDbContext _context;
        public UsuariosController(CmsxDbContext context) { _context = context; }

        private (bool acessoTotal, string? aplicacaoid) UserContext() =>
            (User.FindFirstValue("acessoTotal") == "True", User.FindFirstValue("aplicacaoid"));

        private static Guid? ParseGuid(string? s) =>
            s != null && Guid.TryParse(s, out var g) ? g : null;

        [HttpGet]
        public IEnumerable<object> Get([FromQuery] string? aplicacaoid = null)
        {
            var (acessoTotal, claimAppId) = UserContext();

            if (acessoTotal)
            {
                if (!string.IsNullOrEmpty(aplicacaoid))
                {
                    var adminSet = _context.Relusuariogrupos
                        .Join(_context.Grupos, r => r.Grupoid, g => g.Grupoid, (r, g) => new { r.Usuarioid, g.Acessototal })
                        .Where(x => x.Acessototal)
                        .Select(x => x.Usuarioid)
                        .ToHashSet();

                    return _context.Relusuarioaplicacaos
                        .Where(r => r.Aplicacaoid == aplicacaoid)
                        .Join(_context.Usuarios, r => r.Usuarioid, u => u.Userid,
                            (r, u) => new { u.Userid, u.Nome, u.Sobrenome, u.Apelido, u.Ativo, u.Datainclusao })
                        .AsEnumerable()
                        .Where(u => !adminSet.Contains(u.Userid))
                        .ToArray();
                }

                return _context.Usuarios
                    .Select(u => new { u.Userid, u.Nome, u.Sobrenome, u.Apelido, u.Ativo, u.Datainclusao })
                    .ToArray();
            }

            // Tenant: retorna usuários da sua aplicação excluindo admins
            var adminIds = _context.Relusuariogrupos
                .Join(_context.Grupos, r => r.Grupoid, g => g.Grupoid, (r, g) => new { r.Usuarioid, g.Acessototal })
                .Where(x => x.Acessototal)
                .Select(x => x.Usuarioid)
                .ToHashSet();

            return _context.Relusuarioaplicacaos
                .Where(r => r.Aplicacaoid == claimAppId)
                .Join(_context.Usuarios, r => r.Usuarioid, u => u.Userid,
                    (r, u) => new { u.Userid, u.Nome, u.Sobrenome, u.Apelido, u.Ativo, u.Datainclusao })
                .AsEnumerable()
                .Where(u => !adminIds.Contains(u.Userid))
                .ToArray();
        }

        public class NovoUsuarioDto
        {
            public string Nome { get; set; } = "";
            public string Sobrenome { get; set; } = "";
            public string? Apelido { get; set; }
            public string? Senha { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] NovoUsuarioDto dto)
        {
            var (acessoTotal, claimAppId) = UserContext();

            var usuario = new Usuario
            {
                Userid       = Guid.NewGuid().ToString(),
                Nome         = dto.Nome,
                Sobrenome    = dto.Sobrenome,
                Apelido      = dto.Apelido,
                Senha        = dto.Senha,
                Ativo        = 1,
                Datainclusao = DateTime.Now
            };
            _context.Usuarios.Add(usuario);

            // Tenant: vincula o novo usuário automaticamente à sua aplicação
            if (!acessoTotal && !string.IsNullOrEmpty(claimAppId))
            {
                _context.Relusuarioaplicacaos.Add(new Relusuarioaplicacao
                {
                    Relacaoid   = Guid.NewGuid().ToString(),
                    Usuarioid   = usuario.Userid,
                    Aplicacaoid = claimAppId
                });
            }

            _context.SaveChanges();
            return Ok(usuario);
        }

        public class EditarUsuarioDto
        {
            public string Nome { get; set; } = "";
            public string Sobrenome { get; set; } = "";
            public string? Apelido { get; set; }
            public string? Senha { get; set; }
            public byte? Ativo { get; set; }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] EditarUsuarioDto dto)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var user = _context.Usuarios.Find(id);
            if (user == null) return NotFound();

            if (!acessoTotal)
            {
                var pertence = _context.Relusuarioaplicacaos
                    .Any(r => r.Usuarioid == id && r.Aplicacaoid == claimAppId);
                if (!pertence) return Forbid();
            }

            user.Nome      = dto.Nome;
            user.Sobrenome = dto.Sobrenome;
            user.Apelido   = dto.Apelido;
            user.Ativo     = dto.Ativo;
            if (!string.IsNullOrWhiteSpace(dto.Senha))
                user.Senha = dto.Senha;
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var (acessoTotal, claimAppId) = UserContext();
            var user = _context.Usuarios.Find(id);
            if (user == null) return NotFound();

            if (!acessoTotal)
            {
                var pertence = _context.Relusuarioaplicacaos
                    .Any(r => r.Usuarioid == id && r.Aplicacaoid == claimAppId);
                if (!pertence) return Forbid();
            }

            _context.Usuarios.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}
