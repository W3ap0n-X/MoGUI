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

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSelector), meta:meta, text: text, orientation: orientation, labelPlacement: labelPlacement)
        {
            Options = options;
            Direction = direction;
        }

    }

}
