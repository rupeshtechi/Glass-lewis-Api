using GlassLewis.Company.Api.Common;
using GlassLewis.Company.Api.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GlassLewis.Company.Api.Utilities
{
    public class  Athentication: IAthentication
    {
        private readonly IConfiguration _configuration;
        private string key;
        private string jwtIssuer;
        private string jwtAudience;
        private string jwtExipreMin;

        public Athentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///   Gets the new  access token.
        /// </summary>
        /// <returns>The access token</returns>
        public string GetToken(long userid, TypeOfRoles role, string userdata)
        {
            IConfiguration jwtAppSettingOptions;
            jwtAppSettingOptions = _configuration.GetSection(Constants.JwtIssuerOptions);

            key = jwtAppSettingOptions[Constants.JwtKey];
            jwtIssuer = jwtAppSettingOptions[Constants.JwtIssuer];
            jwtAudience = jwtAppSettingOptions[Constants.JwtAudience];
            jwtExipreMin = jwtAppSettingOptions[Constants.JwtExpireMin];

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: new Claim[] {
                            new Claim( ClaimTypes.Name , userid.ToString(), ClaimValueTypes.Integer64),
                            new Claim( ClaimTypes.Role , role.ToString(), ClaimValueTypes.String),
                            new Claim( ClaimTypes.UserData , userdata, ClaimValueTypes.String)
                        },
                expires: DateTime.Now.AddMinutes(Convert.ToInt16(this.jwtExipreMin)),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
