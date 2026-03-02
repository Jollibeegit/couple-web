using Blazor_Serial_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Serial_Test.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Memo> Memos => Set<Memo>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<Anniversary> Anniversaries => Set<Anniversary>();
}
