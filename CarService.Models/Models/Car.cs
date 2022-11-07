using MessagePack;

namespace CarService.Models.Models
{
    [MessagePackObject]
    public record Car
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string CarName { get; set; }
        [Key(2)]
        public string CarModel { get; set; }
    }
}
