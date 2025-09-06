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
        public TypographySettings labelSettings;
        public TypographySettings listSettings;
        public MoGuiColor background;
        public DDLMeta(MoGuiMeta parent, string name) : base(parent, name)
        {
            background = new MoGuiColor(_parent.Colors.Control.Color);
            labelSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleLeft, _parent.Colors.Text.Color, _parent.font);
            listSettings = new TypographySettings(_parent.fontSize, FontStyle.Bold, TextAnchor.MiddleLeft, _parent.Colors.Text.Color, _parent.font);
            Sizing(_sizeSettings);
        }

    }
}