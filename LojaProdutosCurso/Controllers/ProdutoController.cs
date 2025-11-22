using LojaProdutosCurso.DTO.Produto;
using LojaProdutosCurso.Services.Categoria;
using LojaProdutosCurso.Services.Produto;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutosCurso.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoInterface _produtoInterface;
        private readonly ICategoriaInterface _categoriaInterface;
        public ProdutoController(IProdutoInterface produtoInterface,
                                 ICategoriaInterface categoriaInterface
            )
        {
            _produtoInterface = produtoInterface;
            _categoriaInterface = categoriaInterface;
        }
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoInterface.BuscarProdutos();

            return View(produtos);
        }
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();

            return View();
        }
        public async Task<IActionResult> Remover(int id)
        {
           var poroduto =  await _produtoInterface.Remover(id);
           return RedirectToAction("Index", "Produto");
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _produtoInterface.BuscarProdutoPorId(id);
            return View(produto);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var produto = await _produtoInterface.BuscarProdutoPorId(id);

            var editarProdutoDTO = new EditarProdutoDTO
            {
                Nome = produto.Nome,
                Marca = produto.Marca,
                Valor = produto.Valor,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                CategoriaModelId = produto.CategoriaModelId,
                Foto = produto.Foto
            };

            ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();

            return View(editarProdutoDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(CriarProdutoDTO criaProdutoDTO,IFormFile foto )
        {
            if (ModelState.IsValid)
            {
                var produto =  await _produtoInterface.Cadastrar(criaProdutoDTO, foto);
                //sucesso
                TempData["MensagemSucesso"] = $"Produto {produto.Nome} cadastrado com sucesso";
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                //error
                TempData["MensagemErro"] = $"Não foi possível cadastrar o produto {criaProdutoDTO.Nome}";
                ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();
                return View(criaProdutoDTO);
            }        
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, EditarProdutoDTO editarProdutoDTO, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
               var produto = await _produtoInterface.Editar(editarProdutoDTO, foto);
                TempData["MensagemSucesso"] = $"Produto {produto.Nome} editado com sucesso";
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                TempData["MensagemErro"] = $"Não foi possível editar o produto {editarProdutoDTO.Nome}";
                ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();
                return View(editarProdutoDTO);
            }
        }
    }
}
