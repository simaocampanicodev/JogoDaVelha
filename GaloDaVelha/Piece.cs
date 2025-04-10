using System;

namespace GaloDaVelha
{
    public class Piece
    {
        private PiecesChar chars;
        private string unicoded;

        public Piece()
        {
            chars = 0;
            unicoded = "0000";
        }
        
        /// <summary>
        /// This method only modifies the identifier of the piece and variable chars
        /// </summary>
        /// <param name="modifier">It receives the parameter to change, colour, shape... etc.</param>
        public void charModify(PiecesChar modifier)
        {
            chars ^= modifier;
        }

        /// <summary>
        /// This method sets the Pieces unicoded string variable according to the characteristis in it's identifier, chars
        /// </summary>
        public void unicodedModify()
        {
            string unicodedBuilder = "";

            if ((chars & PiecesChar.BigOrSmall) != 0) unicodedBuilder += "\u25B2";
            else unicodedBuilder += "\u25B4";

            if ((chars & PiecesChar.WhiteOrBlack) != 0) unicodedBuilder += "\u26AA";
            else unicodedBuilder += "\u26AB";

            if ((chars & PiecesChar.CircleOrSquare) != 0) unicodedBuilder += "\u25CF";
            else unicodedBuilder += "\u2BC0";

            if ((chars & PiecesChar.HoleOrNoHole) != 0) unicodedBuilder += "\U0001F573";
            else unicodedBuilder += " ";

            unicoded = unicodedBuilder;
        }

        public string GetUnicoded() => unicoded;
        
        public PiecesChar GetChars() => chars;

    }
}