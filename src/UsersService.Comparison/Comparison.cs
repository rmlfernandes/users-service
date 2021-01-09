using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using UsersService.TestData;

namespace UsersService.Comparison
{
    [MemoryDiagnoser]
    [RankColumn]
    [SimpleJob(RunStrategy.Throughput, launchCount: 3, warmupCount: 10, targetCount: 30)]
    public class Comparison
    {
        private readonly ApiClient apiClient;
        private readonly GrpcClient grpcClient;

        public Comparison()
        {
            this.apiClient = new ApiClient();
            this.grpcClient = new GrpcClient();
        }

        [Benchmark]
        public async Task Api_GetUser()
        {
            await this.apiClient.GetUserAsync(Guid.Parse(UsersData.UserId));
        }

        [Benchmark]
        public async Task Grpc_GetUser()
        {
            await this.grpcClient.GetUserAsync(UsersData.UserId);
        }

        [Benchmark]
        public async Task Api_GetUsers()
        {
            await this.apiClient.GetUsersAsync();
        }

        [Benchmark]
        public async Task Grpc_GetUsers()
        {
            await this.grpcClient.GetUsersAsync();
        }

        [Benchmark]
        public async Task Api_CreateUser()
        {
            await this.apiClient.CreateUserAsync(UsersData.GetUser());
        }

        [Benchmark]
        public async Task Grpc_CreateUser()
        {
            await this.grpcClient.CreateUserAsync(UsersData.GetProtosUser());
        }

        [Benchmark]
        public async Task Api_CreateUsers()
        {
            await this.apiClient.CreateUsersAsync(UsersData.GetUsers());
        }

        [Benchmark]
        public async Task Grpc_CreateUsers()
        {
            await this.grpcClient.CreateUsersAsync(UsersData.GetProtosUsers());
        }
    }
}
