using Nethereum.ABI.FunctionEncoding.Attributes;

namespace TheGallery.Web3UI.Events
{
    [Event("OfferDenied")]
    public class OfferDeniedEventDTO : IEventDTO
    {
        [Parameter("address", "_operator", 1, false)]
        public string Operator { get; set; } = string.Empty;
    }
}
