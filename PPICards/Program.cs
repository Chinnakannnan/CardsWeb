using Microsoft.AspNetCore.Authentication.Cookies;
using PPICards.API_Service;
using Microsoft.AspNetCore.Mvc.Razor;
 
 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IAPIClient, APIClient>();
builder.Services.AddTransient<IAPIClient, APIClient>();
//builder.Services.AddTransient<IAPIClient, APIClient>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
   .AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = new PathString("/Login");
        o.SlidingExpiration = true;
    });
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//app.Services.CreateScope();
app.UseRouting();
app.UseAuthentication();
app.UseSession();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var cookies = context.Request.Cookies;
    await next.Invoke();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
