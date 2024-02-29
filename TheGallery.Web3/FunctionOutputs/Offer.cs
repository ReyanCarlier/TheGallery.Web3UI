namespace TheGallery.Web3UI.FunctionOutputs
{
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Web3;
    using System.Numerics;

    [FunctionOutput]
    public class Offer
    {
        [Parameter("address", "bidder", 1)]
        public string Bidder { get; set; } = string.Empty;

        [Parameter("uint256", "amount", 2)]
        public BigInteger Amount { get; set; }

        [Parameter("uint256", "submittedAt", 3)]
        public BigInteger SubmittedAt { get; set; }

        [Parameter("uint256", "bidLimit", 4)]
        public BigInteger BidLimit { get; set; }
        public decimal AmountInMatic
        {
            get { return Web3.Convert.FromWei(Amount); }
        }
        public decimal AmountInMaticWithDecimals
        {
            get { return Web3.Convert.FromWei(Amount, 18); }
        }
    }

}
