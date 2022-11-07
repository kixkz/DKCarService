using CarService.Models.Models;

namespace CarService.Models.Responses
{
    public class AddTyreResponse : BaseResponse
    {
        public Tyre Tyre { get; set; }
    }
}
