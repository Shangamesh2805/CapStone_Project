using HealthInsuranceAPI.Exceptions;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceApp.Data;
using HealthInsuranceApp.Models;
using System.Linq;

namespace HealthInsuranceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HealthInsuranceAppContext _context;

        public UserRepository(HealthInsuranceAppContext context)
        {
            _context = context;
        }

        public User GetUser(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch {
                throw new UnknownErrorException();
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch(Exception) 
            {
                throw new UserNotFoundException();
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                var user = GetUser(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
            catch (Exception)
            {
                throw new Exception("Unable tp delete the User");

            }
        }
    }
}
