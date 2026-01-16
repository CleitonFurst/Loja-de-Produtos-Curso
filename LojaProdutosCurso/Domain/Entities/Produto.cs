namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Produto do catálogo
    /// </summary>
    public class Produto : BaseEntity
    {
        public string Codigo { get; set; } = string.Empty; // SKU
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string UnidadeMedida { get; set; } = "UN"; // UN, CX, KG, L
        
        // Dimensões (para cálculo de frete)
        public decimal Peso { get; set; } = 0;
        public decimal Altura { get; set; } = 0;
        public decimal Largura { get; set; } = 0;
        public decimal Profundidade { get; set; } = 0;

        // Estoque
        public int QuantidadeEstoque { get; set; } = 0;
        public int EstoqueMinimo { get; set; } = 0;

        // Imagens
        public string ImagemPrincipalUrl { get; set; } = string.Empty;
        public string ImagensGaleria { get; set; } = "[]"; // JSON array

        // Atributos customizáveis
        public string Atributos { get; set; } = "{}"; // JSON: {"cor": "azul", "voltagem": "220V"}

        // Categoria
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }

        // Relacionamentos
        public virtual ICollection<ItemListaPreco> ItensListaPreco { get; set; } = new List<ItemListaPreco>();

        // Métodos de domínio
        public bool EstoqueDisponivel(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }

        public bool EstoqueBaixo()
        {
            return QuantidadeEstoque <= EstoqueMinimo;
        }
    }
}
