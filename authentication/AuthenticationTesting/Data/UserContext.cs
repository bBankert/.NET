using Microsoft.EntityFrameworkCore;

namespace AuthenticationTesting.Data{
    public class UserContext : DbContext{
        public UserContext(DbContextOptions<UserContext> options) : base(options){

        }
        public DbSet<AuthenticationTesting.Models.User> User {get;set;}
    }
}