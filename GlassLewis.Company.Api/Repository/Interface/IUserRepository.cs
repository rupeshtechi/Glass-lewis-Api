using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Repository.Entities;

namespace GlassLewis.Company.Api.Repository.Interface
{
    public interface IUserRepository
    {
        Task<Users?> Login(LoginModel login);
        Task<Users?> GetUserByID(int id);
    }
}
