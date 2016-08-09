using blackjack_api_w_tdd.Models;
using blackjack_api_w_tdd.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace blackjack_api_w_tdd.Controllers
{
    public class CardController : Controller
    {
        HttpClient client = new HttpClient();
        string url = "http://deckofcardsapi.com/api/deck";
        deck currentDeck;

        public CardController()
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/new/shuffle/?deck_count=1");

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                currentDeck = JsonConvert.DeserializeObject<deck>(responseData);
                return View(currentDeck);
            }
            return View("Error");
        }

        //public async Task<ActionResult> DrawACard()
        //{
        //    var deckId = Request.QueryString["deck_id"];

        //    HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + deckId + "/draw/?count=1");

        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var responseData = responseMessage.Content.ReadAsStringAsync().Result;

        //        var viewModel = JsonConvert.DeserializeObject<carddrawmodel>(responseData);

        //        if (viewModel.remaining == 0)
        //            RedirectToAction("Index");

        //        return View(viewModel);
        //    }
        //    return View("Error");
        //}

        public async Task<ActionResult> PlayGame()
        {
            var deckId = Request.QueryString["deck_id"];
            ViewModel game = new ViewModel();
            game.dealer = new person();
            game.player = new person();
            game.dealer.hand = new List<card>();
            game.player.hand = new List<card>();

            for (int i = 0; i < 2; i++)
            {
                game.dealer.hand.Add(GetACard(game.deck_id));
                game.player.hand.Add(GetACard(game.deck_id));
            }

            return View(game);

        }

        public async Task<ActionResult> GetACard(string deck_id)
        {
            var deckId = Request.QueryString["deck_id"];

            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + deckId + "/draw/?count=1");

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var aCard = JsonConvert.DeserializeObject<ViewModel>(responseData);

                if (aCard.remaining == 0)
                    RedirectToAction("Index");

                return View(aCard);
            }
            return View("Error");
        }
    }
}