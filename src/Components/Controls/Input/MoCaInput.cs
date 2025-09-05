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
        public TypographySettings labelSettings;
        public TypographySettings inputSettings;
        public TypographySettings placeholderSettings;
        public Color background;
        public InputMeta(MoGuiMeta parent, string name) : base(parent, name)
        {
            Sizing(_sizeSettings);
            background = _parent.Colors.Panel.Shade;
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleLeft, _parent.fontColor.Color, _parent.font);
            inputSettings = new TypographySettings(_parent.fontSize, FontStyle.Normal, TextAnchor.MiddleLeft, _parent.fontColor.Color, _parent.font);
            placeholderSettings = new TypographySettings(_parent.fontSize, FontStyle.Italic, TextAnchor.MiddleLeft, _parent.fontColor.Shade, _parent.font);
        }

    }
}
