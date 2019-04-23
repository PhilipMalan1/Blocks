using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    public interface IHoldable
    {
        bool IsHeld { get; set; }
        float Width { get; }
    }
}
