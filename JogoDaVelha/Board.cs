using System;

public class Board
{
    private Piece[,] tabuleiro;
    private const int TamanhoTab = 4;

    /// <summary>
    /// Construtor da classe Board.
    /// </summary>
    public Board()
    {
        tabuleiro = new Piece[TamanhoTab, TamanhoTab];
    }


    /// <summary>
    /// Permite colocar uma peça no tabuleiro, verificando se a posição está 
    /// ocupada ou não.
    /// </summary>
    /// <param name="linha">Linha que vai ser colocada a peça.</param>
    /// <param name="coluna">Coluna que vai ser colocada a peça.</param>
    /// <param name="peça">Peça que vai ser posta no tabuleiro.</param>
    /// <returns>Retorna true se a peça for colocada, se não retorna false.</returns>
    public bool ColocarPeça(int linha, int coluna, Piece peça)
    {
        if (linha < 0 || linha >= TamanhoTab || coluna < 0 || coluna >= TamanhoTab)
            return false;

        if (tabuleiro[linha, coluna] == null)
        {
            tabuleiro[linha, coluna] = peça;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Verifica se as peças tem alguma semelhança.
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="p4"></param>
    /// <returns></returns>
    private bool VerIgual(Piece p1, Piece p2, Piece p3, Piece p4)
    {
        if (p1 == null || p2 == null || p3 == null || p4 == null)
            return false;

        return p1.Igual(p2) && p1.Igual(p3) && p1.Igual(p4);
    }

    /// <summary>
    /// Verifica se as peças em uma linha são iguais.
    /// </summary>
    /// <param name="linha">Linha verificada.</param>
    /// <returns></returns>
    private bool VerLinha(int linha)
    {
        return VerIgual(tabuleiro[linha, 0], tabuleiro[linha, 1], tabuleiro[linha, 2], tabuleiro[linha, 3]);
    }

    /// <summary>
    /// Verifica se as peças em uma coluna são iguais.
    /// </summary>
    /// <param name="coluna">Coluna verificada.</param>
    /// <returns></returns>
    private bool VerColuna(int coluna)
    {
        return VerIgual(tabuleiro[0, coluna], tabuleiro[1, coluna], tabuleiro[2, coluna], tabuleiro[3, coluna]);
    }

    /// <summary>
    /// Verifica se as peças na diagonal são iguais.
    /// </summary>
    /// <returns></returns>
    private bool VerDiagonais()
    {
        return VerIgual(tabuleiro[0, 0], tabuleiro[1, 1], tabuleiro[2, 2], tabuleiro[3, 3]) ||
               VerIgual(tabuleiro[0, 3], tabuleiro[1, 2], tabuleiro[2, 1], tabuleiro[3, 0]);
    }

    /// <summary>
    /// Verifica se o jogador ganhou.
    /// </summary>
    /// <returns></returns>
    public bool Vitoria()
    {
        for (int i = 0; i < TamanhoTab; i++)
        {
            if (VerLinha(i) || VerColuna(i))
                return true;
        }

        return VerDiagonais();
    }

    //Utilizamos o ChatGTP para fazer este método 
    /// <summary>
    /// Mostra o Tabuleiro no console
    /// </summary>
    public void Exibir()
    {
        Console.WriteLine("   0  1  2  3");
        for (int i = 0; i < TamanhoTab; i++)
        {
            Console.Write(i + " ");
            for (int j = 0; j < TamanhoTab; j++)
            {
                Console.Write("|");
                if (tabuleiro[i, j] != null)
                    Console.Write(tabuleiro[i, j].ToString());
                else
                    Console.Write("   ");
            }
            Console.WriteLine("|");
        }
    }
}