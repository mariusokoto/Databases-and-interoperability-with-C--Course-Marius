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

        public void game2Players(PlayerGuessRow playerG, PlayerSolution playerS)
        {
            playerS.createSecretRow();

        }

        public string askName()
        {
            string name;
            do
            {
                name = Console.ReadLine();

            } while (name == null);
            return name;
        }

        

    }
}

