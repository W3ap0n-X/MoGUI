using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace MoGUI
{

    public class MoCaSelector : MoGCArgs
    {
        public Dictionary<string, object> Options;
        public ControlOrientation? Direction = null;
        public MoCaSelector(Dictionary<string, object> options,
            string text,
            ControlOrientation? direction = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSelector), meta:meta, text: text)
        {
            Options = options;
            Direction = direction;
        }

    }

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
