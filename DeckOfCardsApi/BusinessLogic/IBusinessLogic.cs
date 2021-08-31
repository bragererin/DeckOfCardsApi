using DeckOfCardsApi.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeckOfCardsApi.BusinessLogic
{
    public interface IBusinessLogic
    {
        /// <summary>
        /// Retrieves a new deck of cards.
        /// </summary>
        /// <returns></returns>
        Task<Deck> GetNewAsync();

        /// <summary>
        /// Retrieves a new deck of cards.
        /// </summary>
        /// <returns></returns>
        Task<Deck> GetNewAsync(bool enableJokers);

        /// <summary>
        /// Retrieves a new card or cards from the deck.
        /// </summary>
        /// <returns></returns>
        Task<Draw> DrawNewCardAsync(int count);

        /// <summary>
        /// Retrieves a new card or cards from a specific deck.
        /// </summary>
        /// <returns></returns>
        Task<Draw> DrawNewCardAsync(string deckId, int count);
    }
}
