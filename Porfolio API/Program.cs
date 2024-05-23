
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Helpers;
using Repository.Interfaces;
using Repository.Repositories;

namespace Porfolio_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add Newtonsoft - ko dính vào object cycle của entity framework
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            //Sau khi đã viết xong các entities và DbContext -> 
            //Add connection db - luôn luôn add trước build
            builder.Services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //AddScoped (interfaced, or sth else) 
            builder.Services.AddScoped<DummyDataGenerator>();
            builder.Services.AddScoped<IStockRepo, StockRepository>();
            builder.Services.AddScoped<ICommentRepo, CommentRepository>();

            //Build khởi động 
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
