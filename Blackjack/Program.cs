using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        const float winMultiplier = 1.5f;
        const int minimumBet = 100;
        enum Cards : int
        {
            TWO = 2,
            THREE = 3,
            FOUR,
            FIVE,
            SIX,
            SEVEN,
            EIGHT,
            NINE,
            TEN,
            JACK = 10,
            QUEEN = 10,
            KING = 10,
            ACE = 11,
        }

        static Random random;
        static List<Cards> hand;
        static List<Cards> deck;
        static float money = 1000;

        static void drawCard(List<Cards> list, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                int at = random.Next(deck.Count);
                Cards card = deck[at];
                deck.RemoveAt(at); //remove card from deck
                list.Add(card);
            }
        }

        static void printHand(List<Cards> list, bool dealer = false)
        {
            int value = 0;
            foreach (Cards card in list)
            {
                value += card == Cards.ACE ? dealer ? 11 : 1 : (int)card;
                Console.Write(card.ToString() + ", ");
            }
            Console.WriteLine("Value: " + value);
        }

        static int getValue(List<Cards> list, bool dealer = false)
        {
            int value = 0;
            foreach (Cards card in list)
            {
                value += card == Cards.ACE ? dealer ? 11 : 1 : (int)card;
            }
            return value;
        }

        static void Main(string[] args)
        {
            //Get Random
            random = new Random();

            Console.WriteLine("Your Money: " + money);
            char gameInput;

            do
            {
                //Init
                //Get deck
                deck = new List<Cards>();
                Array values = Enum.GetValues(typeof(Cards));
                foreach (Cards value in values)
                {
                    for (int i = 0; i < 4; i++) //four of each card
                        deck.Add(value);
                }
                //Get empty Hand
                hand = new List<Cards>();
                //Get Dealer hand
                List<Cards> dealerHand = new List<Cards>();

                //How much money will you sacrifice
                float bet = 0;
                do
                {
                    Console.WriteLine("Enter Bet Money (minimum " + minimumBet + "): ");
                    bet = Convert.ToInt32(Console.ReadLine());
                } while (bet < minimumBet);
                money -= bet;

                //Draw 
                //First Dealer
                drawCard(dealerHand);
                Console.WriteLine("Dealer: ");
                printHand(dealerHand, true);

                //Then player
                drawCard(hand, 2);
                Console.WriteLine("Your Hand: ");
                printHand(hand);

                char cardInput;
                do
                {
                    Console.WriteLine("Draw a new card (y/n)?");
                    cardInput = Convert.ToChar(Console.ReadLine());
                    if (cardInput == 'y')
                    {
                        Console.WriteLine("Drawing a new Card...");
                        drawCard(hand);
                        Console.WriteLine("Your Hand: ");
                        printHand(hand);
                        if (getValue(hand) > 21)
                        {
                            //Automatically lost
                            Console.WriteLine("You fucked up!");
                            break;
                        }
                    }
                } while (cardInput != 'n');

                int handValue = getValue(hand);
                if (handValue <= 21)
                {
                    //Draw for dealer
                    while (getValue(dealerHand) < 17)
                    {
                        drawCard(dealerHand);
                    }
                    Console.WriteLine("Dealer: ");
                    printHand(dealerHand);

                    //Did you win? Prolly not looser
                    int dealerValue = getValue(dealerHand, true);

                    if (dealerValue > 21 || handValue > dealerValue) //yay
                    {
                        Console.WriteLine("You won!");
                        money += bet * winMultiplier;
                    }
                    else
                    {
                        if (handValue == dealerValue) //tie
                        {
                            Console.WriteLine("Tie...");
                            money += bet;
                        }
                        else
                            Console.WriteLine("You lost :c!");
                    }
                }

                Console.WriteLine("Your Money: " + money);

                do
                {
                    Console.WriteLine("Want to play again?");
                    gameInput = Convert.ToChar(Console.ReadLine());
                } while (gameInput != 'y' && gameInput != 'n');

            } while (gameInput != 'n');

            Console.ReadKey();
        }
    }
}
