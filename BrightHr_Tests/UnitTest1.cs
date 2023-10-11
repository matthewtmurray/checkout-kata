using BrightHR_Checkout;

namespace BrightHr_Tests
{
    public class Tests
    {
        private Checkout _checkout;
        private string _singleItemBasket = "A";
        private List<SkuRule> _skuRules_SomeOffers;

        [SetUp]
        public void Setup()
        {
            _skuRules_SomeOffers = new List<SkuRule>
            {
                new SkuRule { Sku = 'A', Price = 50, HasOfferPrice = true, OfferQuantity = 3, OfferPrice = 130 },
                new SkuRule { Sku = 'B', Price = 30, HasOfferPrice = true, OfferQuantity = 2, OfferPrice = 55 },
                new SkuRule { Sku = 'C', Price = 20, HasOfferPrice = false, OfferQuantity = 3, OfferPrice = 50 },
                new SkuRule { Sku = 'D', Price = 15, HasOfferPrice = false, OfferQuantity = 5, OfferPrice = 50 },
            };
        }

        [Test]
        public void IsValidPrice_SingleItemBasket_SomeOffers()
        {
            var basket = _singleItemBasket;
            var expectedBasketTotal = 50;
            _checkout = new Checkout(_skuRules_SomeOffers);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }
    }
}