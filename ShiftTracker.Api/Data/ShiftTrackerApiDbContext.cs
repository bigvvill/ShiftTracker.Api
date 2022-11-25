using Microsoft.EntityFrameworkCore;
using ShiftTracker.Api.Entities;

namespace ShiftTracker.Api.Data;

public class ShiftTrackerApiDbContext : DbContext
{
    public ShiftTrackerApiDbContext(DbContextOptions<ShiftTrackerApiDbContext> options)
        : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
}
