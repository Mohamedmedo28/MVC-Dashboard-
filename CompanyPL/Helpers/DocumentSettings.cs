using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CompanyPL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadImage(IFormFile file , string FolderName )
        {
            //1-Get Located folder path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            //2-get file name and make it uniqe// علشان لو ف فولدر بكذا اسم
            string fileName = $"{Guid.NewGuid()} {file.FileName}";
            //3- Get file Path 
            string FilePath = Path.Combine(FolderPath, fileName);
            //4-save file as Stream [data per time ]
            //create object on fileStream

           using var fs = new FileStream(FilePath, FileMode.Create); 
            file.CopyTo(fs);
            //مينفعش استخدم فايل باث ع هيتكرر 
            return fileName;
        }

        public static void DeleteFile(string fileName , string FolderName)
        {
            if(fileName != null && FolderName != null)
            {
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, fileName);

                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }
            }
        }
    }
}
