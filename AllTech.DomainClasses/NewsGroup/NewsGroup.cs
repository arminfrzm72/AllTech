using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllTech.DomainClasses.NewsGroup
{
    public class NewsGroup
    {
        public NewsGroup()
        {

        }
        [Key]
        public int GroupID { get; set; }
        [Display(Name ="عنوان گروه")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(20,ErrorMessage ="تعداد کاراکترها بیش از حد مجاز می‌باشد")]
        public string GroupTitle { get; set; }

        public virtual List<News.News> News { get; set; }
    }
}
