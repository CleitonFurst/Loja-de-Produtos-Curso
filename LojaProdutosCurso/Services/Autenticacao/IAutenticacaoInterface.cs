namespace LojaProdutosCurso.Services.Autenticacao
{
    public interface IAutenticacaoInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool verificaLogin(string senha, byte[] senhaHash, byte[] senhaSalt);

    }
}
