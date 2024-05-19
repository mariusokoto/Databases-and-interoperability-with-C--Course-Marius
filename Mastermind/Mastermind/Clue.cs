using System;
namespace Mastermind
{
    public class Clue
    {
        public string color { get; set; }

        public Clue()
        {
        }

        public Clue(string color)
        {
            this.color = color;
        }

        public string getClueColor()
        {
            return color;
        }
    }
}

