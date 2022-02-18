using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.ViewModel
{
    public class PayLoadVM
    {
        public string idx { get; set; }
        public decimal amount { get; set; }
        public string mobile { get; set; }
        public string product_identity { get; set; }
        public string product_name { get; set; }
        public string product_url { get; set; }
        public string token { get; set; }
    }
    public class PayloadPostVM
    {
        public decimal amount { get; set; }
        public string token { get; set; }
    }
    public class KhaltiResponseVM
    {
        public string idx { get; set; }
        public KhaltiResponseTypeVM type { get; set; }
        public KhaltiResponseStateVM state { get; set; }
        public decimal amount { get; set; }
        public decimal fee_amount { get; set; }
        public bool refunded { get; set; }
        public string created_on { get; set; }
        public string eBanker { get; set; }
        public KhaltiResponseUserVM user { get; set; }
        public KhaltiResponseMerchantVM merchant { get; set; }
        public string token { get; set; }
        public string status_code { get; set; }
    }

    public class KhaltiResponseMerchantVM
    {
        public string idx { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
    }

    public class KhaltiResponseUserVM
    {
        public string idx { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
    }

    public class KhaltiResponseStateVM
    {
        public string idx { get; set; }
        public string name { get; set; }
        public string template { get; set; }

    }

    public class KhaltiResponseTypeVM
    {
        public string idx { get; set; }
        public string name { get; set; }

    }
}
