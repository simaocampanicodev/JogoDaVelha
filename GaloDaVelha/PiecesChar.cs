using System;

namespace GaloDaVelha
{
    [Flags]
    public enum PiecesChar
    {
        BigOrSmall = 1 << 0,
        WhiteOrBlack = 1 << 1,
        CircleOrSquare = 1 << 2,
        HoleOrNoHole = 1<< 3,
        Invalid = -1
    }
}
