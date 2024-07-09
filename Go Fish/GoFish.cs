using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFish
{
    internal class GoFish
    {
        public static List<Card> deck;
        public static List<Card> pH;
        public static List<Card> dH;
        public static int pS;
        public static int dS;

        static void Main(string[] args)
        {
            GoFish game = new GoFish();

            deck = new List<Card>();
            pH = new List<Card>();
            dH = new List<Card>();
            pS = 0;
            dS = 0;

            GoFish.createDeck();
        }

        public static void createDeck()
        {
            for (int i = 0; i < 4; i++)
            {
                deck.Add(new Card("Ace"));
                deck.Add(new Card("Two"));
                deck.Add(new Card("Three"));
                deck.Add(new Card("Four"));
                deck.Add(new Card("Five"));
                deck.Add(new Card("Six"));
                deck.Add(new Card("Seven"));
                deck.Add(new Card("Eight"));
                deck.Add(new Card("Nine"));
                deck.Add(new Card("Ten"));
                deck.Add(new Card("Jack"));
                deck.Add(new Card("Queen"));
                deck.Add(new Card("King"));
            }

            var shuffled = new List<Card>();
            var rand = new Random();

            while (deck.Count != 0)
            {
                var i = rand.Next(deck.Count);
                shuffled.Add(deck[i]);
                deck.RemoveAt(i);
            }
            deck = shuffled;
        }
    }
}
