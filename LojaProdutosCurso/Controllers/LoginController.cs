using LojaProdutosCurso.DTO.Login;
using LojaProdutosCurso.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutosCurso.Controllers
{
    public class LoginController : Controller
    {
        public IUsuariointerface _usuariointerface { get; }
        public LoginController(IUsuariointerface usuariointerface)
        {
            _usuariointerface = usuariointerface;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDTO loginUsuarioDTO)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuariointerface.Login(loginUsuarioDTO);

                if (usuario == null) 
                {
                    TempData["MensagemErro"] = "Credenciais inválidas.";
                    return View(loginUsuarioDTO);
                }
                TempData["Success"] = "Login realizado com sucesso.";
                return RedirectToAction("Index", "Home");                
            }
            else
            {
                TempData["MensagemErro"] = "Preencha os campos corretamente.";
                return View(loginUsuarioDTO);
            }

        }
    }
}
