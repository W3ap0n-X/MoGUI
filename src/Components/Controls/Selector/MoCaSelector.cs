using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

    public class MoCaSelector : MoGCArgs
    {
        public Dictionary<string, object> Options;
        public MoCaSelector(Dictionary<string, object> options,
            string text,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSelector), meta:meta, text: text)
        {
            Options = options;
        }


    }

    public class SelectorMeta : ControlMeta
    {

        public SelectorMeta(string name) : base(name) { }


    }
}
