using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_Blackjack
{
    class Program
    {
        //variables      
        static Deck deck;
        static List<Card> playerHand;
        static List<Card> dealerHand;

        static void Main(string[] args)
        {
            do //starts the game
            {
                Console.ResetColor(); // resets color of result after game ends
                Console.Clear(); // clears console before new game
                DealHand();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Would you play to play again - y/n?");

            } while (Console.ReadLine().ToLower() == "y"); //continues playing as long as user enters y
        }

        private static void DealHand()
        {
            //creates a new deck and shuffles it
            deck = new Deck();
            deck.Shuffle();

            playerHand = new List<Card> //player's hand is created
            {
                deck.DrawACard(), //takes card from deck
                deck.DrawACard()
            };

            foreach (Card card in playerHand) //if player draws 2 aces this should make one of them value 11 and the other 1 to avoid going bust
            {
                if (card.Face == Face.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            dealerHand = new List<Card> //dealer's hand is created with first 2 cards
            {
                deck.DrawACard(),
                deck.DrawACard()
            };

            foreach (Card card in dealerHand)
            {
                if (card.Face == Face.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            //Outputs player's hand
            Console.ForegroundColor = ConsoleColor.White; //Extra feature: changes the text and background color depending on the player/result. 
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Card dealt is the {0} of {1}, value {2}", playerHand[0].Face, playerHand[0].Suit, playerHand[0].Value);
            Console.WriteLine("Card dealt is the {0} of {1}, value {2}", playerHand[1].Face, playerHand[1].Suit, playerHand[1].Value);
            Console.WriteLine("Your score is {0}\n", playerHand[0].Value + playerHand[1].Value);
            string userOption = "0";

            //if player scores 21 in the first round they win and the game ends
            if (playerHand[0].Value + playerHand[1].Value == 21)

            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Blackjack! Player wins!");
                return;
            }

            //otherwise player chooses whether to stick or twist
            else
            {
                Console.WriteLine("Would you like to stick or twist - s/t?");
                userOption = Console.ReadLine().ToLower();


                while (userOption == "t") //if player twists, another card is drawn
                {
                    playerHand.Add(deck.DrawACard());
                    Console.WriteLine("Card dealt is the {0} of {1}, value {2}", playerHand[playerHand.Count - 1].Face, playerHand[playerHand.Count - 1].Suit, playerHand[playerHand.Count - 1].Value);
                    int totalCardsValue = 0;

                    foreach (Card card in playerHand) //adds points to previous score
                    {
                        totalCardsValue += card.Value;
                    }

                    if (totalCardsValue > 21)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bust!");
                        Console.WriteLine("Player score is {0}", totalCardsValue);

                        return;
                    }

                    else if (totalCardsValue < 21) //as long as player has less than 21 they can twist
                    {

                        Console.WriteLine("Player score is {0}\n", totalCardsValue);
                        Console.WriteLine("Would you like to stick or twist - s/t?");
                        userOption = Console.ReadLine().ToLower();
                    }
                }

                //if player sticks, output dealer's hand
                while (userOption == "s")
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Dealer plays\n");
                    Console.WriteLine("Card dealt is the {0} of {1}, value {2}", dealerHand[0].Face, dealerHand[0].Suit, dealerHand[0].Value);
                    Console.WriteLine("Card dealt is the {0} of {1}, value {2}", dealerHand[1].Face, dealerHand[1].Suit, dealerHand[1].Value);

                    int dealerCardsValue = 0;
                    foreach (Card card in dealerHand)
                    {
                        dealerCardsValue += card.Value;
                    }
                    int playerCardValue = 0;
                    foreach (Card card in playerHand)
                    {
                        playerCardValue += card.Value;
                    }

                    //dealer draws a card until he has a score of at least 17 or goes bust
                    while (dealerCardsValue < 17)
                    {
                        dealerHand.Add(deck.DrawACard());
                        dealerCardsValue = 0;

                        foreach (Card card in dealerHand)
                        {
                            dealerCardsValue += card.Value;
                        }

                        Console.WriteLine("Card dealt is the {0} of {1}, value {2}", dealerHand[dealerHand.Count - 1].Face, dealerHand[dealerHand.Count - 1].Suit, dealerHand[dealerHand.Count - 1].Value);
                    }

                    dealerCardsValue = 0;
                    foreach (Card card in dealerHand)
                    {
                        dealerCardsValue += card.Value;
                    }

                    Console.WriteLine("Dealer score is {0}\n", dealerCardsValue);

                    if (dealerCardsValue > 21)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Dealer is bust! Player wins ");

                        return;
                    }

                    else
                    {
                        if (dealerCardsValue > playerCardValue)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Dealer wins", dealerCardsValue);

                            return;
                        }

                        else if (dealerCardsValue == playerCardValue)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("It's a draw", dealerCardsValue, playerCardValue);

                            return;
                        }

                        else
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Player wins");

                            return;
                        }
                    }
                }
            }
        }
    }
}
