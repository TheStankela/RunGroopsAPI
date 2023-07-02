namespace RunGroops.Application.Models
{
    public class AuthResponse
    {
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
        public bool isSuccess { get; set; } = false;

    }
}
