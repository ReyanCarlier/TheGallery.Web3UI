using Nethereum.ABI.FunctionEncoding.Attributes;

namespace TheGallery.Web3UI.Events
{
    [Event("NewAuction")]
    public class NewAuctionEventDTO
    {
        [Parameter("address", "_auction", 1, false)]
        public string Auction { get; set; } = string.Empty;

        [Parameter("uint", "_submittedAt", 2, false)]
        public uint SubmittedAt { get; set; }
    }
}
