using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

    public class MoCaColor : MoGCArgs
    {
        public Func<Color> Value;
        public MoCaColor(Color value,
            string text = null,
            Func<object> boundText = null,

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiColorBrick), meta, orientation: orientation, labelPlacement: labelPlacement)
        {
            Value = () => value;
            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }
        }

        public MoCaColor(Func<Color> value,
            string text = null,
            Func<object> boundText = null,

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiColorBrick), meta, orientation: orientation, labelPlacement: labelPlacement)
        {
            Value = value;
            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }
            if (boundText != null)
            {
                Text = boundText;
            }
        }

    }

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
