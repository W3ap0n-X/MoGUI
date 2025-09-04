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

        public Color background = GuiMeta.DefaultPanelColor.Shade;

        public MoGuiColor textColor = GuiMeta.DefaultFontColor;

        public InputMeta(string name) : base(name) { }





    }
}
