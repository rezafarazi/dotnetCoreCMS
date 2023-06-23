using OwncmsCore.Models.DatabaseModels;
using System.Security.Cryptography;

namespace OwncmsCore.Services.File
{
    public class File_Service
    {

        public static file_tbl NewFile(IFormFile Upfile)
        {
            using (Database_DB DB = new Database_DB())
            {
                System.Diagnostics.Debug.WriteLine("Upload file");
                if (Upfile != null)
                {

                    string file_extention = System.IO.Path.GetExtension(Upfile.FileName);
                    string path_temp = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads/Temp"));
                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads/FileRepository"));
                    string temp_file_storaged_path = "";

                    using (var fileStream = new FileStream(Path.Combine(path_temp, Upfile.FileName), FileMode.Create))
                    {
                        temp_file_storaged_path = Path.Combine(path_temp, Upfile.FileName);
                        Upfile.CopyToAsync(fileStream);
                    }

                    string hash = SHA256CheckSum(temp_file_storaged_path);
                    var check_file = (from file_tbl in DB.file_tbl where file_tbl.file_hash == hash select file_tbl).ToList();

                    if (check_file.Count <= 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(path, hash + "." + file_extention), FileMode.Create))
                        {
                            Upfile.CopyToAsync(fileStream);
                        }

                        file_tbl file = new file_tbl();

                        file.file_name = hash;
                        file.file_size = Upfile.Length.ToString();
                        file.file_type = Upfile.ContentType.ToString();
                        file.file_date_time = DateTime.Now.ToString();
                        file.file_extention = file_extention;
                        file.file_hash = hash;
                        file.file_address = path;
                        file.file_server_address = path;

                        DB.file_tbl.Add(file);
                        DB.SaveChanges();
                        return file;
                    }
                    else
                    {
                        return check_file[0];
                    }

                }
                else
                {
                    throw new Exception("file is empty");
                }
            }
        }

        public static file_tbl GetFile(string file_hash)
        {
            using(Database_DB DB=new Database_DB())
            {
                return (from file_tbl in DB.file_tbl where file_tbl.file_hash==file_hash select file_tbl).First();
            }
        }

        public static string SHA256CheckSum(string filePath)
        {
            using (var SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = System.IO.File.OpenRead(filePath))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }

    }
}
