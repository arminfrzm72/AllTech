using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllTech.ViewModels.UserAccount
{
    #region Register
    public class RegisterViewModel
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


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Password { get; set; }


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [Compare("Password",ErrorMessage = "کلمه‌های عبور مغایرت دارند")]
        public string RePassword { get; set; }             
    }
    #endregion  

    #region Login
    public class LoginViewModel
    {

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکترها بیش از حد مجاز می‌باشد")]
        public string UserName { get; set; }


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    #endregion

    #region ForgotPassword

    public class ForgotPasswordViewModel
    {

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را صحیح وارد کنید")]
        public string Email { get; set; }
    }


    #endregion

    #region ResetPassword

    public class ResetPasswordViewModel
    {
        public string ActiveCode { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Password { get; set; }


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [Compare("Password", ErrorMessage = "کلمه‌های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }

    #endregion

   
}
