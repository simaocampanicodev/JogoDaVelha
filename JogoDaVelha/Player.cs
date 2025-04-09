public class Player
{
    private string nome;

    /// <summary>
    /// Construtor da classe Player.
    /// </summary>
    /// <param name="nomePlayer">Nome do jogador.</param>
    public Player(string nomePlayer)
    {
        nome = nomePlayer; 
    }

    /// <summary>
    /// Método para retornar o nome do jogador
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return nome;
    }
}
