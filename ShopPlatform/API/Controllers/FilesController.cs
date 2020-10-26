using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShopPlatform.Models;
using ShopPlatform.Models.Items;
using ShopPlatform.Models.Shop;

namespace ShopPlatform.API.Controllers
{
    [Route("cdn/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IWebHostEnvironment webHostEnvironment;
        private DatabaseContext databaseContext;
        public FilesController(IWebHostEnvironment webHostEnvironment, DatabaseContext databaseContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.databaseContext = databaseContext;
        }

        [HttpPost("uploadicon")]
        public async Task<IActionResult> UploadFile()
        {
            string dir = Directory.CreateDirectory($"{webHostEnvironment.WebRootPath}/FilesUploads").FullName;
            List<string> iconNames = new List<string>();
            foreach (var file in Request.Form.Files)
            {
                string iconName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                using (FileStream fs = new FileStream($"{dir}/{iconName}", FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                iconNames.Add(iconName);
            }
            return new JsonResult(new ServerResponse<List<string>>(iconNames));
        }
        [HttpPost("items/uploadicon"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadIconFile()
        {
            string dir = Directory.CreateDirectory($"{webHostEnvironment.WebRootPath}/FilesUploads").FullName;
            List<string> iconNames = new List<string>();
            foreach (var file in Request.Form.Files)
            {
                string iconName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                using (FileStream fs = new FileStream($"{dir}/{iconName}", FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                    await this.databaseContext.ItemIcons.AddAsync(new ItemIcon()
                    {
                        Path = iconName,
                        DateAdded = DateTime.Now
                    });
                }
                iconNames.Add(iconName);
            }
            await this.databaseContext.SaveChangesAsync();
            return new JsonResult(new ServerResponse<List<string>>(iconNames));
        }
        [HttpPost("shops/uploadicon"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadShopFile()
        {
            string dir = Directory.CreateDirectory($"{webHostEnvironment.WebRootPath}/FilesUploads").FullName;
            List<string> iconNames = new List<string>();
            foreach (var file in Request.Form.Files)
            {
                string iconName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                using (FileStream fs = new FileStream($"{dir}/{iconName}", FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                await this.databaseContext.ShopsIcons.AddAsync(new ShopIcon()
                {
                    Path    = iconName,
                    DateAdded = DateTime.Now
                });
                iconNames.Add(iconName);
            }
            await this.databaseContext.SaveChangesAsync();
            return new JsonResult(new ServerResponse<List<string>>(iconNames));
        }
        [HttpGet("icons/{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            return PhysicalFile($"{webHostEnvironment.WebRootPath}/FilesUploads/{id}", "image/jpeg");
        }
    }
}