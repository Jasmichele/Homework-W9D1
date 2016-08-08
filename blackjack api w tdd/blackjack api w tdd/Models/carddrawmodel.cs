using blackjack_api_w_tdd.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blackjack_api_w_tdd.Models
{
    public class carddrawmodel
    {
        public bool success { get; set; }
        public List<card> cards { get; set; }
        public string deck_id { get; set; }
        public int remaining { get; set; }
    }
}