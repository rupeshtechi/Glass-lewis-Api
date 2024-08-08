namespace GlassLewis.Company.Api.Model
{
    public class LoginReponse
    {
        public LoginStatus Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public UserDto? User { get; set; }
    }
}
