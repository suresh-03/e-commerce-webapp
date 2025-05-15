namespace e_commerce_website.Models.auth
{
    public class ApiResponse
    {
        public string Status { get; set; } = string.Empty; // "error" or "success"
        public string Message { get; set; } = string.Empty; // Message to be displayed to the user
        public string RedirectUrl { get; set; } = string.Empty;// URL to redirect to after the action
        public int StatusCode { get; set; } = 200;
    }
}
