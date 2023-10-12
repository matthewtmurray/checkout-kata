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
            List<Basket> basketList = new List<Basket>();

            foreach (var item in basketItemsArray)
            {
                if (!_skuRules.Any(r => r.Sku == item))
                {
                    throw new Exception("Unidentified item in the bagging area");
                }

                if (basketList.Any(b => b.Sku == item))
                {
                    basketList.Where(b => b.Sku == item).ToList().ForEach(b => b.Count++);
                }
                else
                {
                    basketList.Add(new Basket{Sku = item, Count = 1});
                }
            }

            foreach (var basketItem in basketList)
            {
                totalPrice += GetTotalPricePerItem(basketItem.Sku, basketItem.Count);
            }

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
