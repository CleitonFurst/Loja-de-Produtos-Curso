using LojaProdutosCurso.Data;
using LojaProdutosCurso.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutosCurso.Services.Usuario
{
    public class UsuarioService : IUsuariointerface
    {
        private readonly DataContext _context;

        public UsuarioService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<UsuarioModel>> BuscarUsuarios()
        {
            try
            {
                return await _context.Usuarios.Include(u => u.Endereco).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            try
            {
                var usario = await _context.Usuarios.Include(u => u.Endereco).FirstOrDefaultAsync(u => u.Id == id);
                return usario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
