using GlassLewis.Company.Api.Model;

namespace GlassLewis.Company.Api.Utilities
{
    public interface IAthentication
    {
        string GetToken(long userid, TypeOfRoles role, string userdata);
    }
}
