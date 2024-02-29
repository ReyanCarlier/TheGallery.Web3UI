using Microsoft.JSInterop;
using System.Text.Json;
using TheGallery.Web3UI.Utils;

namespace TheGallery.Web3UI.Services
{
    public class MockNFTService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IJSRuntime _jsRuntime = jsRuntime;
        private string _abi = string.Empty;
        private AppConfig? _appConfig = null;

        public async Task<uint> MintNFTAsync()
        {
            if (string.IsNullOrEmpty(_abi))
                _abi = await _httpClient.GetStringAsync("ABI/MockNFTABI.json");
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            try
            {
                var transactionReceiptJson = await _jsRuntime.InvokeAsync<string>("mintNFT", _appConfig.ContractAddresses.MockNFT, _abi);
                var doc = JsonDocument.Parse(transactionReceiptJson);
                uint tokenId = Convert.ToUInt32(doc.RootElement.GetProperty("events").GetProperty("Transfer").GetProperty("returnValues").GetProperty("tokenId").GetString());
                await _jsRuntime.InvokeAsync<string>("approveNFT", _appConfig.ContractAddresses.MockNFT, _abi, _appConfig.ContractAddresses.AuctionFactory, tokenId);
                return tokenId;
            }
            catch
            {
                return 0;
            }
        }
    }
}
