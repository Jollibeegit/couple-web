using Blazor_Serial_Test.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//
// 🔥 Render 필수 포트 설정 (이거 없으면 status 145로 죽음)
//
builder.WebHost.ConfigureKestrel(options =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    options.ListenAnyIP(int.Parse(port));
});

//
// Razor Components (Blazor Server)
//
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//
// EF Core (Neon PostgreSQL)
//
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

//
// 🔥 Blazor Server 매핑
//
app.MapRazorComponents<Blazor_Serial_Test.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
