using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopPlatform.Models;
using ShopPlatform.Models.Accounting;
using ShopPlatform.Properties;

namespace ShopPlatform.API.Controllers
{
    [ApiController]
    public class Authentication : ControllerBase
    {
        private DatabaseContext _DataBaseContext;
        public Authentication(DatabaseContext databaseContext)
        {
            this._DataBaseContext = databaseContext;
        }

        [HttpPost("api/authentication/login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials)
        {
            var account = await _DataBaseContext.Passwords.Include(x => x.Account)
                .SingleOrDefaultAsync(x => x.Account.Email == loginCredentials.Email);
            if (account.ComparePassword(loginCredentials.Password))
            {
                var tokenPair = account.Account.RetreiveToken();
                var tokenResult = new
                {
                    User = account.Account,
                    AccessToken = tokenPair.AccessToken,
                    RefreshToken = tokenPair.RefreshToken
                };
                return new JsonResult(tokenResult);
            }
            return NotFound();
        }
        [HttpPost("api/authentication/register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccount registerAccount)
        {
            Password password = registerAccount.CreatePassword(registerAccount.Password);
            password = (await _DataBaseContext.Passwords.AddAsync(password)).Entity;
            await _DataBaseContext.SaveChangesAsync();
            var tokenChain = password.Account.RetreiveToken();
            var jsonResult = new
            {
                User = password.Account,
                AccessToken = tokenChain.AccessToken,
                RefreshToken = tokenChain.RefreshToken
            };
            return new JsonResult(jsonResult);
        }
    }
}
