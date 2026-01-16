namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Representa uma empresa/fornecedor na plataforma (Tenant)
    /// </summary>
    public class Empresa
    {
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string InscricaoEstadual { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;

        // Configurações
        public bool PermiteMarketplace { get; set; } = false;
        public bool PermiteCashback { get; set; } = false;
        public decimal PercentualComissaoMarketplace { get; set; } = 0;

        // Relacionamentos
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
