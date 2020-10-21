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
    [Authorize]
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
        [HttpPost("additem"), DisableRequestSizeLimit]
        public async Task<IActionResult> CreateNewItem([FromBody] Item newItem)
        {
            var shop = await _DatabaseContext.Shops.Include(x => x.ShopOwner).Where(x => x.ShopOwner.Email == User.Identity.Name)
                .SingleOrDefaultAsync();
            var files = Request.Form.Files;
            string dir = Directory.CreateDirectory($"{hostingEnvironment.WebRootPath}/FilesUploads").FullName;
            string headerFileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
            newItem.PhotoUrl = headerFileName;
            using (FileStream fs = new FileStream($"{dir}/{headerFileName}", FileMode.Create))
            {
                await files[0].CopyToAsync(fs);
            }
            foreach (var file in files.Skip(1))
            {
                
            }
            newItem.Seller = shop;
            await _DatabaseContext.Items.AddAsync(newItem);
            await _DatabaseContext.SaveChangesAsync();
            return new JsonResult(new ServerResponse<string>("Succeeded!"));
        }
        [HttpGet("top")]
        public async Task<IActionResult> GetTopItems()
        {
            var topItems = await _DatabaseContext.Items.OrderBy(x => x.Rating).Take(6).ToListAsync();
            return new JsonResult(new ServerResponse<List<Item>>(topItems));
        }
        [HttpPost("additem")]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            await _DatabaseContext.Items.AddAsync(item);
            await _DatabaseContext.SaveChangesAsync();
            return new JsonResult(new ServerResponse<string>("Succeed!"));
        }
    }
}
