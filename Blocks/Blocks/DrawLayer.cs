using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    //The first layer is drawn first
    public enum DrawLayer
    {
        Background,
        Ground,
        Door,
        FallingPlatform,
        RisingPlatform,
        Arrows,
        FloatingText,
        Goal,
        Player,
        Block,
        Button,
        UI,
        Spikes
    }
}
