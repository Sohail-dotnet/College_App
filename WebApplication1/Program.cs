using College_App.Data.Repository;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Configuration;
using WebApplication1.Data;
using WebApplication1.MyLogging;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Clear default(Builtin) logging providers
            builder.Logging.ClearProviders(); 
            // Add Log4Net logging provider
            builder.Logging.AddLog4Net();

            #region AutoMapper Configuration
            // Auto Mapper Configurations
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            #endregion

            builder.Services.AddDbContext<CollegeDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
            });

            #region Serilog Configuration
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Minute)
            //    .CreateLogger();


            // Use Serilog along with buit-in logging providers
            //builder.Logging.AddSerilog();

            // Use this line to override built-in logging providers
            //builder.Host.UseSerilog();
            #endregion

            //builder.Logging.ClearProviders();
            //builder.Logging.AddConsole();
            //builder.Logging.AddDebug();


            builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable= true).AddXmlSerializerFormatters().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IMyLogger, LogToDB>();

            builder.Services.AddTransient<IStudentRepository, StudentRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
