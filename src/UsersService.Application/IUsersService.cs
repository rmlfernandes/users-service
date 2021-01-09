using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersService.Entities;

namespace UsersService.Application
{
    public interface IUsersService
    {
        Task<User> GetUserAsync(Guid id);

        Task<IList<User>> GetUsersAsync();

        Task InsertUserAsync(User user);

        Task InsertUsersAsync(IList<User> users);
    }
}
