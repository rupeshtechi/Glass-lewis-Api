using GlassLewis.Company.Api.Model;

namespace GlassLewis.Company.Api.Services.Interface
{
    public interface IUserService
    {
        Task<LoginReponse> Login(LoginModel login);
        Task<UserDto?> GetUserByID(int id);
    }
}
