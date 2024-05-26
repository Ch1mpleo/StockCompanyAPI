using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Porfolio_API.Models;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        //base chính là truyền DbContext vào nhưng do ta ko thể truyền DbContext vào ctor được
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        //DbSet là cách ta sử dụng để manipulate data trong table - và cũng giúp EF tự create db
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        //Khởi tạo 1 role mặc định ban đầu 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin", 
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                }
            };
            
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
