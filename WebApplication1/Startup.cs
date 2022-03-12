using Business.Abstract;
using Business.Concrete;
using DataAccess.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using WebApplication1.Middlewares;

namespace WebApplication1
{
    public static class Startup
    {

        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DebugConnection")));
            #region AutoMaapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Scope
            builder.Services.AddScoped<IAuthorService, AuthorManager>();
            builder.Services.AddScoped<IBookService, BookManager>();
            #endregion

           builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "";
            });
            
           builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
             new CultureInfo("en-US"),
             new CultureInfo("tr")
         };

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var languages = context.Request.Headers["Accept-Language"].ToString();
                    var currentLanguage = languages.Split(',').FirstOrDefault();
                    var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? "en-US" : currentLanguage;

                    if (defaultLanguage != "tr" && defaultLanguage != "en-US")
                    {
                        defaultLanguage = "tr";
                    }
                    defaultLanguage = "tr";
                    return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
                }));
            }); 

        }
        private static void Configure(WebApplication app)
        {
            var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSample();
            app.UseCustomException();
            //app.Run(async context => await context.Response.WriteAsync("Run middleware"));

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
