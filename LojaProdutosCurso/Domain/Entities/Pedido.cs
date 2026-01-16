using LojaProdutosCurso.Domain.Enums;

namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Pedido B2B
    /// </summary>
    public class Pedido : BaseEntity
    {
        public string Numero { get; set; } = string.Empty; // PED-2026-00001
        
        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public StatusPedido Status { get; set; } = StatusPedido.AguardandoAprovacao;

        // Valores
        public decimal ValorSubtotal { get; set; }
        public decimal ValorDescontos { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorTotal { get; set; }

        // Entrega
        public int EnderecoEntregaId { get; set; }
        public virtual EnderecoCliente? EnderecoEntrega { get; set; }
        public string? Observacoes { get; set; }

        // Rastreio
        public string? CodigoRastreio { get; set; }
        public DateTime? DataEnvio { get; set; }
        public DateTime? DataEntrega { get; set; }

        // Pagamento
        public string CondicaoPagamento { get; set; } = "Boleto"; // Boleto, Cartão, Prazo
        public int? DiasParaPagamento { get; set; }

        // Relacionamentos
        public virtual ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        // Métodos de domínio
        public void Aprovar()
        {
            if (Status != StatusPedido.AguardandoAprovacao)
                throw new InvalidOperationException("Pedido não está aguardando aprovação");
            Status = StatusPedido.Aprovado;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void IniciarSeparacao()
        {
            if (Status != StatusPedido.Aprovado)
                throw new InvalidOperationException("Pedido precisa estar aprovado");
            Status = StatusPedido.EmSeparacao;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Despachar(string codigoRastreio)
        {
            if (Status != StatusPedido.EmSeparacao)
                throw new InvalidOperationException("Pedido precisa estar em separação");
            Status = StatusPedido.Despachado;
            CodigoRastreio = codigoRastreio;
            DataEnvio = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Entregar()
        {
            if (Status != StatusPedido.Despachado)
                throw new InvalidOperationException("Pedido precisa estar despachado");
            Status = StatusPedido.Entregue;
            DataEntrega = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Cancelar()
        {
            if (Status == StatusPedido.Entregue)
                throw new InvalidOperationException("Não é possível cancelar pedido já entregue");
            Status = StatusPedido.Cancelado;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void CalcularTotais()
        {
            ValorSubtotal = Itens.Sum(i => i.ValorTotal);
            ValorTotal = ValorSubtotal - ValorDescontos + ValorFrete;
        }
    }
}
