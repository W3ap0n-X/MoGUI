using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{



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
            placeholderSettings = new TypographySettings(labelSettings.FontSize, FontStyle.Italic, labelSettings.Alignment, _parent.fontColor.Shade, labelSettings.FontFace);
        }

    }
}
