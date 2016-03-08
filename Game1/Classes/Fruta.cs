using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jumping
{
    class Fruta
    {
        Texture2D Texture;
        Rectangle Position;
        private Texture2D Fruit;
        private Vector2 vector2;
        private int p;

        public Fruta(Texture2D Texture, Vector2 position)
        {
            this.Texture = Texture;
            Position = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
        }

        public Fruta(Texture2D Fruit, Vector2 vector2, int p)
        {
            // TODO: Complete member initialization
            this.Fruit = Fruit;
            this.vector2 = vector2;
            this.p = p;
        }

        public bool isColliding(Rectangle otherBox)
        {
            return Position.Intersects(otherBox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}