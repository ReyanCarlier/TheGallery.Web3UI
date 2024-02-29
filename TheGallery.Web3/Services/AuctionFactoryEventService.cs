using Nethereum.Web3;
using TheGallery.Web3UI.Events;
using TheGallery.Web3UI.Utils;
using System.Text.Json;

namespace TheGallery.Web3UI.Services
{
    public class AuctionFactoryEventService(HttpClient httpClient)
    {
        private readonly Web3 _web3 = new($"https://polygon-mumbai.g.alchemy.com/v2/{ApiKey.AlchemyApiKey}");
        private readonly HttpClient _httpClient = httpClient;
        private AppConfig? _appConfig = null;
        private string _abi = string.Empty;

        private async Task LoadAbiAsync()
        {
            if (string.IsNullOrEmpty(_abi))
                _abi = await _httpClient.GetStringAsync("ABI/AuctionFactoryABI.json");
        }

        public async Task<List<NewAuctionEventDTO>> GetNewAuctionEventsAsync()
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            var contract = _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.AuctionFactory);
            var eventHandler = contract.GetEvent("NewAuction");
            var filterAll = eventHandler.CreateFilterInput(null, null);
            Console.WriteLine(filterAll);
            var changes = await eventHandler.GetAllChangesAsync<NewAuctionEventDTO>(filterAll);
            Console.WriteLine(changes.Count);
            return changes.Select(change => change.Event).ToList();
        }
    }

}
