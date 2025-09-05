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
        private SizeSettings _sizeSettings = new SizeSettings(20, 20, 1, 1);
        public TypographySettings labelSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, 1, FontStyle.Bold, TextAnchor.MiddleCenter, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
        public ColorBlockMeta(string name) : base(name) { Sizing(_sizeSettings); }


    }
}
