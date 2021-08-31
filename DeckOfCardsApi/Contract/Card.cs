using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckOfCardsApi.Contract
{
    public class Card
    {
        public byte[] Image { get; set; }
        public string Value { get; set; }
        public string Suit { get; set; }
        public string Code { get; set; }
    }
}
