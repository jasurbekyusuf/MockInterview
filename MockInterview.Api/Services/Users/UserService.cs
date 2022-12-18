using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MockInterview.Api.Brokers.Storages;
using MockInterview.Api.Models.Responses;
using MockInterview.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MockInterview.Api.Services.Users
{
    public class UserService: IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public UserService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            StorageBroker storageBroker)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task<Response> RegisterUser(RegisterUser regUser)
        {
            var userExists = await userManager.FindByNameAsync(regUser.Username);
            if (userExists != null)
                return new Response { Status = "Error", Message = "User already exists!" };

            IdentityUser user = new()
            {
                Email = regUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = regUser.Username
            };
            var result = await userManager.CreateAsync(user, regUser.Password);
            await IsExistUserRole();
            if (result.Succeeded)
            {
                var createRole = await userManager.AddToRoleAsync(user, UserRoles.User);
                if (!createRole.Succeeded)
                {
                    return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
                }
            }
            return new Response { Status = "Success", Message = "User created successfully!" };
        }

        public async Task<Response> RegisterAdmin(RegisterUser regUser)
        {
            var userExists = await userManager.FindByNameAsync(regUser.Username);
            if (userExists != null)
                return  new Response { Status = "Error", Message = "User already exists!" };

            IdentityUser user = new()
            {
                Email = regUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = regUser.Username
            };
            var result = await userManager.CreateAsync(user, regUser.Password);
            await IsExistAdminRole(); 
            if (result.Succeeded)
            {
               var createRole = await userManager.AddToRoleAsync(user, UserRoles.Admin);
                if (!createRole.Succeeded)
                {
                    return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
                }
            }
           
            return new Response { Status = "Success", Message = "User created successfully!" };
        }
        public async Task<Response> CreateInterviewer(RegisterUser regUser, string token)
        {
            var userExists = await userManager.FindByNameAsync(regUser.Username);
            if (userExists != null)
                return new Response { Status = "Error", Message = "User already exists!" };

            IdentityUser user = new()
            {
                Email = regUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = regUser.Username
            };
            var result = await userManager.CreateAsync(user, regUser.Password);
            await IsExistInterviewerRole();
            if (result.Succeeded)
            {
                var createRole = await userManager.AddToRoleAsync(user, UserRoles.Interviewer);
                if (!createRole.Succeeded)
                {
                    return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
                }
            }

            return new Response { Status = "Success", Message = "User created successfully!" };
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<List<Claim>> GetClaimsAsync(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims.ToList();
                if (claims != null)
                {
                    // return user id from JWT token if validation successful
                    return claims;
                }
                else
                {
                    return claims;
                }
            }
            catch(Exception ex)
            {
                // return null if validation fails
                return null;
            }
        }
        public async Task<Response> ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.FirstOrDefault().Value.ToString();
                if(userName!=null)
                {
                    // return user id from JWT token if validation successful
                    return new Response { Message =userName , Status = "Valid" };
                }
                else
                {
                    return new Response { Message = userName, Status = "Invalid" };
                }
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
        public async Task IsExistAdminRole()
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
        }

        public async Task IsExistUserRole()
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
        }

        public async Task IsExistInterviewerRole()
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Interviewer))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Interviewer));
            }
        }
    }
}
