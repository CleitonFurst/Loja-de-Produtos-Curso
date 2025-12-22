using LojaProdutosCurso.Models;

namespace LojaProdutosCurso.Services.Usuario
{
    public interface IUsuariointerface
    {
        Task<List<UsuarioModel>> BuscarUsuarios();
        Task<UsuarioModel> BuscarUsuarioPorId(int id);
    }
}
