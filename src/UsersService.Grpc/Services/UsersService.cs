using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using UsersService.Application;
using UsersService.Entities;

using Protos = UsersService.Entities.Protos;

namespace UsersService.Grpc.Services
{
    public class UsersService : Protos.UsersService.UsersServiceBase
    {
        private readonly IUsersService service;

        public UsersService(IUsersService service)
        {
            this.service = service;
        }

        public override async Task<Protos.User> RequestUser(
            Protos.GetUserRequest request,
            ServerCallContext context)
        {
            var user = await this.service.GetUserAsync(Guid.Parse(request.Id));

            return GetUser(user);
        }

        public override async Task<Protos.Users> RequestUsers(
            Protos.GetUsersRequest request,
            ServerCallContext context)
        {
            var users = await this.service.GetUsersAsync();

            var protosUsers = new Protos.Users();

            foreach (var user in users)
            {
                protosUsers.Users_.Add(GetUser(user));
            }

            return protosUsers;
        }

        public override async Task<Protos.EmptyResponse> CreateUser(
                    Protos.User request,
                    ServerCallContext context)
        {
            await this.service.InsertUserAsync(GetUser(request));

            return new Protos.EmptyResponse();
        }

        public override async Task<Protos.EmptyResponse> CreateUsers(
                    Protos.Users request,
                    ServerCallContext context)
        {
            var users = new List<User>();

            foreach (var protoUser in request.Users_)
            {
                users.Add(GetUser(protoUser));
            }

            await this.service.InsertUsersAsync(users);

            return new Protos.EmptyResponse();
        }

        private static Protos.User GetUser(User user)
        {
            return new Protos.User
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Gender = (Protos.Gender)(int)user.Gender,
                Email = user.Email,
                Address = new Protos.Address
                {
                    Id = user.Address.Id.ToString(),
                    Street = user.Address.Street,
                    DoorNumber = user.Address.DoorNumber,
                    City = user.Address.City,
                    Country = user.Address.Country,
                    ZipCode = user.Address.ZipCode
                }
            };
        }

        private static User GetUser(Protos.User user)
        {
            return new User
            {
                Id = Guid.Parse(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Gender = (Gender)(int)user.Gender,
                Email = user.Email,
                Address = new Address
                {
                    Id = Guid.Parse(user.Address.Id),
                    Street = user.Address.Street,
                    DoorNumber = user.Address.DoorNumber,
                    City = user.Address.City,
                    Country = user.Address.Country,
                    ZipCode = user.Address.ZipCode
                }
            };
        }
    }
}
