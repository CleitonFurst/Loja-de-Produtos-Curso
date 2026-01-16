using LojaProdutosCurso.Domain.Enums;

namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Lista de preços (pode ser base, por segmento ou por cliente específico)
    /// </summary>
    public class ListaDePreco : BaseEntity
    {
        public string Nome { get; set; } = string.Empty; // "Tabela Gold", "Tabela Padrão"
        public TipoListaPreco Tipo { get; set; }
        
        // Se for por segmento
        public SegmentoCliente? Segmento { get; set; }
        
        // Se for por cliente específico
        public int? ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        // Vigência
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        // Relacionamentos
        public virtual ICollection<ItemListaPreco> Itens { get; set; } = new List<ItemListaPreco>();

        // Métodos de domínio
        public bool EstaVigente()
        {
            var agora = DateTime.UtcNow;
            return Ativo && agora >= DataInicio && (DataFim == null || agora <= DataFim);
        }
    }

    public enum TipoListaPreco
    {
        Base = 1,
        PorSegmento = 2,
        PorCliente = 3
    }
}
