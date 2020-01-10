using AllTech.DomainClasses.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllTech.ViewModels.UserAccount
{
    public class UsersForAdminViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public int PageCount { get; set; }
    }

    public class CreateUserViewModel
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
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        public IFormFile UserAvatar { get; set; }

        public Roles Roles { get; set; }

    }

    public class EditUserViewModel
    {
        public int UserId { get; set; }

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
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(30, ErrorMessage = "{0}نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        public IFormFile UserAvatar { get; set; }

        public List<Roles> UserRoles { get; set; }

        public string AvatarName { get; set; }
    }

}
