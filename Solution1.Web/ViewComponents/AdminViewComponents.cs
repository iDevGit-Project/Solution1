using Microsoft.AspNetCore.Mvc;

namespace Solution1.Web.ViewComponents
{

	#region کامپوننت هدر سایت

	public class Admin_HeaderThemeViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("Admin_HeaderTheme");
		}
	}
	#endregion

	#region کامپوننت سایدبار منوها
	public class Admin_SideBarThemeViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("Admin_SideBarTheme");
		}
	}
	#endregion

	#region کامپوننت منوبار چپ
        
	public class Admin_FooterWithMenuViewComponent : ViewComponent
	{

		public async Task<IViewComponentResult> InvokeAsync()
	    {
			return View("Admin_FooterWithMenu");
		}
	}

	#endregion

	#region کامپوننت وسط محتوا

	public class Admin_CardBodyViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("Admin_CardBody");
		}
	}

	#endregion
}
