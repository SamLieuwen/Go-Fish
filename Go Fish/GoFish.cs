using GoFish;
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
            bool firstTurn = true;

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
                Console.WriteLine("HeadealerScore or Tails: ");
                guess = Console.ReadLine().ToLower();
                coin = rnd.Next(1, 3);

                if (guess != "headealerScore" && guess != "tails")
                {
                    Console.WriteLine("Invalid Response");
                }
                else { break; }
            }
            if (guess == "headealerScore" && coin == 1 || guess == "tails" && coin == 2)
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
                    Decks.DisplayCards();

                    if (Decks.playerHand.Count() > 0)
                    {
                        Actions();
                        playerScore = Decks.Pairs(Decks.playerHand, playerScore);
                    }
                    if (Decks.playerHand.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.playerHand);
                        Decks.Pairs(Decks.playerHand, playerScore);
                    }

                    Decks.DisplayCards();

                    if (Decks.dealerHand.Count() > 0)
                    {
                        DealerActions();
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
                    Decks.DisplayCards();

                    if (Decks.dealerHand.Count() > 0)
                    {
                        DealerActions();
                        dealerScore = Decks.Pairs(Decks.dealerHand, dealerScore);
                    }
                    if (Decks.dealerHand.Count() == 0)
                    {
                        Decks.NoCardsInHand(Decks.dealerHand);
                        Decks.Pairs(Decks.dealerHand, dealerScore);
                    }

                    Decks.DisplayCards();

                    if (Decks.playerHand.Count() > 0)
                    {
                        Actions();
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

        public static void Actions()
        {
            Card temp = new Card(null);
            int dealerCount = Decks.dealerHand.Count();
            bool action = true;
            bool error = true;
            bool check = false;
            bool inHand = false;
            bool indealerHandand = false;

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

                foreach (Card card in Decks.playerHand)
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
                    foreach (Card card in Decks.dealerHand)
                    {
                        if (card.card == temp.card)
                        {
                            action = false;
                            indealerHandand = true;
                            temp = card;
                            break;
                        }
                    }
                    action = false;
                }
                else { Console.WriteLine("\nYour hand doesn't contain a " + temp.card); error = true; }
            }
            if (indealerHandand == true)
            {
                Console.WriteLine("\nYou took the computer's card\nPress any key to continue");
                Console.ReadKey();

                Decks.playerHand.Add(temp);
                Decks.dealerHand.Remove(temp);
            }
            else
            {
                Console.WriteLine("Go Fish!\nYou picked up a: " + Decks.deck[0].card + "\n\nPress any key to continue");
                Console.ReadKey();

                Decks.playerHand.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);
            }
        }
        public static void DealerActions()
        {
            Random rnd = new Random();
            Card temp = new Card(null);
            int dealerHandCount = Decks.dealerHand.Count();
            int index = 0;
            bool inHand = false;

            index = rnd.Next(0, dealerHandCount);

            foreach (Card card in Decks.playerHand)
            {
                if (Decks.dealerHand[index].card == card.card)
                {
                    inHand = true;
                    temp = card;
                }
            }

            if (inHand == true)
            {
                Console.WriteLine("\nDo you have a " + Decks.dealerHand[index].card);
                Console.WriteLine("\nThe computer took your card\nPress any key to continue");

                Decks.dealerHand.Add(temp);
                Decks.playerHand.Remove(temp);

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nDo you have a " + Decks.dealerHand[index].card);
                Console.WriteLine("Go Fish!\nPress any key to continue");

                Decks.dealerHand.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);

                Console.ReadKey();
            }
        }

        public static void Results()
        {
            Console.Clear();
            Console.WriteLine("Your Pairs: " + playerScore + "\nComputer's Pairs: " + dealerScore);

            if (playerScore > dealerScore) { Console.WriteLine("You Win!"); }
            else if (playerScore < dealerScore) { Console.WriteLine("You Lose!"); }
            else if (playerScore == dealerScore) { Console.WriteLine("Tie"); }
        }
    }
}


