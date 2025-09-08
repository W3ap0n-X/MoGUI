using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace MoGUI
{


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

}
