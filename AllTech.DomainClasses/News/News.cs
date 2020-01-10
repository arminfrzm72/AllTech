using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllTech.DomainClasses.News
{
    public class News
    {
        public News()
        {

        }
        [Key]
        public int NewsID { get; set; }
        [Display(Name = "گروه خبر")]
        public int GroupID { get; set; }
        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تعداد کاراکترها بیش از حد مجاز می‌باشد")]
        public string NewsTitle { get; set; }
        [Display(Name = "خلاصه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکترها بیش از حد مجاز می‌باشد")]
        public string ShortDescription { get; set; }
        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MainText { get; set; }
        [Display(Name = "بازدید")]
        public int Visit { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string Tags { get; set; }
        [Display(Name = "منبع")]
        public string Source { get; set; }
        [Display(Name = "نمایش در اسلایدر")]
        public bool ShowInSlider { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }


        //Navigation Property
        [Display(Name ="گروه خبری")]
        public virtual NewsGroup.NewsGroup  NewsGroup { get; set; }
    }
}
