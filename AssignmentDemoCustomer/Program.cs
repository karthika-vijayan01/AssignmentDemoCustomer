using AssignmentDemoCustomer.Model;
using AssignmentDemoCustomer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace AssignmentDemoCustomer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //3.Jason Format
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            //1.Connection String
        builder.Services.AddDbContext<CustomerAssignmentContext>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("PropelAug24Connection")));
            //2. Repository Layer and Service Layer

           builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
