using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{


    public class SliderMeta : ControlMeta
    {
        public ControlOrientation direction = ControlOrientation.horizontal;

        public SizeSettings horizontalizeSettings = new SizeSettings(50, 25, 1, 0, 100, 40);
        public SizeSettings verticalsizeSettings = new SizeSettings(25, 25, 1, 0, 40, 100);

        public MoGuiColor Color;

        public TypographySettings labelSettings;
        public SliderMeta(MoGuiMeta parent, string name) : base(parent, name) 
        {
            Color = _parent.Colors.Control;
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleLeft, _parent.fontColor.Color, _parent.font);
        }

    }

}
