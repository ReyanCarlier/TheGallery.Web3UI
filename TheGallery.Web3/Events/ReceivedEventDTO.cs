using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace TheGallery.Web3UI.Events
{
    [Event("Received")]
    public class ReceivedEventDTO : IEventDTO
    {
        [Parameter("address", "_operator", 1, false)]
        public string Operator { get; set; } = string.Empty;

        [Parameter("address", "_from", 2, false)]
        public string From { get; set; } = string.Empty;

        [Parameter("uint256", "_tokenId", 3, false)]
        public BigInteger TokenId { get; set; }

        [Parameter("bytes", "_data", 4, false)]
        public byte[] Data { get; set; } = [];
    }
}

