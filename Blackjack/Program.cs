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
        enum Cards : int
        {
            ACE = 1,
            TWO = 2,
            THREE,
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
        }

        static Random random;
        static List<Cards> hand;
        static List<Cards> deck;
        static int handValue = 0;
        static float money = 1000;

        static void drawCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                int at = random.Next(deck.Count - 1);
                Cards card = deck[at];
                deck.RemoveAt(at); //remove card from deck
                handValue += (int)card;
                hand.Add(card);
            }
        }

        static void printHand()
        {
            Console.WriteLine("Your Hand:");
            foreach (Cards card in hand)
            {
                Console.Write(card.ToString() + ", ");
            }
            Console.WriteLine("Value: " + handValue);
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
                Console.WriteLine("Enter Bet Money: ");
                float bet = Convert.ToInt32(Console.ReadLine());
                money -= bet;

                drawCard(2);
                printHand();

                char cardInput;
                do
                {
                    Console.WriteLine("Draw a new card (y/n)?");
                    cardInput = Convert.ToChar(Console.ReadLine());
                    if (cardInput == 'y')
                    {
                        Console.WriteLine("Drawing a new Card...");
                        drawCard();
                        printHand();
                        if (handValue > 21)
                        {
                            Console.WriteLine("You fucked up!");
                            break;
                        }
                    }
                    else
                        Console.WriteLine("Unrecognized Command. Draw a new card (y/n)?");
                } while (cardInput != 'n');

                if (handValue == 21)
                {
                    money += bet * winMultiplier;
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
