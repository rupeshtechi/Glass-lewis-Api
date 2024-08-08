using GlassLewis.Company.Api.Repository.Interface;
using GlassLewis.Company.Api.Model;
using GlassLewis.Company.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;
using GlassLewis.Company.Api.Repository.Entities;

namespace GlassLewis.Company.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CompanyContext _dbContext;

        public UserRepository(CompanyContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region User   
        public async Task<Users?> Login(LoginModel login)
        {
            return await _dbContext.Users
                        .Where(s => s.Email == login.UserName 
                                && s.Password == login.Password
                                && s.IsActive == true)
                        .FirstOrDefaultAsync<Users>();
        }

        public async Task<Users?> GetUserByID(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
        #endregion

    }
}
