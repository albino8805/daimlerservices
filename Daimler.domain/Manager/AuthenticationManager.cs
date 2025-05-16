using Daimler.data.IRepository;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Daimler.domain.Manager
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private IUserRepository _userRepository;
        private IModuleActionRepository _moduleActionRepository;
        readonly JwtSetting _jwtSetting;

        public AuthenticationManager(IOptions<JwtSetting> options,
            IUserRepository userRepository,
            IModuleActionRepository moduleActionRepository)
        {
            _jwtSetting = options.Value;
            _userRepository = userRepository;
            _moduleActionRepository = moduleActionRepository;
        }

        public LoginViewModel ValidateUser(AuthenticationViewModel authentication)
        {
            var user = _userRepository.GetByEmail(authentication.Email);

            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }

            if (user.Password != authentication.Password)
            {
                throw new Exception("Credenciales invalidas");
            }

            var timeLifeInDays = _jwtSetting.TimeLifeInDays;
            var expires = DateTime.UtcNow.AddDays(timeLifeInDays);
            var permissions = _moduleActionRepository.GetByProfile(user.ProfileFK);

            LoginViewModel login = new LoginViewModel
            {
                User = new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email
                },
                Permissions = permissions.Select(p => new ModuleActionViewModel()
                {
                    Id = p.Id,
                    ModuleFK = p.ModuleFK,
                    ActionFK = p.ActionFK,
                    ProfileFK = p.ProfileFK,
                }).ToList(),
                Token = GenJwt(user.Id.ToString(), expires, null),
                Expires = expires
            };

            return login;
        }

        private string GenJwt(string id, DateTime expires, List<Claim> extraClaims)
        {
            byte[] key = Encoding.ASCII.GetBytes(_jwtSetting.Secret);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            List<Claim> subjetClaims = new List<Claim> {
                new Claim(ClaimTypes.Sid, id),
                new Claim(ClaimTypes.System, "Daimler" )
            };

            if (extraClaims != null && extraClaims.Count > 0)
                subjetClaims.AddRange(extraClaims);

            var issuedAt = DateTime.UtcNow;

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "Daimler",
                Expires = expires,
                IssuedAt = issuedAt,
                Subject = new ClaimsIdentity(subjetClaims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
