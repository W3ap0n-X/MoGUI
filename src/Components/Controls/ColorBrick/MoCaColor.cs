using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

    public class MoCaColor : MoGCArgs
    {
        public Func<Color> Value;
        public MoCaColor(Color value,
            //Func<object> text,
            //Action onClickAction,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiColorBrick), meta)
        {
            Value = () => value;
        }

        public MoCaColor(Func<Color> value,
            //Func<object> text,
            //Action onClickAction,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiColorBrick), meta)
        {
            Value = value;
        }

    }

    public class ColorBlockMeta : ControlMeta
    {
        public ColorBlockMeta(string name) : base(name) { MinSize(new Vector2(10, 10)); }


    }
}
