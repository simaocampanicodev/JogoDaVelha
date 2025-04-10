using System;

namespace GaloDaVelha
{
    class Program
    {
        /// <summary>
        /// This main creates a an object game of the class Game, and uses it's Start Method, to begin the game
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
