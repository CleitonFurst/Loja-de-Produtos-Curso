using LojaProdutosCurso.Enums;
using LojaProdutosCurso.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LojaProdutosCurso.DTO.Usuario
{
    public class CriarUsuarioDTO
    {
        [Required(ErrorMessage = "Digite o nome !")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione o cargo !")]
        public CargoEnum Cargo { get; set; }
        [Required(ErrorMessage = "Digite o logradouro !")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Digite o bairro !")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Digite o número !")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Digite o CEP !")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Digite o cidade !")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Digite o estado !")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Digite o complemento !")]
        public string? Complemento { get; set; }
        [Required(ErrorMessage = "Digite o senha !")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Digite o confirmação senha !"), Compare("Senha", ErrorMessage = "As senhas não coincidem !")]
        public string ConfirmarSenha { get; set; }

    }
}
