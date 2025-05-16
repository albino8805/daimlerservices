using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Daimler.domain.Helpers
{
    public class TokenHelper
    {
        /// <summary>
        /// Decodes the specified token.
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <returns></returns>
        public static JwtSecurityToken Decode(string Token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(Token);
        }
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException">
        /// </exception>
        public static string GetToken(HttpRequest request)
        {
            string token = request.Headers[nameof(Authorization)].ToString();
            string[] items = token.Split(" ");
            token = items.Length > 1 ? items[1] : items[0];
            token = token.Replace("Bearer", "").Replace("bearer", "").Trim();

            if (string.IsNullOrEmpty(token) || token == "null")
                //throw new UnauthorizedAccessException();
                return null;

            try
            {
                JwtSecurityToken jwtSecurityToken = Decode(token);
            }
            catch (Exception e)
            {
                throw new UnauthorizedAccessException();
            }

            return token;
        }
        /// <summary>
        /// Gets the value claim.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="claimName">Name of the claim.</param>
        /// <returns></returns>
        public static string GetValueClaim(string token, string claimName)
        {
            string value = string.Empty;
            JwtSecurityToken jwtSecurityToken = Decode(token);
            Claim claim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == claimName);
            value = claim?.Value;
            return value;
        }
        /// <summary>
        /// Gets the value claim.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">The request.</param>
        /// <param name="claimName">Name of the claim.</param>
        /// <returns></returns>
        public static T GetValueClaim<T>(HttpRequest request, string claimName)
        {
            string token = GetToken(request);
            string valueStr = GetValueClaim(token, claimName);
            if (string.IsNullOrEmpty(valueStr)) return default(T);
            object value = null;
            switch (typeof(T).Name.ToLower())
            {
                case "string":
                    value = string.IsNullOrEmpty(valueStr) ? "" : valueStr;
                    break;
                case "int":
                case "int32":
                    value = Convert.ToInt32(valueStr);
                    break;
                case "bool":
                case "boolean":
                    value = Convert.ToBoolean(valueStr);
                    break;
                default:
                    value = valueStr;
                    break;
            }

            return (T)value;
        }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static int GetUserId(HttpRequest request)
        {
            string userIdStr = GetValueClaim<string>(request, ClaimTypes.Sid);
            return Convert.ToInt32(userIdStr);
        }
        /// <summary>
        /// Gets the business group identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static string GetBusinessGroupId(HttpRequest request)
        {
            return GetValueClaim<string>(request, ClaimTypes.GroupSid);
        }
    }
}
