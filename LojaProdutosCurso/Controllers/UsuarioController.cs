using LojaProdutosCurso.DTO.Usuario;
using LojaProdutosCurso.Services.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            return View(usuarios);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar(CriarUsuarioDTO criarUsuarioDTO)
        {
            if(ModelState.IsValid)
            {
                if(await _usuarioInterface.VerifcaSeExisteEmail(criarUsuarioDTO))
                {
                    TempData["MensagemErro"] = "E-mail já cadastrado !";
                    return View(criarUsuarioDTO);
                }
                var usuarioCadastrado = await _usuarioInterface.CadastrarUsuario(criarUsuarioDTO);

                if(usuarioCadastrado != null)
                {
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso !";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Houve um erro ao cadastrar o usuário, tente novamente !";
                    return View(criarUsuarioDTO);
                }
            }
            else
            {
                TempData["MensagemErro"] = "Dados invalidos, verifique e tente novamente !";
                return View(criarUsuarioDTO);
            }
        }
    }
}
