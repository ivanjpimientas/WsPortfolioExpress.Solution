using WsPortfolioExpress.Common.Entities;

namespace WsPortfolioExpress.Web.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        ICollection<User> GetUsers();
        User GetUserLogin(string email, string password);
        User GetUser(int userId);
        User GetUserByEmail(string email);
        bool ExistUser(string name);
        bool ExistUser(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
