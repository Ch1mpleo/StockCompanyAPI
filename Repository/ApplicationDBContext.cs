using Microsoft.EntityFrameworkCore;
using Porfolio_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplicationDBContext : DbContext
    {
        //base chính là truyền DbContext vào nhưng do ta ko thể truyền DbContext vào ctor được
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        //DbSet là cách ta sử dụng để manipulate data trong table - và cũng giúp EF tự create db
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
