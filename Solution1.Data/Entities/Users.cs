using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Data.Entities
{
	public class Users
	{
		public int UserId { get; set; }

		[Display(Name = "نام")]
		[StringLength(200)]
		public string UName { get; set; }

		[Display(Name = "فامیلی")]
		[StringLength(200)]
		public string UFamily { get; set; }

		[Display(Name = "کد ملی")]
		[StringLength(200)]
		public string UserNcode { get; set; }

		[Display(Name = "کد پرسنلی")]
		[StringLength(200)]
		public string UserPcode { get; set; }

		[Display(Name = "تاریخ تولد")]
		[StringLength(200)]
		public string UserBirthDay { get; set; }

		[Display(Name = "جنسیت")]
		[StringLength(100)]
		public string UserGender { get; set; }

		[Display(Name = "ایمیل")]
		[StringLength(100)]
		public string UserEmail { get; set; }

		[Display(Name = "تلفن")]
		[StringLength(100)]
		public string UserPhone { get; set; }

		[Display(Name = "آدرس")]
		[StringLength(512)]
		public string UserAddress { get; set; }
	}
}
