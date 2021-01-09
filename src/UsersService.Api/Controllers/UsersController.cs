using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersService.Application;
using UsersService.Entities;

namespace UsersService.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public Task<User> GetUserAsync(Guid id)
        {
            return this.service.GetUserAsync(id);
        }

        [HttpGet]
        public Task<IList<User>> GetUsersAsync()
        {
            return this.service.GetUsersAsync();
        }

        [HttpPost]
        public Task CreateUserAsync([FromBody] User user)
        {
            return this.service.InsertUserAsync(user);
        }

        [HttpPost("bulk")]
        public Task CreateUsersAsync([FromBody] List<User> users)
        {
            return this.service.InsertUsersAsync(users);
        }
    }
}
