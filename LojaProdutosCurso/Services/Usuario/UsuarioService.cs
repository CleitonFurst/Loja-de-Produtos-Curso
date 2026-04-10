using AutoMapper;
using LojaProdutosCurso.Data;
using LojaProdutosCurso.DTO.Login;
using LojaProdutosCurso.DTO.Usuario;
using LojaProdutosCurso.Models;
using LojaProdutosCurso.Services.Autenticacao;
using LojaProdutosCurso.Services.Sessao;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutosCurso.Services.Usuario
{
    public class UsuarioService : IUsuariointerface
    {
        private readonly DataContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IMapper _mapper;
        private readonly ISessaoInterface _sessaoInterface;

        public UsuarioService(DataContext context, IAutenticacaoInterface autenticacaoInterface, IMapper mapper, ISessaoInterface sessaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
            _sessaoInterface = sessaoInterface;
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

        public async Task<UsuarioModel> Excluir(int id)
        {
            try
            {
                var usuario = await BuscarUsuarioPorId(id);
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> Editar(EditarUsuarioDTO editarUsuarioDTO)
        {
            try
            {
                var usuarioBanco = await _context.Usuarios.Include(u => u.Endereco).FirstOrDefaultAsync(u => u.Id == editarUsuarioDTO.Id);

                usuarioBanco.Nome = editarUsuarioDTO.Nome;
                usuarioBanco.Email = editarUsuarioDTO.Email;
                usuarioBanco.Cargo = editarUsuarioDTO.Cargo;
                usuarioBanco.DataAlteracao = DateTime.Now;
                usuarioBanco.Endereco = _mapper.Map<EnderecoModel>(editarUsuarioDTO.Endereco);

                _context.Update(usuarioBanco);
                await _context.SaveChangesAsync();

                return usuarioBanco;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> Login(LoginUsuarioDTO loginUsuarioDTO)
        {
            try
            {
                var usuarioBanco = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginUsuarioDTO.Email); //verifica se o email existe no banco, se não existir retorna null

                if (usuarioBanco == null)
                {
                    return null;
                }
                if(!_autenticacaoInterface.VerificaLogin(loginUsuarioDTO.Senha, usuarioBanco.SenhaHash, usuarioBanco.SenhaSalt))//verifica se a senha digitada é igual a senha do banco, se não for retorna null
                { 
                    return null;
                }
                _sessaoInterface.CriarSessao(usuarioBanco);
                return usuarioBanco;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
