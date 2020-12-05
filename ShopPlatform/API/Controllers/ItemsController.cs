using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ShopPlatform.Models;
using ShopPlatform.Models.Items;

namespace ShopPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private DatabaseContext _DatabaseContext;
        private IWebHostEnvironment hostingEnvironment;
        public ItemsController(DatabaseContext databaseContext, IWebHostEnvironment hostingEnvironment)
        {
            this._DatabaseContext = databaseContext;
            this.hostingEnvironment = hostingEnvironment;
        }
        [Authorize]
        [HttpPost("additem"), DisableRequestSizeLimit]
        public async Task<IActionResult> CreateNewItem([FromBody] Item newItem)
        {
            var shop = await _DatabaseContext.Shops.Include(x => x.ShopOwner).Where(x => x.ShopOwner.Email == User.Identity.Name)
                .SingleOrDefaultAsync();
            if (shop == null)
            {
                return Unauthorized();
            }

            newItem.ViewId = Guid.NewGuid().ToString().Replace("-", string.Empty);
            newItem.Seller = shop;
            await _DatabaseContext.Items.AddAsync(newItem);
            await _DatabaseContext.SaveChangesAsync();
            foreach (var imgRef in newItem.Images)
            {
                var icon = await this._DatabaseContext.ItemIcons.SingleOrDefaultAsync(x => x.Path == imgRef);
                icon.Reference = newItem;
                this._DatabaseContext.ItemIcons.Update(icon);
            }
            await _DatabaseContext.SaveChangesAsync();
            return new JsonResult(new ServerResponse<string>("Succeeded!"));
        }
        
        [HttpGet("top")]
        public async Task<IActionResult> GetTopItems()
        {
            var topItems = await _DatabaseContext.Items.Include(x => x.Seller).OrderBy(x => x.Rating).Take(6).ToListAsync();
            return new JsonResult(new ServerResponse<List<Item>>(topItems));
        }
        [HttpGet("{id?}")]
        public async Task<IActionResult> GetItem(string id)
        {
            var item = await _DatabaseContext.Items.Include(x => x.Seller).SingleOrDefaultAsync(x => x.ViewId == id);
            item.Images = await _DatabaseContext.ItemIcons.Include(x => x.Reference).Where(x => x.Reference.Id == item.Id)
                .Select(x => x.Path).ToListAsync();
            return new JsonResult(new ServerResponse<Item>(item));
        }
    }
}
