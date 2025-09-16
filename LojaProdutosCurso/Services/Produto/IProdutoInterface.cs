using LojaProdutosCurso.DTO.Produto;
using LojaProdutosCurso.Models;

namespace LojaProdutosCurso.Services.Produto
{
    public interface IProdutoInterface
    {
        Task<List<ProdutoModel>> BuscarProdutos();
        Task<ProdutoModel> Cadastrar(CriarProdutoDTO criarProdutoDTO, IFormFile foto);
        Task<ProdutoModel> Editar(EditarProdutoDTO criarProdutoDTO, IFormFile? foto);

        Task<ProdutoModel> BuscarProdutoPorId(int id);
    }
}
