using BlogMainStructure.Business.DTOs.MailDTOs;
using BlogMainStructure.Business.Extensions;
using BlogMainStructure.Infrastructure.Extensions;
using BlogMainStructure.UI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddUIServices();

builder.Services.AddBusinessServices();

builder.Services.Configure<SmtpSettingsDTO>(builder.Configuration.GetSection("SmtpSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapDefaultControllerRoute();

app.Run();
