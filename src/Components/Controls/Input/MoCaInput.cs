using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

    public class MoCaInput : MoGCArgs
    {
        public object Value { get; set; }
        public MoCaInput(Action<object> onEditAction,
            Func<object> onUpdateAction,
            Func<object> text,
            string valType = "none",

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiInput), meta:meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType, orientation: orientation, labelPlacement: labelPlacement)
        {

        }

        public MoCaInput(Action<object> onEditAction,
            Func<object> onUpdateAction,
            object text = null,
            string valType = "none",

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiInput), meta:meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType, orientation: orientation, labelPlacement: labelPlacement)
        {

        }

        public MoCaInput(object value,
            
            string valType = "none",
            object text = null,
            Action<object> onEditAction = null,
            Func<object> onUpdateAction = null,

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiInput), meta:meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType, orientation: orientation, labelPlacement: labelPlacement)
        {
            Value = value;
        }

    }


}
