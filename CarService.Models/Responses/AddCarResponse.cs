using CarService.Models.Models;

namespace CarService.Models.Responses
{
    public class AddCarResponse : BaseResponse
    {
        public Car Car { get; set; }
    }
}
