using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightHR_Checkout
{
    public class SkuRule
    {
        public char Sku { get; set; }
        public int Price { get; set; }
        public bool HasOfferPrice { get; set; }
        public int OfferQuantity { get; set; }
        public int OfferPrice { get; set; }
    }
}
