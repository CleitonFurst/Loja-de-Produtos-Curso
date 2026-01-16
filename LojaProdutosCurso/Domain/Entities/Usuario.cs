using LojaProdutosCurso.Domain.Enums;

namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Usuário do sistema (funcionário do fornecedor ou usuário do cliente B2B)
    /// </summary>
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] SenhaHash { get; set; } = Array.Empty<byte>();
        public byte[] SenhaSalt { get; set; } = Array.Empty<byte>();
        public PerfilUsuario Perfil { get; set; }

        // Se for usuário de cliente B2B
        public int? ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        // Relacionamento com empresa (tenant)
        public Guid EmpresaId { get; set; }
        public virtual Empresa? Empresa { get; set; }

        public virtual Endereco? Endereco { get; set; }
    }
}
