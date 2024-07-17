using GoFish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    internal class Decks
    {
        public static List<Card> referenceDeck;
        public static List<Card> deck;
        public static List<Card> pH;
        public static List<Card> dH;

        public static void CreateDeck()
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
        public static void ReferenceDeck()
        {
            referenceDeck.Add(new Card("Ace"));
            referenceDeck.Add(new Card("Two"));
            referenceDeck.Add(new Card("Three"));
            referenceDeck.Add(new Card("Four"));
            referenceDeck.Add(new Card("Five"));
            referenceDeck.Add(new Card("Six"));
            referenceDeck.Add(new Card("Seven"));
            referenceDeck.Add(new Card("Eight"));
            referenceDeck.Add(new Card("Nine"));
            referenceDeck.Add(new Card("Ten"));
            referenceDeck.Add(new Card("Jack"));
            referenceDeck.Add(new Card("Queen"));
            referenceDeck.Add(new Card("King"));
        }

        public static int Pairs(List<Card> hand, int score)
        {
            for (int c = 0; c < 2; c++)
            {
                for (int i = 0; i < hand.Count() - 1; i++)
                {
                    for (int j = hand.Count() - 1; j > i; j--)
                    {
                        if (hand[i].card == hand[j].card)
                        {
                            hand.RemoveAt(j);
                            hand.RemoveAt(i);
                            score++;
                            j--;
                        }
                    }
                }
            }
            return score;

        }
        public static void NoCardsInHand(List<Card> hand)
        {
            if (Decks.deck.Count() >= 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    hand.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }
            else
            {
                for (int i = 0; i < Decks.deck.Count(); i++)
                {
                    hand.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }
        }
        public static void DisplayCards()
        {
            Console.Clear();
            Console.Write("Cards left in deck: " + Decks.deck.Count());
            Console.Write("\nYour Pairs: " + GoFish.pS + "\nYour Hand: ");
            foreach (Card card in Decks.pH)
            {
                Console.Write(card.card + " ");
            }
            Console.WriteLine();
        }
    }
}
