using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            base.Initialize();  

        }

        protected override void LoadContent()
        {
            Contexto.Inicializar(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            foreach(Keys k in keys)
            {
                if (k.Equals(Keys.W))
                {
                    Contexto.PG.moverY(-2);
                }
                if (k.Equals(Keys.S))
                {
                    Contexto.PG.moverY(2);
                }
                if (k.Equals(Keys.D))
                {
                    Contexto.PG.moverX(2);
                }
                if (k.Equals(Keys.A))
                {
                    Contexto.PG.moverX(-2);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Contexto.BG, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(Contexto.CH.texture, Contexto.CH.getVector(), Color.White);
            spriteBatch.Draw(Contexto.IN.texture, Contexto.IN.getVector(), Color.White);
            spriteBatch.Draw(Contexto.PG.texture, Contexto.PG.getVector(), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
