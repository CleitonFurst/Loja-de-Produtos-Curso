using LojaProdutosCurso.Models;
using Newtonsoft.Json;

namespace LojaProdutosCurso.Services.Sessao
{
    public class SessaoService : ISessaoInterface
    {
        private readonly IHttpContextAccessor _ctx;

        public SessaoService(IHttpContextAccessor ctx)
        {
            _ctx = ctx;
        }
        public UsuarioModel BuscarSessao()
        {
            string sessaoUsuarioJson = _ctx.HttpContext.Session.GetString("sessaoUsuarioLogado");
            
            if(string.IsNullOrEmpty(sessaoUsuarioJson))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuarioJson);//tranforma o json em um objeto do tipo UsuarioModel
        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string usuarioJson = JsonConvert.SerializeObject(usuario);
            _ctx.HttpContext.Session.SetString("sessaoUsuarioLogado", usuarioJson);
        }

        public void RemoverSessao()
        {
            _ctx.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
