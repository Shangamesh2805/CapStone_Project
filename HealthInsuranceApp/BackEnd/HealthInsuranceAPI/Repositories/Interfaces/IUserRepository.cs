using HealthInsuranceAPI.Models;

namespace HealthInsuranceAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
