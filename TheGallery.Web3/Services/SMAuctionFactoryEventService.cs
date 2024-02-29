using Nethereum.Web3;
using TheGallery.Web3UI.Events;
using TheGallery.Web3UI.Utils;
using System.Text.Json;

namespace TheGallery.Web3UI.Services
{
    public class SMAuctionFactoryEventService(HttpClient httpClient)
    {
        private readonly Web3 _web3 = new($"https://polygon-mumbai.g.alchemy.com/v2/{ApiKey.AlchemyApiKey}");
        private readonly HttpClient _httpClient = httpClient;
        private AppConfig? _appConfig = null;
        private string _abi = string.Empty;

        private async Task LoadAbiAsync()
        {
            if (string.IsNullOrEmpty(_abi))
                _abi = await _httpClient.GetStringAsync("ABI/SMAuctionFactoryABI.json");
        }

        public async Task<List<NewAuctionEventDTO>> GetNewAuctionEventsAsync()
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            var contract = _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.SMAuctionFactory);
            var eventHandler = contract.GetEvent("NewSecondaryMarketAuction");
            var filterAll = eventHandler.CreateFilterInput();
            var changes = await eventHandler.GetAllChangesAsync<NewAuctionEventDTO>(filterAll);
            return changes.Select(change => change.Event).ToList();
        }
    }

}
