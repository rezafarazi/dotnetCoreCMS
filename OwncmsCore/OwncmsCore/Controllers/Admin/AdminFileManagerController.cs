using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Services.Panel.FileManager;

namespace OwncmsCore.Controllers.Admin
{
<<<<<<< HEAD
=======

>>>>>>> master
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminFileManagerController : Controller
    {


<<<<<<< HEAD

=======
>>>>>>> master
        //File Manager Start
        [Route("FileManager")]
        public async Task<ActionResult> FileManager([FromQuery] string Dir)
        {


            ViewBag.Dir = Dir;

            //Get Read Directory Start
            {
                if (Dir != String.Empty)
                {
                    GET_FILE_LIST(Dir);
                }
                else
                {
                    GET_FILE_LIST("");
                }
            }
            //Get Read Directory End

            return View("../Admin/Dashboard/filemanager");
        }

        [HttpPost]
        [Route("New_Folder")]
        public async Task<ActionResult> New_Folder(string Folder_Name, string Addrs)
        {

            //New Folder Event Start
            {
                string Addr = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads/" + Addrs + "/" + Folder_Name));
                if (!Directory.Exists(Addr))
                    Directory.CreateDirectory(Addr);
            }
            //New Folder Event End

            Response.Redirect(Request.Headers.Referer.ToString());
            return View("../Admin/Dashboard/index");
        }

        [HttpPost]
        [Route("Upload_File")]
        public async Task<ActionResult> Upload_File(IFormFile Upload_File, string Address)
        {

            //Upload File Start
            {
                try
                {
                    if (Upload_File != null)
                    {
                        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads/" + Address));

                        using (var fileStream = new FileStream(Path.Combine(path, Upload_File.FileName), FileMode.Create))
                        {
                            await Upload_File.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception Error)
                {
                }
            }
            //Upload File End

            Response.Redirect(Request.Headers.Referer);
            return View("../Admin/Dashboard/index");
        }

        [Route("Delete_File")]
        public void Delete_File([FromQuery] string Delete)
        {
            string File_Address = Delete;

            if (System.IO.File.Exists(File_Address))
            {
                System.IO.File.Delete(File_Address);
            }
            else if (System.IO.Directory.Exists(File_Address))
            {
                System.IO.Directory.Delete(File_Address, true);
            }

            Response.Redirect(Request.Headers.Referer);
        }

        public void GET_FILE_LIST(string Address)
        {
            File_Manager.Files.Clear();

            string[] L_F_Files = Directory.GetFiles(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads\\" + Address)));
            string[] L_D_Files = Directory.GetDirectories(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads\\" + Address)));

            for (int i = 0; i < L_D_Files.Length; i++)
            {
                File_Manager_Items Item = new File_Manager_Items();
                string[] Dir_Name = (L_D_Files[i] + "\\").Split('\\');
                Item.Name = Dir_Name[Dir_Name.Length - 2];
                Item.Type = "Directory";
                Item.Address = L_D_Files[i];
                File_Manager.Files.Add(Item);
            }

            for (int i = 0; i < L_F_Files.Length; i++)
            {
                File_Manager_Items Item = new File_Manager_Items();
                string[] File_Name = (L_F_Files[i] + "\\").Split('\\');
                Item.Name = File_Name[File_Name.Length - 2];
                Item.Type = "File";
                Item.Address = L_F_Files[i];
                File_Manager.Files.Add(Item);
            }
        }
        //File Manager End


<<<<<<< HEAD

=======
>>>>>>> master
    }
}
