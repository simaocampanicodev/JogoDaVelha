using System;
using System.Numerics;


namespace GaloDaVelha
{
    public class Player
    {
        private PiecesChar LastPieceInput;
        private int [] LastPlaceInput;
        private string name;
        public Player(string _name)
        {
            name = _name;
            LastPieceInput = 0;
            LastPlaceInput = new int [2] {0, 0};
        }  

        /// <summary>
        /// This method Converts, and assures that the received input is valid to proceed and places it into LastPieceInput.
        /// 
        /// This method receives an input, which is then split and put into an array words.
        /// The method also initializes Pieces chars to 0. In the body of the method, the desired length of 4 is verified, otherwise error is true.
        /// Then, if there were no errors, 4 foreach are run, to see if, despite the order, there are all the necessary words in the array, otherwise error is true.
        /// If there are no errors, the initialized PiecesChar variable should now have the characteristics of the specified input, which is passed on to the object's LastPieceInput.
        /// Else, LastPieceInput shall become "Invalid".
        /// </summary>
        /// <param name="input"> Here the method receives a string with hopefully 4 words, separeted by spaces</param>
        public void ConvertPieceInput(string input)
        {
            string[] words = input.Split(' ');
            PiecesChar chars = 0;
            bool error = false;

            if (words.Length == 4)
            {
                foreach (string word in words)
                {
                    if (word == "big" || word == "bi") 
                    {
                        chars |= PiecesChar.BigOrSmall;
                        error = false;
                        break;
                    }
                    else if (word == "small" || word == "sm")
                    {
                        chars &= ~PiecesChar.BigOrSmall;
                        error = false;
                        break;
                    }
                    else error = true;
                }

                if (error == false)
                {
                    foreach (string word in words)
                    {
                        if (word == "white" || word == "w")
                        {
                            chars |= PiecesChar.WhiteOrBlack;
                            error = false;
                            break;
                        }
                        else if (word == "black" || word == "bl")
                        {
                            chars &= ~PiecesChar.WhiteOrBlack;
                            error = false;
                            break;
                        }
                        else error = true;
                    }
                }

                if (error == false)
                {
                    foreach (string word in words)
                    {
                        if (word == "circle" || word == "c")
                        {
                            chars |= PiecesChar.CircleOrSquare;
                            error = false;
                            break;
                        }
                        else if (word == "square" || word == "sq")
                        {
                            chars &= ~PiecesChar.CircleOrSquare;
                            error = false;
                            break;
                        }
                        else error = true;
                    }
                }

                if (error == false)
                {
                    foreach (string word in words)
                    {
                        if (word == "hole" || word == "h")
                        {
                            chars |= PiecesChar.HoleOrNoHole;
                            error = false;
                            break;
                        }
                        else if (word == "nohole" || word == "n")
                        {
                            chars &= ~PiecesChar.HoleOrNoHole;
                            error = false;
                            break;
                        }
                        else error = true;
                    }
                }
                
                if (error) Console.WriteLine("Wrong wording. Try again. ");
            }
            else
            {
                Console.WriteLine("Wrong amount of words or incorrect spacing. Try again. ");
                error = true;
            }

            if (error == true) LastPieceInput = PiecesChar.Invalid;
            else LastPieceInput = chars;
        }

        /// <summary>
        /// This method Converts, and assures that the received input is valid to proceed and places it into LastPlaceInput.
        /// 
        /// The method starts by initializing the array of init[2] and error.
        /// Then in the body of the method, the input is checked for the correct length of 2 (for two coordinates), if not accepted error becomes true.
        /// Inside the if, the first char of input is checked, to see if it matches any of the coordinates stated in the board rendering.
        /// if so, the corresponding coordinate will be applied to pos. Else error will become true.
        /// Then the second char of input is checked to be between 0 and 3, if so it will go directly into pos.
        /// At last, if there are no errors, pos is applied to the object's LastPlaceInput. Otherwise LastPlaceInput[0] will become "Invalid"
        /// </summary>
        /// <param name="input"> Here the variable hopefully receives a string of one letter and one number (coordinates according to the rendered board) </param>
        public void ConvertPlaceInput(string input)
        {
            int [] pos = new int[2];
            bool error = false;

            if (input.Length == 2)
            {
                if (input[0] == 'a') pos[1] = 0;
                else if (input[0] == 'b') pos[1] = 1;
                else if (input[0] == 'c') pos[1] = 2;
                else if (input[0] == 'd') pos[1] = 3;
                else 
                {
                    Console.WriteLine("Wrong vertical input. Try again.");
                    error = true;
                }

                if (0 <= ((int) input[1]) || ((int) input[1]) <= 3) pos[0] = int.Parse(input[1].ToString());
                else
                {
                    Console.WriteLine("Wrong horizontal input. Try again.");
                    error = true;
                }
            }
            else
            {
                Console.WriteLine("Wrong amount of letters. Try again. ");
                error = true;
            }

            if (error == true) LastPlaceInput[0] = -1;
            else LastPlaceInput = pos;
        }
        public string GetName() => name;
        public PiecesChar GetLastPieceInput() => LastPieceInput;
        public int [] GetLastPlaceInput() => LastPlaceInput;
        
    }
}