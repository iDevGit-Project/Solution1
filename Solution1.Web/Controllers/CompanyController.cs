using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Solution1.Data.Context;
using Solution1.Data.Entities;
using Solution1.Data.VM;
using System.Text.Json;

namespace Solution1.Web.Controllers
{
	public class CompanyController : Controller
	{
		private readonly ApplicationDbContext _DbContext;
		private readonly ILogger<CompanyController> _logger;
        public CompanyController(ApplicationDbContext dbContext, ILogger<CompanyController> logger)
		{
			_DbContext = dbContext;
			_logger = logger;
        }
        public async Task<IActionResult> Index()
		{
            var companies = await _DbContext.Companies
                                .Select(c => new CompanyIndexViewModels
                                {
                                    CompanyId = c.CompanyId,
                                    CompanyName = c.CompanyName,
                                    CompanyActivity = c.CompanyActivity,
                                    CompanyPhone = c.CompanyPhone,
                                    CompanyAddress = c.CompanyAddress,
                                    IsActive = c.IsActive
                                })
                                .ToListAsync();

            ViewBag.ColumnNames = typeof(CompanyIndexViewModels)
                                    .GetProperties()
                                    .Select(p => p.Name)
                                    .ToList();

            return View(companies);   // strongly‑typed view
        }

        #region Create – GET
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Data.VM.CompanyCreateViewModels());
        }
        #endregion

        #region Create – POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Data.VM.CompanyCreateViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    type = "error",
                    message = "اطلاعات وارد شده صحیح نمی‌باشد."
                });
            }

            var newDataCompany = new Company
            {
                CompanyName = model.CompanyName,
                CompanyActivity = model.CompanyActivity,
                CompanyPhone = model.CompanyPhone,
                CompanyAddress = model.CompanyAddress,
                IsActive = model.IsActive
            };

            try
            {
                _DbContext.Add(newDataCompany);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "");
                return StatusCode(500, new
                {
                    type = "error",
                    message = "خطا در ذخیره‌سازی. دوباره تلاش کنید."
                });
            }

            // موفقیت – همان ساختار JSON
            return Ok(new
            {
                type = "success",
                message = "اطلاعات با موفقیت ثبت شد."
            });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Data.VM.CompanyCreateViewModels model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new
        //        {
        //            type = "error",
        //            message = "اطلاعات وارد شده صحیح نمی‌باشد."
        //        });
        //    }

        //    var newDataCompany = new Company
        //    {
        //        CompanyName = model.CompanyName,
        //        CompanyActivity = model.CompanyActivity,
        //        CompanyPhone = model.CompanyPhone,
        //        CompanyAddress = model.CompanyAddress
        //    };

        //    try
        //    {
        //        _DbContext.Add(newDataCompany);
        //        await _DbContext.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger?.LogError(ex, "");
        //        return StatusCode(500, new
        //        {
        //            type = "error",
        //            message = "خطا در ذخیره‌سازی. دوباره تلاش کنید."
        //        });
        //    }

        //    // موفقیت
        //    return Ok(new
        //    {
        //        type = "success",
        //        message = "اطلاعات با موفقیت ثبت شد."
        //    });
        //}

        #endregion

        //جهت ویرایش اطلاعات Edit متد
        #region جهت ویرایش اطلاعات Edit متد
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _DbContext.Companies
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(c => c.CompanyId == id);

            if (company == null) return NotFound();

            var model = new CompanyEditViewModels
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                CompanyActivity = company.CompanyActivity,
                CompanyPhone = company.CompanyPhone,
                CompanyAddress = company.CompanyAddress,
                IsActive = company.IsActive          // <‑‑ اضافه شد
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyEditViewModels Editmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    type = "error",
                    message = "اطلاعات وارد شده صحیح نمی‌باشد."
                });
            }

            var existing = await _DbContext.Companies
                                           .FirstOrDefaultAsync(c => c.CompanyId == Editmodel.CompanyId);

            if (existing == null) return NotFound();

            // update
            existing.CompanyName = Editmodel.CompanyName;
            existing.CompanyActivity = Editmodel.CompanyActivity;
            existing.CompanyPhone = Editmodel.CompanyPhone;
            existing.CompanyAddress = Editmodel.CompanyAddress;
            existing.IsActive = Editmodel.IsActive;          // <‑‑ اضافه شد

            try
            {
                _DbContext.Companies.Update(existing);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"ویرایش شرکت {Editmodel.CompanyId} خطا داد.");
                return StatusCode(500, new
                {
                    type = "error",
                    message = "خطا در ذخیره‌سازی. دوباره تلاش کنید."
                });
            }

            return Ok(new
            {
                type = "success",
                message = "اطلاعات با موفقیت ویرایش شد."
            });
        }


        #endregion

        #region Delete اکشن متد 
        // 1️⃣  GET – نمایش صفحه تأیید حذف
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _DbContext.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            if (company == null)
            {
                return NotFound();               // 404
            }

            // از همان ViewModel برای نمایش جزئیات استفاده می‌کنیم
            var model = new CompanyEditViewModels
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                CompanyActivity = company.CompanyActivity,
                CompanyPhone = company.CompanyPhone,
                CompanyAddress = company.CompanyAddress
            };

            return View(model);                  // strongly‑typed
        }

        [HttpPost]  // POST (یا DELETE)
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
                return BadRequest(new
                {
                    type = "error",
                    message = "شناسه معتبر نمی‌باشد."
                });

            var entity = await _DbContext.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            if (entity == null)
                return NotFound();

            try
            {
                _DbContext.Companies.Remove(entity);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"حذف شرکت {id} خطا داد.");
                return StatusCode(500, new
                {
                    type = "error",
                    message = "خطا در ذخیره‌سازی. دوباره تلاش کنید."
                });
            }

            return Ok(new
            {
                type = "success",
                message = "اطلاعات با موفقیت حذف شد."
            });
        }

        #endregion
    }
}
