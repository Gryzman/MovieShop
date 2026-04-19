namespace Project1_v2.Models
{
    class Client
    {
        public int ClientId {  get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Client(int clientId, string name, string surname, string email, string phone)
        {
            ClientId = clientId;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
        }
    }
}
