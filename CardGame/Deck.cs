using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame
{
    public static class Deck
    {
        #region Fields

        private static readonly List<int> _deck = new List<int>();
        private static readonly Random _random = new Random();

        #endregion Fields

        #region Methods

        #region Public Static Methods

        /// <summary>
        /// Adds cards to winners discard pile after one turn. Also adds cards from previous draw turns if there were any
        /// </summary>
        /// <param name="cards">Cards from last turn</param>
        /// <param name="discardPile">Winner discard pile for last turn</param>
        /// <param name="cardsInGame">Cards from previous draw turns if there were any</param>
        public static void CalculateDiscartPile(int[] cards, List<int> discardPile, List<int> cardsInGame)
        {
            if (cards.Length > 0)
                discardPile.AddRange(cards);

            if (cardsInGame.Count > 0)
                discardPile.AddRange(cardsInGame);
        }

        /// <summary>
        /// Filling the deck with forty cards, shuffle and split them to both players by twenty
        /// </summary>
        /// <param name="player1">out parameter for First player initialization</param>
        /// <param name="player2">out parameter for Second player initialization</param>
        public static void FillDrawPile(out Player player1, out Player player2)
        {
            FillDeck();
            player1 = new Player();
            player2 = new Player();
            player1.DrawPile = _deck.Take(20).ToList();
            player2.DrawPile = _deck.Skip(Math.Max(0, _deck.Count - 20)).ToList();
        }

        /// <summary>
        /// Returning new card from one of the piles, depending of their counts to player turn
        /// </summary>
        /// <param name="player">Player which taking the card</param>
        /// <param name="card">out card parameter</param>
        public static void TakeCard(Player player, out int card)
        {
            if (player.DrawPile.Count > 1)
            {
                card = player.DrawPile.FirstOrDefault();
                player.DrawPile.RemoveAt(0);
            }
            else if (player.DiscardPile.Count > 0)
            {
                card = player.DrawPile.FirstOrDefault();
                player.DrawPile.RemoveAt(0);
                Shuffle(player.DiscardPile);
                player.DrawPile.AddRange(player.DiscardPile);
                player.DiscardPile.Clear();
                card = player.DrawPile.FirstOrDefault();
                player.DrawPile.RemoveAt(0);
            }
            else
            {
                card = 0;
            }
        }

        #endregion Public Static Methods

        #region Private Static Methods

        /// <summary>
        /// Filling the deck with forty elements. Four times with numbers from one to ten and shuffle them all
        /// </summary>
        private static void FillDeck()
        {
            for (int i = 0; i < 4; i++)
            {
                _deck.AddRange(Enumerable.Range(1, 10).ToList());
            }
            Shuffle(_deck);
        }

        /// <summary>
        /// Shuffle list of elements with Fisher-Yates Shuffle Algorithm
        /// </summary>
        /// <param name="pile">list of elements which will be shuffled</param>
        private static void Shuffle(List<int> pile)
        {
            int n = pile.Count;

            for (int i = 0; i < (n - 1); i++)
            {
                int r = _random.Next(n - 1);
                int t = pile[r];
                pile[r] = pile[i];
                pile[i] = t;
            }
        }

        #endregion Private Static Methods

        #endregion Methods
    }
}