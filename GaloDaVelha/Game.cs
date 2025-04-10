using System;

namespace GaloDaVelha
{
    class Game
    {
        private Board panel;
        private string input;
        private Player player1;
        private Player player2;
        private Player player;
        private Player piecePlayer;
        private int playerTurn;

        /// <summary>
        /// The constructor of the class Game creates instances of the necessary objects for the game
        /// </summary>
        public Game()
        {
            panel = new Board();
            input = "";
            playerTurn = 0;
        }

        /// <summary>
        /// This method is the game loop
        /// </summary>
        public void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Here are the Initial instructions for the game
            Console.WriteLine("Welcome to Galo da Velha.");
            Console.WriteLine();
            Console.WriteLine("This game is won by the first person who places a piece and");
            Console.WriteLine("completes a horizontal, vertical or diagonal line of pieces");
            Console.WriteLine("with one of the following characteristics in common: ");
            Console.WriteLine();
            Console.WriteLine("Size, Colour, Shape and if it has a hole, or not.");
            Console.WriteLine();
            Console.WriteLine("One player will first choose the piece to be moved, and the");
            Console.WriteLine("other will follow by placing it on the board.");
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue. ");
            Console.ReadLine();

            // Setting the nicknames for the players
            Console.Write("Input player 1's name: ");
            player1 = new Player(Console.ReadLine());
            Console.Write("Input player 2's name: ");
            player2 = new Player(Console.ReadLine());

            piecePlayer = player2;
            player = player1;

            while (true)
            {
                Console.WriteLine();
                // Show available pieces in panel.piecesleft
                panel.ShowAvailable();
                // the panel is rendered here
                panel.Render();

                // o codigo que ve se um player ganhou deveria estar aqui, ja que o jogo deveria mostrar o render da ultima jogada antes de dar break
                if (panel.CheckWin(player.GetLastPlaceInput()))
                {
                    Console.WriteLine($"{player.GetName()} is the Winner!");
                    break;
                }
                
                while (true)
                {
                    Console.WriteLine($"{player.GetName()}, please input the next piece to be moved. ");
                    input = Console.ReadLine();
                    input = input.ToLower();
                    input = input.Trim();

                    if (input == "exit") break;
                    else
                    {
                        // here the input is converted either into a PiecesChar or an int -1 (false or true)
                        player.ConvertPieceInput(input);
                        //here goes the checker to make sure the input is correct
                        if (!CheckInput(player.GetLastPieceInput())) break;
                    }
                }
                if (input == "exit") break;

                switchPlayer();
                Console.WriteLine();

                while (true)
                {
                    Console.WriteLine($"{player.GetName()}, please input the positioning for the piece {ToPieceUnicoded(piecePlayer.GetLastPieceInput())}. ");
                    input = Console.ReadLine().Replace(" ", "");
                    input = input.ToLower();
                    input = input.Trim();

                    if (input == "exit") break;
                    else
                    {
                        // here the input is converted either into an int[2] or an int -1 (false or true)
                        player.ConvertPlaceInput(input);
                        // here goes the checker to make sure the input is correct
                        if (!CheckInput(player.GetLastPlaceInput())) break;
                    }
                }
                if (input == "exit") break;

                //here the piece should be set in panel
                panel.PiecePlacer(piecePlayer.GetLastPieceInput(), player.GetLastPlaceInput());
            }
        }

        /// <summary>
        /// Here the current player and the pieceplayer are switched with a bit by bit variable.
        /// </summary>
        public void switchPlayer()
        {
            playerTurn ^= 1;
            if (playerTurn == 0)
            {
                player = player1;
                piecePlayer = player2;
            }
            else
            {
                player = player2;
                piecePlayer = player1;
            }
        }

        /// <summary>
        /// This is a CheckInput method specific for PieceChar
        /// It first checks if the previously converted input is -1 (the PieceChar values of invalid). If true, returns true.
        /// Else, if the piece is still inside piecesLeft, it returns false. Else, an error is issued, and returns true.
        /// </summary>
        /// <param name="input"> A variable containing the converted input of some piece, or an Invalid Piecechar </param>
        /// <returns>returns true, or false</returns>
        public bool CheckInput(PiecesChar input)
        {
            bool hasError = false;

            if (input == PiecesChar.Invalid)
            {
                hasError = true;
            }
            else
            {
                foreach (Piece piece in panel.GetpiecesLeft())
                {
                    if (piece != null && input == piece.GetChars())
                    {
                        hasError = false;
                        break;
                    }
                    else
                    {
                        hasError = true;
                    }
                }
                if ( hasError == true ) Console.WriteLine("This Piece has already been played. Try again. ");
            }
        
            return hasError;
        }

        /// <summary>
        /// This is a CheckInput method specific for int[]
        /// It first checks if the previously converted input is -1 (the PieceChar values of invalid). If true, returns true.
        /// Else, if the space in the game board is empty, it returns false. Else, an error is issued, and returns true.
        /// </summary>
        /// <param name="input"> A variable containing the converted input of some coordinates, or an Invalid Piecechar </param>
        /// <returns>returns true, or false</returns>
        public bool CheckInput(int[] input)
        {
            bool hasError = false;

            if (input[0] == -1)
            {
                hasError = true; 
            }
            else
            {
                if (panel.Getboard()[input[0], input[1]] == null)
                {
                    hasError = false;
                }
                else
                {
                    Console.WriteLine("This Placement already has a piece in it. Try again. ");
                    hasError = true;
                }
            }
            return hasError;
        }

        /// <summary>
        /// This method searches through the pieces left to find the specified characteristics piecechar in order to correlate them with a string
        /// </summary>
        /// <param name="piecechar"> This is are the characteristics that the method will be looking for</param>
        /// <returns> It returns a string of unicoded characters specific to a Piece </returns>
        public string ToPieceUnicoded(PiecesChar piecechar)
        {
            string result = "no name"; // default value
            foreach (Piece piece in panel.GetpiecesLeft())
            {
                if (piece != null && piecechar == piece.GetChars())
                {
                    result = piece.GetUnicoded();
                    break; // Break the loop once the piece is found
                }
            }
            return result;
        }
    } 
}