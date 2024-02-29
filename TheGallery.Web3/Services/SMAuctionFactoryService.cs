using Microsoft.JSInterop;
using Nethereum.Web3;
using System.Numerics;
using System.Text.Json;
using TheGallery.Web3UI.FunctionOutputs;
using TheGallery.Web3UI.Utils;

namespace TheGallery.Web3UI.Services
{
    public class SMAuctionFactoryService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        private readonly Web3 _web3 = new($"https://polygon-mumbai.g.alchemy.com/v2/{ApiKey.AlchemyApiKey}");
        private readonly HttpClient _httpClient = httpClient;
        private readonly IJSRuntime _jsRuntime = jsRuntime;

        private string _abi = string.Empty;
        private string _abiNFT = string.Empty;
        private AppConfig? _appConfig = null;

        private async Task LoadAbiAsync()
        {
            if (string.IsNullOrEmpty(_abi))
                _abi = await _httpClient.GetStringAsync("ABI/SMAuctionFactoryABI.json");
        }

        private async Task LoadAbiNFTAsync()
        {
            if (string.IsNullOrEmpty(_abiNFT))
                _abiNFT = await _httpClient.GetStringAsync("ABI/MockNFTABI.json");
        }

        public async Task<string> GetContractOwnerAsync()
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            return await _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.SMAuctionFactory).GetFunction("owner").CallAsync<string>();
        }

        public async Task<string?> CreateAuctionAsync(Auction auction)
        {
            await LoadAbiAsync();
            await LoadAbiNFTAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            try
            {
                BigInteger floorPriceWei = Web3.Convert.ToWei(auction.FloorPrice);
                BigInteger reservePriceWei = Web3.Convert.ToWei(auction.ReservePrice);
                await _jsRuntime.InvokeAsync<string>("approveNFT", _appConfig.ContractAddresses.MockNFT, _abiNFT, _appConfig.ContractAddresses.SMAuctionFactory, auction.TokenId);
                var tx = await _jsRuntime.InvokeAsync<string>("newSMAuction", _appConfig.ContractAddresses.SMAuctionFactory, _abi, _appConfig.ContractAddresses.MockNFT, auction.TokenId, floorPriceWei.ToString(), reservePriceWei.ToString(), auction.Coefficient, auction.BuyNow);
                return tx;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de l'enchère : {ex.Message}");
                return null;
            }

        }
    }
}
