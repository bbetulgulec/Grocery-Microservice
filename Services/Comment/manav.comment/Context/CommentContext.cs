using manav.component.Entities;
using Microsoft.EntityFrameworkCore;

namespace manav.component.Context;

public class CommentContext : DbContext
{
    public CommentContext(DbContextOptions<CommentContext> options)
        : base(options)
    {
    }

    public DbSet<UserComment> Comments { get; set; }
}