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
            Row row = createRow();
            saveRow(row);
        }


        public void saveRow(Row guessingRow)
        {
            guessedRows.Add(guessingRow);

        }

    }
}

