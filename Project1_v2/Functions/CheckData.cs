using System.Text.RegularExpressions;

namespace Project1_v2.Functions
{
    class CheckData
    {
        // sprawdzanie Emaila
        public bool CheckEmail(string email)
        {
            string patternEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            Regex regexEmail = new Regex(patternEmail);

            if (regexEmail.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // sprawdzanie numeru telefonu
        public bool CheckPhoneNumber(string phoneNumber)
        {
            string patternPhone = "^[1-9][0-9]{8}$";

            Regex regexPhone = new Regex(patternPhone);

            if (regexPhone.IsMatch(phoneNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // sprawdzanie imienia, nazwiska
        public bool CheckName(string name)
        {
            string patternName = "^[A-Z]";

            Regex regexName = new Regex(patternName);

            if (regexName.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
