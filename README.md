# Users Service
An application to explore the usage of .NET 5, gRPC and compare both using BenchmarkDotNet.

![alt text](https://miro.medium.com/max/256/1*dQXGvdzDoIdWUfo0Mv_6GA.png "BenchmarkDotNet")

## Usage

1. Run the UsersService.Api

    `dotnet run -c Release -p .\UsersService.Api\UsersService.Api.csproj`

2. Run the UsersService.Grpc

    `dotnet run -c Release -p .\UsersService.Grpc\UsersService.Grpc.csproj`

3. Run the Benchmark

    `dotnet run -c Release -p .\UsersService.Comparison\UsersService.Comparison.csproj`