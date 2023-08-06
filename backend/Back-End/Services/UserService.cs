using System.Net.Http;
using Back_End.Contexts;
using Back_End.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Back_End.Services
{
    public class UserService : IUserService
    {
        private UserContext _userContext;

        public UserService(UserContext context)
        {
            _userContext = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _userContext.Users.SingleOrDefault(user => user.Username == username && user.Password == password));

            // Return null if user not found
            if (user == null)
                return null;

            return user;
        }

        public async Task<User> CreateUser(User user)
        {
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

    }
}