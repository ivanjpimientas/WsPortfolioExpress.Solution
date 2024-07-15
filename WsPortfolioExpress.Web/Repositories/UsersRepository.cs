using WsPortfolioExpress.Common.Entities;
using WsPortfolioExpress.Web.Context;
using WsPortfolioExpress.Web.Repositories.Interfaces;
using WsPortfolioExpress.Web.Services;

namespace WsPortfolioExpress.Web.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public bool ExistUser(string name)
        {
            bool value = _context.Users.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistUser(int id)
        {
            return _context.Users.Any(c => c.Id == id);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(c => c.Name).ToList();
        }

        public User GetUserLogin(string email, string password)
        {
            return _context.Users.FirstOrDefault(c => c.Email == email && c.Password == Utilities.ConvertToSHA256(password));
        }

        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(c => c.Id == userId);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(c => c.Email == email);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
