namespace LojaProdutosCurso.Services.Autenticacao
{
    public interface IAutenticacaoInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);

    }
}
