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
        public static Texture2D ground, player, block;

        public static Texture2D mainMenuBackground;
        public static Texture2D mainMenuButton;
        public static SpriteFont mainMenuFont;
        public static Texture2D ground, player;
        public static void LoadContent(ContentManager content)
        {
            ground = content.Load<Texture2D>("Ground");
            mainMenuBackground = content.Load<Texture2D>("start back round");
            mainMenuButton = content.Load<Texture2D>("Button");
            mainMenuFont = content.Load<SpriteFont>("MainMenuFont");
            ground = content.Load<Texture2D>("Ground2");
            player = content.Load<Texture2D>("Player");
            block = content.Load<Texture2D>("Block");
        }
    }
}
