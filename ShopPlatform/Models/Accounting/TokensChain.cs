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
        [JsonIgnore]
        public Account Account { get; set; }

        public TokensChain(Account account, string accessToken, string refreshToken)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
            this.Account = account;
        }

        public TokensChain()
        {

        }
    }
}
