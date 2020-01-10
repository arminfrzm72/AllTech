using AllTech.Utilities.Generator;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;

namespace TopLearn.Core.Utilities
{
    public class Picture
    {
        public static string SavePicture(IFormFile addPic, string adress)
        {         
            var picName = NameGenerator.GenerateUniqueCode() + Path.GetExtension(addPic.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), adress, picName);          
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                addPic.CopyToAsync(stream);
            }
            
            return picName;
        }

        public static void DeletePicture(string picName,string adress)
        {
            string deletePath = Path.Combine(Directory.GetCurrentDirectory(), adress, picName);
            if (File.Exists(deletePath))
            {
                File.Delete(deletePath);
            }
        }
    }
}
