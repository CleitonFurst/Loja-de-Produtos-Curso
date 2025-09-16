using System.ComponentModel.DataAnnotations;

namespace LojaProdutosCurso.DTO.Produto
{
    public class EditarProdutoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome !")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite a Marca !")]
        public string Marca { get; set; }
        public string? Foto { get; set; }
        [Required(ErrorMessage = "Digite o valor !")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Digite a quantidade !")]
        public int QuantidadeEstoque { get; set; }
        [Required(ErrorMessage = "Selecione a categoria !")]
        public int CategoriaModelId { get; set; }
    }
}
