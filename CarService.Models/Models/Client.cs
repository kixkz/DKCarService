namespace CarService.Models.Models
{
    public record Client
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public int CarId { get; set; }
    }
}
