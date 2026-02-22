using MazeLearner.Screen.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Widgets
{
    public class EnumSlider<T> : BaseEnumSlider<T> where T : Enum
    {
        public EnumSlider(int x, int y, int w, int h, int defVal) : base(x, y, w, h, defVal)
        {

        }
    }
}
