using LojaProdutosCurso.DTO.Endereco;
using LojaProdutosCurso.Enums;

namespace LojaProdutosCurso.DTO.Usuario
{
    public class EditarUsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public CargoEnum Cargo { get; set; }
        public EditarEnderecoDTO Endereco { get; set; }
    }
}
