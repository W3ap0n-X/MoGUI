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
        public MoCaSlider(Vector2 range,
            Action<object> onEditAction,
            Func<object> onUpdateAction,
            Func<object> text = null,
            string valType = "none",
            Func<float> boundMin = null,
            Func<float> boundMax = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSlider), meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {
            Range = range;
            if (boundMin != null) { BoundMin = boundMin; }
            if (boundMax != null) { BoundMax = boundMax; }


        }

        public MoCaSlider(Vector2 range,
            Action<object> onEditAction,
            Func<object> onUpdateAction,
            object text = null,
            string valType = "none",
            Func<float> boundMin = null,
            Func<float> boundMax = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSlider), meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {
            Range = range;
            if (boundMin != null) { BoundMin = boundMin; }
            if (boundMax != null) { BoundMax = boundMax; }
        }




    }


    public class SliderMeta : ControlMeta
    {


        public MoGuiColor Color = GuiMeta.DefaultPanelColor;

        public SliderMeta(string name) : base(name) { }


    }


}
