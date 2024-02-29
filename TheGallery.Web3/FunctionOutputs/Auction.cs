namespace TheGallery.Web3UI.FunctionOutputs
{
    public class Auction
    {
        public decimal FloorPrice { get; set; }
        public decimal ReservePrice { get; set; }
        public uint TokenId { get; set; }
        public Offer LastOffer { get; set; } = new Offer();
        public bool BuyNow { get; set; }
        public bool Ended { get; set; }
        public uint Coefficient { get; set; } = 0;
    }
}
