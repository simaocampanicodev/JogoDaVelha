using System;

namespace GaloDaVelha
{
    /// <summary>
    /// Configuration of the board, firstly defines its dimensions, then creates
    /// the pieces, the visual representation of the board and shows what positions
    /// are available. Lastly it places the pieces chosen by the players on the 
    /// coordinates they have determined.
    /// </summary>
    public class Board
    {
        private int height;
        private int width;
        private Piece[] piecesLeft;
        private Piece[,] board;

        public Board()
        {
            height = 4;
            width = 4;
            piecesLeft = new Piece[height * width];
            board = new Piece[height, width];
            createPieces();
        }

        /// <summary>
        /// Creates all the pieces of the game inside the array piecesLeft, 
        /// to be used later to render the board and choose pieces, etc
        /// </summary>
        public void createPieces()
        {
            for (int i = 0; i < piecesLeft.Length; i++)
            {
                // Creates a piece and adds it to piecesLeft
                piecesLeft[i] = new Piece();

                // Checks if the i is valid to give each piece their characteristic
                if (i % 2 == 0)
                {
                    piecesLeft[i].charModify(PiecesChar.BigOrSmall);
                }
                if ((i >= 0 && i < 2) || (i >= 4 && i < 6) || (i >= 8 && i < 10) || (i >= 12 && i < 14) || (i >= 16 && i < 18))
                {
                    piecesLeft[i].charModify(PiecesChar.WhiteOrBlack);
                }
                if ((i >= 0 && i < 4) || (i >= 8 && i < 12))
                {
                    piecesLeft[i].charModify(PiecesChar.CircleOrSquare);
                }
                if (i >= 0 && i < 8)
                {
                    piecesLeft[i].charModify(PiecesChar.HoleOrNoHole);
                }

                piecesLeft[i].unicodedModify();
            }
        }

        /// <summary>
        /// Visual Representation of the board, aswell as other helpfull instructions
        /// </summary>
        public void Render()
        {
            string[] inst = new string[]
            {
                " win with",
                " | \u2810\u2810\u2810\u2810 |   big \u25B2 / small \u25B4",
                " |   \u2847  |   white \u26AA / black \u26AB",
                " | \u2880\u2820\u2810\u2808 |   circle \u25CF / square \u2BC0",
                " | \u2801\u2802\u2804\u2840 |   hole \U0001F573 / nohole"
            };

            Console.WriteLine(" ___ _____ _____ _____ _____");
            Console.WriteLine($"|   |  A  |  B  |  C  |  D  |{inst[0]}");
            for (int i = 0; i < height; i++)
            {
                Console.Write($"| {i} |");
                for (int j = 0; j < width; j++)
                {
                    if (board[i, j] != null)
                    {
                        Console.Write($"{board[i, j].GetUnicoded()}|");
                    }
                    else
                    {
                        Console.Write("     |");
                    }
                }
                Console.WriteLine(inst[i +1]);
            }
            //followed by some instructions
            Console.WriteLine("Commands: exit/*size *colour *shape *hole/*placement");
            Console.WriteLine("examples: 'big white square nohole' 'A0'");
            Console.WriteLine();
        }
        
        /// <summary>
        /// This method makes the pieces available show on the screen. If the
        /// chosen piece isn't null, then it will be added to a string that is
        /// printed, showing the players what pieces are available. If the piece
        /// chosen is considered as "already used" (null) then it won't be added
        /// to the string and consequently won't appear on screen.
        /// </summary>
        public void ShowAvailable()
        {
            Console.WriteLine("Available Pieces: ");

            Console.Write("|");
            for (int i = 0; i < 8; i++)
            {
                if (piecesLeft[i] != null) //Verifies that the piece as not been used
                {
                    //Prints the available pieces
                    Console.Write($"{piecesLeft[i].GetUnicoded()}|");
                }    
            }
            Console.WriteLine();

            Console.Write("|");
            for (int i = 8; i < 16; i++)
            {
                if (piecesLeft[i] != null) //Verifies that the piece as not been used
                {
                    //Prints the available pieces
                    Console.Write($"{piecesLeft[i].GetUnicoded()}|");
                }    
            }
            Console.WriteLine();
        }

        /// <summary>
        /// This method accepts a piece chosen by a player and its coordinates,
        /// then puts the piece at those coordinates if they are free.
        /// </summary>
        /// <param name="_piece">This represent the chosen piece</param>
        /// <param name="coord">This are the (x,y) coordinates of the piece</param>
        public void PiecePlacer(PiecesChar _piece, int[] coord)
        {
            // Runs through all of piecesLeft array
            for (int i = 0; i < piecesLeft.Length; i++)
            {
                // Checks if the piece chosen is correspondent to a piece in piecesLeft 
                if (piecesLeft[i] != null && piecesLeft[i].GetChars() == _piece)
                {
                    // Puts the piece at the given coordinates
                    board[coord[0], coord[1]] = piecesLeft[i];

                    // The piece is "marked" as used
                    piecesLeft[i] = null;

                    break; // Exit the loop, which means that the move ends
                }
            }
        }

        /// <summary>
        /// This method checks specifically according to the last piece played, so as to not have to check every column and row of board.
        /// firstly it initiates the bool to be returned, and the checkers that are to be incremented until 4 (as in a line of 4 to win).
        /// There is aslo a PieceChar list for easy reading of the method as char, aswell as x, and y.
        /// Then the body of the method starts by running a foreach that gets the next characteristic char
        /// in the previous list that is to be evaluated, and resets the win checkers.
        /// Next, a for if run to change which value piece in board we are checking.
        /// With the established char and i, ifs are then run with every possible win type.
        /// </summary>
        /// <param name="input"> the coordinates of the last piece played</param>
        /// <returns> win returns true if any of the winH, winV, winD have reached 4 </returns>
        public bool CheckWin(int[] input)
        {
            bool win = false;
            int winH;
            int winV;
            int winD1;
            int winD2;
            PiecesChar[] piecechar = new PiecesChar[]
            {
                PiecesChar.BigOrSmall,
                PiecesChar.WhiteOrBlack,
                PiecesChar.CircleOrSquare,
                PiecesChar.HoleOrNoHole
            };
            int y = input[0];
            int x = input[1];

            // here the foreach is called to check for every kind of characteristic that the pieces could have
            foreach (PiecesChar chars in piecechar)
            {
                winH = 0;
                winV = 0;
                winD1 = 0;
                winD2 = 0;

                // this for checks for a horizontal, vertical or diagonal line with the same flag
                for (int i = 0; i < height; i++)
                {

                    // here a horizontal line is checked for
                    // here the ifs are separated for the method to be more readable
                    if ( board[y,i] != null )
                    {
                        if ((board[y,x].GetChars() & chars) != 0 && (board[y,i].GetChars() & chars) != 0)
                        {
                            winH += 1;
                        }
                        else if ((~board[y,x].GetChars() & chars) != 0 && (~board[y,i].GetChars() & chars) != 0)
                        {
                            winH += 1;
                        }
                    }

                    // here a vertical line is checked for
                    // here the ifs are separated for the method to be more readable
                    if ( board[i,x] != null )
                    {
                        if ((board[y,x].GetChars() & chars) != 0 && (board[i,x].GetChars() & chars) != 0)
                        {
                            winV += 1;
                        }
                        else if ((~board[y,x].GetChars() & chars) != 0 && (~board[i,x].GetChars() & chars) != 0)
                        {
                            winV += 1;
                        }
                    }

                    // here a diagonal line is checked for only if the piece is in the diagonal lines itself
                    // here the ifs are separated for the method to be more readable
                    if ( x == y || x + y == 3 )
                    {
                        // diagonal from topleft to bottomright
                        if ( board[i,i] != null )
                        {
                            if ((board[y,x].GetChars() & chars) != 0 && (board[i,i].GetChars() & chars) != 0)
                            {
                                winD1 += 1;
                            }
                            else if ((~board[y,x].GetChars() & chars) != 0 && (~board[i,i].GetChars() & chars) != 0)
                            {
                                winD1 += 1;
                            }
                        }

                        // diagonal from topright to bottomleft
                        if ( board[i,3-i] != null )
                        {
                            if ((board[y,x].GetChars() & chars) != 0 && (board[i,3-i].GetChars() & chars) != 0)
                            {
                                winD2 += 1;
                            }
                            else if ((~board[y,x].GetChars() & chars) != 0 && (~board[i,3-i].GetChars() & chars) != 0)
                            {
                                winD2 += 1;
                            }
                        }
                    }
                }

                if ( winH == 4 || winV == 4 || winD1 == 4 || winD2 == 4 )
                {
                    win = true;
                    break;
                }
            }

            return win;
        }
        
        public Piece[] GetpiecesLeft() => piecesLeft;

        public Piece[,] Getboard() => board;
        
    }
}
