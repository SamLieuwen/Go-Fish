using GoFish;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFish
{
    internal class GoFish
    {
        public static int pS;
        public static int dS;
        public static bool runGame;

        static void Main(string[] args)
        {
            GoFish game = new GoFish();
            Random rnd = new Random();
            string guess;
            int coin;
            bool firstTurn = true;

            Decks.referenceDeck = new List<Card>();
            Decks.deck = new List<Card>();
            Decks.pH = new List<Card>();
            Decks.dH = new List<Card>();
            pS = 0;
            dS = 0;
            runGame = true;

            Decks.ReferenceDeck();
            Decks.CreateDeck();

            for (int i = 0; i < 7; i++)
            {
                Decks.pH.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);
                Decks.dH.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);
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

            pS = Decks.Pairs(Decks.pH, pS);
            dS = Decks.Pairs(Decks.dH, dS);

            if (firstTurn == true)
            {
                while (runGame)
                {
                    Decks.DisplayCards();

                    if (Decks.pH.Count() > 0)
                    {
                        Actions();
                        pS = Decks.Pairs(Decks.pH, pS);
                    }
                    if (Decks.pH.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.pH);
                        Decks.Pairs(Decks.pH, pS);
                    }

                    Decks.DisplayCards();

                    if (Decks.dH.Count() > 0)
                    {
                        DealerActions();
                        dS = Decks.Pairs(Decks.dH, dS);
                    }
                    if (Decks.dH.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.dH);
                        Decks.Pairs(Decks.dH, dS);
                    }

                    if (Decks.deck.Count() == 0 && Decks.pH.Count() == 0 && Decks.dH.Count() == 0)
                    {
                        runGame = false;
                        Results();
                    }
                }
            }
            else
            {
                while (runGame)
                {
                    Decks.DisplayCards();

                    if (Decks.dH.Count() > 0)
                    {
                        DealerActions();
                        dS = Decks.Pairs(Decks.dH, dS);
                    }
                    if (Decks.dH.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.dH);
                        Decks.Pairs(Decks.dH, dS);
                    }

                    Decks.DisplayCards();

                    if (Decks.pH.Count() > 0)
                    {
                        Actions();
                        pS = Decks.Pairs(Decks.pH, pS);
                    }
                    if (Decks.pH.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.pH);
                        Decks.Pairs(Decks.pH, pS);
                    }

                    if (Decks.deck.Count() == 0 && Decks.pH.Count() == 0 && Decks.dH.Count() == 0)
                    {
                        runGame = false;
                        Results();
                    }
                }
            }
        }

        public static void Actions()
        {
            Card temp = new Card(null);
            int dealerCount = Decks.dH.Count();
            bool action = true;
            bool error = true;
            bool check = false;
            bool inHand = false;
            bool inDHand = false;

            while (action)
            {
                while (error)
                {
                    try
                    {
                        Console.WriteLine("\nWhich card would you like to ask for?");
                        temp.card = Console.ReadLine().ToLower();
                        temp.card = char.ToUpper(temp.card[0]) + temp.card.Substring(1);

                        foreach (Card card in Decks.referenceDeck)
                        {
                            if (card.card == temp.card)
                            {
                                check = true;
                                error = false;
                                break;
                            }
                        }
                        if (check == false)
                        {
                            Console.WriteLine("Invalid Response");
                        }
                    }
                    catch (Exception) { Console.WriteLine("Invalid Response"); }
                }

                foreach (Card card in Decks.pH)
                {
                    if (card.card == temp.card)
                    {
                        temp = card;
                        inHand = true;
                        break;
                    }
                }

                if (inHand == true)
                {
                    foreach (Card card in Decks.dH)
                    {
                        if (card.card == temp.card)
                        {
                            action = false;
                            inDHand = true;
                            temp = card;
                            break;
                        }
                    }
                    action = false;
                }
                else { Console.WriteLine("\nYour hand doesn't contain a " + temp.card); error = true; }
            }
            if (inDHand == true)
            {
                Console.WriteLine("\nYou took the computer's card\nPress any key to continue");
                Console.ReadKey();

                Decks.pH.Add(temp);
                Decks.dH.Remove(temp);
            }
            else
            {
                Console.WriteLine("Go Fish!\nYou picked up a: " + Decks.deck[0].card + "\n\nPress any key to continue");
                Console.ReadKey();

                Decks.pH.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);
            }
        }
        public static void DealerActions()
        {
            Random rnd = new Random();
            Card temp = new Card(null);
            int dHCount = Decks.dH.Count();
            int index = 0;
            bool inHand = false;

            index = rnd.Next(0, dHCount);

            foreach (Card card in Decks.pH)
            {
                if (Decks.dH[index].card == card.card)
                {
                    inHand = true;
                    temp = card;
                }
            }

            if (inHand == true)
            {
                Console.WriteLine("\nDo you have a " + Decks.dH[index].card);
                Console.WriteLine("\nThe computer took your card\nPress any key to continue");

                Decks.dH.Add(temp);
                Decks.pH.Remove(temp);

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nDo you have a " + Decks.dH[index].card);
                Console.WriteLine("Go Fish!\nPress any key to continue");

                Decks.dH.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);

                Console.ReadKey();
            }
        }

        public static void Results()
        {
            Console.Clear();
            Console.WriteLine("Your Pairs: " + pS + "\nComputer's Pairs: " + dS);

            if (pS > dS) { Console.WriteLine("You Win!"); }
            else if (pS < dS) { Console.WriteLine("You Lose!"); }
            else if (pS == dS) { Console.WriteLine("Tie"); }
        }
    }
}


