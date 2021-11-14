using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Gallery.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Gallery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly UploadImageDBContext _context;

        public GalleryController(UploadImageDBContext db)
        {
            _context = db;
            
        }

        [HttpGet]
        public IActionResult Get(int Id)
        {

            var savedImages = _context.Image.ToList();
            return Ok(savedImages);
        }
       
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                var deleteImage = _context.Image.FirstOrDefault(x => x.ImagesId == Id);
                if (deleteImage == null) return Ok(false);
                _context.Remove(deleteImage);
                _context.SaveChanges();
                return Ok(true);
            }
            catch(Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post(IFormFile file, [FromForm] Images data)
        {
            try
            {
                //var formCollection = await Request.ReadFormAsync();
                //var file = formCollection.Files.First();
                var folderName = Path.Combine("Images","uploadedImages");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if(file.Length>0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var newimg = new Images
                    {
                        ImagesDescription = data.ImagesDescription,
                        ImagesPath = dbPath,
                        Featured = data.Featured
                    };
                    _context.Image.Add(newimg);
                    _context.SaveChanges();
                    return Ok(newimg);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
