using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstGame
{
    /// <summary>
    /// The sprite class will hold all the information pertaining to a sprite, such as its image, position, and color.
    /// </summary>
    class Sprite
    {
        public Texture2D image;
        public Vector2 position;
        public Color color;

        public Sprite(Vector2 vector, Color col)
        {
            position = vector;
            color = col;
        }
        // No image in the constructor because we will ContentLoad that in Load().
        public Sprite(Color col)
        {
            color = col;
        }



    }
}
