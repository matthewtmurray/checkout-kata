namespace BrightHR_Checkout
{
    public class Checkout : ICheckout
    {
        private readonly List<SkuRule> _skuRules;
        private string _basketItems = string.Empty;
        public Checkout(List<SkuRule> skuRules)
        {
            _skuRules = skuRules;
        }
        public void Scan(string item)
        {
            _basketItems += item;
        }

        public int GetTotalPrice()
        {
            var totalPrice = 0;
            var basketItemsArray = _basketItems.ToCharArray();

            foreach (var item in basketItemsArray)
            {
                if (!_skuRules.Any(r => r.Sku == item))
                {
                    throw new Exception("Unidentified item in the bagging area");
                }
            }

            var aCount = basketItemsArray.Count(letter => letter == 'A');
            var bCount = basketItemsArray.Count(letter => letter == 'B');
            var cCount = basketItemsArray.Count(letter => letter == 'C');
            var dCount = basketItemsArray.Count(letter => letter == 'D');

            totalPrice += GetTotalPricePerItem('A', aCount);
            totalPrice += GetTotalPricePerItem('B', bCount);
            totalPrice += GetTotalPricePerItem('C', cCount);
            totalPrice += GetTotalPricePerItem('D', dCount);

            return totalPrice;
        }

        private int GetTotalPricePerItem(char item, int itemCount)
        {
            var totalPrice = 0;

            if (itemCount <= 1 || !_skuRules.Where(r => r.Sku == item).Select(r => r.HasOfferPrice).FirstOrDefault())
            {
                totalPrice += _skuRules.Where(r => r.Sku == item).Select(r => r.Price).FirstOrDefault() * itemCount;
            }
            else
            {
                var offerQuantity = _skuRules.Where(r => r.Sku == item).Select(r => r.OfferQuantity).FirstOrDefault();
                var offerPrice = _skuRules.Where(r => r.Sku == item).Select(r => r.OfferPrice).FirstOrDefault();
                var fullPrice = _skuRules.Where(r => r.Sku == item).Select(r => r.Price).FirstOrDefault();
                var singlePricedItems = itemCount % offerQuantity;
                var offerQuantityMultiple = (itemCount - singlePricedItems) / offerQuantity;
                totalPrice += (offerQuantityMultiple * offerPrice) + (singlePricedItems * fullPrice);
            }

            return totalPrice;
        }
    }
}
