using Forum.Application.Statics;
using Forum.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Extensions
{
    public static class UserExtensions
    {
        public static long GetUserId(this ClaimsPrincipal claims)
        {
            var identifier = claims.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            if (identifier == null)
            {
                return 0;
            }

            return long.Parse(identifier.Value);
        }
        public static string GetUserName(this ClaimsPrincipal claims)
        {
            var identifier = claims.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name);
            if (identifier == null)
            {
                return "";
            }

            return identifier.Value;
        }

        public static string DisplayName(this User user)
        {
            if (!string.IsNullOrEmpty(user.Display_Name))
            {
                return user.Display_Name;
            }

            return user.Username.Trim().Replace(" ", "-").ToLower();
        }

        public static string DisplayBio(this User user)
        {
            if (!string.IsNullOrEmpty(user.Bio))
            {
                return user.Bio;
            }

            return "بیوگرافـی وارد نشـده !";
        }

        public static string CheckUserAvatar(this User user)
        {
            if (user.Avatar.Contains("https://www.gravatar.com/avatar"))
            {
                return user.Avatar;
            }
            else
            {
                return $"{Paths.UserAvatarPath}{user.Avatar}";
            }
        }
    }
}
