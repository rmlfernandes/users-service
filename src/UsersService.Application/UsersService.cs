using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersService.Application.Repositories;
using UsersService.Entities;

namespace UsersService.Application
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository repository;

        public UsersService(IUsersRepository repository)
        {
            this.repository = repository;
        }

        public Task<User> GetUserAsync(Guid id)
        {
            return this.repository.GetUserAsync(id);
        }

        public Task<IList<User>> GetUsersAsync()
        {
            return this.repository.GetUsersAsync();
        }

        public Task InsertUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return this.repository.InsertUserAsync(user);
        }

        public Task InsertUsersAsync(IList<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            if (users.Count == 0)
            {
                return Task.CompletedTask;
            }

            return this.repository.InsertUsersAsync(users);
        }
    }
}
