using System.Windows;
using Project1_v2.Functions;

namespace Project1_v2.Windows
{
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }

        // dodawanie klienta
        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            var name = clName.Text;
            var surname = clSurname.Text;
            var email = clEmail.Text;
            var phone = clPhone.Text;

            var isDataCorrect = true;

            CheckData checkData = new CheckData();
            Shop shop = new Shop();

            // sprawdzanie, czy dane są poprawne
            if (!checkData.CheckName(name) || !checkData.CheckName(surname) || !checkData.CheckEmail(email) || !checkData.CheckPhoneNumber(phone))
            {
                isDataCorrect = false;
            }

            if (!isDataCorrect)
            {
                MessageBox.Show("Wprowadzono nieprawidłowe dane. Podaj je ponownie.", "Błąd dodania klienta do bazy");
            }
            else
            {
                try
                {
                    shop.AddClientToDatabase(name, surname, email, phone);
                }
                catch (Exception ex)
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
