using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        private string connectionString = @"Server=DELL-5490\MSSQLSERVER01;Database=shop;Trusted_Connection=True";

        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>().HasData(new List<Products>()
            {
                new Products(){ID=1, Name= "Blossom bouquet flower", Quantity=100 , Price=60000,CategoryID=1, Image="product-1.jpg", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam fringilla augue nec est tristique auctor. Ipsum metus feugiat sem, quis fermentum turpis eros eget velit. Donec ac tempus ante. Fusce ultricies massa massa. Fusce aliquam, purus eget sagittis vulputate, sapien libero hendrerit est, sed commodo augue nisi non neque.Cras neque metus, consequat et blandit et, luctus a nunc. Etiam gravida vehicula tellus, in imperdiet ligula euismod eget. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nam erat mi, rutrum at sollicitudin rhoncus", Color="purple", Size="Medium(30cm)"  },
                //new Products(){ID=2, Name= "BB", Number=15 , Price=20000  },
                //new Products(){ID=3, Name= "CC", Number=20 , Price=30000  },
                //new Products(){ID=4, Name= "DD", Number=11 , Price=20000  },
                //new Products(){ID=5, Name= "EE", Number=12 , Price=70000  },
                //new Products(){ID=6, Name= "FF", Number=16 , Price=65000  },
                //new Products(){ID=7, Name= "GG", Number=17 , Price=30000  },
                //new Products(){ID=8, Name= "HH", Number=15 , Price=70000  },
            });

            modelBuilder.Entity<Category>().HasData(new List<Category>()
            {
                new Category(){ID=1, Name= "Holiday flowers", Description="flowers for holidays"  },
                new Category(){ID=2, Name= "Wedding flowers", Description="flowers for wedding"  },
                new Category(){ID=3, Name= "Condolence flowers", Description="flowers for sadness"  },
                new Category(){ID=4, Name= "Anniversary flowers", Description="flowers for anniversary events"  },
                new Category(){ID=5, Name= "Mother's day flowers", Description="flowers for mother's day"  }
            });
        }
    }
}
