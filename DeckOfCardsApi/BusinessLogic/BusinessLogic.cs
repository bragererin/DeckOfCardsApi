using DeckOfCardsApi.Client.DeckOfCardsApi;
using DeckOfCardsApi.Contract;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeckOfCardsApi.BusinessLogic
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger<BusinessLogic> _logger;
        private readonly IDeckOfCardsApiClient _deckOfCardsApiClient;

        public BusinessLogic(ILogger<BusinessLogic> logger, IDeckOfCardsApiClient deckOfCardsApiClient)
        {
            _logger = logger;
            _deckOfCardsApiClient = deckOfCardsApiClient;
        }

        public async Task<Deck> GetNewAsync()
        {
            var deck = await _deckOfCardsApiClient.GetNewAsync();
            return deck;
        }

        public async Task<Deck> GetNewAsync(bool enableJokers)
        {
            string endpoint = $"?jokersEnabled={enableJokers}";
            var deck = await _deckOfCardsApiClient.GetNewAsync(endpoint);
            return deck;
        }

        public async Task<Draw> DrawNewCardAsync(int count)
        {
            string endpoint = $"?count={count}";
            var response = await _deckOfCardsApiClient.DrawNewCardAsync(endpoint);
            return response;
        }

        public async Task<Draw> DrawNewCardAsync(string deckId, int count)
        {
            string endpoint = $"?count={count}";
            var response = await _deckOfCardsApiClient.DrawNewCardAsync(deckId, endpoint);
            return response;
        }
    }
}
