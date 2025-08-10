namespace OwncmsCore.Services.UserProfileImage
{
    public class UserProfileImage
    {

        //Change user profile image
        public static async Task<string> SaveUserProfileImage(IFormFile profileimage)
        {
            try
            {
                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Profiles"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, profileimage.FileName), FileMode.Create))
                {
                    await profileimage.CopyToAsync(fileStream);
                    return path + "\\" + profileimage.FileName;
                }
            }
            catch
            {

            }
            return "";
        }


    }
}
