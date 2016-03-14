using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Fruta
    {
        Texture2D Texture;
        Rectangle Position;

        public Fruta(Texture2D Texture, Vector2 position, int score)
        {
            this.Texture = Texture;
            Position = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
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