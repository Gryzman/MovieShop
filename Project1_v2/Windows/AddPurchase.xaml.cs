using System.Windows;
using Project1_v2.Functions;

namespace Project1_v2.Windows
{
    public partial class AddPurchase : Window
    {
        public AddPurchase()
        {
            InitializeComponent();
        }

        // rejestrowanie zakupu
        private void btnRegisterPurchase_Click(object sender, RoutedEventArgs e)
        {
            var phone = clPhone.Text;
            var title = moTitle.Text;

            var isDataCorrect = true;

            CheckData checkData = new CheckData();
            Shop shop = new Shop();

            // sprawdzanie czy pola są puste
            if(string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(title))
            {
                isDataCorrect = false;
            }

            if (!isDataCorrect)
            {
                MessageBox.Show("Wprowadzono nieprawidłowe dane. Podaj je ponownie.", "Błąd zarejestrowania zakupu do bazy");
            }
            else
            {
                try
                {
                    shop.RegisterPurchaseToDatabase(phone, title);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message);
                }
            }
        }

        // przycisk do zamknięcia okna
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
