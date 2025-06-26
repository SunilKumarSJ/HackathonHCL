using Microsoft.IdentityModel.Tokens;
using BLL.Helper;
using Microsoft.OpenApi.Models;
using System.Text;
using BLL;
using DAL;
using HackathonHCL.Server.Mapper;

namespace HackathonHCL.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(GlobalMapper));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<JWTSetting>(builder.Configuration.GetSection("JwtTokenFields"));
            builder.Services.AddTransient<IUserDAL, UserDAL>();
            builder.Services.AddTransient<IAttendanceDAL, AttendanceDAL>();
            builder.Services.AddTransient<IShiftSchedulesDAL, ShiftSchedulesDAL>();
            builder.Services.AddTransient<IUserBLL, UserBLL>();
            builder.Services.AddTransient<ITokenBLL, TokenBLL>();
            builder.Services.AddTransient<IAttendanceBLL, AttendanceBLL>();
            builder.Services.AddTransient<IShiftSchedulesBLL, ShiftSchedulesBLL>();
            //var key = Encoding.ASCII.GetBytes("amSunilUsingthisStringAsAccessTokenKey");
            var key = Encoding.ASCII.GetBytes(config["JwtTokenFields:AccessTokenKey"]);
            builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "SunilKumarSjIsTheIssuer",
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false,
        };
    });

            builder.Services.AddAuthorization();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Add JWT Bearer support to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });
            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(x => x.WithOrigins("https://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()); 
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
