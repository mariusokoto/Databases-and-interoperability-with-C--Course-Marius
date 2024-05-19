using System;
namespace Mastermind


{
    public class PlayerGuessRow : Player
    {
        private List<Row> guessedRows { get; set; }

        public PlayerGuessRow()
        {
            guessedRows = new List<Row>(); 
        }

        public void takeGuessRow()
        {
            Console.WriteLine("Guessing player, enter a combination of 5 colors(yellow, red, orange, black, white, green, brown, blue)");
            Console.WriteLine();
            Row row = createRow();
            saveRow(row);
            
        }

        public List<Row> GetRows()
        {
            return guessedRows;
        }

        public static string displayRow(Row row)
        {
            return string.Join(" | ", row.GetRow().Select(pawn => pawn.GetColor())); 
        }

        public void saveRow(Row guessingRow)
        {
            guessedRows.Add(guessingRow);
        }
    }

}

