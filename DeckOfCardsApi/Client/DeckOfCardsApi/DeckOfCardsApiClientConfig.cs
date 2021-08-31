using System;

namespace DeckOfCardsApi.Client.DeckOfCardsApi
{
    public class DeckOfCardsApiClientConfig
    {
        public const string DeckOfCardsApiClient = "DeckOfCardsApi";

        public Uri BaseUri { get; internal set; }
    }
}
