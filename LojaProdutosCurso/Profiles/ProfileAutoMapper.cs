using AutoMapper;
using LojaProdutosCurso.DTO.Endereco;
using LojaProdutosCurso.Models;

namespace LojaProdutosCurso.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<EnderecoModel, EditarEnderecoDTO>().ReverseMap();
        }
    }
}
