namespace TheGallery.Web3UI.Utils
{
    public static class ApiKey
    {
        readonly public static string AlchemyApiKey = "YourAlchemyAPIKey";
    }

    public class AppConfig
    {
        public ContractAddresses ContractAddresses { get; set; } = new();
    }

    public class ContractAddresses
    {
        public string AuctionFactory { get; set; } = string.Empty;
        public string MockNFT { get; set; } = string.Empty;
        public string SMAuctionFactory { get; set; } = string.Empty;
        public string PaymentSplitter { get; set; } = string.Empty;
    }
}
