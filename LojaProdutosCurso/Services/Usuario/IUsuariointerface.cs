using LojaProdutosCurso.DTO.Login;
using LojaProdutosCurso.DTO.Usuario;
using LojaProdutosCurso.Models;

namespace LojaProdutosCurso.Services.Usuario
{
    public interface IUsuariointerface
    {
        Task<List<UsuarioModel>> BuscarUsuarios();
        Task<UsuarioModel> BuscarUsuarioPorId(int id);
        Task<UsuarioModel> Excluir(int id);
        Task<bool> VerifcaSeExisteEmail(CriarUsuarioDTO criarUsuarioDTO);
        Task<CriarUsuarioDTO> CadastrarUsuario(CriarUsuarioDTO criarUsuarioDTO);

        Task<UsuarioModel> Editar(EditarUsuarioDTO editarUsuarioDTO);
        Task<UsuarioModel> Login(LoginUsuarioDTO loginUsuarioDTO);

    }
}
