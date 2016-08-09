using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blackjack_api_w_tdd.Models
{
    public class ViewModel
    {
        public string deck_id { get; set; }
        public person dealer { get; set; }
        public person player { get; set; }
        public int remaining { get; set; }
    }
}