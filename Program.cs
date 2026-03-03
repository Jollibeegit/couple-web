using Blazor_Serial_Test.Components;
using Blazor_Serial_Test.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Render 필수: PORT 환경변수로 리슨 (없으면 10000 같은 임의포트/상태145/500 등 꼬임)
builder.WebHost.ConfigureKestrel(options =>
{
    var portStr = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    if (!int.TryParse(portStr, out var port)) port = 8080;

    options.ListenAnyIP(port);
});

// ✅ Razor Components (Blazor Server)
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ EF Core (Neon PostgreSQL) — 연결 문자열 없으면 스킵해 500 방지
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var hasConnection = !string.IsNullOrWhiteSpace(connectionString);

if (hasConnection)
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });
}
else
{
    Console.WriteLine("DB WARNING: DefaultConnection not set. Skipping DbContext registration.");
}

var app = builder.Build();

// ✅ 자동 마이그레이션 (문제 나면 Render 로그에 DB ERROR로 찍히게)
if (hasConnection)
{
    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
            Console.WriteLine("DB MIGRATE OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine("DB ERROR: " + ex);
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// ❌ Render에서는 HTTPS 포트 알 수 없어서 경고/오동작 나는 경우 많음 → 끄는 게 안전
// app.UseHttpsRedirection();
 
app.UseStaticFiles();
app.UseAntiforgery();

// ✅ Blazor 루트 컴포넌트 매핑 (App.razor)
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();