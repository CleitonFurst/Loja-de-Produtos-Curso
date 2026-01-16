namespace LojaProdutosCurso.Domain.Entities
{
    /// <summary>
    /// Endereço de cliente B2B (pode ter múltiplos)
    /// </summary>
    public class EnderecoCliente : BaseEntity
    {
        public string Nome { get; set; } = string.Empty; // Ex: "Loja Centro", "Depósito"
        public string Logradouro { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        public bool EnderecoPadrao { get; set; } = false;
        public bool EnderecoFaturamento { get; set; } = false;

        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }
    }
}
