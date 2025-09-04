using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

    public class MoCaToggleBT : MoGCArgs
    {
        public new Action<bool> OnClickAction;


        public Func<bool> boundValue;
        public bool _value;

        public MoCaToggleBT(
        bool? value = null,
        Func<bool> boundValue = null,
        Action<bool> onClickAction = null,
        string text = null,
        Func<object> boundText = null,
        MoGuiMeta meta = null
    ) : base(typeof(MoGuiToggleBt), meta)
        {
            if (boundValue != null)
            {
                this.boundValue = boundValue;
            }
            else if (value.HasValue)
            {
                _value = value.Value;
            }

            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }

            if (onClickAction != null)
            {
                OnClickAction = onClickAction;
            }
        }
    }

    public class MoCaToggle : MoGCArgs
    {
        
        public new Action<bool> OnClickAction;


        public Func<bool> boundValue;
        public bool _value;

        public MoCaToggle(
        bool? value = null,
        Func<bool> boundValue = null,
        Action<bool> onClickAction = null,
        string text = null,
        Func<object> boundText = null,
        MoGuiMeta meta = null
    ) : base(typeof(MoGuiToggle), meta)
        {
            if (boundValue != null)
            {
                this.boundValue = boundValue;
            }
            else if (value.HasValue)
            {
                _value = value.Value;
            }

            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }

            if (onClickAction != null)
            {
                OnClickAction = onClickAction;
            }
        }
    }


    public class ToggleMeta :ControlMeta
    {

        // Checkbox

        public Color background = GuiMeta.DefaultPanelColor.Shade;
        public Color checkBox = GuiMeta.DefaultPanelColor.Tint;

        public Vector4 checkBoxSize = new Vector4(15, 15, 20, 20);

        public ToggleMeta(string name) : base(name) { }



        public ToggleMeta Background(Color _color)
        {
            background = _color;
            return this;
        }

        public ToggleMeta CheckBox(Color _color)
        {
            checkBox = _color;
            return this;
        }



    }

}
