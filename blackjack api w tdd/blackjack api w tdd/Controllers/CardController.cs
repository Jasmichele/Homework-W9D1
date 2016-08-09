using blackjack_api_w_tdd.Models;
//using blackjack_api_w_tdd.Scripts;
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

        public async Task<ActionResult> PlayGame()
        {
            CardsViewModel game = new Models.CardsViewModel();
            game.deckId = Request.QueryString["deck_id"];
            game.dealer = new person();
            game.player = new person();
            game.dealer.hand = new List<card>();
            game.player.hand = new List<card>();

            for (int i = 0; i < 2; i++)
            {
                game.dealer.hand.Add(await GetACard(game.deckId));
                game.player.hand.Add(await GetACard(game.deckId));
            }

            Session["DealerHand"] = game;
            return View(game);

        }

        public async Task<card> GetACard(string deckID)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + deckID + "/draw/?count=1");

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var drawnCard = JsonConvert.DeserializeObject<carddrawmodel>(responseData);

                if (drawnCard.remaining == 0)
                    RedirectToAction("Index");

                return drawnCard.cards[0];
            }
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> GetACard()
        {

        }

    }
}