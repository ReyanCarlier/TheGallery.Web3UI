using Nethereum.ABI.FunctionEncoding.Attributes;

namespace TheGallery.Web3UI.Events
{
    [Event("OfferAccepted")]
    public class OfferAcceptedEventDTO : IEventDTO
    {
        [Parameter("address", "_operator", 1, false)]
        public string Operator { get; set; } = string.Empty;

        [Parameter("uint", "_amount", 2, false)]
        public uint Amount { get; set; }

        public decimal AmountInMatic => Amount / 1e18m;
    }
}
