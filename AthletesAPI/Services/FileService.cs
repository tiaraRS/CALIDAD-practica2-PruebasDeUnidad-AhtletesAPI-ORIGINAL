using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Services
{
    public class FileService : IFileService
    {
        public string UploadFile(IFormFile file)
        {
            string imagePath = string.Empty;
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {

                string extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid().ToString()}{extension}";
                var fullPath = Path.Combine(pathToSave, fileName);
                imagePath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return imagePath;
        }
    }
}
