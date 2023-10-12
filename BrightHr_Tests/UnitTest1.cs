using BrightHR_Checkout;

namespace BrightHr_Tests
{
    public class Tests
    {
        private Checkout _checkout;
        private List<SkuRule> _skuRules_NoOffers;
        private List<SkuRule> _skuRules_AllOffers;
        private List<SkuRule> _skuRules_SomeOffers;
        private List<SkuRule> _skuRules_LowMultiples;
        private List<SkuRule> _skuRules_HighMultiples;
        private List<SkuRule> _skuRules_ExtendedItems;
        private string _singleItemBasket = "A";
        private string _oneOfEachItemBasket = "ABCD";
        private string _lowMultiplesBasket = "AABCCCD";
        private string _highMultiplesBasket = "AAAAAAAAAAAAAAAAAAAABBBBBBBBBBBBBBBBBBBBBCCCCCCD";
        private string _highMultiplesRandomOrderBasket = "BADDDACAACABBBBBCACACDCCCD";
        private string _invalidItemBasket = "J";
        private string _extendedItemBasket = "ABCDEFGH";

        [SetUp]
        public void Setup()
        {
            _skuRules_NoOffers = new List<SkuRule>
            {
                new SkuRule { Sku = 'A', Price = 50, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'B', Price = 30, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'C', Price = 20, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'D', Price = 15, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
            };

            _skuRules_ExtendedItems = new List<SkuRule>
            {
                new SkuRule { Sku = 'A', Price = 50, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'B', Price = 30, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'C', Price = 20, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'D', Price = 15, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'E', Price = 15, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'F', Price = 15, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'G', Price = 15, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
                new SkuRule { Sku = 'H', Price = 15, HasOfferPrice = false, OfferQuantity = 0, OfferPrice = 0 },
            };

            _skuRules_AllOffers = new List<SkuRule>
            {
                new SkuRule { Sku = 'A', Price = 50, HasOfferPrice = true, OfferQuantity = 3, OfferPrice = 130 },
                new SkuRule { Sku = 'B', Price = 30, HasOfferPrice = true, OfferQuantity = 2, OfferPrice = 55 },
                new SkuRule { Sku = 'C', Price = 20, HasOfferPrice = true, OfferQuantity = 3, OfferPrice = 50 },
                new SkuRule { Sku = 'D', Price = 15, HasOfferPrice = true, OfferQuantity = 5, OfferPrice = 50 },
            };

            _skuRules_SomeOffers = new List<SkuRule>
            {
                new SkuRule { Sku = 'A', Price = 50, HasOfferPrice = true, OfferQuantity = 3, OfferPrice = 130 },
                new SkuRule { Sku = 'B', Price = 30, HasOfferPrice = true, OfferQuantity = 2, OfferPrice = 55 },
                new SkuRule { Sku = 'C', Price = 20, HasOfferPrice = false, OfferQuantity = 3, OfferPrice = 50 },
                new SkuRule { Sku = 'D', Price = 15, HasOfferPrice = false, OfferQuantity = 5, OfferPrice = 50 },
            };

            _skuRules_LowMultiples = new List<SkuRule>
            {
                new SkuRule { Sku = 'A', Price = 50, HasOfferPrice = true, OfferQuantity = 1, OfferPrice = 40 },
                new SkuRule { Sku = 'B', Price = 30, HasOfferPrice = true, OfferQuantity = 1, OfferPrice = 30},
                new SkuRule { Sku = 'C', Price = 20, HasOfferPrice = true, OfferQuantity = 1, OfferPrice = 18 },
                new SkuRule { Sku = 'D', Price = 15, HasOfferPrice = true, OfferQuantity = 1, OfferPrice = 12 },
            };

            _skuRules_HighMultiples = new List<SkuRule>
            {
                new SkuRule { Sku = 'A', Price = 50, HasOfferPrice = true, OfferQuantity = 10, OfferPrice = 450 },
                new SkuRule { Sku = 'B', Price = 30, HasOfferPrice = true, OfferQuantity = 10, OfferPrice = 250},
                new SkuRule { Sku = 'C', Price = 20, HasOfferPrice = true, OfferQuantity = 15, OfferPrice = 280 },
                new SkuRule { Sku = 'D', Price = 15, HasOfferPrice = true, OfferQuantity = 15, OfferPrice = 180 },
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

        [Test]
        public void IsValidPrice_OneOfEach_NoOffers()
        {
            var basket = _oneOfEachItemBasket;
            var expectedBasketTotal = 115;
            _checkout = new Checkout(_skuRules_NoOffers);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }

        [Test]
        public void IsValidPrice_LowMultiplesBasket_NoOffers()
        {
            var basket = _lowMultiplesBasket;
            var expectedBasketTotal = 205;
            _checkout = new Checkout(_skuRules_NoOffers);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }

        [Test]
        public void IsValidPrice_HighMultiplesBasket_NoOffers()
        {
            var basket = _highMultiplesBasket;
            var expectedBasketTotal = 1765;
            _checkout = new Checkout(_skuRules_NoOffers);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }

        [Test]
        public void IsValidPrice_HighMultiplesBasket_AllOffers()
        {
            var basket = _highMultiplesBasket;
            var expectedBasketTotal = 1575;
            _checkout = new Checkout(_skuRules_AllOffers);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }

        [Test]
        public void IsValidPrice_HighMultiplesBasket_LowMultipleOffers()
        {
            var basket = _highMultiplesBasket;
            var expectedBasketTotal = 1553;
            _checkout = new Checkout(_skuRules_LowMultiples);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }

        [Test]
        public void IsValidPrice_HighMultiplesRandomOrderBasket_HiMultipleOffers()
        {
            var basket = _highMultiplesRandomOrderBasket;
            var expectedBasketTotal = 765;
            _checkout = new Checkout(_skuRules_HighMultiples);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }

        [Test]
        public void IsValidPrice_HighMultiplesRandomOrderBasket_NoOffers()
        {
            var basket = _highMultiplesRandomOrderBasket;
            var expectedBasketTotal = 765;
            _checkout = new Checkout(_skuRules_NoOffers);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }

        [Test]
        public void IsValidPrice_HighMultiplesRandomOrderBasket_LowMultipleOffers()
        {
            var basket = _highMultiplesRandomOrderBasket;
            var expectedBasketTotal = 664;
            _checkout = new Checkout(_skuRules_LowMultiples);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }

        [Test]
        public void PriceCheckThowsError_InvalidItemBasket_LowMultipleOffers()
        {
            var basket = _invalidItemBasket;
            var expectedBasketTotal = 0;
            _checkout = new Checkout(_skuRules_LowMultiples);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            Assert.Throws<Exception>(()=> _checkout.GetTotalPrice());
        }

        [Test]
        public void IsValidPrice_ExtendedItemsBasket_NoOffers()
        {
            var basket = _extendedItemBasket;
            var expectedBasketTotal = 175;
            _checkout = new Checkout(_skuRules_ExtendedItems);
            foreach (var item in basket.ToCharArray())
            {
                _checkout.Scan(item.ToString());
            }

            var actualBasketTotal = _checkout.GetTotalPrice();
            Assert.AreEqual(expectedBasketTotal, actualBasketTotal);
        }
    }
}