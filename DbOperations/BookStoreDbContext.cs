using BookStoreWebApi.Entities;
using Microsoft.EntityFrameworkCore;
namespace BookStoreWebApi.DbOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) 
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }    
        
    }
}
