using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Data.VM
{
	public class CompanyCreateViewModels
	{
		[Display(Name = "نام شرکت")]
		[StringLength(200)]
		[Required(ErrorMessage ="لطفاً نام شرکت را وارد نمایید")]
		public string CompanyName { get; set; }

		[Display(Name = "نوع فعالیت شرکت")]
		[StringLength(200)]
		[Required(ErrorMessage = "لطفاً نوع فعالیت را وارد نمایید")]
		public string CompanyActivity { get; set; }

		[Display(Name = "تلفن شرکت")]
		[StringLength(200)]
        [Required(ErrorMessage = "لطفاً تلفن شرکت را وارد نمایید")]
        public string CompanyPhone { get; set; }

		[Display(Name = "آدرس شرکت")]
		[StringLength(512)]
        [Required(ErrorMessage = "لطفاً آدرس شرکت را وارد نمایید")]
        public string CompanyAddress { get; set; }

        [Display(Name = "وضعیت شرکت")]
        public bool IsActive { get; set; } = false;     // پیش‌فرض “غیرفعال”
    }
}
