namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Categoria de produtos (hierárquica)
    /// </summary>
    public class Categoria : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        // Hierarquia
        public int? CategoriaPaiId { get; set; }
        public virtual Categoria? CategoriaPai { get; set; }
        public virtual ICollection<Categoria> Subcategorias { get; set; } = new List<Categoria>();

        // Relacionamentos
        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
