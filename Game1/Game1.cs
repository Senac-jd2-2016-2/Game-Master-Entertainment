using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState prevKB;

        Personagem Ball;
        List<Plataforma> Blocks;
        List<Fruta> Coins;

        List<char[,]> Levels = new List<char[,]>();

        int tileWidth, tileHeight;
        int score;
        int currentLevel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 500;
            this.graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            Blocks = new List<Plataforma>();
            Coins = new List<Fruta>();

            char[,] Level1 = {{'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','C','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','-','-','-','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','C','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','-','-','-','-','.','.','.'},
                              {'.','.','.','.','.','C','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','#','#','#','.','.','.','.','.','.','.','.','.'},
                              {'P','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'}};

            char[,] Level2 = {{'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','C','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','C','.','.','#','.','.','.'},
                              {'.','.','.','.','.','.','.','-','-','-','.','.','#','.','C','.'},
                              {'.','.','.','.','.','C','.','.','.','.','.','.','#','.','C','.'},
                              {'.','.','.','-','-','-','.','.','.','.','.','.','#','.','C','.'},
                              {'C','.','.','.','.','.','.','.','.','.','.','.','#','.','C','.'},
                              {'#','#','.','.','.','.','.','.','.','.','.','.','#','.','C','.'},
                              {'#','#','.','.','.','.','.','.','.','P','.','.','#','.','C','.'}};

            Levels.Add(Level1);
            Levels.Add(Level2);

            currentLevel = 0;
            score = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D ballSprite = Content.Load<Texture2D>("Player");
            Ball = new Personagem(ballSprite, Vector2.Zero, 6.0f, new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));

            LoadLevel(currentLevel);

        }

        void LoadLevel(int level)
        {
            Blocks.Clear();
            Coins.Clear();

            Ball.Position = Vector2.Zero;

            score = 0;

            tileWidth = Levels[level].GetLength(1);
            tileHeight = Levels[level].GetLength(0);

            Texture2D blockSpriteA = Content.Load<Texture2D>("blockA");
            Texture2D blockSpriteB = Content.Load<Texture2D>("blockB");
            Texture2D coin = Content.Load<Texture2D>("coin");

            for (int x = 0; x < tileWidth; x++)
            {
                for (int y = 0; y < tileHeight; y++)
                {
                    //Inpassable Blocks
                    if (Levels[level][y, x] == '#')
                    {
                        Blocks.Add(new Plataforma(blockSpriteA, new Vector2(x * 50, y * 50), 1));
                    }
                    //Blocks that are only passable if going up them
                    if (Levels[level][y, x] == '-')
                    {
                        Blocks.Add(new Plataforma(blockSpriteB, new Vector2(x * 50, y * 50), 2));
                    }
                    if (Levels[level][y, x] == 'C')
                    {
                        Coins.Add(new Fruta(coin, new Vector2(x * 50, y * 50), 50));
                    }
                    if (Levels[level][y, x] == 'P' && Ball.Position == Vector2.Zero)
                    {
                        Ball.Position = new Vector2(x * 50, (y + 1) * 50 - Ball.Texture.Height);
                    }
                    else if (Levels[level][y, x] == 'P' && Ball.Position != Vector2.Zero)
                    {
                        throw new Exception("Only one 'P' is needed for each level");
                    }
                }
            }

            if (Ball.Position == Vector2.Zero)
            {
                throw new Exception("Player Position needs to be set with 'P'");
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            HandleInput(Keyboard.GetState());
            Ball.Update(gameTime);

            foreach (Plataforma b in Blocks)
            {
                Ball = b.BlockCollision(Ball);
            }

            for (int i = Coins.Count - 1; i >= 0; i--)
            {
                if (Coins[i].isColliding(new Rectangle((int)Ball.Position.X, (int)Ball.Position.Y, Ball.Texture.Width, Ball.Texture.Height)))
                {
                    score += Coins[i].score;
                    Coins.RemoveAt(i);
                }
            }

            prevKB = Keyboard.GetState();

            base.Update(gameTime);
        }

        void HandleInput(KeyboardState keyState)
        {
            Ball.Input(keyState);
            if (prevKB.IsKeyUp(Keys.F) && keyState.IsKeyDown(Keys.F))
            {
                this.graphics.ToggleFullScreen();
                this.graphics.ApplyChanges();
            }

            if (prevKB.IsKeyUp(Keys.L) && keyState.IsKeyDown(Keys.L))
            {
                currentLevel = (currentLevel + 1) % Levels.Count;
                LoadLevel(currentLevel);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            foreach (Plataforma b in Blocks)
            {
                b.Draw(spriteBatch);
            }
            foreach (Fruta c in Coins)
            {
                c.Draw(spriteBatch);
            }

            Ball.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}