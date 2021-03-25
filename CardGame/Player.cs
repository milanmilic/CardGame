using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    public class Player
    {
        #region Properties

        //DiscardPile property for player
        public List<int> DiscardPile { get; set; } = new List<int>();

        //DrawPile property for player
        public List<int> DrawPile { get; set; } = new List<int>();

        #endregion Properties
    }
}