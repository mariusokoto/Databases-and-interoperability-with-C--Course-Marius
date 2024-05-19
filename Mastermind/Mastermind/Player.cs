using System;
namespace Mastermind
{
    public class Player
    {
        public string name { get; set; }

        public Player()
        {
        }

        public Row createRow()
        {
            Pawn[] tabDePions = new Pawn[5];

            for (int i = 0; i < 5; i++)
            {
                tabDePions[i] = createPawn(askColor(i));
            }
            Row row = new Row(tabDePions);

            return row;
        }

        public string askColor(int ite)
        {
            string input;
            bool verif = true;
            do
            {
                Console.WriteLine($"Enter color {ite + 1}");
                input = Console.ReadLine();

            } while (!verifyColor(input));

            return input;
        }

        public Pawn createPawn(string couleur)
        {
            Pawn pawn1 = new Pawn(couleur);
            return pawn1;

        }

        public bool verifyColor(string color)
        {
            List<string> listColors = new List<string>();
            listColors.Add("yellow");
            listColors.Add("red");
            listColors.Add("orange");
            listColors.Add("black");
            listColors.Add("white");
            listColors.Add("green");
            listColors.Add("brown");
            listColors.Add("blue");

            if (listColors.Contains(color))
            {
                return true;
            }
            return false;
        }


    }
}

