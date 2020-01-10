using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllTech.DomainClasses.User
{
    [Flags]
    public enum Roles : byte
    {
        [Display(Name ="مدیر سایت")]
        Admin=1,
        [Display(Name ="نویسنده")]
        Author=2,
        [Display(Name ="کاربر ویژه")]
        FeaturedUser = 4,
        [Display(Name = "کاربر عادی")]
        NormalUser = 8
    }
    public class Role
    {
        public string GetRoleName(Roles roleId)
        {
            byte i = (byte)roleId;
            switch (i)
            {
                case 1:
                    return "مدیر سایت";
                case 2:
                    return "نویسنده";
                case 4:
                    return "کاربر ویژه";
                case 8:
                    return "کاربر عادی";
                default:
                    return "ERROR 404";
            }
        }
    }
   
}
