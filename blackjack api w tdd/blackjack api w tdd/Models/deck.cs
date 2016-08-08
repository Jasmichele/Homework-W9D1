using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blackjack_api_w_tdd.Models
{
    public class deck
    {
        public bool success { get; set; }
        public string deck_id { get; set; }
        public bool shuffled { get; set; }
        public int remaining { get; set; }
    }
}