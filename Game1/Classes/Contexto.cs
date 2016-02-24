using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Contexto
    {
        public static Atr[] perso = new Atr[5];

        public static void Inicializar (ContentManager content)
        {
            jogador.texture = content.Load<Textura2D>("Pe");
        }
    }
}
