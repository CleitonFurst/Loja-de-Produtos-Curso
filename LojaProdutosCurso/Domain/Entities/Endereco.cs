namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Endereço de usuário (1:1)
    /// </summary>
    public class Endereco : BaseEntity
    {
        public string Logradouro { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Complemento { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
