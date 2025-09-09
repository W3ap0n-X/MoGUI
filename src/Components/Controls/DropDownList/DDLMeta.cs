using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace MoGUI
{

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