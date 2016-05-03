using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Framework
{
    class Cenario
    {

        private Camera camera = new Camera();

        private List<GameObject> objetos = new List<GameObject>();


        public void AddGameObject(GameObject go)
        {
            objetos.Add(go);
        }

        public void Update(GameTime gameTime)
        {
            foreach(GameObject go in objetos){
                go.Update(gameTime);
            }
        }

        private Rectangle getGameObjectRectangle(GameObject go)
        {
            return new Rectangle((int)go.posicao.X, (int)go.posicao.Y, go.texture.Width, go.texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in objetos)
            {
                spriteBatch.Draw(go.texture, getGameObjectRectangle(go), Color.White);
            }

        }

    }
}
