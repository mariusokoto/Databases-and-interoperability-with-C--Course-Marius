using System;
using System.Reflection.PortableExecutable;

using MySqlConnector;

namespace E7THEPROFESSORMARIUSLEORAT
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;port=3306;password=LeoSQL29!;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("MENU:");
                Console.WriteLine("1. Create a new user.");
                Console.WriteLine("2. Change the user password.");
                Console.WriteLine("3. Grant permissions to a user for a specific database.");
                Console.WriteLine("4. Example code to show transactions (with a correct commit and a rollback).");
                Console.WriteLine("5. Exit.");
                Console.WriteLine();
                Console.Write("Choose an option: ");
                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 1:
                            CreateNewUser(connection);
                            break;
                        case 2:
                            ChangeUserPassword(connection);
                            break;
                        case 3:
                            GrantUserPermissions(connection);
                            break;
                        case 4:
                            ShowTransactionExample();
                            break;
                        case 5:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }

                Console.WriteLine();
            }
        }

        // Function to create a new user
        // Note: Creating users should be done with caution. Ensure the user has the minimum necessary privileges.
        static void CreateNewUser(MySqlConnection connection)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            string query = $"CREATE USER '{username}'@'%' IDENTIFIED BY '{password}';";
            using var command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine($"User '{username}' created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        // Function to change user password
        // Note: Changing passwords frequently enhances security. Use strong passwords.
        static void ChangeUserPassword(MySqlConnection connection)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter new password: ");
            string newPassword = Console.ReadLine();
            string query = $"SET PASSWORD FOR '{username}'@'localhost' = '{newPassword}';";
            using var command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine($"Password for user '{username}' changed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        // Function to grant permissions to a user for a specific database
        // Note: Granting permissions should be done carefully to avoid unauthorized access. Grant only necessary privileges.
        static void GrantUserPermissions(MySqlConnection connection)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter the name of the database: ");
            string databaseName = Console.ReadLine();

            Console.WriteLine("Select the permissions to grant:");
            Console.WriteLine("1. SELECT");
            Console.WriteLine("2. INSERT");
            Console.WriteLine("3. UPDATE");
            Console.WriteLine("4. DELETE");
            Console.WriteLine("5. CREATE");
            Console.WriteLine("6. DROP");
            Console.WriteLine("7. ALTER");
            Console.WriteLine("8. ALL PRIVILEGES");

            Console.Write("Choose an option: ");
            if (int.TryParse(Console.ReadLine(), out int option))
            {
                string permission = option switch
                {
                    1 => "SELECT",
                    2 => "INSERT",
                    3 => "UPDATE",
                    4 => "DELETE",
                    5 => "CREATE",
                    6 => "DROP",
                    7 => "ALTER",
                    8 => "ALL PRIVILEGES",
                    _ => null
                };

                if (permission == null)
                {
                    Console.WriteLine("Invalid option. Please try again.");
                    return;
                }

                string query = $"GRANT {permission} ON {databaseName}.* TO '{username}'@'%';";
                using var command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Granted {permission} permission(s) to user '{username}' for database '{databaseName}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }

        // Function to demonstrate a transaction with commit and rollback
        // Note: Transactions ensure data integrity. Use commit to save changes and rollback to undo them if an error occurs.
        static void ShowTransactionExample()
        {
            string connectionString = "server=localhost;port=3306;database=database_test;user=root;password=YourPassword;";
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                command.CommandText = "INSERT INTO users (username) VALUES ('user1');";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO users (username) VALUES ('user2');";
                command.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine("Transaction committed. Both entries were added.");
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    Console.WriteLine("Transaction rolled back. No entries were added.");
                }
                catch (Exception rollbackEx)
                {
                    Console.WriteLine("An error occurred during rollback: " + rollbackEx.Message);
                }
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
