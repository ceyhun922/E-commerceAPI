namespace BasetApi.Models.Dto
{
    public class LoginDto
    {
        public string? UserMail { get; set; }
        public string? Password { get; set; }
        public bool? RememberMe { get; set; }
    }
}