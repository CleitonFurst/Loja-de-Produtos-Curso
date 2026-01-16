using LojaProdutosCurso.Domain.Enums;

namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Campanha promocional
    /// </summary>
    public class Campanha : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public TipoCampanha Tipo { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        // Segmentação (JSON arrays)
        public string SegmentosElegiveis { get; set; } = "[]"; // [1,2,3]
        public string ClientesEspecificos { get; set; } = "[]"; // [id1, id2]
        public string CategoriasElegiveis { get; set; } = "[]";
        public string ProdutosEspecificos { get; set; } = "[]";

        // Regras de desconto
        public decimal? PercentualDesconto { get; set; }
        public decimal? ValorDescontoFixo { get; set; }
        public bool FreteGratis { get; set; }
        public decimal? ValorMinimoCompra { get; set; }

        // Desconto progressivo (JSON)
        public string RegrasProgressivas { get; set; } = "[]"; 
        // [{"quantidadeMinima": 10, "percentualDesconto": 5}]

        // Limites
        public int? LimiteUsoPorCliente { get; set; }
        public int? LimiteUsoTotal { get; set; }
        public int UsosRealizados { get; set; }

        // Prioridade
        public int Prioridade { get; set; } = 1;
        public bool AcumulaComOutras { get; set; }

        // Copy gerado por IA (fase futura)
        public string? TituloSugerido { get; set; }
        public string? CallToActionSugerido { get; set; }

        // Métricas
        public decimal ReceitaGerada { get; set; }
        public int PedidosComEssaCampanha { get; set; }

        // Método de domínio
        public bool EstaVigente()
        {
            var agora = DateTime.UtcNow;
            return Ativo && agora >= DataInicio && agora <= DataFim;
        }

        public bool PodeSerUsadaPorCliente(int clienteId, int usosJaRealizados)
        {
            if (!EstaVigente()) return false;
            if (LimiteUsoPorCliente.HasValue && usosJaRealizados >= LimiteUsoPorCliente.Value) return false;
            if (LimiteUsoTotal.HasValue && UsosRealizados >= LimiteUsoTotal.Value) return false;
            return true;
        }
    }

    public enum TipoCampanha
    {
        DescontoPercentual = 1,
        DescontoProgressivo = 2,
        FreteGratis = 3,
        Combo = 4,
        Brinde = 5
    }
}
