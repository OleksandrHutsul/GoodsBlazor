using GoodsBlazor.DAL.Context;
using GoodsBlazor.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsBlazor.DAL.Seeders;

internal class DataSeeder(GoodsDbContext dbContext): IDataSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Users.Any())
            {
                var users = GetUsers();
                dbContext.Users.AddRange(users);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Products.Any())
            {
                var products = GetProducts();
                dbContext.Products.AddRange(products);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.CartItems.Any())
            {
                var users = dbContext.Users.ToList();
                var products = dbContext.Products.ToList();
                var cartItems = GetCartItems(users, products);
                dbContext.CartItems.AddRange(cartItems);
                await dbContext.SaveChangesAsync();
            }

        }
    }

    private IEnumerable<User> GetUsers()
    {
        List<User> users =
            [
                new()
                {
                    Email = "user1@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345a"),
                    Role = Role.User
                },
                new()
                {
                    Email = "user2@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345b"),
                    Role = Role.User
                },
                new()
                {
                    Email = "user3@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345c"),
                    Role = Role.User
                },
                new()
                {
                    Email = "admin1@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345d"),
                    Role = Role.Admin
                },
                new()
                {
                    Email = "admin2@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345e"),
                    Role = Role.Admin
                },
                new()
                {
                    Email = "admin3@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345af"),
                    Role = Role.Admin
                }
            ];

        return users;
    }

    private IEnumerable<Product> GetProducts()
    {
        List<Product> products =
            [
                new()
                {
                    Name = "Visual Studio",
                    Description = "The most comprehensive IDE for .NET and C++ developers on Windows for building web, cloud, desktop," +
                    "mobile apps, services and games.",
                    Price = 10.0M,
                },
                new()
                {
                    Name = "Miscrosoft Azure",
                    Description = "This page provides an inventory of all Azure SDK library packages, code, and documentation. " +
                    "The Client Libraries and Management Libraries tabs contain libraries that follow the new Azure SDK guidelines. " +
                    "The All tab contains the aforementioned libraries and those that don’t follow the new guidelines.",
                    Price = 5.0M,
                },
                new()
                {
                    Name = "SQL Server 2022",
                    Description = "SQL Server 2022 is the most Azure-enabled release of SQL Server yet, with continued innovation in " +
                    "security, availability, and performance.",
                    Price = 15.0M,
                },
                new()
                {
                    Name = "Git",
                    Description = "Git is a free and open source distributed version control system designed to handle everything from " +
                    "small to very large projects with speed and efficiency.",
                    Price = 7.0M,
                },
                new()
                {
                    Name = "GitHub",
                    Description = "Focus on what matters instead of fighting with Git. Whether you're new to Git or a seasoned user, " +
                    "GitHub Desktop simplifies your development workflow.",
                    Price = 4.0M,
                },
                new()
                {
                    Name = "Visual Studio Code",
                    Description = "Code wherever you're most productive, whether you're connected to the cloud, a remote repository, " +
                    "or in the browser with VS Code for the Web (vscode.dev).",
                    Price = 10.0M,
                },
                new()
                {
                    Name = "MongoDb",
                    Description = "Developers love MongoDB Atlas because it’s intuitive, flexible, and maps easily to object-oriented " +
                    "languages. With the latest release, the world’s most popular document database is faster than ever and easier to " +
                    "scale at lower cost.",
                    Price = 3.0M,
                },
                new()
                {
                    Name = "MySQL",
                    Description = "The most comprehensive set of advanced features, management tools and technical support to achieve " +
                    "the highest levels of MySQL scalability, security, reliability, and uptime.",
                    Price = 2.0M,
                },
                new()
                {
                    Name = "Docker",
                    Description = "Accelerate your development by building Docker images locally or in the cloud with Docker Build Cloud. " +
                    "Create multiple containers using Docker Compose without the hassle of local build constraints.",
                    Price = 1.0M,
                },
                new()
                {
                    Name = "AWS",
                    Description = "Amazon Web Services (AWS) is a leading cloud computing platform renowned for its extensive range of " +
                    "services and global infrastructure.",
                    Price = 25.0M,
                }
            ];

        return products;
    }

    private IEnumerable<CartItem> GetCartItems(List<User> users, List<Product> products)
    {
        List<CartItem> cartItems = [];

        if (!users.Any() || !products.Any())
        {
            return cartItems;
        }

        var random = new Random();

        foreach (var user in users)
        {
            int numOfProducts = random.Next(1, 4);
            var selectedProducts = products.OrderBy(p => random.Next()).Take(numOfProducts);

            foreach (var product in selectedProducts)
            {
                cartItems.Add(new CartItem
                {
                    UserId = user.Id,
                    ProductId = product.Id
                });
            }
        }

        return cartItems;
    }
}
