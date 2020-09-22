namespace UserService.Models.Request
{
    public class UpdateUserPasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}