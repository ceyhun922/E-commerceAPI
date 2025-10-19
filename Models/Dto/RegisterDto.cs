namespace BasetApi.Models.Dto
{
    public class RegisterDto
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? EMail { get; set; }
        public string? Number { get; set; }

        public string? Username { get; set; }

        public string? Role { get; set; }
        public string? UserPassword { get; set; }
        public string? UserPasswordConfirm { get; set; }
    }
}