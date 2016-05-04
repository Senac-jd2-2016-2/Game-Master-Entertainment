using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Framework
{
    public class Cenario
    {

        private Camera camera = new Camera();

        private List<GameObject> objetos = new List<GameObject>();


        public void AddGameObject(GameObject go)
        {
            objetos.Add(go);
        }

        public void RemoveGameObject(GameObject go)
        {
            go.OnRemove();
            objetos.Remove(go);
        }


        private bool TouchTopOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 1 &&
                    r1.Bottom <= r2.Top + (r2.Height / 2) &&
                    r1.Right >= r2.Left + r2.Width / 5 &&
                    r1.Left <= r2.Right - r2.Width / 5);
        }

        private bool TouchBottomOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 5) &&
                    r1.Top >= r2.Bottom - 1 &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5));
        }

        private bool TouchLeftOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                    r1.Right >= r2.Right - 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        private bool TouchRightOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Left &&
                    r1.Left >= r2.Left - 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        private void detectarColisao()
        {
            for(int i = 0; i < objetos.Count; i++)
            {
                GameObject obj1 = objetos[i];
                for (int j = i + 1; j < objetos.Count; j++)
                {
                    GameObject obj2 = objetos[j];


                    Rectangle rectObj1 = getGameObjectRectangle(obj1);
                    Rectangle rectObj2 = getGameObjectRectangle(obj2);

                    if (TouchTopOf(rectObj1, rectObj2))
                    {
                        obj1.Colidir(this, obj2, Posicao.CIMA);
                    }

                    if (TouchLeftOf(rectObj1, rectObj2))
                    {
                        obj1.Colidir(this, obj2, Posicao.ESQUERDA);
                    }
                    if (TouchRightOf(rectObj1, rectObj2))
                    {
                        obj1.Colidir(this, obj2, Posicao.DIREITA);
                    }
                    if (TouchBottomOf(rectObj1, rectObj2))
                    {
                        obj1.Colidir(this, obj2, Posicao.BAIXO);
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(GameObject go in objetos){
                go.Update(gameTime);
            }
            detectarColisao();
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
