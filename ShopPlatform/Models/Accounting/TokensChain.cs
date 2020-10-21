using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopPlatform.Models.Accounting
{
    public class TokensChain
    {
        [Key]
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TokensChain(string accessToken, string refreshToken, DateTime expirationDate)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
            this.ExpirationDate = expirationDate;
        }

        public void UpdateToken(string accessToken, string refreshToken, DateTime expirationDate)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
            this.ExpirationDate = expirationDate;
        }
        public TokensChain()
        {

        }
    }

    public class TokenResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string PhotoUrl { get; set; }
        public Guid ProfileId { get; set; }
    }
}
