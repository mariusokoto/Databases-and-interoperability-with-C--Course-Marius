﻿using System;
namespace Mastermind
{
    public class ClueBlack: Clue
    {
        public string color = "black";


        public ClueBlack()
        {
        }

        public int find_Clue(Row guess_row, Row secret_row)
        {
            return 0;
        }

    }
}

