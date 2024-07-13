using Forum.Application.Statics;
using Forum.Domain.Models.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.Domain.Models.Content;

namespace Forum.Application.Extensions
{
    public static class FileExtensions
    {
        public static bool UploadFile(this IFormFile file, string fileName, string path)
        {
            var fileFormat = Path.GetExtension(file.FileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var finalPath = path + fileName;
            using (var stream = new FileStream(finalPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return true;
        }

        public static bool DeleteCurrentBannerIMage(Banner banner)
        {
            var userAvatarPath = Path.Combine(Paths.BannerServerPath, banner.Image);
            // Delete current avatar
            if (File.Exists(userAvatarPath))
            {
                File.Delete(userAvatarPath);
                return true;
            }

            return false;
        }

        public static bool DeleteCurrentAvatar(User user)
        {
            if (!user.Avatar.Contains("https://www.gravatar.com/avatar"))
            {
                var userAvatarPath = Path.Combine(Paths.UserAvatarServerPath, user.Avatar);
                // Delete current avatar
                if (File.Exists(userAvatarPath))
                {
                    File.Delete(userAvatarPath);
                    return true;
                }
            }

            return false;
        }
    }
}