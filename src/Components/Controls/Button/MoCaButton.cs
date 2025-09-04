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
        Font font;
        int fontsize;
        public MoGuiColor background = new MoGuiColor(GuiMeta.DefaultPanelColor.Tint);
        // Vector2 minSize;
        // Vector2 Size;

        public ButtonMeta(string name) : base(name) { }

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
