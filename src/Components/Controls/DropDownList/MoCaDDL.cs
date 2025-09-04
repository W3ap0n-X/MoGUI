using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoGUI
{

    public class MoCaDDL : MoGCArgs
    {

        public List<string> DDLOptions;
        public Dictionary<string, int> DDLBoundOptions = null;
        public MoCaDDL(List<string> dDLOptions,
            Action<object> onEditAction = null,
            string valType = "none",
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiDDL), meta, valType: valType)
        {
            if (onEditAction != null) { OnEditAction = onEditAction; }
            DDLOptions = dDLOptions;
        }

        public MoCaDDL(Dictionary<string, int> dDLBoundOptions,
            Action<object> onEditAction = null,
            string valType = "none",
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiDDL), meta, valType: valType)
        {
            if (onEditAction != null) { OnEditAction = onEditAction; }

            DDLBoundOptions = dDLBoundOptions;
        }

    }

    public class DDLMeta : ControlMeta
    {


        public MoGuiColor Color = GuiMeta.DefaultPanelColor;
        public MoGuiColor textColor = GuiMeta.DefaultFontColor;
        public DDLMeta(string name) : base(name) { }


    }
}