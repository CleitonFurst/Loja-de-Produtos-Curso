using ClosedXML.Excel;
using LojaProdutosCurso.Services.Estoque;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LojaProdutosCurso.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IEstoqueInterface _estoqueInterface;

        public EstoqueController(IEstoqueInterface estoqueInterface)
        {
            _estoqueInterface = estoqueInterface;
        }
        public IActionResult Index()
        {
            var registros = _estoqueInterface.ListagemRegistros();
            return View(registros);
        }

        public IActionResult GerarRelatorio()
        {
            //Buscar os dados

            var dados = BusacaDados();

            //Retornar o arquivo
            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dados, "Dados Vendas");

                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Relatorio_Vendas_Produtos.xlsx");
                }
            }
        }
        private DataTable BusacaDados()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Dados Vendas -  Produtos";

            dataTable.Columns.Add("ProdutoId", typeof(int));
            dataTable.Columns.Add("Categoria", typeof(string));
            dataTable.Columns.Add("Data da Compra", typeof(DateTime));
            dataTable.Columns.Add("Valor Total", typeof(decimal));


            var dados = _estoqueInterface.ListagemRegistros();

            if(dados.Count > 0)
            {
                foreach (var registro in dados)
                {
                    dataTable.Rows.Add(
                        registro.ProdutoId,
                        registro.CategoriaNome,
                        registro.DataCompra,
                        registro.TotalGeral
                    );
                }
            }
            return dataTable;
        }

        [HttpPost]
        public async Task<IActionResult> BaixarEstoque(int id)
        {
            var produtoBaixado = await _estoqueInterface.CriarRegistro(id);
            TempData["MensagemSucesso"] = "Compra Realizada com Sucesso!";
            return RedirectToAction("Index", "Home");
        }
    }
}
