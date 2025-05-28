using Codelabs.BLL;
using Codelabs.BLL.Types;
using Codelabs.UI.Components;
using Codelabs.UI.Components.InternalTypes;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Codelabs.UI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddBlazorBootstrap();
        builder.Services.AddScoped<DockerService>();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(
                    options =>
                    {
                        options.Cookie.Name = "auth_token";
                        options.LoginPath = "/login";
                        options.Cookie.MaxAge = TimeSpan.FromDays(31);
                    });
        
        builder.Services.AddAuthorization();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddHttpContextAccessor();  
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<CounterService>();
        builder.Services.AddScoped<ExercisesBurgerService>();
        builder.Services.AddSingleton(_ =>
        {
            var service = new LanguagesService();
            
            service.AddLanguage(new CppLanguage());
            service.AddLanguage(new RustLanguage());
            service.AddLanguage(new PythonLanguage());
            return service;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseStatusCodePagesWithReExecute("/404");
        app.UseAntiforgery();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}