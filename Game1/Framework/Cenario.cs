﻿using Microsoft.Xna.Framework;
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
                    r1.Bottom <= r2.Top &&
                    r1.Right >= r2.Left &&
                    r1.Left <= r2.Right);
        }

        private bool TouchBottomOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom &&
                    r1.Top >= r2.Bottom  &&
                    r1.Right >= r2.Left  &&
                    r1.Left <= r2.Right );
        }

        private bool TouchLeftOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                    r1.Right >= r2.Right &&
                    r1.Top <= r2.Bottom &&
                    r1.Bottom >= r2.Top);
        }

        private bool TouchRightOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Left &&
                    r1.Left >= r2.Left &&
                    r1.Top <= r2.Bottom &&
                    r1.Bottom >= r2.Top);
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

                    if (rectObj1.Intersects(rectObj2))
                    {

                        double w = 0.5 * (rectObj1.Width + rectObj2.Width);
                        double h = 0.5 * (rectObj1.Height + rectObj2.Height);
                        float dx = rectObj1.Center.X - rectObj2.Center.X;
                        float dy = rectObj1.Center.Y - rectObj2.Center.Y;

                        if (Math.Abs(dx) <= w && Math.Abs(dy) <= h)
                        {
                            double wy = w * dy;
                            double hx = h * dx;

                            if (wy > hx)
                            {
                                if (wy > -hx)
                                {
                                    /* collision at the top */
                                    obj1.Colidir(this, obj2, Posicao.CIMA);
                                    obj2.Colidir(this, obj1, Posicao.BAIXO);
                                }
                                else
                                {
                                    /* on the left */
                                    obj1.Colidir(this, obj2, Posicao.DIREITA);
                                    obj2.Colidir(this, obj1, Posicao.ESQUERDA);
                                }
                            }
                            else
                            {
                                if (wy > -hx)
                                {
                                    /* on the right */
                                    obj1.Colidir(this, obj2, Posicao.ESQUERDA);
                                    obj2.Colidir(this, obj1, Posicao.DIREITA);
                                }
                                else
                                {
                                    /* at the bottom */
                                    obj1.Colidir(this, obj2, Posicao.BAIXO);
                                    obj2.Colidir(this, obj1, Posicao.CIMA);
                                }
                            }
                        }

                    }

                        //if (TouchTopOf(rectObj1, rectObj2))
                        //{
                        //    obj1.Colidir(this, obj2, Posicao.CIMA);
                        //    obj2.Colidir(this, obj1, Posicao.BAIXO);
                        //}

                        //if (TouchLeftOf(rectObj1, rectObj2))
                        //{
                        //    obj1.Colidir(this, obj2, Posicao.ESQUERDA);
                        //    obj2.Colidir(this, obj1, Posicao.DIREITA);
                        //}
                        //if (TouchRightOf(rectObj1, rectObj2))
                        //{
                        //    obj1.Colidir(this, obj2, Posicao.DIREITA);
                        //    obj2.Colidir(this, obj1, Posicao.ESQUERDA);
                        //}
                        //if (TouchBottomOf(rectObj1, rectObj2))
                        //{
                        //    obj1.Colidir(this, obj2, Posicao.BAIXO);
                        //    obj2.Colidir(this, obj1, Posicao.CIMA);
                        //}
                    }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(GameObject go in objetos){
                go.posicaoAnterior = go.posicao;
                go.Update(gameTime);
            }
            detectarColisao();
        }

        public Rectangle getGameObjectRectangle(GameObject go)
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
