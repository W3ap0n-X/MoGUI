using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

    public class MoCaInput : MoGCArgs
    {
        public object Value { get; set; }
        public MoCaInput(Action<object> onEditAction,
            Func<object> onUpdateAction,
            Func<object> text,
            string valType = "none",
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiInput), meta:meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {

        }

        public MoCaInput(Action<object> onEditAction,
            Func<object> onUpdateAction,
            object text = null,
            string valType = "none",
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiInput), meta:meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {

        }

        public MoCaInput(object value,
            
            string valType = "none",
            object text = null,
            Action<object> onEditAction = null,
            Func<object> onUpdateAction = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiInput), meta:meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {
            Value = value;
        }

    }

    public class InputMeta : ControlMeta
    {
        private SizeSettings _sizeSettings = new SizeSettings(20, 30, 1, 0, 80, 40);
        public TypographySettings labelSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, FontStyle.Bold, TextAnchor.MiddleLeft, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
        public TypographySettings inputSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, FontStyle.Normal, TextAnchor.MiddleLeft, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
        public TypographySettings placeholderSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, FontStyle.Italic, TextAnchor.MiddleLeft, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Shade);
        public Color background = MoGuiMeta.DefaultPanelColor.Shade;
        public MoGuiColor textColor = MoGuiMeta.DefaultFontColor;
        public InputMeta(MoGuiMeta parent, string name) : base(parent, name)
        {
            Sizing(_sizeSettings);
        }

    }
}
