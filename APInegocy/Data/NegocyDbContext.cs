using APInegocy.Models;
using Microsoft.EntityFrameworkCore;

public class NegocyDbContext : DbContext
{
    public NegocyDbContext(DbContextOptions<NegocyDbContext> options)
        : base(options)
    {
    }

    public DbSet<Service> Service { get; set; }
    public DbSet<Productos> Productos { get; set; }
    public DbSet<User> Users { get; set; }



}