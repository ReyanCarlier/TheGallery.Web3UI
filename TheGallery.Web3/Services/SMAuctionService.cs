using Microsoft.JSInterop;
using Nethereum.Contracts;
using Nethereum.Web3;
using System.Numerics;
using TheGallery.Web3UI.FunctionOutputs;
using TheGallery.Web3UI.Utils;

namespace TheGallery.Web3UI.Services
{
    public class SMAuctionService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        private readonly Web3 _web3 = new($"https://polygon-mumbai.g.alchemy.com/v2/{ApiKey.AlchemyApiKey}");
        private readonly HttpClient _httpClient = httpClient;
        private string _abi = string.Empty;
        private readonly IJSRuntime _jsRuntime = jsRuntime;

        public async Task<Auction?> GetAuctionDetailsAsync(string contractAddress)
        {
            if (string.IsNullOrEmpty(_abi))
                _abi = await _httpClient.GetStringAsync("ABI/SMAuctionABI.json");
            Contract contract;
            try
            {
                contract = _web3.Eth.GetContract(_abi, contractAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur lors de la récupération du contrat : {e}");
                return null;
            }
            var token = await contract.GetFunction("getToken").CallAsync<BigInteger>();
            var lastOffer = await contract.GetFunction("lastOffer").CallDeserializingToObjectAsync<Offer>();
            var floorPrice = await contract.GetFunction("getFloorPrice").CallAsync<BigInteger>();
            var buyNow = await contract.GetFunction("getBuyNow").CallAsync<bool>();
            var reservePrice = await contract.GetFunction("getReservePrice").CallAsync<BigInteger>();
            var ended = await contract.GetFunction("getEnded").CallAsync<bool>();
            return new Auction
            {
                FloorPrice = Web3.Convert.FromWei(floorPrice),
                TokenId = (uint)token,
                LastOffer = lastOffer,
                BuyNow = buyNow,
                ReservePrice = Web3.Convert.FromWei(reservePrice),
                Ended = ended
            };
        }

        private async Task LoadAbiAsync()
        {
            if (string.IsNullOrEmpty(_abi))
                _abi = await _httpClient.GetStringAsync("ABI/SMAuctionABI.json");
        }

        public async Task<string> GetContractOwnerAsync(string contractAddress)
        {
            await LoadAbiAsync();
            return await _web3.Eth.GetContract(_abi, contractAddress).GetFunction("owner").CallAsync<string>();
        }

        public async Task<string> SubmitOfferAsync(BigInteger bidLimit, BigInteger offerAmount, string contractAddress)
        {
            await LoadAbiAsync();
            return await _jsRuntime.InvokeAsync<string>("callBidFunction", contractAddress, _abi, bidLimit.ToString(), offerAmount.ToString());
        }

        public async Task<string> AcceptLastOfferAsync(string contractAddress)
        {
            await LoadAbiAsync();
            return await _jsRuntime.InvokeAsync<string>("callAcceptLastOfferFunction", contractAddress, _abi);
        }

        public async Task<string> RefuseLastOfferAsync(string contractAddress)
        {
            await LoadAbiAsync();
            return await _jsRuntime.InvokeAsync<string>("callRefuseLastOfferFunction", contractAddress, _abi);
        }

        public async Task<string> CancelAuctionAsync(string contractAddress)
        {
            await LoadAbiAsync();
            return await _jsRuntime.InvokeAsync<string>("callCancelAuctionFunction", contractAddress, _abi);
        }
    }
}
