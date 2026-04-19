using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using Project1_v2.Windows;

namespace Project1_v2;

public partial class MainWindow : Window
{
    private string connectionString = @"Server=LAPTOP-8THJJ3C4\SQLEXPRESS; Database=MovieShop; Integrated Security=True; TrustServerCertificate=True;";

    public MainWindow()
    {
        InitializeComponent();
        loadClients();
        loadMovies();
    }

    private void AddClient_Click(object sender, RoutedEventArgs e)
    {
        AddClient addClient = new AddClient();

        if(addClient.ShowDialog() == true)
        {
            loadClients();
        }
    }

    private void AddMovie_Click(object sender, RoutedEventArgs e)
    {
        AddMovie addMovie = new AddMovie();
        
        if(addMovie.ShowDialog() == true)
        {
            loadMovies();
        }
    }

    private void AddPurchase_Click(object sender, RoutedEventArgs e)
    {

    }

    public void loadClients()
    {
        string query = "SELECT * FROM Clients";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                ClientsGrid.ItemsSource = dataTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    public void loadMovies()
    {
        string query = "SELECT * FROM Movies";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                MoviesGrid.ItemsSource = dataTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    private void ClientsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DeleteClient.IsEnabled = (ClientsGrid.SelectedItem != null);
    }

    private void DeleteClient_Click(object sender, RoutedEventArgs e)
    {
        var selectedRow = ClientsGrid.SelectedItem as DataRowView;

        if(selectedRow != null)
        {
            var clientId = selectedRow["ClientID"];
            string clientName = $"{selectedRow["Name"]} {selectedRow["Surname"]}";

            var result = MessageBox.Show($"Czy na pewno chcesz usunąć klienta {clientName}?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DeleteClientFromDB(clientId);
            }
        }
    }

    private void DeleteClientFromDB(object id)
    {
        string query = "DELETE FROM Clients WHERE ClientID = @id";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                loadClients();
                DeleteClient.IsEnabled = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    private void MoviesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DeleteMovie.IsEnabled = (MoviesGrid.SelectedItem != null);
    }

    private void DeleteMovie_Click(object sender, RoutedEventArgs e)
    {
        var selectedRow = MoviesGrid.SelectedItem as DataRowView;

        if (selectedRow != null)
        {
            var movieId = selectedRow["MovieID"];
            string movieTitle = $"{selectedRow["Title"]}";

            var result = MessageBox.Show($"Czy na pewno chcesz usunąć film {movieTitle}?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DeleteMovieFromDB(movieId);
            }
        }
    }

    private void DeleteMovieFromDB(object id)
    {
        string query = "DELETE FROM Movies WHERE MovieID = @id";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                loadMovies();
                DeleteMovie.IsEnabled = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }
}