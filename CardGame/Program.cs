using System;
using System.Collections.Generic;

namespace CardGame
{
    public static class Program
    {
        #region Methods

        #region Public Static Methods

        public static void Main(string[] args)
        {
            var cardsInGame = new List<int>();

            Deck.FillDrawPile(out Player player1, out Player player2);

            do
            {
                Deck.TakeCard(player1, out int cardPlayer1);
                Deck.TakeCard(player2, out int cardPlayer2);

                if (cardPlayer1 == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Player 2 wins the game!");
                    break;
                }
                else if (cardPlayer2 == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Player 1 wins the game!");
                    break;
                }

                Console.WriteLine();
                Console.WriteLine($"Player 1 ({player1.DrawPile.Count + player1.DiscardPile.Count} cards): {cardPlayer1}");
                Console.WriteLine($"Player 2 ({player2.DrawPile.Count + player2.DiscardPile.Count} cards): {cardPlayer2}");

                if (cardPlayer1 > cardPlayer2)
                {
                    Deck.CalculateDiscartPile(new int[] { cardPlayer1, cardPlayer2 }, player1.DiscardPile, cardsInGame);
                    cardsInGame.Clear();
                    Console.WriteLine("Player 1 wins this round");
                }
                else if (cardPlayer1 < cardPlayer2)
                {
                    Deck.CalculateDiscartPile(new int[] { cardPlayer1, cardPlayer2 }, player2.DiscardPile, cardsInGame);
                    cardsInGame.Clear();
                    Console.WriteLine("Player 2 wins this round");
                }
                else
                {
                    //adding cards in cardsInGame if turn is draw
                    cardsInGame.AddRange(new List<int> { cardPlayer1, cardPlayer2 });
                    Console.WriteLine("No winner in this round");
                }
            } while (player1.DrawPile.Count > 0 || player2.DrawPile.Count > 0);
        }

        #endregion Public Static Methods

        #endregion Methods
    }
}