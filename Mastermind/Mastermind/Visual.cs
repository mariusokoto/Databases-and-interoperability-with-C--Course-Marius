using System;
namespace Mastermind
{
    public class Visual
    {
        public Visual()
        {
        }

        public void displayID()
        {



            MariaDB maria = new MariaDB();

            string username2=maria.CheckUserExist();

            displayLogo(username2,maria);
        }





        /// <summary>
        /// Displays Mastermind Logo
        /// </summary>
        public void displayLogo(string username, MariaDB maria)
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
            Console.WriteLine("                        Welcome to Mastermind game " + username.ToUpper());
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("          >>> Press any key to enter the game, Esc to exit <<<");

            Console.ForegroundColor = ConsoleColor.White;
            ConsoleKeyInfo info = Console.ReadKey(true);
            if (info.Key != ConsoleKey.Escape)
            {
                
                    displayGameMenu(maria, username);
                
            }


        }

        /// <summary>
        /// Displays game menu
        /// </summary>
        public void displayGameMenu(MariaDB maria, string username)
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
                Console.WriteLine("     |     1- Launch new game                                                   |");
                Console.WriteLine("     |     L- See List Players                                                  |");
                Console.WriteLine("     |     M- See records                                                       |");
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
                            display2PlayersGame(maria,username);
                            optionMenuChosen = true;
                            break;
                        case ConsoleKey.L:
                            maria.ListPlayers(maria,username);
                            optionMenuChosen = true;
                            break;
                        case ConsoleKey.M:         
                            maria.ShowMainScore(maria,username);
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


        public void display2PlayersGame(MariaDB maria,string username) {

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(@" _______                    ________                         ________   ________ ");
            Console.WriteLine(@" \      \   ______  _  __  /  _____/_____    _____   ____   /_   \   \ /   /_   |");
            Console.WriteLine(@" /   |   \_/ __ \ \/ \/ / /   \  ___\__  \  /     \_/ __ \   |   |\   Y   / |   |");
            Console.WriteLine(@"/    |    \  ___/\     /  \    \_\  \/ __ \|  Y Y  \  ___/   |   | \     /  |   |");
            Console.WriteLine(@"\____|__  /\___  >\/\_/    \______  (____  /__|_|  /\___  >  |___|  \___/   |___|");
            Console.WriteLine(@"        \/     \/                 \/     \/      \/     \/                       ");
            Console.WriteLine();
            Console.WriteLine();

            
            
            PlayerGuessRow playerG = new PlayerGuessRow
            {
                name = username,
            };            
            PlayerSolution playerS = new PlayerSolution
            {
                name = "Computer",
            };

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;


            Board board = new Board();
            PlayGameMastermind play = new PlayGameMastermind();
            play.game2Players(maria, playerG, playerS, board);

        }


    }
}

