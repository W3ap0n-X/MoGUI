using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace MoGUI
{
    public class MoCaToggle : MoGCArgs
    {
        
        public new Action<bool> OnClickAction;
        public ToggleType ToggleType;

        public Func<bool> boundValue;
        public bool _value;

        public MoCaToggle(
            bool? value = null,
            Func<bool> boundValue = null,
            Action<bool> onClickAction = null,
            string text = null,
            Func<object> boundText = null,
            ToggleType? toggleType = null,
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

            if (toggleType != null)
            {
                this.ToggleType = (ToggleType)toggleType;
            }
        }
    }

    public class ToggleMeta :ControlMeta
    {

        public Color background;
        public Color checkBox;

        public SizeSettings checkBoxSize = new SizeSettings(15, 15, 0, 0, 20, 20);
        public SizeSettings buttonSize = new SizeSettings(60, 30, 1, 0);
        public TypographySettings labelSettings;
        public ToggleType toggleType = MoGUI.ToggleType.checkbox;

        public ToggleMeta(MoGuiMeta parent, string name) : base(parent, name) 
        {
            background = _parent.Colors.Panel.Shade;
            checkBox = _parent.Colors.Control.Color;
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.UpperLeft, _parent.fontColor.Color, _parent.font);
        }

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

        public ToggleMeta ToggleType(ToggleType type)
        {
            toggleType = type;
            return this;
        }

    }

    public struct SizeSettings
    {
        public float minWidth;
        public float minHeight;
        public float? flexibleHeight;
        public float? flexibleWidth;
        public float? preferredWidth;
        public float? preferredHeight;

        public SizeSettings(float minW, float minH, float? flexW = null, float? flexH = null, float? prefW = null, float? prefH = null)
        {
            minWidth = minW;
            minHeight = minH;
            flexibleWidth = flexW;
            flexibleHeight = flexH;
            preferredWidth = prefW;
            preferredHeight = prefH;
        }

        public SizeSettings SetMin(Vector2 sizes)
        {
            minWidth = sizes.x;
            minHeight = sizes.y;
            return this;
        }

        public SizeSettings SetFlex(Vector2 sizes) 
        {
            flexibleWidth = sizes.x;
            flexibleHeight = sizes.y;
            return this;
        }

        public SizeSettings SetPref(Vector2 sizes)
        {
            preferredWidth = sizes.x;
            preferredHeight = sizes.y;
            return this;
        }
    }

}
