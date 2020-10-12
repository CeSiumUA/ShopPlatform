using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ShopPlatform.Models.Accounting
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        public Account Account { get; set; }
        public string PasswordHash { get; set; }

        public Password(string password, Account account)
        {
            this.Account = account;
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            this.PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 32
            ));
        }
        public bool ComparePassword(string password)
        {

            return true;
        }
    }
}
