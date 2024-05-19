using System;
using System.Xml.Linq;
using System.Diagnostics;

namespace Mastermind
{
    public class PlayGameMastermind
    {
        public Board board;
        public int attemptsLeft;

        public PlayGameMastermind()
        { }

        public void game2Players(MariaDB maria, PlayerGuessRow playerG, PlayerSolution playerS, Board board)
        {
            playerS.createSecretRow();
            Console.Clear();

            int cmpt = 0;

            bool checkWinner = false;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            do
            {
                cmpt += 1;

                Console.WriteLine(cmpt+ " guessing round");

                playerG.takeGuessRow();
                checkWinner = checkGameFinished( playerG, playerS,cmpt);

            } while (cmpt < 10 & checkWinner==false);

            stopwatch.Stop();
            int score = 0;

            if (checkWinner)
            {
                score = CalculateScore(stopwatch, cmpt);

                Console.WriteLine();
                Console.WriteLine(" __      __.__                            ");
                Console.WriteLine("/  \\    /  \\__| ____   ____   ___________ ");
                Console.WriteLine("\\   \\/\\/   /  |/    \\ /    \\_/ __ \\_  __ \\");
                Console.WriteLine(" \\        /|  |   |  \\   |  \\  ___/|  | \\/");
                Console.WriteLine("  \\__/\\  / |__|___|  /___|  /\\___  >__|   ");
                Console.WriteLine("       \\/          \\/     \\/     \\/       ");
                Console.WriteLine();
                Console.WriteLine("Congratulations you have won the game in "+cmpt+" rounds");
                Console.WriteLine($"Duration: {stopwatch.Elapsed.TotalSeconds} seconds");
                Console.WriteLine();
                Console.WriteLine($"Your score is: {score}");
                Console.WriteLine();
                Console.WriteLine("The Secret Row was");
                Row secretRow = playerS.GetSolutionRow();
                Console.WriteLine(PlayerGuessRow.displayRow(secretRow));
                Console.WriteLine();
                Console.WriteLine("          >>> Press any key to return to menu, Esc to exit <<<");

                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Escape)
                {
                    Console.Clear();
                }

            }

            else
            {
                Console.WriteLine(".____    ________    ______________________________ ");
                Console.WriteLine("|    |   \\_____  \\  /   _____/\\_   _____/\\______   \\");
                Console.WriteLine("|    |    /   |   \\ \\_____  \\  |    __)_  |       _/");
                Console.WriteLine("|    |___/    |    \\/        \\ |        \\ |    |   \\");
                Console.WriteLine("|_______ \\_______  /_______  //_______  / |____|_  /");
                Console.WriteLine("        \\/       \\/        \\/         \\/         \\/ ");
                Console.WriteLine();
                Console.WriteLine("You have lost");
                Console.WriteLine($"Duration: {stopwatch.Elapsed.TotalSeconds} seconds");
                Console.WriteLine();
                Console.WriteLine($"Your score is: {score}");
                Console.WriteLine();
                Console.WriteLine("          >>> Press any key to return to menu, Esc to exit <<<");

                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Escape)
                {
                    Console.Clear();
                }
            }


            maria.giveScore(playerG.name, score);
       
        }

        public bool checkGameFinished(PlayerGuessRow playerG, PlayerSolution playerS,int cmpt)
        {
            bool checkWinner = false;

            if (playerG.GetRows().Any()) 
            {
             
                Row lastRow = playerG.GetRows().Last();
                Row secretRow = playerS.GetSolutionRow();
                //Console.WriteLine("The Secret Row");
                //Console.WriteLine(PlayerGuessRow.displayRow(secretRow));
                Console.WriteLine();
                Console.WriteLine("The "+cmpt+" Guessing Row");
                Console.WriteLine(PlayerGuessRow.displayRow(lastRow));
                Console.WriteLine();
                

                Clue[] clues = playerS.giveClues(lastRow);
                Console.WriteLine("Clues: " + string.Join(" | ", clues.Select(clue => clue.getClueColor())));

                checkWinner = clues.All(clue => clue.getClueColor() == "black") && clues.Length == lastRow.GetRow().Length;
                Console.WriteLine();
                Console.WriteLine();
            }


            return checkWinner;
        }

        public int CalculateScore(Stopwatch stopwatch, int cmpt)
        {
            double timeTaken = stopwatch.Elapsed.TotalSeconds;
            int score = (int)(1000 / timeTaken)*(10-cmpt); 
            return score;
        }

        
        

    }
}

