using LAB1.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB1.Data
{
    public class ApplicationDbContext:DbContext
    {
        //Constructor, pass the options to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }

        //To retrieve the property from the table
        public DbSet<Car> Cars { get; set; }

    }
}
