using System.Security.Cryptography;

namespace LojaProdutosCurso.Services.Autenticacao
{
    public class AutententicacaoServices : IAutenticacaoInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;//codigo unico para cada usuario
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }
    }
}
