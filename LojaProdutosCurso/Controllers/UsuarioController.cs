using LojaProdutosCurso.Services.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LojaProdutosCurso.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuariointerface _usuarioInterface;

        public UsuarioController(IUsuariointerface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }
        public async Task<IActionResult> Index()
        {
            //Pega todos os usuarios do banco 
            var usuarios = await _usuarioInterface.BuscarUsuarios();
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
    }
}
