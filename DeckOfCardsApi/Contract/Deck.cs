using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckOfCardsApi.Contract
{
    public class Deck
    {
        public bool Success { get; set; }

        public string DeckId { get; set; }

        public int Remaining { get; set; }
        public bool Shuffle { get; set; }
    }
}
