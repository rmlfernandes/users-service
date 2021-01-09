using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Application.Repositories;
using UsersService.Entities;

namespace UsersService.Repository.InMemory
{
    public class UsersRepository : IUsersRepository
    {
        private IList<User> users;

        public UsersRepository()
        {
            this.users = new List<User>();
        }

        public Task<User> GetUserAsync(Guid id)
        {
            return Task.FromResult(this.users
                .FirstOrDefault(user => user.Id == id));
        }

        public Task<IList<User>> GetUsersAsync()
        {
            return Task.FromResult(this.users);
        }

        public Task InsertUserAsync(User user)
        {
            this.users.Add(user);

            return Task.CompletedTask;
        }

        public Task InsertUsersAsync(IList<User> users)
        {
            this.users = this.users.Concat(users).ToList();

            return Task.CompletedTask;
        }
    }
}
