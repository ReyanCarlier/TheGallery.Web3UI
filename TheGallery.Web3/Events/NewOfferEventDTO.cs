using Nethereum.ABI.FunctionEncoding.Attributes;

namespace TheGallery.Web3UI.Events
{
    [Event("NewOffer")]
    public class NewOfferEventDTO : IEventDTO
    {
        [Parameter("address", "_operator", 1, false)]
        public string Operator { get; set; } = string.Empty;

        [Parameter("uint", "_bidLimit", 2, false)]
        public uint BidLimit { get; set; }

        [Parameter("uint", "_amount", 3, false)]
        public uint Amount { get; set; }

        public decimal AmountInMatic => Amount / 1e18m;
    }
}
