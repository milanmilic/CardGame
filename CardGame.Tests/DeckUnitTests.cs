using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Tests
{
    [TestClass]
    public class DeckUnitTests
    {
        #region Methods

        #region Public Methods

        [TestMethod]
        public void AfterDrawNextRoundWinnerShouldWinFourCards()
        {
            // Arrange and Act
            var cardsInGame = new List<int>();
            Deck.FillDrawPile(out Player player1, out Player player2);
            Deck.TakeCard(player1, out int card1);
            Deck.TakeCard(player2, out int card2);
            card1 = 10;
            card2 = 10;
            cardsInGame.AddRange(new List<int> { card1, card2 });
            var player1DrawPile = player1.DrawPile;
            var player2DrawPile = player2.DrawPile;
            var player1DiscardPile = player1.DiscardPile;
            var player2DiscardPile = player2.DiscardPile;

            // Assert
            Assert.IsTrue(card1 == card2);
            Assert.AreEqual(19, player1DrawPile.Count);
            Assert.AreEqual(19, player2DrawPile.Count);
            Assert.AreEqual(0, player1DiscardPile.Count);
            Assert.AreEqual(0, player2DiscardPile.Count);
            Assert.AreEqual(2, cardsInGame.Count);

            // Arrange and Act
            Deck.TakeCard(player1, out int card3);
            Deck.TakeCard(player2, out int card4);
            card3 = 8;
            card4 = 4;
            Deck.CalculateDiscartPile(new int[] { card3, card4 }, player1.DiscardPile, cardsInGame);
            cardsInGame.Clear();

            // Assert
            Assert.IsTrue(card3 > card4);
            Assert.AreEqual(18, player1DrawPile.Count);
            Assert.AreEqual(18, player2DrawPile.Count);
            Assert.AreEqual(4, player1DiscardPile.Count);
            Assert.AreEqual(0, player2DiscardPile.Count);
            Assert.AreEqual(0, cardsInGame.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CalculateDiscartPileShouldReturnNullReferenceExceptionWithNullCardsInGame()
        {
            // Arrange
            Deck.FillDrawPile(out Player player1, out Player player2);

            Deck.TakeCard(player1, out int card1);
            Deck.TakeCard(player2, out int card2);

            // Act
            Deck.CalculateDiscartPile(new int[] { card1, card2 }, player1.DiscardPile, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CalculateDiscartPileShouldReturnNullReferenceExceptionWithNullDiscardPile()
        {
            // Arrange
            var cardsInGame = new List<int>();
            Deck.FillDrawPile(out Player player1, out Player player2);

            Deck.TakeCard(player1, out int card1);
            Deck.TakeCard(player2, out int card2);

            // Act
            Deck.CalculateDiscartPile(new int[] { card1, card2 }, null, cardsInGame);
        }

        [TestMethod]
        public void DeckShouldContainFortyCardsAndSouldBeShuffled()
        {
            // Arrange and Act
            Deck.FillDrawPile(out Player player1, out Player player2);
            var drawPilePlayer1 = player1.DrawPile;
            var drawPilePlayer2 = player2.DrawPile;
            var fullDeckCardNumber = drawPilePlayer1.Count + drawPilePlayer2.Count;

            // Assert
            Assert.AreEqual(40, fullDeckCardNumber);
            Assert.AreNotEqual(drawPilePlayer1, drawPilePlayer1.OrderBy(x => x).ToList());
            Assert.AreNotEqual(drawPilePlayer2, drawPilePlayer2.OrderBy(x => x).ToList());
        }

        [TestMethod]
        public void DrawPileShouldBeRefilledAfterEmpty()
        {
            // Arrange
            Deck.FillDrawPile(out Player player1, out Player player2);

            // Act And Assert
            player1.DiscardPile.AddRange(player1.DrawPile);
            player1.DrawPile.Clear();
            player1.DrawPile.Add(10);

            Assert.AreEqual(1, player1.DrawPile.Count);
            Assert.AreEqual(20, player1.DiscardPile.Count);

            Deck.TakeCard(player1, out int card1);
            Deck.TakeCard(player2, out int card2);

            Assert.AreEqual(19, player1.DrawPile.Count);
            Assert.AreEqual(0, player1.DiscardPile.Count);
            Assert.AreEqual(19, player2.DrawPile.Count);
            Assert.AreEqual(0, player2.DiscardPile.Count);
        }

        [TestMethod]
        public void EmptyDrawPileShouldReturnZeroCard()
        {
            // Arrange
            Deck.FillDrawPile(out Player player1, out Player player2);
            player1.DrawPile.Clear();

            // Act
            Deck.TakeCard(player1, out int card1);

            // Assert
            Assert.AreEqual(0, card1);
        }

        [TestMethod]
        public void HigherCardShouldWinTheRound()
        {
            // Arrange
            var cardsInGame = new List<int>();
            Deck.FillDrawPile(out Player player1, out Player player2);

            Deck.TakeCard(player1, out int card1);
            Deck.TakeCard(player2, out int card2);

            // Act and Assert
            if (card1 > card2)
            {
                Deck.CalculateDiscartPile(new int[] { card1, card2 }, player1.DiscardPile, cardsInGame);
                Assert.IsTrue(card1 > card2);
                Assert.AreEqual(2, player1.DiscardPile.Count);
            }
            else if (card1 < card2)
            {
                Deck.CalculateDiscartPile(new int[] { card1, card2 }, player2.DiscardPile, cardsInGame);
                Assert.IsTrue(card1 < card2);
                Assert.AreEqual(2, player2.DiscardPile.Count);
            }
            else
            {
                cardsInGame.AddRange(new List<int> { card1, card2 });
                Assert.IsTrue(card1 == card2);
                Assert.AreEqual(0, player1.DiscardPile.Count);
                Assert.AreEqual(0, player2.DiscardPile.Count);
            }
        }

        [TestMethod]
        public void PlayersShouldHaveTwentyCardsFromDeckOnDrawPile()
        {
            // Arrange and Act
            Deck.FillDrawPile(out Player player1, out Player player2);
            var drawPilePlayer1 = player1.DrawPile.Count;
            var drawPilePlayer2 = player2.DrawPile.Count;

            // Assert
            Assert.AreEqual(20, drawPilePlayer1);
            Assert.AreEqual(20, drawPilePlayer2);
        }

        #endregion Public Methods

        #endregion Methods
    }
}