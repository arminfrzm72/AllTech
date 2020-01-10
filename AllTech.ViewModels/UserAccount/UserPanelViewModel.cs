using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AllTech.ViewModels.UserAccount
{
    public class UserInformationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int WalletBalance { get; set; }

    }
    public class UserPanelSideBarViewModel
    {
        public string UserName { get; set; }
        public string ImageName { get; set; }
    }
    public class EditProfileViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکترها بیش از حد مجاز می‌باشد")]
        public string FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکترها بیش از حد مجاز می‌باشد")]
        public string LastName { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکترها بیش از حد مجاز می‌باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را صحیح وارد کنید")]
        public string Email { get; set; }

        public IFormFile UserAvatar { get; set; }
        public string AvatarName { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string OldPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [Compare("Password", ErrorMessage = "کلمه‌های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
}
