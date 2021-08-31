using DeckOfCardsApi.Contract;
using Refit;
using System.Threading.Tasks;

namespace DeckOfCardsApi.Client.DeckOfCardsApi
{
    [Headers("accept: application/json")]
    public interface IDeckOfCardsApiClient
    {
        /// <summary>
        /// Returns a new deck of cards.
        /// </summary>
        /// <returns></returns>
        [Get("/api/deck/new/")]
        Task<Deck> GetNewAsync();

        /// <summary>
        /// Returns a new deck of cards.
        /// </summary>
        /// <returns></returns>
        [Get("/api/deck/new/{endpoint}")]
        Task<Deck> GetNewAsync(string endpoint);

        /// <summary>
        /// Returns a new card from a new deck.
        /// </summary>
        /// <returns></returns>
        [Get("/api/deck/new/draw/{endpoint}")]
        Task<Draw> DrawNewCardAsync(string endpoint);

        /// <summary>
        /// Returns a new card from a new deck.
        /// </summary>
        /// <returns></returns>
        [Get("/api/deck/{deckId}/draw/{endpoint}")]
        Task<Draw> DrawNewCardAsync(string deckId, string endpoint);
    }
}
