using MediaCollectionLibrary.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaCollectionLibrary.Api
{
    public class MclContext : DbContext
    {
        public MclContext(DbContextOptions<MclContext> options) : base(options)
        {

        }

        public DbSet<Medium> Media { get; set; }
    }
}
