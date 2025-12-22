using LojaProdutosCurso.DTO.Usuario;
using LojaProdutosCurso.Models;

namespace LojaProdutosCurso.Services.Usuario
{
    public interface IUsuariointerface
    {
        Task<List<UsuarioModel>> BuscarUsuarios();
        Task<UsuarioModel> BuscarUsuarioPorId(int id);
        Task<bool> VerifcaSeExisteEmail(CriarUsuarioDTO criarUsuarioDTO);
        Task<CriarUsuarioDTO> CadastrarUsuario(CriarUsuarioDTO criarUsuarioDTO);
    }
}
