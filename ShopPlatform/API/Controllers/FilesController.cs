using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ShopPlatform.API.Controllers
{
    [Route("cdn/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IWebHostEnvironment webHostEnvironment;

        public FilesController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("uploadicon")]
        public async Task<IActionResult> UploadFile()
        {
            string dir = Directory.CreateDirectory($"{webHostEnvironment.WebRootPath}/FilesUploads").FullName;
            string iconName = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var file = Request.Form.Files[0];
            using (FileStream fs = new FileStream($"{dir}/{iconName}", FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return new JsonResult(new ServerResponse<string>(iconName));
        }
        [HttpGet("icons/{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            return PhysicalFile($"{webHostEnvironment.WebRootPath}/FilesUploads/{id}", "image/jpeg");
        }
    }
}