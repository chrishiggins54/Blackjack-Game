using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_Blackjack
{
    public enum Suit   //array of suits using enums to be read-only
    {
        Hearts,
        Diamonds,
        Spades,
        Clubs
    }

    public enum Face //array of faces
    {
        Ace,//1 to 11
        Two,//2
        Three,//3
        Four,//4
        Five,//5
        Six,//6
        Seven,//7
        Eight,//8
        Nine,//9
        Ten,//10
        Jack,//10
        Queen,//10
        King,//10
    }

    public class Card //card constructor
    {
        public Suit Suit { get; set; }
        public Face Face { get; set; }
        public int Value { get; set; }
    }
}