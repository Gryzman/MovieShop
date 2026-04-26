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
        LoadClients();
        LoadMovies();
        LoadRecentPurchaseHistory();
    }

    private void AddClient_Click(object sender, RoutedEventArgs e)
    {
        AddClient addClient = new AddClient();

        if(addClient.ShowDialog() == true)
        {
            LoadClients();
            LoadRecentPurchaseHistory();
        }
    }

    private void AddMovie_Click(object sender, RoutedEventArgs e)
    {
        AddMovie addMovie = new AddMovie();
        
        if(addMovie.ShowDialog() == true)
        {
            LoadMovies();
            LoadRecentPurchaseHistory();
        }
    }

    private void AddPurchase_Click(object sender, RoutedEventArgs e)
    {
        AddPurchase addPurchase = new AddPurchase();

        if(addPurchase.ShowDialog() == true)
        {
            LoadRecentPurchaseHistory();
        }
    }

    public void LoadClients()
    {
        string query = "SELECT * FROM Clients WHERE Active = 1";

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

    public void LoadMovies()
    {
        string query = "SELECT * FROM Movies WHERE Active = 1";

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

    public void LoadRecentPurchaseHistory()
    {
        string query = @"SELECT TOP 10 p.DateOfPurchase, c.Name, c.Surname, m.Title, m.Price 
                            FROM Purchases1 AS p
                            JOIN Clients AS c ON c.ClientID = p.ClientID
                            JOIN Movies AS m ON m.MovieID = p.MovieID
                            ORDER BY p.PurchaseID DESC
                            ";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                PurchasesGrid.ItemsSource = dataTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    private void ShowRecentPurchaseHistory_Click(object sender, RoutedEventArgs e)
    {
        LoadRecentPurchaseHistory();

        MoviesGrid.SelectedItem = null;
        ClientsGrid.SelectedItem = null;

        DeleteMovie.IsEnabled = false;
        ShowMoviePurchaseHistory.IsEnabled = false;
        DeleteClient.IsEnabled = false;
        ShowClientPurchaseHistory.IsEnabled = false;
    }

    private void ClientsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(ClientsGrid.SelectedItem != null)
        {
            MoviesGrid.SelectedItem = null;
            DeleteMovie.IsEnabled = false;
            ShowMoviePurchaseHistory.IsEnabled = false;

            DeleteClient.IsEnabled = true;
            ShowClientPurchaseHistory.IsEnabled = true;
        }
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
        string query = @"UPDATE Clients 
                        SET Active = 0
                        WHERE ClientID = @id";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                LoadClients();
                DeleteClient.IsEnabled = false;
                ShowClientPurchaseHistory.IsEnabled = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    private void ShowClientPurchaseHistory_Click(object sender, RoutedEventArgs e)
    {
        var selectedRow = ClientsGrid.SelectedItem as DataRowView;

        if (selectedRow != null)
        {
            var clientId = selectedRow["ClientID"];

            ClientPurchaseHistoryFromDB(clientId);
        }
    }

    private void ClientPurchaseHistoryFromDB(object id)
    {
        string query = @"SELECT p.DateOfPurchase, c.Name, c.Surname, m.Title, m.Price 
                            FROM Purchases1 AS p
                            JOIN Clients AS c ON c.ClientID = p.ClientID
                            JOIN Movies AS m ON m.MovieID = p.MovieID
                            WHERE p.ClientID = @id
                            ";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                PurchasesGrid.ItemsSource = dataTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    private void MoviesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(MoviesGrid.SelectedItem != null)
        {
            ClientsGrid.SelectedItem = null;
            DeleteClient.IsEnabled = false;
            ShowClientPurchaseHistory.IsEnabled = false;

            DeleteMovie.IsEnabled = true;
            ShowMoviePurchaseHistory.IsEnabled = true;
        }
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
        string query = @"UPDATE Movies
                        SET Active = 0
                        WHERE MovieID = @id";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                LoadMovies();
                DeleteMovie.IsEnabled = false;
                ShowMoviePurchaseHistory.IsEnabled = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    private void ShowMoviePurchaseHistory_Click(object sender, RoutedEventArgs e)
    {
        var selectedRow = MoviesGrid.SelectedItem as DataRowView;

        if (selectedRow != null)
        {
            var movieId = selectedRow["MovieID"];

            MoviePurchaseHistoryFromDB(movieId);
        }
    }

    private void MoviePurchaseHistoryFromDB(object id)
    {
        string query = @"SELECT p.DateOfPurchase, c.Name, c.Surname, m.Title, m.Price 
                            FROM Purchases1 AS p
                            JOIN Clients AS c ON c.ClientID = p.ClientID
                            JOIN Movies AS m ON m.MovieID = p.MovieID
                            WHERE p.MovieID = @id
                            ";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                PurchasesGrid.ItemsSource = dataTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }
}