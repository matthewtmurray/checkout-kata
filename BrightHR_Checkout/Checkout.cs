using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightHR_Checkout
{
    public class Checkout : ICheckout
    {
        private List<SkuRule> _skuRules;
        private string basketItems = string.Empty;
        public Checkout(List<SkuRule> skuRules)
        {
            _skuRules = skuRules;
        }
        public void Scan(string item)
        {
            basketItems = basketItems + item;
        }

        public int GetTotalPrice()
        {
            var totalPrice = 0;
            totalPrice += _skuRules.Where(r => r.Sku == basketItems[0]).Select(r => r.Price).FirstOrDefault();
            return totalPrice;
        }
    }
}
