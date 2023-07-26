using BogdanPredica_API.DataContext;
using BogdanPredica_API.Repositories;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API
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

            var clubLibraConnectionString = builder.Configuration.GetConnectionString("ClubLibraConnection");
            builder.Services.AddDbContext<ClubLibraDataContext>(options =>
                options.UseSqlServer(clubLibraConnectionString));

            //register repository
            builder.Services.AddTransient<IAnnouncementsRepository, AnnouncementsRepository>();
            
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