using GraphQlLibary.Domain.Interfaces.Services;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Web.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GraphQlLibary.Web.Auth
{
    public class TokenService: ITokenService
    {
        private readonly AuthSettings _appSettings;

        private readonly IRoleService _roleService;

        private readonly IUserService _userService;

        public TokenService(IOptions<AuthSettings> appSettings, IRoleService roleService, IUserService userService)
        {
            _appSettings = appSettings.Value;
            _roleService = roleService;
            _userService = userService;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetClaims(user)),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public int GetId(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();

                try
                {
                    var jsonToken = handler.ReadToken(token);
                }
                catch
                {
                    return 0;
                }

                var jwtEncodedString = new JwtSecurityToken(jwtEncodedString: token);
                var claim = jwtEncodedString.Claims.First(x => x.Type == "nameid");
                int id;

                if (claim == null)
                {
                    return 0;
                }

                int.TryParse(claim.Value, out id);

                return id;
            }

            return 0;
        }

        public User GetUser(string token)
        {
            int id = GetId(token);

            if (id == 0)
            {
                return null;
            }

            var user = _userService.Get(id);

            return user;
        }

        public bool IsAdmin(string token)
        {
            var user = GetUser(token);

            if (user == null)
            {
                return false;
            }

            var isAdmin = user?.UserRole.Select(x => x.Role.Name).Any(x => x == "Admin");

            if (isAdmin == null)
            {
                return false;
            }

            return isAdmin.Value;
        }

        public bool IsUser(string token)
        {
            var user = GetUser(token);

            if (user == null)
            {
                return false;
            }

            var isAdmin = user?.UserRole.Select(x => x.Role.Name).Any(x => x == "User");

            if (isAdmin == null)
            {
                return false;
            }

            return isAdmin.Value;
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
                             {
                                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                 new Claim(ClaimTypes.Name, user.Name),
                                 new Claim(ClaimTypes.Email, user.Email)
                             };

            var roles = user.UserRole.Select(x => _roleService.Get(x.RoleId)).ToList();

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return claims;
        }
    }
}
