namespace CarService.Models.Requests
{
    public class AddTyreRequest
    {
        public string TyreName { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
