using System;
namespace Mastermind
{
    public class Visual
    {
        public Visual()
        {
        }


        /// <summary>
        /// Displays Mastermind Logo
        /// </summary>
        public void displayLogo()
        {
            Console.WriteLine("  _____                   __                       .__            .___ ");
            Console.WriteLine("  /     \\ _____    _______/  |_  ___________  _____ |__| ____    __| _/ ");
            Console.WriteLine(" /  \\ /  \\\\__  \\  /  ___/\\   __\\/ __ \\_  __ \\/     \\|  |/    \\  / __ |  ");
            Console.WriteLine("/    Y    \\/ __ \\_\\___ \\  |  | \\  ___/|  | \\/  Y Y  \\  |   |  \\/ /_/ |  ");
            Console.WriteLine("\\____|__  (____  /____  > |__|  \\___  >__|  |__|_|  /__|___|  /\\____ |  ");
            Console.WriteLine("        \\/     \\/     \\/            \\/            \\/        \\/      \\/  ");
            Console.WriteLine();
            Console.WriteLine("          --------------------------------------------------------              ");
            Console.WriteLine();
            Console.WriteLine("                        Welcome to Mastermind game");
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("          >>> Press any key to enter the game, Esc to exit <<<");

            Console.ForegroundColor = ConsoleColor.White;
            ConsoleKeyInfo info = Console.ReadKey(true);
            if (info.Key != ConsoleKey.Escape)
            {

                displayGameMenu();
            }


        }

        /// <summary>
        /// Displays game menu
        /// </summary>
        public void displayGameMenu()
        {

            bool quit = false;
            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(@"                                   |V| _ __                                    ");
                Console.WriteLine(@"     --------------------------    | |(/_| ||_|    --------------------------- ");
                Console.WriteLine("     |     1- Launch new game 2 players                                         |");
                Console.WriteLine("     |     2- Launch new game 1 player versus Computer                          |");
                Console.WriteLine("     |     3- See records                                                       |");
                Console.WriteLine("     |     Esc - Quit                                                           |");
                Console.WriteLine("      ------------------------------------------------------------------------- ");


                bool optionMenuChosen = true;
                do
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    switch (info.Key)
                    {
                        case ConsoleKey.Escape:
                            optionMenuChosen = true;
                            quit = true;
                            break;
                        case ConsoleKey.D1:           
                        case ConsoleKey.NumPad1:
                            display2PlayersGame();
                            optionMenuChosen = true;
                            break;
                        case ConsoleKey.D2:        
                        case ConsoleKey.NumPad2:
                            display1PlayerGame();
                            optionMenuChosen = true;
                            break;
                        case ConsoleKey.D3:         
                        case ConsoleKey.NumPad3:
                            displayRecord();
                            optionMenuChosen = true;
                            break;
                        default: 
                            optionMenuChosen = false;
                            break;

                    }
                } while (!optionMenuChosen);
            }
            while (!quit);
        }


        public void display2PlayersGame() {

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            //Affiche le titre
            Console.WriteLine(@" _______                    ________                         ________   ________ ");
            Console.WriteLine(@" \      \   ______  _  __  /  _____/_____    _____   ____   /_   \   \ /   /_   |");
            Console.WriteLine(@" /   |   \_/ __ \ \/ \/ / /   \  ___\__  \  /     \_/ __ \   |   |\   Y   / |   |");
            Console.WriteLine(@"/    |    \  ___/\     /  \    \_\  \/ __ \|  Y Y  \  ___/   |   | \     /  |   |");
            Console.WriteLine(@"\____|__  /\___  >\/\_/    \______  (____  /__|_|  /\___  >  |___|  \___/   |___|");
            Console.WriteLine(@"        \/     \/                 \/     \/      \/     \/                       ");
            Console.WriteLine();


            string nom;
            do
            {
                Console.WriteLine("Enter guessing player name");
                nom = Console.ReadLine();
            } while (string.IsNullOrEmpty(nom));
            PlayerGuessRow playerG = new PlayerGuessRow
            {
                name = nom,
            };

            do
            {
                Console.WriteLine("Enter solution player name");
                nom = Console.ReadLine();
            } while (string.IsNullOrEmpty(nom));
            PlayerSolution playerS = new PlayerSolution
            {
                name = nom,
            };

            PlayGameMastermind play = new PlayGameMastermind();
            play.game2Players(playerG, playerS);

        }

        

        public void display1PlayerGame()
        {
            Console.WriteLine("Option 2");
            string inpu = Console.ReadLine();
        }
        public void displayRecord()
        {
            Console.WriteLine("Option 3");
            string inpu = Console.ReadLine();
        }



    }
}

