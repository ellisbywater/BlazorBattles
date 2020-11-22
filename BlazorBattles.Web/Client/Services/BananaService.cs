using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorBattles.Web.Client.Services
{
    public class BananaService : IBananaService
    {
        private readonly HttpClient _client;
        public BananaService(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public event Action OnChange;
        public int Bananas { get; set; } = 1000;

        public void EatBananas(int amount)
        {
            Bananas -= amount;
            BananasChanged();
        }
        public async Task AddBananas(int amount)
        {
            var result = await _client.PutAsJsonAsync<int>("api/User/AddBananas", amount);
            Bananas = await result.Content.ReadFromJsonAsync<int>();
            BananasChanged();
        }

        public async Task GetBananas()
        {
            Bananas = await _client.GetFromJsonAsync<int>("api/User/GetBananas");
            BananasChanged();
            
        }

        void BananasChanged() => OnChange.Invoke();
    }
}
