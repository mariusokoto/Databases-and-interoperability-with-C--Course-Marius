using System;
namespace Mastermind
{
    public class Pawn
    {
        private string color { get; set; }

        public Pawn(string color)
        {
            this.color = color;
        }

        public string GetColor()
        {
            return color;
        }

    }
}

