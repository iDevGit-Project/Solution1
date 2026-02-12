using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Solution1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Filter globally: فقط ردیف‌های `IsDeleted == false` برگردانده شود
            modelBuilder.Entity<Company>()
                .HasQueryFilter(c => !c.IsDeleted);

            // (اختیاری) فیلد پیش‌فرض برای DateTime؟
            modelBuilder.Entity<Company>()
                .Property(c => c.DeletedAt)
                .HasDefaultValueSql("NULL");     // اگر می‌خواهید در SQL مقدار پیش‌فرض را تنظیم کنید

            base.OnModelCreating(modelBuilder);
        }
    }
}
