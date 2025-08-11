using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.File;

namespace OwncmsCore.Controllers.File
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
            byte[] fileBytes = GetFileToByte(file.file_address + "/" + file.file_hash + file.file_extention);

            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileName
            };

        }
        //Get file end



        //Get Upload file start
        [HttpPost]
        [Route("File/Upload")]
        public async Task<IActionResult> UploadFile(IFormFile UploadFile)
        {
            file_tbl rescvice = await File_Service.NewFile(UploadFile);

            string hash_result = rescvice.file_hash;

            return Json(new File_Result_Model() { file_id = hash_result });
        }
        //Get upload file end



        public byte[] GetFileToByte(string fileName)
        {
            byte[] fileContent = null;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fs);
            long byteLength = new FileInfo(fileName).Length;
            fileContent = binaryReader.ReadBytes((int)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            return fileContent;
        }



    }
}
