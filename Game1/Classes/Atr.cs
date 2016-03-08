﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Personagem
    {
        public int vida;
        public int furia;
        public bool atack = false;
        public Rectangle Pers = new Rectangle();
        public Texture2D Texture;

        public Vector2 Velocity;
        public Vector2 Position;
        public float ground;
        private float Speed;

        private Rectangle screenBound;

        public bool isJumping; //are we jumping or not
        public bool goingUp; //if going up or not

        public float u; //initial velocity
        private float jumpU;
        private float g; //gravity
        public float t; //time

        private KeyboardState prevKB;
        private int p1;
        private int p2;

        public Personagem(Texture2D Texture, Vector2 Position, float Speed, Rectangle screenBound)
        {
            this.Texture = Texture;
            this.Position = Position;
            ground = Position.Y;
            this.Speed = Speed;
            this.screenBound = screenBound;
            Velocity = Vector2.Zero;
            isJumping = goingUp = false;
            jumpU = 2.5f;
            g = -9.8f;
            t = 0;
        }

        public Personagem(int p1, int p2)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.p2 = p2;
        }

        public void Update(GameTime gameTime)
        {
            Position.X += (Velocity.X * Speed);
            //Set the Y position to be subtracted so that the upward movement would be done by decreasing the Y value
            Position.Y -= (Velocity.Y * Speed);

            goingUp = (Velocity.Y > 0);

            // TODO: Add your update logic here
            if (isJumping == true)
            {
                //motion equation using velocity: v = u + at
                Velocity.Y = (float)(u + (g * t));
                //Increase the timer
                t += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (isJumping == true && Position.Y > screenBound.Height - Texture.Height)
            {
                Position.Y = ground = screenBound.Height - Texture.Height;
                Velocity.Y = 0;
                isJumping = false;
                t = 0;
            }

            if (Position.X < 0)
            {
                //if Texture touches left side of the screen, set the position to zero and the velocity to zero.
                Position.X = 0;
                Velocity.X = 0;
            }
            else if (Position.X + Texture.Width > screenBound.Width)
            {
                //if Texture touches left side of the screen, set the position to zero and the velocity to zero.
                Position.X = screenBound.Width - Texture.Width;
                Velocity.X = 0;
            }
            if (Position.Y < 0)
            {
                //if the Texture touches the top of the screen, reset the timer and set the initial velocity to zero.
                Position.Y = 0;
                t = 0;
                u = 0;
            }
        }

        public void Input(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W) && (isJumping == false || Position.Y == ground))
            {
                isJumping = true;
                u = jumpU;
            }
            if (keyState.IsKeyDown(Keys.A) && !keyState.IsKeyDown(Keys.D))
            {
                if (keyState.IsKeyDown(Keys.D))
                {
                    if (Velocity.X > -2.0f)
                        Velocity.X -= (1.0f / 10);
                }
                else if (Velocity.X > -1.0f)
                {
                    Velocity.X -= (1.0f / 10);
                }
                else
                {
                    Velocity.X = -1.0f;
                }
            }
            else if (!keyState.IsKeyDown(Keys.A) && keyState.IsKeyDown(Keys.D))
            {
                if (keyState.IsKeyDown(Keys.A))
                {
                    if (Velocity.X < 2.0f)
                        Velocity.X += (1.0f / 10);
                }
                else if (Velocity.X < 1.0f)
                {
                    Velocity.X += (1.0f / 10);
                }
                else
                {
                    Velocity.X = 1.0f;
                }
            }
            else
            {
                if (Velocity.X > 0.05 || Velocity.X < -0.05)
                    Velocity.X *= 0.90f;
                else
                    Velocity.X = 0;
            }
            
            prevKB = keyState;
        }

        public void Fall()
        {
            t = 0;
            u = 0;
        }


        internal void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
