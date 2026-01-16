using LojaProdutosCurso.Domain.Enums;

namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Cliente B2B (lojista que compra do fornecedor)
    /// </summary>
    public class Cliente : BaseEntity
    {
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        // Comercial
        public int? VendedorResponsavelId { get; set; }
        public virtual Usuario? VendedorResponsavel { get; set; }
        public decimal LimiteCredito { get; set; } = 0;
        public decimal SaldoDevedor { get; set; } = 0;

        // Segmentação
        public SegmentoCliente Segmento { get; set; } = SegmentoCliente.Bronze;
        public string Tags { get; set; } = "[]"; // JSON array
        public string Regiao { get; set; } = string.Empty;
        public CategoriaAtuacao CategoriaAtuacao { get; set; }

        // Histórico (para análise RFM)
        public DateTime? DataUltimaCompra { get; set; }
        public decimal ValorTotalCompras { get; set; } = 0;
        public int QuantidadePedidos { get; set; } = 0;
        public string ScoreRFM { get; set; } = "{}"; // JSON: {R:int, F:int, M:int}

        // Relacionamentos
        public virtual ICollection<EnderecoCliente> Enderecos { get; set; } = new List<EnderecoCliente>();
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        // Métodos de domínio
        public bool EstaInativo()
        {
            return DataUltimaCompra == null || DataUltimaCompra < DateTime.UtcNow.AddMonths(-3);
        }

        public int DiasDesdeUltimaCompra()
        {
            if (DataUltimaCompra == null) return int.MaxValue;
            return (DateTime.UtcNow - DataUltimaCompra.Value).Days;
        }
    }
}
