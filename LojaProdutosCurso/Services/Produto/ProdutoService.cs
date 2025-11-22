using LojaProdutosCurso.Data;
using LojaProdutosCurso.DTO.Produto;
using LojaProdutosCurso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutosCurso.Services.Produto
{
    public class ProdutoService : IProdutoInterface
    {
        private readonly DataContext _context;
        private readonly string _sistema;
        public ProdutoService(DataContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _sistema = sistema.WebRootPath;
        }

        public async Task<List<ProdutoModel>> BuscarProdutoFiltro(string? pesquisar)
        {
            try
            {
                var produtos = await _context.Produtos
                    .Include(x => x.Categoria)
                    .Where(p => p.Nome.ToLower().Contains(pesquisar.ToLower()) || p.Marca.ToLower().Contains(pesquisar.ToLower()))
                    .ToListAsync();

                return produtos;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutoModel> BuscarProdutoPorId(int id)
        {
            try
            {
                // verifica se algum produto existe com o id fornecido
                var produto = await _context.Produtos.Include(c=> c.Categoria).FirstOrDefaultAsync(p => p.Id == id);

                return produto == null ? throw new Exception("Produto não encontrado.") : produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProdutoModel>> BuscarProdutos()
        {
            try
            {
                return await _context.Produtos.Include(c => c.Categoria).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutoModel> Cadastrar(CriarProdutoDTO criarProdutoDTO, IFormFile foto)
        {
            try
            {
                var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                var produto = new ProdutoModel
                {
                    Nome = criarProdutoDTO.Nome,
                    Marca = criarProdutoDTO.Marca,
                    Valor = criarProdutoDTO.Valor,
                    CategoriaModelId = criarProdutoDTO.CategoriaModelId,
                    Foto = nomeCaminhoImagem,
                    QuantidadeEstoque = criarProdutoDTO.QuantidadeEstoque
                };

                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

 

        public async Task<ProdutoModel> Editar(EditarProdutoDTO editarProdutoDTO, IFormFile? foto)
        {
            try
            {
                var produto = await BuscarProdutoPorId(editarProdutoDTO.Id);
                var nomeCaminhoImagem = string.Empty;
                if (foto != null)
                {
                    string caminhoCapaExixtente = _sistema + "\\imagem\\" + produto.Foto;
                    if(File.Exists(caminhoCapaExixtente))
                    {
                        File.Delete(caminhoCapaExixtente);
                    }
                    nomeCaminhoImagem = GeraCaminhoArquivo(foto);
                    produto.Foto = nomeCaminhoImagem;
                }

                produto.Nome = editarProdutoDTO.Nome;
                produto.Marca = editarProdutoDTO.Marca;
                produto.Valor = editarProdutoDTO.Valor;
                produto.QuantidadeEstoque = editarProdutoDTO.QuantidadeEstoque;
                produto.CategoriaModelId = editarProdutoDTO.CategoriaModelId;

                produto.Foto = nomeCaminhoImagem == string.Empty ? produto.Foto : nomeCaminhoImagem;

                _context.Produtos.Update(produto);
                
                await _context.SaveChangesAsync();

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProdutoModel> Remover(int id)
        {
            try
            {
                var produto = await BuscarProdutoPorId(id);
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            var caminhoParaSalvarImagens = _sistema + "\\imagem\\";

            //Verifica se o diretorio existe, caso não exista cria um novo
            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            //Cria uma copia da imagem dentro da pasta imagem
            using (var strem = File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
            {
                foto.CopyToAsync(strem).Wait();
            }

            return nomeCaminhoImagem;
        }


         
    }
}
