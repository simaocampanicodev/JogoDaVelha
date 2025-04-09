using System;

namespace GaloDaVelha
{   
    class Program
    {   
        /// <summary>
        /// Método principal do programa.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao GaloDaVelha!");

            Console.WriteLine("Instruções de Jogo:");
            Console.WriteLine("1. Cada jogador escolhe uma peça para o adversário colocar no tabuleiro.");
            Console.WriteLine("2. O objetivo é formar uma linha de quatro peças com pelo menos uma característica semelhante.");
            Console.WriteLine("3. Uma linha pode ser vertical, horizontal ou diagonal.");
            Console.WriteLine("4. As características são: tamanho(Grande ou Pequena), cor(Clara ou Escura), forma(Redonda ou Quadrada) e furo(Com ou Sem).");

            Console.Write("Nome do Jogador 1: ");
            string nomePlayer1 = Console.ReadLine();
            Player player1 = new Player(nomePlayer1);

            Console.Write("Nome do Jogador 2: ");
            string nomePlayer2 = Console.ReadLine();
            Player player2 = new Player(nomePlayer2);

            Game game = new Game(player1, player2);
            game.Play();
        }
    }
}
