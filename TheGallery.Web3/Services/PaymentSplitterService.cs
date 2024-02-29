using Microsoft.JSInterop;
using Nethereum.Web3;
using System.Numerics;
using System.Text.Json;
using TheGallery.Web3UI.Utils;

namespace TheGallery.Web3UI.Services
{
    public class PaymentSplitterService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        private readonly Web3 _web3 = new($"https://polygon-mumbai.g.alchemy.com/v2/{ApiKey.AlchemyApiKey}");
        private readonly HttpClient _httpClient = httpClient;
        private readonly IJSRuntime _jsRuntime = jsRuntime;

        private string _abi = string.Empty;
        private AppConfig? _appConfig = null;


        private async Task LoadAbiAsync()
        {
            if (string.IsNullOrEmpty(_abi))
                _abi = await _httpClient.GetStringAsync("ABI/PaymentSplitterABI.json");
        }

        /**
         * @dev Returns the total amount of shares distributed.
         */
        public async Task<BigInteger> GetTotalSharesAsync()
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            return await _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.PaymentSplitter).GetFunction("totalShares").CallAsync<BigInteger>();
        }

        /**
         * @dev Returns the total amount of MATIC released to date.
         */
        public async Task<BigInteger> GetTotalReleased()
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            return await _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.PaymentSplitter).GetFunction("totalReleased").CallAsync<BigInteger>();
        }

        /**
         * @dev Returns the amount of shares belonging to an address.
         */
        public async Task<BigInteger> GetSharesAsync(string address)
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            return await _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.PaymentSplitter).GetFunction("shares").CallAsync<BigInteger>(address);
        }

        /**
         * @dev Returns the total amount of shares released to an address.
         */
        public async Task<BigInteger> GetReleasedAsync(string address)
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            return await _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.PaymentSplitter).GetFunction("released").CallAsync<BigInteger>(address);
        }

        /**
         * @dev Returns the address of the payee with the given index.
         */
        public async Task<string> GetPayeeAsync(int index)
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            return await _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.PaymentSplitter).GetFunction("payee").CallAsync<string>(index);
        }

        /**
         * @dev Returns the amount of MATIC releasable with the given address.
         */
        public async Task<BigInteger> GetReleasableAsync(string address)
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            return await _web3.Eth.GetContract(_abi, _appConfig.ContractAddresses.PaymentSplitter).GetFunction("releasable").CallAsync<BigInteger>(address);
        }

        /*
         * @dev Triggers a transfer to `account` of the amount of Ether they are owed, according to their percentage of the
         * total shares and their previous withdrawals.
         */
        public async Task<string> ReleaseAsync(string address)
        {
            await LoadAbiAsync();
            _appConfig ??= JsonSerializer.Deserialize<AppConfig>(await _httpClient.GetStringAsync("settings/ContractAddresses.json")) ?? new AppConfig();
            return await _jsRuntime.InvokeAsync<string>("callReleaseFunction", _appConfig.ContractAddresses.PaymentSplitter, _abi, address);
        }
    }
}
