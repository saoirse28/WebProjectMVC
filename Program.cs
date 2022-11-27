using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebProjectMVC.Data;
using WebProjectMVC.Interface.Services;
using WebProjectMVC.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebProjectMVC.Hubs;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail=true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IPhotos,PhotoRepository>();
builder.Services.AddScoped<IPhotoDetails, PhotoDetailRepository>();
builder.Services.AddScoped<IReactions,ReactionRepository>();
builder.Services.AddScoped<IPhotoDetailComments,PhotoDetailCommentRepository>();
builder.Services.AddScoped<IPhotoReactions, PhotoReactionRepository>();
builder.Services.AddScoped<IPhotoDetailReactions, PhotoDetailReactionRepository>();


//SignalR
builder.Services.AddSignalR(cfg => { cfg.EnableDetailedErrors=true;});
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", p => p.AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "default",
    pattern: "{controller=Photos}/{action=Index}/{id?}");

app.MapRazorPages();

//SignalR
app.UseCors("CorsPolicy");
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
