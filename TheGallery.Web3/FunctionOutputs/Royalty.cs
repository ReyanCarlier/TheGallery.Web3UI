using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace TheGallery.Web3UI.FunctionOutputs
{
    [FunctionOutput]
    public class Royalty
    {
        [Parameter("address", "receiver", 1)]
        public string Receiver { get; set; } = string.Empty;

        [Parameter("uint256", "royaltyAmount", 2)]
        public ulong RoyaltyAmount { get; set; } = 0;

        public decimal RoyaltyAmountInMatic
        {
            get { return Web3.Convert.FromWei(RoyaltyAmount); }
        }

        public decimal RoyaltyAmountInMaticWithDecimals
        {
            get { return Web3.Convert.FromWei(RoyaltyAmount, 18); }
        }
    }
}
