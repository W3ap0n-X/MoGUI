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
        private SizeSettings _sizeSettings = new SizeSettings(40, 30, 1, 1);
        public TypographySettings labelSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, FontStyle.Bold, TextAnchor.MiddleLeft, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
        public TypographySettings listSettings = new TypographySettings(MoGuiMeta.DefaultFontSize, FontStyle.Normal, TextAnchor.MiddleLeft, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
        public MoGuiColor background = new MoGuiColor(MoGuiMeta.DefaultPanelColor.TintRaw, 0.6f);
        public MoGuiColor textColor = MoGuiMeta.DefaultFontColor;
        public DDLMeta(MoGuiMeta parent, string name) : base(parent, name)
        {
            Sizing(_sizeSettings);
        }

    }
}