using LojaProdutosCurso.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutosCurso.Services.Categoria
{
    public class CategoriaService : ICategoriaInterface
    {
        private readonly Data.DataContext _context;
        public CategoriaService(Data.DataContext context)
        {
            _context = context;
        }
        public async Task<List<CategoriaModel>> BuscarCategorias()
        {
            try
            {
                var categorias = await _context.Categorias.ToListAsync();
                return categorias;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
