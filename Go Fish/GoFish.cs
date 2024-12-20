﻿using GoFish;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoFish
{
    internal class GoFish
    {
        public static int playerScore;
        public static int dealerScore;
        public static bool runGame;

        static void Main(string[] args)
        {
            GoFish game = new GoFish();
            Random rnd = new Random();
            string guess;
            int coin;
            bool firstTurn = false;

            Decks.referenceDeck = new List<Card>();
            Decks.deck = new List<Card>();
            Decks.playerHand = new List<Card>();
            Decks.dealerHand = new List<Card>();
            playerScore = 0;
            dealerScore = 0;
            runGame = true;

            Decks.ReferenceDeck();
            Decks.CreateDeck();

            for (int i = 0; i < 7; i++)
            {
                Decks.playerHand.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);
                Decks.dealerHand.Add(Decks.deck[0]);
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

            playerScore = Decks.Pairs(Decks.playerHand, playerScore);
            dealerScore = Decks.Pairs(Decks.dealerHand, dealerScore);

            if (firstTurn == true)
            {
                while (runGame)
                {
                    DisplayCards();

                    if (Decks.playerHand.Count() > 0)
                    {
                        Actions.PlayerActions();
                        playerScore = Decks.Pairs(Decks.playerHand, playerScore);
                    }
                    if (Decks.playerHand.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.playerHand);
                        Decks.Pairs(Decks.playerHand, playerScore);
                    }

                    DisplayCards();

                    if (Decks.dealerHand.Count() > 0)
                    {
                        Actions.DealerActions();
                        dealerScore = Decks.Pairs(Decks.dealerHand, dealerScore);
                    }
                    if (Decks.dealerHand.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.dealerHand);
                        Decks.Pairs(Decks.dealerHand, dealerScore);
                    }

                    if (Decks.deck.Count() == 0 && Decks.playerHand.Count() == 0 && Decks.dealerHand.Count() == 0)
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
                    DisplayCards();

                    if (Decks.dealerHand.Count() > 0)
                    {
                        Actions.DealerActions();
                        dealerScore = Decks.Pairs(Decks.dealerHand, dealerScore);
                    }
                    if (Decks.dealerHand.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.dealerHand);
                        Decks.Pairs(Decks.dealerHand, dealerScore);
                    }

                    DisplayCards();

                    if (Decks.playerHand.Count() > 0)
                    {
                        Actions.PlayerActions();
                        playerScore = Decks.Pairs(Decks.playerHand, playerScore);
                    }
                    if (Decks.playerHand.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.playerHand);
                        Decks.Pairs(Decks.playerHand, playerScore);
                    }

                    if (Decks.deck.Count() == 0 && Decks.playerHand.Count() == 0 && Decks.dealerHand.Count() == 0)
                    {
                        runGame = false;
                        Results();
                    }
                }
            }
        }

        public static void DisplayCards()
        {
            Console.Clear();
            Console.Write("Cards left in deck: " + Decks.deck.Count());
            Console.Write("\nYour Pairs: " + playerScore + "\nYour Hand: ");
            foreach (Card card in Decks.playerHand)
            {
                Console.Write(card.card + " ");
            }
            Console.WriteLine();
        }

        public static void Results()
        {
            Console.Clear();
            Console.WriteLine("Your Pairs: " + playerScore + "\nComputer's Pairs: " + dealerScore);

            if (playerScore > dealerScore) { Console.WriteLine("You Win!\nWould you like to play again?"); }
            else if (playerScore < dealerScore) { Console.WriteLine("You Lose!\nWould you like to play again?"); }
            else if (playerScore == dealerScore) { Console.WriteLine("Tie\nWould you like to play again?"); }

            if ("yes" == Console.ReadLine().ToLower())
            {
                Console.Clear();
                Main(null);
            }
        }
    }
}


