using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_Blackjack
{
    public class Deck
    {
        public List<Card> cards; //creates deck as list 

        public Deck()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            cards = new List<Card>();

            for (int i = 0; i < 4; i++) //gives cards appropriate suit and face
            {
                for (int j = 0; j < 13; j++)
                {
                    cards.Add(new Card() { Suit = (Suit)i, Face = (Face)j });

                    if (j <= 8) //if card value is not a face card, give it correct value
                        cards[cards.Count - 1].Value = j + 1;
                    else //otherwise given value of 10
                        cards[cards.Count - 1].Value = 10;
                }
            }
        }

        public void Shuffle() //shuffles the cards
        {
            Random rand = new Random();
            int next = cards.Count;
            while (next > 1)
            {
                next--;
                int k = rand.Next(next + 1);
                Card card = cards[k];
                cards[k] = cards[next];
                cards[next] = card;
            }
        }

        public Card DrawACard() //takes a card and moves through deck
        {
            if (cards.Count <= 0)
            {
                this.Initialize();
                this.Shuffle();
            }

            Card cardToReturn = cards[cards.Count - 1]; 
            cards.RemoveAt(cards.Count - 1);
            return cardToReturn;
        }
    }
}
