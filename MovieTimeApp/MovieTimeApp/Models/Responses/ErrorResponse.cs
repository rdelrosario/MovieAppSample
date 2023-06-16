using Newtonsoft.Json;

namespace MovieTimeApp.Models.Responses
{
    public class ErrorResponse
    {
        [JsonProperty("status_code")]
        public int Code { get; set; }

        [JsonProperty("status_message")]
        public string Message { get; set; }
    }
}