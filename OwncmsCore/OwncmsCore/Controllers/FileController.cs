using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Services.File;

namespace OwncmsCore.Controllers
{
    public class FileController : Controller
    {


        //Get file start
        [HttpGet]
        [Route("File/{file_hash}")]
        public async Task<FileContentResult> GetFile(string file_hash)
        {
            file_tbl file = File_Service.GetFile(file_hash);
            var fileName = file.file_hash + "." + file.file_extention;
            var mimeType = file.file_type;
            byte[] fileBytes = GetFileToByte(file.file_address+"/"+file.file_hash+"."+file.file_extention);

            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileName
            };

        }
        //Get file end



        public byte[] GetFileToByte(string fileName)
        {
            byte[] fileContent = null;
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
            long byteLength = new System.IO.FileInfo(fileName).Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            return fileContent;
        }



    }
}
