using AlirezaEShop.Models;

namespace AlirezaEShop.Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        bool IsExistUserByEmail(string email);
        User GetUserForLogin(string Email,string Password);
    }

    public class UserRepository : IUserRepository
    {
        AlirezaEShopContext _context;
        public UserRepository(AlirezaEShopContext context)
        {
            _context = context; 
        }
        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges(); 
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(c => c.Email == email);
        }

        public User GetUserForLogin(string Email,string Password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == Email && u.Password == Password);
        }
    }

}
