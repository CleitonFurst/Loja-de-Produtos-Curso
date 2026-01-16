namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Item de um pedido
    /// </summary>
    public class ItemPedido : BaseEntity
    {
        public int PedidoId { get; set; }
        public virtual Pedido? Pedido { get; set; }

        public int ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PercentualDesconto { get; set; }
        
        public decimal ValorTotal => Quantidade * PrecoUnitario * (1 - PercentualDesconto / 100);
    }
}
