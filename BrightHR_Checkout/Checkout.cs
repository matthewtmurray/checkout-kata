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
        public Checkout(List<SkuRule> skuRules)
        {
            _skuRules = skuRules;
        }
        public void Scan(string item)
        {
            throw new NotImplementedException();
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
