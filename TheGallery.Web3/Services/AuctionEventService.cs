using Nethereum.Web3;
using TheGallery.Web3UI.Events;
using TheGallery.Web3UI.Utils;

namespace TheGallery.Web3UI.Services
{
    public class AuctionEventService(HttpClient httpClient)
    {
        private readonly Web3 _web3 = new($"https://polygon-mumbai.g.alchemy.com/v2/{ApiKey.AlchemyApiKey}");
        private readonly HttpClient _httpClient = httpClient;
        private string _abi = string.Empty;

        private async Task LoadAbiAsync()
        {
            if (string.IsNullOrEmpty(_abi))
                _abi = await _httpClient.GetStringAsync("ABI/AuctionABI.json");
        }

        public async Task<List<ReceivedEventDTO>> GetReceivedEventsAsync(string contractAddress)
        {
            await LoadAbiAsync();
            var contract = _web3.Eth.GetContract(_abi, contractAddress);
            var receivedEvent = contract.GetEvent("Received");
            var filterAll = receivedEvent.CreateFilterInput();
            var changes = await receivedEvent.GetAllChangesAsync<ReceivedEventDTO>(filterAll);

            return changes.Select(change => change.Event).ToList();
        }

        public async Task<List<NewOfferEventDTO>> GetNewOfferEventsAsync(string contractAddress)
        {
            await LoadAbiAsync();
            var contract = _web3.Eth.GetContract(_abi, contractAddress);
            var newOfferEvent = contract.GetEvent("NewOffer");
            var filterAll = newOfferEvent.CreateFilterInput();
            var changes = await newOfferEvent.GetAllChangesAsync<NewOfferEventDTO>(filterAll);

            return changes.Select(change => change.Event).ToList();
        }

        public async Task<List<NewOfferOverReservePriceEventDTO>> GetNewOfferOverReservePriceEventsAsync(string contractAddress)
        {
            await LoadAbiAsync();
            var contract = _web3.Eth.GetContract(_abi, contractAddress);
            var eventHandler = contract.GetEvent("NewOfferOverReservePrice");
            var filterAll = eventHandler.CreateFilterInput();
            var changes = await eventHandler.GetAllChangesAsync<NewOfferOverReservePriceEventDTO>(filterAll);

            return changes.Select(change => change.Event).ToList();
        }

        public async Task<List<OfferAcceptedEventDTO>> GetOfferAcceptedEventsAsync(string contractAddress)
        {
            await LoadAbiAsync();
            var contract = _web3.Eth.GetContract(_abi, contractAddress);
            var eventHandler = contract.GetEvent("OfferAccepted");
            var filterAll = eventHandler.CreateFilterInput();
            var changes = await eventHandler.GetAllChangesAsync<OfferAcceptedEventDTO>(filterAll);

            return changes.Select(change => change.Event).ToList();
        }

        public async Task<List<OfferDeniedEventDTO>> GetOfferDeniedEventsAsync(string contractAddress)
        {
            await LoadAbiAsync();
            var contract = _web3.Eth.GetContract(_abi, contractAddress);
            var eventHandler = contract.GetEvent("OfferDenied");
            var filterAll = eventHandler.CreateFilterInput();
            var changes = await eventHandler.GetAllChangesAsync<OfferDeniedEventDTO>(filterAll);

            return changes.Select(change => change.Event).ToList();
        }
    }
}