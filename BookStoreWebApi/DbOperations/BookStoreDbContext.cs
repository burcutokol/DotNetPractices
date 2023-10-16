using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
namespace BookStoreWebApi.DbOperations
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) 
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }    
        public DbSet<Author> Authors { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges(); // DbContext SaveChanges metodunu override ediyor.
                                       // Bu sayede IBookStoreDbContext üzerinden de SaveChanges yapılabilir.

        }

    }
}
