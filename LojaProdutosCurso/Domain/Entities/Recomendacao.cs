using LojaProdutosCurso.Domain.Enums;

namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Recomendação gerada pelo motor de IA
    /// </summary>
    public class Recomendacao : BaseEntity
    {
        public TipoRecomendacao Tipo { get; set; }
        public DateTime DataGeracao { get; set; } = DateTime.UtcNow;
        public StatusRecomendacao Status { get; set; } = StatusRecomendacao.Pendente;

        // Conteúdo
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Justificativa { get; set; } = string.Empty;
        public decimal ConfiancaScore { get; set; } // 0-100

        // Se for recomendação de campanha
        public int? CampanhaSugeridaId { get; set; }
        public virtual Campanha? CampanhaSugerida { get; set; }

        // Se for pricing dinâmico
        public int? ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }
        public decimal? PrecoAtual { get; set; }
        public decimal? PrecoSugerido { get; set; }

        // Segmento alvo
        public SegmentoCliente? SegmentoAlvo { get; set; }
        public string ClientesAlvo { get; set; } = "[]"; // JSON array de IDs

        // Impacto esperado
        public decimal ReceitaEstimada { get; set; }
        public decimal MargemEstimada { get; set; }
        public int ConversaoEstimadaPorcentagem { get; set; }

        // Aprovação
        public int? AprovadoPorUsuarioId { get; set; }
        public virtual Usuario? AprovadoPor { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public DateTime? DataRejeicao { get; set; }
        public string? MotivoRejeicao { get; set; }

        // Métodos de domínio
        public void Aprovar(int usuarioId)
        {
            if (Status != StatusRecomendacao.Pendente)
                throw new InvalidOperationException("Recomendação já foi processada");
            
            Status = StatusRecomendacao.Aprovada;
            AprovadoPorUsuarioId = usuarioId;
            DataAprovacao = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Rejeitar(int usuarioId, string motivo)
        {
            if (Status != StatusRecomendacao.Pendente)
                throw new InvalidOperationException("Recomendação já foi processada");
            
            Status = StatusRecomendacao.Rejeitada;
            AprovadoPorUsuarioId = usuarioId;
            DataRejeicao = DateTime.UtcNow;
            MotivoRejeicao = motivo;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void MarcarComoExecutada()
        {
            if (Status != StatusRecomendacao.Aprovada)
                throw new InvalidOperationException("Apenas recomendações aprovadas podem ser executadas");
            
            Status = StatusRecomendacao.Executada;
            AtualizadoEm = DateTime.UtcNow;
        }
    }

    public enum TipoRecomendacao
    {
        CampanhaReativacao = 1,
        CampanhaUpsell = 2,
        CampanhaCrossSell = 3,
        AjustePricing = 4,
        ComboInteligente = 5
    }

    public enum StatusRecomendacao
    {
        Pendente = 1,
        Aprovada = 2,
        Rejeitada = 3,
        Executada = 4
    }
}
