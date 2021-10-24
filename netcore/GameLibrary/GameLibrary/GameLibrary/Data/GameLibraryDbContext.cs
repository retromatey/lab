using GameLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Data
{
    public class GameLibraryDbContext : DbContext
    {
        public GameLibraryDbContext(DbContextOptions<GameLibraryDbContext> options) : base(options) { }
        
        public DbSet<Game> Games { get; set; }
    }
}