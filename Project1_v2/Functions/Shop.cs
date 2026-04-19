using System.Windows;
using Microsoft.Data.SqlClient;

namespace Project1_v2.Functions
{
    class Shop
    {
        private string connectionString = @"Server=LAPTOP-8THJJ3C4\SQLEXPRESS; Database=MovieShop; Integrated Security=True; TrustServerCertificate=True;";

        // wyświetl 10 ostatnich zamówień
        //string query = "SELECT TOP 10 * FROM Purchases ORDER BY ID DESC";

        // wyświetl zamówienia klienta
        //string query = "SELECT * FROM Purchases WHERE ID = (SELECT ID FROM Clients WHERE PhoneNumber = @phone)";

        // wyświetl zamówienia filmu
        //string query = "SELECT * FROM Purchases WHERE ID = (SELECT ID FROM Movies WHERE Title = @title)";
        public void addClientToDatabase(string name, string surname, string email, string phone)
        {
            string query = "INSERT INTO Clients (Name, Surname, Email, PhoneNumber) VALUES (@name, @surname, @email, @phone)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phone", phone);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Pomyślnie dodano klienta!");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Błąd bazy danych: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }

        public void addMovieToDatabase(string title, string director, int yearOfPremiere, string genre, double price)
        {
            string query = "INSERT INTO Movies (Title, Director, YearOfPremiere, Genre, Price) VALUES (@title, @director, @yearOfPremiere, @genre, @price)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@director", director);
                    command.Parameters.AddWithValue("@yearOfPremiere", yearOfPremiere);
                    command.Parameters.AddWithValue("@genre", genre);
                    command.Parameters.AddWithValue("@price", price);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Pomyślnie dodano film!");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Błąd bazy danych: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }
    }
}
