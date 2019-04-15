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
        public static Texture2D mainMenuBackground;
        public static Texture2D mainMenuButton;
        public static SpriteFont mainMenuFont;
        public static Texture2D ground, player;
        // R = Right
        // UR = Up Right
        public static Texture2D ArrowR;
        public static Texture2D ArrowUR;

        public static void LoadContent(ContentManager content)
        {
            ground = content.Load<Texture2D>("Ground");
            mainMenuBackground = content.Load<Texture2D>("start back round");
            mainMenuButton = content.Load<Texture2D>("Button");
            mainMenuFont = content.Load<SpriteFont>("MainMenuFont");
            ground = content.Load<Texture2D>("Ground2");
            player = content.Load<Texture2D>("Player");
            ArrowR = content.Load<Texture2D>("Arrow.png");
            ArrowUR = content.Load<Texture2D>("Arrow2.png");
        }
    }
}
