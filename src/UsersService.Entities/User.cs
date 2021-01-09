using System;

namespace UsersService.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }
    }
}
