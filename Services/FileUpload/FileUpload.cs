using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Services.FileUpload
{
    public class FileUpload : IFileUpload
    {
        public byte[] ImageToByte(IFormFile File)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(File.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)File.Length);
            return CoverImageBytes;
        }

        public FileContentResult ByteToImage(Byte[] ByteFile)
        {
            return ByteFile != null
                ? new FileContentResult(ByteFile, "image/jpeg")
                : null;
        }


    }
}
