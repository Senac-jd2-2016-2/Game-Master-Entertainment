using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Game1;

namespace Jumping
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState tecle;

        Personagem Lycans;
        List<Plataforma> Plats;
        List<Fruta> Frutas;

        List<char[,]> Levels = new List<char[,]>();

        int tileWidth, tileHeight;
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
            Plats = new List<Plataforma>();
            Frutas = new List<Fruta>();

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D Sprite = Content.Load<Texture2D>("Pe");
            Lycans = new Personagem(Sprite, Vector2.Zero, 6.0f, new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));

            LoadLevel(currentLevel);
        }

        void LoadLevel(int level)
        {
            Plats.Clear();
            Frutas.Clear();

            Lycans.Position = Vector2.Zero;

            tileWidth = Levels[level].GetLength(1);
            tileHeight = Levels[level].GetLength(0);

            Texture2D blockSpriteA = Content.Load<Texture2D>("Chão1");
            Texture2D blockSpriteB = Content.Load<Texture2D>("Chão2");
            Texture2D Fruit = Content.Load<Texture2D>("Fruta");

            for (int x = 0; x < tileWidth; x++)
            {
                for (int y = 0; y < tileHeight; y++)
                {
                    //Inpassable Blocks
                    if (Levels[level][y, x] == '#')
                    {
                        Plats.Add(new Plataforma(blockSpriteA, new Vector2(x * 50, y * 50), 1));
                    }
                    //Blocks that are only passable if going up them
                    if (Levels[level][y, x] == '-')
                    {
                        Plats.Add(new Plataforma(blockSpriteB, new Vector2(x * 50, y * 50), 2));
                    }
                    if (Levels[level][y, x] == 'C')
                    {
                        Frutas.Add(new Fruta(Fruit, new Vector2(x * 50, y * 50), 50));
                    }
                    if (Levels[level][y, x] == 'P' && Lycans.Position == Vector2.Zero)
                    {
                        Lycans.Position = new Vector2(x * 50, (y + 1) * 50 - Lycans.Texture.Height);
                    }
                    else if (Levels[level][y, x] == 'P' && Lycans.Position != Vector2.Zero)
                    {
                        throw new Exception("Only one 'P' is needed for each level");
                    }
                }
            }

            if (Lycans.Position == Vector2.Zero)
            {
                throw new Exception("Player Position needs to be set with 'P'");
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            HandleInput(Keyboard.GetState());
            Lycans.Update(gameTime);

            foreach (Plataforma b in Plats)
            {
                Lycans = b.BlockCollision(Lycans);
            }

            for (int i = Frutas.Count - 1; i >= 0; i--)
            {
                if (Frutas[i].isColliding(new Rectangle((int)Lycans.Position.X, (int)Lycans.Position.Y, Lycans.Texture.Width, Lycans.Texture.Height)))
                {
                    Frutas.RemoveAt(i);
                }
            }

            tecle = Keyboard.GetState();

            base.Update(gameTime);
        }

        void HandleInput(KeyboardState keyState)
        {
            Lycans.Entrada(keyState);
            if (tecle.IsKeyUp(Keys.F) && keyState.IsKeyDown(Keys.F))
            {
                this.graphics.ToggleFullScreen();
                this.graphics.ApplyChanges();
            }

            if (tecle.IsKeyUp(Keys.L) && keyState.IsKeyDown(Keys.L))
            {
                currentLevel = (currentLevel + 1) % Levels.Count;
                LoadLevel(currentLevel);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            foreach (Plataforma b in Plats)
            {
                b.Draw(spriteBatch);
            }
            foreach (Fruta c in Frutas)
            {
                c.Draw(spriteBatch);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}