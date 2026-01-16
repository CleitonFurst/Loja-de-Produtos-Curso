namespace LojaProdutosCurso.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? AtualizadoEm { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
