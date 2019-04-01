using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class LoadedContent
    {
        public static Texture2D ground;

        public static void LoadContent(ContentManager content)
        {
            ground = content.Load<Texture2D>("Ground");
        }
    }
}
