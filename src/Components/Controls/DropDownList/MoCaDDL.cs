using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace MoGUI
{

    public class MoCaDDL : MoGCArgs
    {

        public List<string> DDLOptions;
        public Dictionary<string, int> DDLBoundOptions = null;
        public MoCaDDL(List<string> dDLOptions,
            Action<object> onEditAction = null,
            string valType = "none",
            string text = null,
            Func<object> boundText = null,

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiDDL), meta, valType: valType, orientation: orientation, labelPlacement: labelPlacement)
        {
            if (onEditAction != null) { OnEditAction = onEditAction; }
            DDLOptions = dDLOptions;
            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }
        }

        public MoCaDDL(Dictionary<string, int> dDLBoundOptions,
            Action<object> onEditAction = null,
            string valType = "none",
            string text = null,
            Func<object> boundText = null,

            ControlOrientation? orientation = null,
            ControlLabelPlacement? labelPlacement = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiDDL), meta, valType: valType, orientation: orientation, labelPlacement: labelPlacement)
        {
            if (onEditAction != null) { OnEditAction = onEditAction; }

            DDLBoundOptions = dDLBoundOptions;
            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }
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