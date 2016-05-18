using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Framework
{
    public enum Posicao{
        CIMA, BAIXO, ESQUERDA, DIREITA
    }

    public class GameObject
    {
        public Texture2D texture;

        public Vector2 posicao;

        public Vector2 posicaoAnterior;

        public void retornarAnterior()
        {
            posicao = posicaoAnterior;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Colidir(Cenario cenario, GameObject obj, Posicao posicao)
        {

        }

        public virtual void OnRemove()
        {

        }

    }
}
