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
        [HttpGet("icons/{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            return PhysicalFile($"{webHostEnvironment.WebRootPath}/FilesUploads/{id}", "image/jpeg");
        }
    }
}