using DeckOfCardsApi.BusinessLogic;
using DeckOfCardsApi.Client.DeckOfCardsApi;
using DeckOfCardsApi.Contract;
using FluentAssertions;

using Microsoft.Extensions.Logging.Abstractions;

using NSubstitute;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnitTest.AutomatedTesting;

namespace BusinessLogicUnitTest
{
    [TestFixture]
    public class Tests
    {
        private BusinessLogic logic;
        private IDeckOfCardsApiClient client;
        Browser browser;
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            browser = new Browser();
            client = Substitute.For<IDeckOfCardsApiClient>();
            logic = new BusinessLogic(new NullLogger<BusinessLogic>(), client);
        }

        [Test]
        public void StatusCodeTest_For_New_Deck_Endpoint_using_RestSharp()
        {
            //arrange
            RestClient client = new RestClient("https://deckofcardsapi.com/api/deck/new/");
            RestRequest request = new RestRequest(string.Empty, Method.GET);

            //act
            IRestResponse response = client.Execute(request);

            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));           
        }

        [Test]
        public void StatusCodeTest_For_New_Deck_Jokers_Enabled_Endpoint_using_RestSharp()
        {
            //arrange
            RestClient client = new RestClient("https://deckofcardsapi.com/api/deck/new/?jokersEnabled=true");
            RestRequest request = new RestRequest(string.Empty, Method.GET);

            //act
            IRestResponse response = client.Execute(request);

            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void StatusCodeTest_For_Draw_New_Card_Endpoint_using_RestSharp()
        {
            //arrange
            RestClient client = new RestClient("https://deckofcardsapi.com/api/deck/new/draw/?count=1");
            RestRequest request = new RestRequest(string.Empty, Method.GET);

            //act
            IRestResponse response = client.Execute(request);

            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetNewAsync_Should_Return_New_Deck_using_RestSharp()
        {
            //arrange
            RestClient client = new RestClient("https://deckofcardsapi.com/api/deck/new/");
            RestRequest request = new RestRequest(string.Empty, Method.GET);

            //act
            IRestResponse response = client.Execute(request);
            var newDeckResponse = new JsonDeserializer().Deserialize<Deck>(response);

            //assert
            Assert.IsNotNull(newDeckResponse);
            Assert.That(newDeckResponse.Success, Is.True);
        }

        [Test]
        public void JokerEnabled_GetNewAsync_Should_Return_New_Deck_using_RestSharp()
        {
            //arrange
            RestClient client = new RestClient("https://deckofcardsapi.com/api/deck/new/?jokersEnabled=true");
            RestRequest request = new RestRequest(string.Empty, Method.GET);

            //act
            IRestResponse response = client.Execute(request);
            var newDeckResponse = new JsonDeserializer().Deserialize<Deck>(response);

            //assert
            Assert.IsNotNull(newDeckResponse);
            Assert.That(newDeckResponse.Success, Is.True);
        }

        [Test]
        public void GetNewAsync_Should_Return_A_Different_Deck_each_Call_using_RestSharp()
        {
            //arrange
            RestClient firstClient = new RestClient("https://deckofcardsapi.com/api/deck/new/");
            RestClient secondClient = new RestClient("https://deckofcardsapi.com/api/deck/new/");
            RestRequest request = new RestRequest(string.Empty, Method.GET);
            
            IRestResponse response = firstClient.Execute(request);
            
            //act
            var firstDeckResponse = new JsonDeserializer().Deserialize<Deck>(response);            
            var secondDeckResponse = new JsonDeserializer().Deserialize<Deck>(secondClient.Execute(request));

            //assert
            Assert.AreNotEqual(firstDeckResponse.DeckId, secondDeckResponse.DeckId);
        }

        [Test]
        public void JokersEnabled_GetNewAsync_Should_Return_A_Different_Deck_each_Call_using_RestSharp()
        {
            //arrange
            RestClient firstClient = new RestClient("https://deckofcardsapi.com/api/deck/new/?jokersEnabled=true");
            RestClient secondClient = new RestClient("https://deckofcardsapi.com/api/deck/new/?jokersEnabled=true");
            RestRequest request = new RestRequest(string.Empty, Method.GET);

            IRestResponse response = firstClient.Execute(request);

            //act
            var firstDeckResponse = new JsonDeserializer().Deserialize<Deck>(response);
            var secondDeckResponse = new JsonDeserializer().Deserialize<Deck>(secondClient.Execute(request));

            //assert
            Assert.AreNotEqual(firstDeckResponse.DeckId, secondDeckResponse.DeckId);
        }

        [Test]
        public void Automated_testing_of_New_Deck_JokersEnabled_Draw_Procedure_using_Selenium()
        {
            //arrange
            browser.Init_Browser();
            string deckId = string.Empty;
            int numberOfCardsDrawn = 2;
            int secondDraw = 3;
            int deckSize = 0;
            int remaining = 0;
            string[] seperators = { ": ", ", ", "remaining" };

            //act
            //Get new deck.
            browser.Goto("https://deckofcardsapi.com/api/deck/new/?jokersEnabled=true");            
            driver = browser.getDriver;
            System.Threading.Thread.Sleep(1000);

            //Retrieve deckId.  
            
            string [] pageSource = driver.PageSource.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            deckId = String.Concat(Array.FindAll(pageSource[5].ToCharArray(), Char.IsLetterOrDigit));
            deckSize = Convert.ToInt32(String.Concat(Array.FindAll(pageSource[8].ToCharArray(), Char.IsDigit)));

            //Draw cards.
            browser.Goto($"https://deckofcardsapi.com/api/deck/{deckId}/draw/?count={numberOfCardsDrawn}");
            System.Threading.Thread.Sleep(1000);
            
            //Draw cards again.
            browser.Goto($"https://deckofcardsapi.com/api/deck/{deckId}/draw/?count={secondDraw}");
            driver = browser.getDriver;
            pageSource = driver.PageSource.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            remaining = Convert.ToInt32(String.Concat(Array.FindAll(pageSource[48].ToCharArray(), Char.IsDigit)));

            //assert
            Assert.That(remaining, Is.EqualTo(deckSize - (numberOfCardsDrawn + secondDraw)));
        }
    }
}