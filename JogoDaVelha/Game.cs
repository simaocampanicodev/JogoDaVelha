using System;

public class Game
{
    private Board board;
    private Player[] players;
    private int playerAtual;

    /// <summary>
    /// Construtor da classe Game.
    /// </summary>
    /// <param name="player1">Primeiro jogador.</param>
    /// <param name="player2">Segundo jogador.</param>
    public Game(Player player1, Player player2)
    {
        board = new Board();
        players = new Player[] {player1, player2};
        playerAtual = 0;
    }

    /// <summary>
    /// Método que inicia o jogo.
    /// </summary>
    public void Play()
    {
        bool jogoAtivo = true;
        while (jogoAtivo)
        {   
            //Muda a cor da consola para azul.
            Console.BackgroundColor = ConsoleColor.Black;
            
            Console.WriteLine("Jogador atual: " + players[playerAtual].GetName());
            board.Exibir();

            Console.WriteLine("Adversário, escolha uma peça para o jogador atual.");
            Console.WriteLine("\nTamanho(Grande ou Pequena), cor(Clara ou Escura), forma(Redonda ou Quadrada) e furo(Com ou Sem): ");
            var pieceAdv = Console.ReadLine().Trim().Split(',');

            //Código sugerido pelo ChatGTP para o parsing das peças
            Tamanho tamanhoAdv = (Tamanho)Enum.Parse(typeof(Tamanho), pieceAdv[0]);
            Cor corAdv = (Cor)Enum.Parse(typeof(Cor), pieceAdv[1]);
            Forma formaAdv = (Forma)Enum.Parse(typeof(Forma), pieceAdv[2]);
            Furo furoAdv = (Furo)Enum.Parse(typeof(Furo), pieceAdv[3]);

            Piece pieceFinal = new Piece(tamanhoAdv, corAdv, formaAdv, furoAdv);

            // Colocar a peça escolhida pelo adversário no tabuleiro
            Console.WriteLine("Jogador atual, coloque a peça no tabuleiro.");
            Console.WriteLine("Escolha a linha para colocar a peça (0 a 3): ");
            int linha = int.Parse(Console.ReadLine());
            Console.WriteLine("Escolha a coluna para colocar a peça (0 a 3): ");
            int coluna = int.Parse(Console.ReadLine());

            if (board.ColocarPeça(linha, coluna, pieceFinal))
            {
                if (board.Vitoria())
                {
                    board.Exibir();
                    Console.WriteLine("O jogador " + players[playerAtual].GetName() + " venceu!");
                    jogoAtivo = false;
                }

                //Alterna entre os jogadores
                else
                {
                    playerAtual = (playerAtual + 1) % 2;
                }
            }
            else
            {
                Console.WriteLine("Posição inválida!");
                Console.ReadKey();
            }

        }
    }

}
