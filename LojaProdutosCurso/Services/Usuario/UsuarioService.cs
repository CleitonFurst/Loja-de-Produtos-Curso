using LojaProdutosCurso.Data;
using LojaProdutosCurso.DTO.Usuario;
using LojaProdutosCurso.Models;
using LojaProdutosCurso.Services.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutosCurso.Services.Usuario
{
    public class UsuarioService : IUsuariointerface
    {
        private readonly DataContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;

        public UsuarioService(DataContext context, IAutenticacaoInterface autenticacaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
        }
        public async Task<List<UsuarioModel>> BuscarUsuarios()
        {
            try
            {
                return await _context.Usuarios.Include(u => u.Endereco).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            try
            {
                var usario = await _context.Usuarios.Include(u => u.Endereco).FirstOrDefaultAsync(u => u.Id == id);
                return usario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerifcaSeExisteEmail(CriarUsuarioDTO criarUsuarioDTO)
        {
            try
            {
                var usuaro = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == criarUsuarioDTO.Email);
                if (usuaro == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CriarUsuarioDTO> CadastrarUsuario(CriarUsuarioDTO criarUsuarioDTO)
        {
            try
            {
                //servico que cria a senhaHash e senhaSalt, são passados variaveis vazias por referencia
                _autenticacaoInterface.CriarSenhaHash(criarUsuarioDTO.Senha, out byte[] senhaHash, out byte[] senhaSalt);
                var usuarioModel = new UsuarioModel()
                {
                    Nome = criarUsuarioDTO.Nome,
                    Email = criarUsuarioDTO.Email,
                    Cargo = criarUsuarioDTO.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt,              
                  
                };

                var endereco = new EnderecoModel()
                {
                    CEP = criarUsuarioDTO.CEP,
                    Logradouro = criarUsuarioDTO.Logradouro,
                    Numero = criarUsuarioDTO.Numero,
                    Complemento = criarUsuarioDTO.Complemento,
                    Bairro = criarUsuarioDTO.Bairro,
                    Cidade = criarUsuarioDTO.Cidade,
                    Estado = criarUsuarioDTO.Estado,
                    Usuario = usuarioModel
                };

                usuarioModel.Endereco = endereco;
                _context.Usuarios.Add(usuarioModel);

                await _context.SaveChangesAsync();

                return criarUsuarioDTO;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
