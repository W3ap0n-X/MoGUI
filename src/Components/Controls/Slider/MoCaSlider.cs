using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoCaSlider : MoGCArgs
    {
        public Vector2 Range;
        public Func<float> BoundMin;
        public Func<float> BoundMax;
        public ControlOrientation? Direction = null;
        public MoCaSlider(Vector2 range,
            Action<object> onEditAction,
            Func<object> onUpdateAction,
            Func<object> text = null,
            string valType = "none",
            Func<float> boundMin = null,
            Func<float> boundMax = null,
            ControlOrientation? direction = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSlider), meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {
            Range = range;
            if (boundMin != null) { BoundMin = boundMin; }
            if (boundMax != null) { BoundMax = boundMax; }
            Direction = direction;
        }

        public MoCaSlider(Vector2 range,
            Action<object> onEditAction,
            Func<object> onUpdateAction,
            object text = null,
            string valType = "none",
            Func<float> boundMin = null,
            Func<float> boundMax = null,
            ControlOrientation? direction = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSlider), meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {
            Range = range;
            if (boundMin != null) { BoundMin = boundMin; }
            if (boundMax != null) { BoundMax = boundMax; }
            Direction = direction;
        }

    }

    public class SliderMeta : ControlMeta
    {
        public ControlOrientation direction = ControlOrientation.horizontal;

        private SizeSettings _sizeSettings = new SizeSettings(50, 25, 1, 0, 100, 40);

        public MoGuiColor Color = MoGuiMeta.DefaultPanelColor;

        public TypographySettings labelSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, 1, FontStyle.Bold, TextAnchor.MiddleLeft, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
        public SliderMeta(string name) : base(name) 
        {
            Sizing(_sizeSettings);
        }

    }

}
