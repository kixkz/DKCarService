using System.Net;

namespace CarService.Models.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; init; }

        public string Message { get; set; }
    }
}
