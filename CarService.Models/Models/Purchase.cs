using MessagePack;

namespace CarService.Models.Models
{
    [MessagePackObject]
    public record Purchase
    {
        [Key(0)]
        public Guid PurchaseId { get; set; }
        [Key(1)]
        public int ClientId { get; init; }
        [Key(2)]
        public int TyreId { get; set; }
        [Key(3)]
        public int Quantity { get; set; }
        [Key(4)]
        public decimal TotalMoney { get; set; }
    }
}
