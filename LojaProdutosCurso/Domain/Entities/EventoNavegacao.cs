namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Eventos de navegação para análise de comportamento
    /// </summary>
    public class EventoNavegacao : BaseEntity
    {
        public int? ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        public int? UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public TipoEvento Tipo { get; set; }
        public DateTime DataHora { get; set; } = DateTime.UtcNow;

        // Contexto do evento
        public int? ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }

        public int? CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }

        public int? PedidoId { get; set; }
        public virtual Pedido? Pedido { get; set; }

        public string? TermoBusca { get; set; }

        // Dados extras (JSON)
        public string MetadataAdicional { get; set; } = "{}";
    }

    public enum TipoEvento
    {
        VisualizacaoProduto = 1,
        BuscaRealizada = 2,
        ProdutoAdicionadoCarrinho = 3,
        ProdutoRemovidoCarrinho = 4,
        PedidoFinalizado = 5,
        EmailCampanhaAberto = 6,
        EmailCampanhaClicado = 7
    }
}
