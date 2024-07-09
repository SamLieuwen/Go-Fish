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
        public static bool runGame;

        static void Main(string[] args)
        {
            GoFish game = new GoFish();
            Random rnd = new Random();
            string guess;
            int coin;
            bool firstTurn = false;

            deck = new List<Card>();
            pH = new List<Card>();
            dH = new List<Card>();
            pS = 0;
            dS = 0;
            runGame = true;

            GoFish.createDeck();

            for (int i = 0; i < 7; i++)
            {
                pH.Add(deck[0]);
                deck.RemoveAt(0);
                dH.Add(deck[0]);
                deck.RemoveAt(0);
            }

            Console.Clear();
            Console.WriteLine("Welcome to Go Fish!");

            while (true)
            {
                Console.WriteLine("Heads or Tails: ");
                guess = Console.ReadLine().ToLower();
                coin = rnd.Next(1, 3);

                if (guess != "heads" && guess != "tails")
                {
                    Console.WriteLine("Invalid Response");
                }
                else { break; }
            }

            if (guess == "heads" && coin == 1 || guess == "tails" && coin == 2)
            {
                firstTurn = true;
                Console.WriteLine("\nYou go first\nPress any key to continue");
            }
            else { Console.WriteLine("\nComputer goes first\nPress any key to continue"); }
            Console.ReadKey();

            if (firstTurn == true)
            {
                while (runGame)
                {
                    Console.Clear();
                    Console.WriteLine("Your Hand: ");
                    foreach (Card card in pH)
                    {
                        Console.Write(card.card + " ");
                    }
                }
            }
            else
            {
                while (runGame)
                {
                    Console.Clear();
                    Console.WriteLine("Your Hand: ");
                    foreach (Card card in pH)
                    {
                        Console.Write(card.card + " ");
                    }
                }
            }
            
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
