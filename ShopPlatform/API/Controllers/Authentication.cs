using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            var account = await _DataBaseContext.Accounts.Include(x => x.Password).Include(x => x.TokensChain).SingleOrDefaultAsync(x => x.Email == loginCredentials.Email);
            if (account == null)
            {
                return new JsonResult(new ServerResponse<object>(new ServerError(ServerError.UserNotFound)));
            }
            var validAccount = account?.Password.ComparePassword(loginCredentials.Password);
            if (validAccount.HasValue && validAccount.Value)
            {
                account.RetreiveToken();
                _DataBaseContext.Accounts.Update(account);
                await _DataBaseContext.SaveChangesAsync();
                var response = new ServerResponse<TokenResult>(account.GetTokens());
                return new JsonResult(response);
            }
            return new JsonResult(new ServerResponse<object>(new ServerError(ServerError.InvalidPassword)));
        }

        [Authorize]
        [HttpPost("api/authentication/refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            return new JsonResult(new ServerResponse<object>(new ServerError(ServerError.TokenExpiredOrInvalid)));
        }
        [HttpPost("api/authentication/register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccount registerAccount)
        {
            if (!string.IsNullOrEmpty(registerAccount.Email) && !string.IsNullOrEmpty(registerAccount.PasswordStr))
            {
                if ((await _DataBaseContext.Accounts.SingleOrDefaultAsync(x => x.Email == registerAccount.Email)) !=
                    null)
                {
                    return new JsonResult(new ServerResponse<object>(new ServerError(ServerError.EmailExists)));
                }
                Account account = registerAccount.GetAccount();
                var tokenChain = account.RetreiveToken();
                account = (await _DataBaseContext.Accounts.AddAsync(account)).Entity;
                await _DataBaseContext.SaveChangesAsync();
                return new JsonResult(new ServerResponse<TokenResult>(account.GetTokens()));
            }
            return new JsonResult(new ServerResponse<object>(new ServerError(ServerError.InvalidCredentials)));
        }
    }

    public class ServerError
    {
        public static int EmailExists = 1;
        public static int InvalidCredentials = 2;
        public static int UserNotFound = 3;
        public static int InvalidPassword = 4;
        public static int TokenExpiredOrInvalid = 5;
        public static int AccessDenied = 6;
        public static int ShopsLimitExceeded = 7;
        public ServerError(int errorCode)
        {
            this.ErrorCode = errorCode;
        }
        public int ErrorCode { get; set; }
    }
}
