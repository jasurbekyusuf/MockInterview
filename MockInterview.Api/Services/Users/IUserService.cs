using MockInterview.Api.Models.Responses;
using MockInterview.Api.Models.Users;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MockInterview.Api.Services.Users
{
    public partial interface IUserService
    {
        JwtSecurityToken GetToken(List<Claim> authClaims);

        Task<Response> RegisterUser(RegisterUser regUser);
        Task<Response> RegisterAdmin(RegisterUser regUser);
        Task<Response> CreateInterviewer(RegisterUser regUser, string token);
        Task<Response> ValidateToken(string token);
        Task IsExistAdminRole();
        Task IsExistUserRole();
        Task IsExistInterviewerRole();
        Task<List<Claim>> GetClaimsAsync(string token);

    }
}
