public class ResetPasswordDto
{
    public string? Token { get; set; }
    public string? NewPassword { get; set; }
    public string? NewPasswordConfirm { get; set; }
}