﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFish
{
    internal class GoFish
    {
        public static List<Card> referenceDeck;
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
            bool firstTurn = true;

            referenceDeck = new List<Card>();
            deck = new List<Card>();
            pH = new List<Card>();
            dH = new List<Card>();
            pS = 0;
            dS = 0;
            runGame = true;

            GoFish.ReferenceDeck();
            GoFish.CreateDeck();

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

            pS = Pairs(pH, pS);
            dS = Pairs(dH, dS);

            if (firstTurn == true)
            {
                while (runGame)
                {
                    DisplayCards();

                    if (pH.Count() > 0)
                    {
                        Actions();
                        pS = Pairs(pH, pS);
                    }
                    if (pH.Count() == 0)
                    {
                        NoCardsInHand(pH);
                        Pairs(pH, pS);
                    } 

                    DisplayCards();

                    if (dH.Count() > 0)
                    {
                        DealerActions();
                        dS = Pairs(dH, dS);
                    }
                    if (dH.Count() == 0)
                    {
                        NoCardsInHand(dH);
                        Pairs(dH, dS);
                    }

                    if (deck.Count() == 0 && pH.Count() == 0 && dH.Count() == 0)
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
                    Console.Clear();
                    DisplayCards();

                    DealerActions();
                    dS = Pairs(dH, dS);

                    Console.Clear();
                    DisplayCards();

                    Actions();
                    pS = Pairs(pH, pS);

                    if (deck.Count() > 0)
                    {
                        if (pH.Count() == 0)
                        {
                            NoCardsInHand(pH);
                            Pairs(pH, pS);
                        }
                        if (dH.Count() == 0)
                        {
                            NoCardsInHand(dH);
                            Pairs(dH, dS);
                        }
                    }
                    else if (deck.Count() == 0 && pH.Count() == 0 && dH.Count() == 0)
                    {
                        Results();
                    }
                }
            }
        }

        public static void Actions()
        {
            Card temp = new Card(null);
            int dealerCount = dH.Count();
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

                        foreach (Card card in referenceDeck)
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

                foreach (Card card in pH)
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
                    foreach (Card card in dH)
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

                pH.Add(temp);
                dH.Remove(temp);
            }
            else
            {
                Console.WriteLine("Go Fish!\nYou picked up a: " + deck[0].card + "\n\nPress any key to continue");
                Console.ReadKey();

                pH.Add(deck[0]);
                deck.RemoveAt(0);
            }
        }
        public static void DealerActions()
        {
            Random rnd = new Random();
            Card temp = new Card(null);
            int dHCount = dH.Count();
            int index = 0;
            bool inHand = false;
            
            index = rnd.Next(0, dHCount);

            foreach (Card card in pH)
            {
                if (dH[index].card == card.card)
                {
                    inHand = true;
                    temp = card;
                }
            }

            if (inHand == true)
            {
                Console.WriteLine("\nDo you have a " + dH[index].card);
                Console.WriteLine("\nThe computer took your card\nPress any key to continue");

                dH.Add(temp);
                pH.Remove(temp);

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nDo you have a " + dH[index].card);
                Console.WriteLine("Go Fish!\nPress any key to continue");

                dH.Add(deck[0]);
                deck.RemoveAt(0);

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
            if (deck.Count() >= 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    hand.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }
            else
            {
                for (int i = 0; i < deck.Count(); i++)
                {
                    hand.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }
        }
        public static void DisplayCards()
        {            
            Console.Clear();
            Console.Write("Cards left in deck: " + deck.Count());
            Console.Write("\nYour Pairs: " + pS + "\nYour Hand: ");
            foreach (Card card in pH)
            {
                Console.Write(card.card + " ");
            }
            Console.Write("\nComputer Pairs: " + dS + "\nComputer Hand: ");
            foreach (Card card in dH)
            {
                Console.Write(card.card + " ");
            }
            Console.WriteLine();
        }
    }
}
