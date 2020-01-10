using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllTech.DomainClasses.User
{
    public class User
    {
        public User()
        {

        }
        [Key]
        public int UserID { get; set; }


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


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Password { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل را صحیح وارد کنید")]
        public string Email { get; set; }


        [Display(Name ="کد فعالسازی")]
        [MaxLength(50,ErrorMessage ="{0}نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ActiveCode { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }


        [Display(Name ="آواتار")]
        [MaxLength(100,ErrorMessage ="{0}نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string UserAvatar { get; set; }


        #region Relation

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<Wallet.Wallet> Wallet { get; set; }

        #endregion

    }
}
