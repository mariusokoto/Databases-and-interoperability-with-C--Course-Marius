using System;
namespace Mastermind
{
    public class PlayerSolution: Player
    {
        private Row secretRow { get; set; }

        public PlayerSolution()
        { }

        public void give_Clues(Row guessing_Row, Row secret_one){
            
        }

        public void createSecretRow()
        {
            secretRow = createRow();
        }


    }
}

