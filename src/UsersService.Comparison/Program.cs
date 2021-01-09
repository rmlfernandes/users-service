using System;
using BenchmarkDotNet.Running;

namespace UsersService.Comparison
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting comparison..");

            _ = BenchmarkRunner.Run<Comparison>();
        }
    }
}
