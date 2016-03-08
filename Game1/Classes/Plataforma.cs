using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1;

namespace Jumping
{
    class Plataforma
    {
        Texture2D Texture;
        Vector2 Position;
        int BlockState;

        public Plataforma(Texture2D Texture, Vector2 Position, int BlockState)
        {
            this.Texture = Texture;
            this.Position = Position;
            this.BlockState = BlockState;
        }

        public Personagem BlockCollision(Personagem personagem)
        {
            Rectangle top = new Rectangle((int)Position.X + 5, (int)Position.Y - 10, Texture.Width - 10, 10);
            Rectangle bottom = new Rectangle((int)Position.X + 5, (int)Position.Y + Texture.Height, Texture.Width - 10, 10);
            Rectangle left = new Rectangle((int)Position.X - 10, (int)Position.Y + 5, 10, Texture.Height - 10);
            Rectangle right = new Rectangle((int)Position.X + Texture.Width, (int)Position.Y + 5, 10, Texture.Height - 10);

            if (BlockState != 2 || (BlockState == 2 && !personagem.goingUp))
            {
                if (top.Intersects(new Rectangle((int)personagem.Position.X, (int)personagem.Position.Y, personagem.Texture.Width, personagem.Texture.Height)))
                {
                    if (personagem.Position.Y + personagem.Texture.Height > Position.Y && personagem.Position.Y + personagem.Texture.Height < Position.Y + Texture.Height / 2)
                    {
                        personagem.Position.Y = personagem.ground = Position.Y - personagem.Texture.Height;
                        personagem.Velocity.Y = 0;
                        personagem.isJumping = false;
                        personagem.t = 0;
                    }

                }
                else if (personagem.isJumping == false && personagem.ground == Position.Y - personagem.Texture.Height)
                {
                    personagem.isJumping = true;
                    personagem.u = 0;
                }

                if (BlockState != 2)
                {
                    if (bottom.Intersects(new Rectangle((int)personagem.Position.X, (int)personagem.Position.Y, personagem.Texture.Width, personagem.Texture.Height)))
                    {
                        if (personagem.Position.Y < Position.Y + Texture.Height)
                        {
                            personagem.Position.Y = Position.Y + Texture.Height;
                            personagem.t = 0;
                            personagem.u = 0;
                        }

                    }

                    if (left.Intersects(new Rectangle((int)personagem.Position.X, (int)personagem.Position.Y, personagem.Texture.Width, personagem.Texture.Height)))
                    {
                        if (personagem.Position.X + personagem.Texture.Width > Position.X)
                        {
                            personagem.Position.X = Position.X - personagem.Texture.Width;
                            personagem.Velocity.X = 0;
                        }
                    }

                    if (right.Intersects(new Rectangle((int)personagem.Position.X, (int)personagem.Position.Y, personagem.Texture.Width, personagem.Texture.Height)))
                    {
                        if (personagem.Position.X < Position.X + Texture.Width)
                        {
                            personagem.Position.X = Position.X + Texture.Width;
                            personagem.Velocity.X = 0;
                        }
                    }
                }
            }
            return personagem;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height), Color.White);
        }
    }
}