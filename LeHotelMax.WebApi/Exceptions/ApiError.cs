namespace LeHotelMax.WebApi.Exceptions
{
    public class ApiError
    {
        public int Code { get; set; }
        public required string Message { get; set; }
        public List<string> Errors { get; } = [];
        public DateTime Timestamp { get; set; }
    }
}
