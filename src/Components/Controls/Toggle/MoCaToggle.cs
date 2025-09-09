using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace MoGUI
{
    public class MoCaToggle : MoGCArgs
    {
        
        public new Action<bool> OnClickAction;
        public ToggleType ToggleType;

        public Func<bool> boundValue;
        public bool _value;

        public MoCaToggle(
            bool? value = null,
            Func<bool> boundValue = null,
            Action<bool> onClickAction = null,
            string text = null,
            Func<object> boundText = null,
            ToggleType? toggleType = null,

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiToggle), meta, orientation: orientation, labelPlacement: labelPlacement)
        {
            if (boundValue != null)
            {
                this.boundValue = boundValue;
            }
            else if (value.HasValue)
            {
                _value = value.Value;
            }

            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }

            if (onClickAction != null)
            {
                OnClickAction = onClickAction;
            }

            if (toggleType != null)
            {
                this.ToggleType = (ToggleType)toggleType;
            }
        }
    }


}
