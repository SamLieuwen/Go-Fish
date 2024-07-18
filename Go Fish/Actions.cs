using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish
{
    internal class Actions
    {
        public static void PlayerActions()
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
    }
}
