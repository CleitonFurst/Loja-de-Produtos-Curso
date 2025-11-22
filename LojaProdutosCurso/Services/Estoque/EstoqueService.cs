using LojaProdutosCurso.Data;
using LojaProdutosCurso.Models;
using LojaProdutosCurso.Services.Produto;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutosCurso.Services.Estoque
{
    public class EstoqueService : IEstoqueInterface
    {
        private readonly DataContext _context;
        private readonly IProdutoInterface _produtoInterface;

        //atravez da interface podemos acessar os metodos da interface
        public EstoqueService(DataContext context, IProdutoInterface produtoInterface)
        {
            _context = context;
            _produtoInterface = produtoInterface;
        }

        public async Task<ProdutosBaixadosModel> CriarRegistro(int IdProduto)
        {
            try
            {
                var produto = await _produtoInterface.BuscarProdutoPorId(IdProduto);
                // cria um novo registro de produto baixado passando o objeto produto e o id do produto
                var produtoBaixado = new ProdutosBaixadosModel()
                {
                    ProdutoModelId = IdProduto,
                    Produto = produto
                };

                //salva o registro no banco de dados
                _context.Add(produtoBaixado);
                await _context.SaveChangesAsync();

                //baixar a quantidade do estoque
                BaixarEstoque(produto);

                return produtoBaixado;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void BaixarEstoque(ProdutoModel produto)
        {
            try
            {
                //valida se a quantidade em estoque é maior que zero
                if (produto.QuantidadeEstoque > 0)
                {
                    produto.QuantidadeEstoque -= 1;
                    _context.Update(produto);
                    _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Estoque insuficiente para o produto: " + produto.Nome);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RegistroProdutoModel> ListagemRegistros()
        {
            try
            {
                var resultado = from c in _context.ProdutosBaixados.Include(x => x.Produto)
                                                                    .Include(x => x.Produto.Categoria)
                                                                    .ToList()
                                group c by new { c.Produto.CategoriaModelId, c.DataDaBaixa } into total
                                select new
                                {
                                    ProdutoId = total.First().Produto.Categoria.Id,
                                    NomeCategoria = total.First().Produto.Categoria.Nome,
                                    DataCompra = total.First().DataDaBaixa,
                                    Total = total.Sum(c => c.Produto.Valor)
                                };

                var totalGeral = _context.ProdutosBaixados.Include(x => x.Produto)
                                                                    .Include(x => x.Produto.Categoria)
                                                                    .Sum(x => x.Produto.Valor);

                List<RegistroProdutoModel> lista = new List<RegistroProdutoModel>();
                
                foreach (var item in resultado)
                {
                    lista.Add(new RegistroProdutoModel()
                    {
                        ProdutoId = item.ProdutoId,
                        CategoriaNome = item.NomeCategoria,
                        DataCompra = item.DataCompra,
                        Total = (double)item.Total,
                        TotalGeral = (double)totalGeral
                    });
                }
                return lista;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

     
    }
}
