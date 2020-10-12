using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ShopPlatform.Models.Accounting
{
    public class RegisterAccount : Account
    {
        public string Password { get; set; }
    }
    public static class RegisterAccountExtensionClass
    {
        public static Password CreatePassword(this RegisterAccount account, string password)
        {
            return new Password(account: account, password: password);
        }

        private static TokensChain GetTokensChain(this Account account, 
            JwtSecurityToken accessToken,
            JwtSecurityToken refreshToken)
        {
            return new TokensChain(account: account, accessToken: new JwtSecurityTokenHandler().WriteToken(accessToken), refreshToken: new JwtSecurityTokenHandler().WriteToken(refreshToken));
        }

        public static TokensChain RetreiveToken(this Account account)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, account.Email));
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, account.AccountType.ToString()));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationType: "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            JwtSecurityToken jwtAccessToken = new JwtSecurityToken(claims: claimsIdentity.Claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Properties.Resources.TokenSecurityKey)),
                    SecurityAlgorithms.HmacSha512), audience: Properties.Resources.TokenAudience, issuer: Properties.Resources.TokenIssuer, expires: DateTime.Now.AddHours(12), notBefore: DateTime.Now);
            JwtSecurityToken jwtRefreshToken = new JwtSecurityToken(claims: claimsIdentity.Claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Properties.Resources.TokenSecurityKey)),
                    SecurityAlgorithms.HmacSha512), audience: Properties.Resources.TokenAudience, issuer: Properties.Resources.TokenIssuer, expires: DateTime.Now.AddMonths(3), notBefore: DateTime.Now);
            return account.GetTokensChain(accessToken: jwtAccessToken, refreshToken: jwtRefreshToken);
        } 
    }
}
