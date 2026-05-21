using GymSystemBLL;
using GymSystemBLL.Services.Classes;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Data.Repository;
using GymSystemDAL.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace GymSystemPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymDbContext>(
                  optionBuilder => optionBuilder.UseLazyLoadingProxies()
                 .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                 ));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddAutoMapper(X => X.AddProfile<MappingProfile>());
            var app = builder.Build();
            //Using to close the scope after we done
            //we are getting service that are scoped
            using var scope = app.Services.CreateScope();

            var gymDbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();

            var pendingMigration = gymDbContext.Database.GetPendingMigrations();
            if (pendingMigration?.Any() ?? false)
            {
                gymDbContext.Database.Migrate();
            }

			GymDbContextSeeding.DataSeed(gymDbContext);

			// Configure the HTTP request pipeline.
			#region Configure MiddleWares
			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                ); 
            #endregion

            app.Run();
        }
    }
}
