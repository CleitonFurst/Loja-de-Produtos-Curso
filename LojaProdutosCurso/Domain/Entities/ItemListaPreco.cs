namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Item de uma lista de preços
    /// </summary>
    public class ItemListaPreco : BaseEntity
    {
        public int ListaDePrecoId { get; set; }
        public virtual ListaDePreco? ListaDePreco { get; set; }

        public int ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }

        public decimal PrecoUnitario { get; set; }
        public decimal? PrecoPromocional { get; set; }
        public int? QuantidadeMinima { get; set; } // Para desconto progressivo

        // Método de domínio
        public decimal ObterPrecoFinal()
        {
            return PrecoPromocional ?? PrecoUnitario;
        }
    }
}
