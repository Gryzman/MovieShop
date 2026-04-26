using System.Windows;
using Project1_v2.Functions;

namespace Project1_v2.Windows
{
    public partial class AddMovie : Window
    {
        public AddMovie()
        {
            InitializeComponent();
        }

        // dodawanie filmu
        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            var title = moTitle.Text;
            var director = moDirector.Text;
            var yearOfPremiere = 0;
            int.TryParse(moYearOfPremiere.Text, out yearOfPremiere);
            var genre = moGenre.Text;
            var price = 0.0;
            double.TryParse(moPrice.Text, out price);

            var isDataCorrect = true;

            Shop shop = new Shop();

            // sprawdzanie czy dane są poprawne
            if (yearOfPremiere < 1900 && yearOfPremiere > 2026 || price <= 0.0 || string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(director) || string.IsNullOrWhiteSpace(genre))
            {
                isDataCorrect = false;
            }

            if (!isDataCorrect)
            {
                MessageBox.Show("Wprowadzono nieprawidłowe dane. Podaj je ponownie.", "Błąd dodania filmu do bazy");
            }
            else
            {
                try
                {
                    shop.AddMovieToDatabase(title, director, yearOfPremiere, genre, price);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message);
                }
            }
        }

        // przycisk do zamknięcia okna
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
