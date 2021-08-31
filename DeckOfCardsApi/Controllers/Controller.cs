using DeckOfCardsApi.BusinessLogic;
using DeckOfCardsApi.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckOfCardsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class Controller: ControllerBase
    {
        private readonly ILogger<Controller> _logger;
        private readonly IBusinessLogic _businessLogic;

        public Controller(IBusinessLogic bl, ILogger<Controller> logger)
        {
            _businessLogic = bl;
            _logger = logger;
        }

        /// <summary>
        ///     Returns a new deck of cards.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        [HttpGet("/deck/new/")]
        public async Task<Deck> GetNewDeckAsync()
        {
            return await _businessLogic.GetNewAsync(false);
        }

        /// <summary>
        ///     Returns a new deck of cards.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        [HttpGet("/deck/new/")]
        public async Task<Deck> GetNewDeckAsync(bool enableJokers)
        {            
            return await _businessLogic.GetNewAsync(enableJokers);
        }


        /// <summary>
        ///     Returns a new card from a new deck.
        /// </summary>
        /// <param name = "count" ></ param >
        /// < returns ></ returns >
        /// < response code="200">OK</response>
        [HttpGet("/deck/new/draw/")]
        public async Task<Draw> DrawNewCardAsync(int count)
        {
            return await _businessLogic.DrawNewCardAsync(count);
        }

        /// <summary>
        ///     Returns a new card from a specific deck.
        /// </summary>
        /// <param name = "count" ></ param >
        /// < returns ></ returns >
        /// < response code="200">OK</response>
        [HttpGet("/deck/new/draw/")]
        public async Task<Draw> DrawNewCardAsync(string deckId, int count)
        {
            return await _businessLogic.DrawNewCardAsync(count);
        }
    }
}
