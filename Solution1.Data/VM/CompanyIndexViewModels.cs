using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Data.VM
{
    public class CompanyIndexViewModels
    {
        [Display(Name = "ردیف")]
        public int CompanyId { get; set; }

        [Display(Name = "نام شرکت")]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [Display(Name = "نوع فعالیت شرکت")]
        [StringLength(200)]
        public string CompanyActivity { get; set; }

        [Display(Name = "تلفن شرکت")]
        [StringLength(200)]
        public string CompanyPhone { get; set; }

        [Display(Name = "آدرس شرکت")]
        [StringLength(512)]
        public string CompanyAddress { get; set; }

        [Display(Name = "وضعیت شرکت")]
        public bool IsActive { get; set; } = false;     // پیش‌فرض “غیرفعال”
    }
}
