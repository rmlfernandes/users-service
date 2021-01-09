using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Net.Client;
using UsersService.Entities.Protos;

namespace UsersService.Comparison
{
    public class GrpcClient
    {
        private readonly GrpcChannel channel;
        private readonly Entities.Protos.UsersService.UsersServiceClient client;

        public GrpcClient()
        {
            this.channel = GrpcChannel.ForAddress("https://localhost:5003");
            this.client = new Entities.Protos.UsersService.UsersServiceClient(channel);
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var request = new GetUserRequest
            {
                Id = userId
            };

            return await this.client.RequestUserAsync(request);
        }

        public async Task<Users> GetUsersAsync()
        {
            var request = new GetUsersRequest();

            return await this.client.RequestUsersAsync(request);
        }

        public async Task<EmptyResponse> CreateUserAsync(User user)
        {
            return await this.client.CreateUserAsync(user);
        }

        public async Task<EmptyResponse> CreateUsersAsync(List<User> users)
        {
            var usersRequest = new Users();
            usersRequest.Users_.AddRange(users);

            return await this.client.CreateUsersAsync(usersRequest);
        }
    }
}
