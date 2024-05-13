using System;
using System.Xml.Linq;

namespace Mastermind
{
    public class PlayGameMastermind
    {
        public Board board;
        public int attemptsLeft;

        public PlayGameMastermind()
        { }

        public void game2Players(PlayerGuessRow playerG, PlayerSolution playerS, Board board)
        {
            playerS.createSecretRow();
            Console.Clear();

            playerG.createRow();
            Console.Clear();
       
        }

        public void checkWin(PlayerGuessRow playerG, PlayerSolution playerS)
        {

        }

        

    }
}

