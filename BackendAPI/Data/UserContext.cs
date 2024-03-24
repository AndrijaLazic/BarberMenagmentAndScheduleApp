using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    public class UserContext : DbContext
    {
        
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        private DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();
        }

        public bool RegisterUser(RegistrationDTO registrationDTO)
        {
            
            try
            {
                User user = new User(
                        registrationDTO.Name,
                        registrationDTO.Email,
                        registrationDTO.Password,
                        registrationDTO.PhoneNumber);

                if(CheckIfUserExists(user))
                    return false;

 
                Console.WriteLine(Users.Add(user));
         


                this.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        private bool CheckIfUserExists(User newUser)
        {
            
            try
            {
                User? oldUser=Users.Where(user =>
                    user.Email.Equals(newUser.Email) ||
                    user.PhoneNumber.Equals(newUser.PhoneNumber)
                ).FirstOrDefault();
                if (oldUser==null)
                    return false;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            

            return true;
        }

    }
}
