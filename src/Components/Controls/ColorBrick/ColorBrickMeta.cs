using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{



    public class ColorBlockMeta : ControlMeta
    {
        private SizeSettings _sizeSettings = new SizeSettings(20, 20, 1, 1);
        public TypographySettings labelSettings;
        public ColorBlockMeta(MoGuiMeta parent, string name) : base(parent, name) 
        {
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleCenter, _parent.Colors.Text.Color, _parent.font);
            Sizing(_sizeSettings); 
        }


    }
}
