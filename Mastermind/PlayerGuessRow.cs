using System;
namespace Mastermind


{
    public class PlayerGuessRow: Player
    {
        private List<Row> guessedRows { get; set; }

        public PlayerGuessRow()
        {
        }

        public void takeGuessRow()
        {
            Console.WriteLine("Guessing player enter a combination of colors");
            Console.WriteLine();
            Row row = createRow();
            //saveRow(row);
            Console.WriteLine(displayRow(row));
        }

        public static string displayRow(Row row)
        {
            return string.Join(" | ", row);
        }

        public void saveRow(Row guessingRow)
        {
            guessedRows.Add(guessingRow);
        }

    }
}

