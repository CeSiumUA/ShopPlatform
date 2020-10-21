using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopPlatform.Models;
using ShopPlatform.Models.Shop;

namespace ShopPlatform.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private DatabaseContext _DatabaseContext;
        public ShopController(DatabaseContext databaseContext)
        {
            this._DatabaseContext = databaseContext;
        }
        [HttpPost("createshop")]
        public async Task<IActionResult> CreateShop([FromBody]Shop newShop)
        {
            if ((await _DatabaseContext.Shops.Include(x => x.ShopOwner).Where(x => x.ShopOwner.Email == User.Identity.Name).FirstOrDefaultAsync()) != null)
            {
                return new JsonResult(new ServerResponse<object>(new ServerError(ServerError.ShopsLimitExceeded)));
            }
            newShop.ShopOwner = _DatabaseContext.Accounts.SingleOrDefault(x => x.Email == User.Identity.Name);
            newShop.CreationDate = DateTime.Now;
            await _DatabaseContext.Shops.AddAsync(newShop);
            await _DatabaseContext.SaveChangesAsync();
            return new JsonResult(new ServerResponse<string>("Succeed!"));
        }

        [HttpGet("location")]
        public async Task<IActionResult> GetLocations([FromQuery] string cc, [FromQuery] string kw)
        {
            List<Location> locations = new List<Location>();
            if (!string.IsNullOrEmpty(kw) && !string.IsNullOrEmpty(cc))
            {
                locations = await this._DatabaseContext.Locations
                    .Where(x => x.CountryCode == cc && (x.LocationName.ToLower().Contains(kw.ToLower()))).ToListAsync();
            }

            return new JsonResult(new ServerResponse<List<Location>>(locations));
        }

        [HttpGet("shops/{shopId}")]
        public async Task<IActionResult> GetShop(int shopId)
        {
            var shop = _DatabaseContext.Shops.Include(x => x.ShopOwner).SingleOrDefault(x => x.Id == shopId);
            if (shop.OwnerReference.email != User.Identity.Name)
            {
                return new JsonResult(new ServerResponse<object>(new ServerError(ServerError.AccessDenied)));
            }
            return new JsonResult(new ServerResponse<Shop>(shop));
        }

        [HttpGet("myshops")]
        public async Task<IActionResult> GetMyShops()
        {
            var account = await _DatabaseContext.Accounts.SingleOrDefaultAsync(x => x.Email == User.Identity.Name);
            if (account != null)
            {
                var myshops = _DatabaseContext.Shops.Include(x => x.ShopOwner).Where(x => x.ShopOwner.Id == account.Id);
                return new JsonResult(new ServerResponse<IQueryable<Shop>>(myshops));
            }
            return new JsonResult(new ServerResponse<object>(new ServerError(ServerError.UserNotFound)));
        }
        [HttpGet("shops/{searchPattern}")]
        public async Task<IActionResult> SearchShopByPattern(string searchPattern)
        {
            var shops = await _DatabaseContext.Shops.Include(x => x.ShopOwner).Where(x =>
                x.ShopOwner.Email.Contains(searchPattern) || x.ShopOwner.FirstName.Contains(searchPattern) ||
                x.ShopOwner.LastName.Contains(searchPattern)
                || x.CreationDate.ToString().Contains(searchPattern) || x.Id.ToString().Contains(searchPattern) ||
                x.ShopName.Contains(searchPattern) || x.ShopDescription.Contains(searchPattern) ||
                x.MainCategory.Contains(searchPattern) || x.Rating.ToString().Contains(searchPattern)).ToListAsync();
            return new JsonResult(new ServerResponse<List<Shop>>(shops));
        }
    }
}
