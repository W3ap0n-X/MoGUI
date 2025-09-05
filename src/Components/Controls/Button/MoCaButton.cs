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
        public TypographySettings labelSettings;
        public MoGuiColor background;

        public ButtonMeta(MoGuiMeta parent, string name) : base(parent, name) 
        {
            background = new MoGuiColor(_parent.Colors.Panel.TintRaw, 0.6f);
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleCenter, _parent.Colors.Text.Color, _parent.font);
            Sizing(_sizeSettings);
        }

        public ButtonMeta Background(Color _color)
        {
            background = new MoGuiColor(_color);
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleCenter, _parent.Colors.Text.Color, _parent.font);
            return this;
        }

        public ButtonMeta Background(MoGuiColor _color)
        {
            background = _color;
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleCenter, _parent.Colors.Text.Color, _parent.font);
            return this;
        }
    }
}
