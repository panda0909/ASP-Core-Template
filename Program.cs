using ASP_Core_Template.Middleware;
using ASP_Core_Template.Services;
using ASP_Core_Template.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        // Singleton 整個生命週期只有一個實例
        // builder.Services.AddSingleton<ITemplateService, TemplateService>();
        // Scoped 每次請求都會建立一個新的實例
        builder.Services.AddScoped<ITemplateService, TemplateService>();
        // Transient 每次注入都會建立一個新的實例
        //builder.Services.AddTransient<ITemplateService, TemplateService>();
        // 選項模式
        builder.Services.Configure<TemplateOption>(builder.Configuration.GetSection("TemplateOption"));
        
        // Swagger UI
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }else{
            app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
        //中間件
        app.UseMiddleware<LoggingMiddleware>();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapGet("/hello",async context =>
        {
            await context.Response.WriteAsync("Hello World!");
        });
        app.MapGet("/hello/{name}",async context =>
        {
            var name = context.Request.RouteValues["name"];
            await context.Response.WriteAsync($"Hello {name}!");
        });
        app.MapGet("/users/{id:int}",async context =>
        {
            var id = context.Request.RouteValues["id"];
            await context.Response.WriteAsync($"User ID: {id}");
        });

        //全域錯誤處理
        app.UseExceptionHandler("/Error");
        app.MapGet("/error",async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            await context.Response.WriteAsync($"Error: {exception.Message}");
        });

        app.Run();
    }
}