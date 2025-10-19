using Microsoft.EntityFrameworkCore;
using PingIdentityApp.Data.Entities;

namespace GetChatty.Data;

/// <summary>
/// Represents the data context for threads, posts, and comments.
/// </summary>
/// <remarks>
/// This class represents the data context for the threads, posts, and comments.
/// </remarks>
public class GetChattyDataContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetChattyDataContext"/> class using the specified options.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    /// <remarks>
    /// This constructor initializes a new instance of the <see cref="GetChattyDataContext"/> class using the specified options.
    /// </remarks>
    public GetChattyDataContext(DbContextOptions<GetChattyDataContext> options)
        : base(options){}

    /// <summary>
    /// Gets the set of all <see cref="AccessRequest"/> entities in the context.
    /// </summary>
    public DbSet<AccessRequest> AccessRequests => Set<AccessRequest>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}