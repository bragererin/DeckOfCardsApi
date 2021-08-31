using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckOfCardsApi.Contract
{
    public class Draw
    {
        public bool Success { get; set; }
        public List<Card> Cards { get; set; }
        public string DeckId { get; set; }
        public int RemainingCards { get; set; }
    }
}
