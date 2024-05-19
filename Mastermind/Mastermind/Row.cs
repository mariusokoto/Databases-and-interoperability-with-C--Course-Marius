using System;

namespace Mastermind
{
    public class Row
    {
        private Pawn[] row { get; set; } 

        public Row(Pawn[] pawns)
        {
            this.row = pawns;
        }

        public Pawn[] GetRow()
        {
            return row;
        }

    }
}

