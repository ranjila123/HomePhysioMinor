using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Services.FileUpload
{
    public interface IFileUpload
    {
        byte[] ImageToByte(IFormFile File);
        FileContentResult ByteToImage(Byte[] ByteFile);
    }
}
