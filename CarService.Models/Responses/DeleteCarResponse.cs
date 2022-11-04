using CarService.Models.Models;

namespace CarService.Models.Responses
{
    public class DeleteCarResponse : BaseResponse
    {
        public Car Car { get; set; }
    }
}
