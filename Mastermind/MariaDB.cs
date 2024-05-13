using System;
using System.Reflection.PortableExecutable;
using MySqlConnector;


namespace Mastermind
{
    public class MariaDB
    {
        string myConnectionString;
        string connStr = "server=localhost;user=root;port=3306;password=LeoSQL29!;database=MastermindGame;";
        int connected;

        public MariaDB()
        {
            myConnectionString = "server=localhost;uid=root;pwd=LeoSQL29!;database=MastermindGame;";

        }

        public void testConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(myConnectionString);
                conn.Open();
                var stm = "SELECT VERSION()";
                var cmd = new MySqlCommand(stm, conn);

                var version = cmd.ExecuteScalar().ToString();
                Console.WriteLine($"MariaDB version: {version}");
            }
            catch (Exception ex) { }

        }

        public void createDB(string name)
        {
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{name}`;";
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");
            }
        }

        public void CreateUsersTable()
        {
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS users (
                                id INT AUTO_INCREMENT PRIMARY KEY,
                                username VARCHAR(255) NOT NULL,
                                password VARCHAR(255) NOT NULL,
                                connected INT DEFAULT 0)";
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");
            }
        }


        public void CreateTablePlays()
        {
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS plays (
                                id INT AUTO_INCREMENT PRIMARY KEY,
                                username VARCHAR(255),
                                date_time DATETIME,
                                score INT)";
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");

                cmd.CommandText = @"ALTER TABLE plays ADD COLUMN surname VARCHAR(255)";
                response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");
            }
        }


        public void createUser(string username, string password)
        {
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO user(username,password) VALUES(@username, @password)";
                cmd.Parameters.AddWithValue("@username", $"{username}");
                cmd.Parameters.AddWithValue("@password", $"{password}");
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void ListPlayers(MariaDB maria, string username)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT username FROM users", conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("List of Players:");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["username"]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // handle exception here
            }

            ConsoleKeyInfo info = Console.ReadKey(true);
            if (info.Key != ConsoleKey.Escape)
            {
                Visual vis = new Visual();
                vis.displayGameMenu(maria, username);

            }
        }

        public void deletePlayer(string name)
        {
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "Delete from players where name = '" + name + "'";
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");

            }
        }

        public string CheckUserExist()
        {
            string username = "";
            bool isAuthenticated = false;

            while (!isAuthenticated)
            {
                Console.WriteLine("Enter username");
                username = Console.ReadLine();
                Console.WriteLine("Enter password");
                string password = Console.ReadLine();

                using (var conn = new MySqlConnection(connStr))
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();

                        MySqlCommand command = new MySqlCommand("SELECT username FROM users WHERE username = @username", conn);
                        command.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine("User exists.");

                                reader.Close();

                                command.CommandText = "SELECT username FROM users WHERE username = @username AND password = @password";
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@username", username);
                                command.Parameters.AddWithValue("@password", password);

                                using (MySqlDataReader passwordReader = command.ExecuteReader())
                                {
                                    if (passwordReader.Read())
                                    {
                                        Console.WriteLine("Access allowed. Welcome, " + username);
                                        isAuthenticated = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect password. Please try again.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("User doesn't exist. Creating a new user.");

                                reader.Close(); // Close the existing reader before executing another command
                                command.CommandText = "INSERT INTO users (username, password, connected) VALUES (@username, @password, 1)";
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@username", username);
                                command.Parameters.AddWithValue("@password", password);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("New user added: " + username);
                                    isAuthenticated = true; // Authenticate the new user
                                }
                                else
                                {
                                    Console.WriteLine("Failed to add the new user.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        // handle exception here
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return username;
        }



        public void ListRecords(string username, MariaDB maria)
        {
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM plays WHERE username = @username";
                    cmd.Parameters.AddWithValue("@username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("ID\tUsername\tDate\t\t\tScore");
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string playUsername = reader.GetString("username");
                            DateTime date = reader.GetDateTime("date_time");
                            int score = reader.GetInt32("score");

                            Console.WriteLine($"{id}\t{playUsername}\t{date}\t{score}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }


            ConsoleKeyInfo info = Console.ReadKey(true);
            if (info.Key != ConsoleKey.Escape)
            {
                Visual vis = new Visual();
                vis.displayGameMenu(maria, username);

            }
        }

    }








    }

