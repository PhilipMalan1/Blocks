using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    interface IInput
    {
        void UpdateInput(GameTime gameTime, KeyboardState key, KeyboardState keyi, MouseState mouse, MouseState mousei);
    }
}
