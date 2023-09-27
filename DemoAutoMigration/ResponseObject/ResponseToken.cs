using System.Net;

namespace DemoAutoMigration.ResponseObject
{
    public class ResponseToken
    {
        public HttpStatusCode statusCode { get; set; }
        public string? token { get; set; }
        public string? refreshToken { get; set; }
        public string? message { get; set; }
    }
}
