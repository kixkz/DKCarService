namespace CarService.Models.Models
{
    public record Tyre
    {
        public int Id { get; set; }

        public string TyreName { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
