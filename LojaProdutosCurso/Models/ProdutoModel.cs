using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaProdutosCurso.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }        
        public string Foto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        public int QuantidadeEstoque { get; set; }        
        public int CategoriaModelId { get; set; }

        [ValidateNever]
        public CategoriaModel Categoria { get; set; }
    }
}
