using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace MoGUI
{



    public class SelectorMeta : ControlMeta
    {
        private ControlOrientation _orientation = ControlOrientation.vertical;
        public ControlOrientation direction = ControlOrientation.vertical;
        public TypographySettings labelSettings;
        public SelectorMeta(MoGuiMeta parent, string name) : base(parent, name) 
        {
            labelSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, FontStyle.Bold, TextAnchor.MiddleCenter, _parent.Colors.Text.Color, _parent.font);
            orientation = _orientation;
        }

    }
}
