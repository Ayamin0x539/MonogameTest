using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstGame
{
    /// <summary>
    /// The Coordinate class will act like a linked list.
    /// It will store two values: a x-coordinate and a y-coordinate, and a pointer to the next node in the list.
    /// In the constructor, the next node is set to null by default.
    /// </summary>
    class Coordinate
    {

        public int xpoint { get; set; }
        public int ypoint { get; set; }
        public Coordinate next { get; set; }


        public Coordinate(int x, int y, Coordinate coordinate = null)
        {
            xpoint = x;
            ypoint = y;
            next = coordinate;
        }
    }
}
