using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UsersService.Entities;

namespace UsersService.Comparison
{
    public class ApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetUserAsync(Guid userId)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetStringAsync($"https://localhost:5001/api/users/{userId}");
        }

        public async Task<string> GetUsersAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetStringAsync("https://localhost:5001/api/users");
        }

        public async Task<string> CreateUserAsync(User user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("https://localhost:5001/api/users", content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateUsersAsync(List<User> users)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(
                JsonConvert.SerializeObject(users),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("https://localhost:5001/api/users/bulk", content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
