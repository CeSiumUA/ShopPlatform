using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ShopPlatform.Models.Accounting
{
    public class RegisterAccount : Account
    {
        public string PasswordStr { get; set; }
    }
    public static class RegisterAccountExtensionClass
    {
        public static Account GetAccount(this RegisterAccount account)
        {
            Account acc = account;
            acc.Password = new Password(account.PasswordStr);
            return acc;
        }

        private static Account GetTokensChain(this Account account, 
            JwtSecurityToken accessToken,
            string refreshToken)
        {
            if (account.TokensChain == null)
            {
                account.TokensChain =
                    new TokensChain(accessToken: new JwtSecurityTokenHandler().WriteToken(accessToken),
                        refreshToken: refreshToken, DateTime.Now.AddMonths(3));
            }
            else
            {
                account.TokensChain.UpdateToken(accessToken: new JwtSecurityTokenHandler().WriteToken(accessToken),
                    refreshToken: refreshToken, DateTime.Now.AddMonths(3));
            }

            return account;
        }

        public static Account RetreiveToken(this Account account)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, account.Email));
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, account.AccountType.ToString()));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationType: "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            JwtSecurityToken jwtAccessToken = new JwtSecurityToken(claims: claimsIdentity.Claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Properties.Resources.TokenSecurityKey)),
                    SecurityAlgorithms.HmacSha512), audience: Properties.Resources.TokenAudience, issuer: Properties.Resources.TokenIssuer, expires: DateTime.Now.AddHours(12), notBefore: DateTime.Now);
            string RefreshToken = "";
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                RefreshToken = Convert.ToBase64String(randomNumber);
            }
            
            return account.GetTokensChain(accessToken: jwtAccessToken, refreshToken: RefreshToken);
        }

        public static TokenResult GetTokens(this Account account)
        {
            return new TokenResult()
            {
                AccessToken = account.TokensChain.AccessToken,
                RefreshToken = account.TokensChain.RefreshToken,
                FirstName = account.FirstName,
                LastName = account.LastName,
                PhotoUrl = account.PhotoUrl,
                ProfileId = account.ProfileId
            };
        }
    }
}
