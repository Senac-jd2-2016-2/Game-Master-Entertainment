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
        GraphicsDeviceManager Graficos;
        SpriteBatch spriteBatch;
        KeyboardState prevKB;

        Personagem Lycans;
        List<Plataforma> Blocks;
        List<Fruta> Frutas;

        List<char[,]> Levels = new List<char[,]>();

        int tileWidth, tileHeight;
        int currentLevel;

        public Game1()
        {
            Graficos = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.Graficos.PreferredBackBufferWidth = 1200;
            this.Graficos.PreferredBackBufferHeight = 750;
            this.Graficos.ApplyChanges();
        }

        protected override void Initialize()
        {
            Blocks = new List<Plataforma>();
            Frutas = new List<Fruta>();

            char[,] Level1 = {{'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','F','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','F','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','-','-','-','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'C','.','.','.','.','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'#','#','P','.','.','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'#','#','#','#','#','#','#','#','.','.','.','.','#','#','#','#','#','#','#','#','#','#','#','#'}};

            char[,] Level2 = {{'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','F','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','-','-','.','.','-','-','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'-','-','.','.','.','.','.','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','.','F','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','.','-','-','.','.','.','.','.','#','.','.','.','.','.','.','.','.','.','.','.'},
                              {'C','.','.','.','.','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'-','-','.','.','.','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','F','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'.','.','.','.','-','-','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'C','.','.','.','.','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'#','#','.','P','.','.','.','.','.','.','.','.','#','.','F','.','.','.','.','.','.','.','.','.'},
                              {'#','#','#','#','#','#','#','#','.','.','.','.','#','#','#','#','#','#','#','#','#','#','#','#'}};

            Levels.Add(Level1);
            Levels.Add(Level2);

            currentLevel = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D ballSprite = Content.Load<Texture2D>("Player");
            Lycans = new Personagem(ballSprite, Vector2.Zero, 6.0f, new Rectangle(0, 0, this.Graficos.PreferredBackBufferWidth, this.Graficos.PreferredBackBufferHeight));

            LoadLevel(currentLevel);

        }

        void LoadLevel(int level)
        {
            Blocks.Clear();
            Frutas.Clear();

            Lycans.Posicao = Vector2.Zero;

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
                    if (Levels[level][y, x] == '#') //Verde
                    {
                        Blocks.Add(new Plataforma(blockSpriteA, new Vector2(x * 50, y * 50), 1));
                    }
                    //Blocks that are only passable if going up them
                    if (Levels[level][y, x] == '-') //Marrom
                    {
                        Blocks.Add(new Plataforma(blockSpriteB, new Vector2(x * 50, y * 50), 2));
                    }
                    if (Levels[level][y, x] == 'F') //Fruta
                    {
                        Frutas.Add(new Fruta(coin, new Vector2(x * 50, y * 50), 50));
                    }
                    if (Levels[level][y, x] == 'P' && Lycans.Posicao == Vector2.Zero)
                    {
                        Lycans.Posicao = new Vector2(x * 50, (y + 1) * 50 - Lycans.Texture.Height);
                    }
                    else if (Levels[level][y, x] == 'P' && Lycans.Posicao != Vector2.Zero)
                    {
                        throw new Exception("Only one 'P' is needed for each level");
                    }
                }
            }

            if (Lycans.Posicao == Vector2.Zero)
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
            Lycans.Update(gameTime);

            foreach (Plataforma b in Blocks)
            {
                Lycans = b.BlockCollision(Lycans);
            }

            for (int i = Frutas.Count - 1; i >= 0; i--)
            {
                if (Frutas[i].isColliding(new Rectangle((int)Lycans.Posicao.X, (int)Lycans.Posicao.Y, Lycans.Texture.Width, Lycans.Texture.Height)))
                {
                    Frutas.RemoveAt(i);
                }
            }

            prevKB = Keyboard.GetState();

            base.Update(gameTime);
        }

        void HandleInput(KeyboardState keyState)
        {
            Lycans.Entrada(keyState);
            if (prevKB.IsKeyUp(Keys.F) && keyState.IsKeyDown(Keys.F))
            {
                this.Graficos.ToggleFullScreen();
                this.Graficos.ApplyChanges();
            }

            if (prevKB.IsKeyUp(Keys.L) && keyState.IsKeyDown(Keys.L))
            {
                currentLevel = (currentLevel + 1) % Levels.Count;
                LoadLevel(currentLevel);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);

            spriteBatch.Begin();
            foreach (Plataforma b in Blocks)
            {
                b.Draw(spriteBatch);
            }
            foreach (Fruta c in Frutas)
            {
                c.Draw(spriteBatch);
            }

            Lycans.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}