namespace Project1_v2.Models
{
    class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int YearOfPremiere { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }

        public Movie(int movieId, string title, string director, int yearOfPremiere, string genre, double price)
        {
            MovieId = movieId;
            Title = title;
            Director = director;
            YearOfPremiere = yearOfPremiere;
            Genre = genre;
            Price = price;
        }
    }
}
