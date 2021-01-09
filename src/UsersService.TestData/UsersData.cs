using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UsersService.Entities;

namespace UsersService.TestData
{
    public static class UsersData
    {
        private const string AddressId = "5B324D1C-F7B4-42DB-8259-E5E36E29E1F8";

        public static string UserId = "BB19B505-8FF3-472B-B5F4-47C0906F89DD";

        public static User GetUser()
        {
            return new User
            {
                Id = Guid.Parse(UserId),
                FirstName = "John",
                LastName = "Doe",
                Age = 30,
                Gender = Gender.Male,
                Email = "john.doe@email.com",
                Address = new Address
                {
                    Id = Guid.Parse(AddressId),
                    City = "New York",
                    Street = "23rd Street",
                    DoorNumber = 20,
                    Country = "United States of America",
                    ZipCode = "499"
                }
            };
        }

        public static List<User> GetUsers()
        {
            return JsonConvert.DeserializeObject<List<User>>(Properties.Resources.UsersData);
        }

        public static Entities.Protos.User GetProtosUser()
        {
            return new Entities.Protos.User
            {
                Id = UserId,
                FirstName = "John",
                LastName = "Doe",
                Age = 30,
                Gender = Entities.Protos.Gender.Male,
                Email = "john.doe@email.com",
                Address = new Entities.Protos.Address
                {
                    Id = AddressId,
                    City = "New York",
                    Street = "23rd Street",
                    DoorNumber = 20,
                    Country = "United States of America",
                    ZipCode = "499"
                }
            };
        }

        public static List<Entities.Protos.User> GetProtosUsers()
        {
            return JsonConvert.DeserializeObject<List<Entities.Protos.User>>(Properties.Resources.UsersData);
        }
    }
}
