using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

    public class MoCaButton : MoGCArgs
    {

        public MoCaButton(Func<object> text,
            Action onClickAction,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiButton), meta, text: text)
        {
            OnClickAction = onClickAction;
        }

        public MoCaButton(object text,
            Action onClickAction,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiButton), meta, text: text)
        {
            OnClickAction = onClickAction;
        }
    }

    public class ButtonMeta : ControlMeta
    {
        private SizeSettings _sizeSettings = new SizeSettings(60,30,1,0,100,40);
        public TypographySettings labelSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, 1, FontStyle.Bold, TextAnchor.MiddleCenter, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
        public MoGuiColor background = new MoGuiColor(MoGuiMeta.DefaultPanelColor.TintRaw, 0.6f);

        public ButtonMeta(string name) : base(name) 
        {
            Sizing(_sizeSettings);
        }

        public ButtonMeta Background(Color _color)
        {
            background = new MoGuiColor(_color);
            return this;
        }

        public ButtonMeta Background(MoGuiColor _color)
        {
            background = _color;
            return this;
        }
    }
}
