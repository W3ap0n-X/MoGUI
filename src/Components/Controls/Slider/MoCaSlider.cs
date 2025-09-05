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

        public SizeSettings horizontalizeSettings = new SizeSettings(50, 25, 1, 0, 100, 40);
        public SizeSettings verticallizeSettings = new SizeSettings(25, 25, 1, 0, 40, 100);

        public MoGuiColor Color;

        public TypographySettings labelSettings;
        public SliderMeta(MoGuiMeta parent, string name) : base(parent, name) 
        {
            Color = _parent.Colors.Panel;
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleLeft, _parent.fontColor.Color, _parent.font);
        }

    }

}
