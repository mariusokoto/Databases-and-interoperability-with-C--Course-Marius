using System;
namespace Mastermind
{
    public class PlayerSolution: Player
    {
        private Row secretRow;
        private static List<string> listColors = new List<string>
    {
        "yellow", "red", "orange", "black", "white", "green", "brown", "blue"
    };

        public PlayerSolution()
        {
            createSecretRow();
        }


        public void createSecretRow()
        {
            Random rand = new Random();
            int rowLength = 5; 
            Pawn[] pawns = new Pawn[rowLength];

            for (int i = 0; i < rowLength; i++)
            {
                string color = listColors[rand.Next(listColors.Count)];
                pawns[i] = new Pawn(color);
            }

            secretRow = new Row(pawns);
        }

        public static string displayRow(Row row)
        {
            return string.Join(" | ", row.GetRow().Select(pawn => pawn.GetColor()));
        }

        public Row GetSolutionRow()
        {
            return secretRow;
        }


        public Clue[] giveClues(Row guessedRow)
        {
            List<Clue> clues = new List<Clue>();
            Pawn[] guessedPawns = guessedRow.GetRow();
            Pawn[] solutionPawns = secretRow.GetRow();
            bool[] guessedMatched = new bool[guessedPawns.Length];
            bool[] solutionMatched = new bool[solutionPawns.Length];

            for (int i = 0; i < guessedPawns.Length; i++)
            {
                if (guessedPawns[i].GetColor() == solutionPawns[i].GetColor())
                {
                    clues.Add(new Clue("black"));
                    guessedMatched[i] = true;
                    solutionMatched[i] = true;
                }
            }

            for (int i = 0; i < guessedPawns.Length; i++)
            {
                if (!guessedMatched[i])
                {
                    for (int j = 0; j < solutionPawns.Length; j++)
                    {
                        if (!solutionMatched[j] && guessedPawns[i].GetColor() == solutionPawns[j].GetColor())
                        {
                            clues.Add(new Clue("white"));
                            solutionMatched[j] = true;
                            break;
                        }
                    }
                }
            }

            return clues.ToArray();
        }

    }
}

