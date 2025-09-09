using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



namespace MoGUI
{
    
    

    public class HeaderMeta
    {
        MoGuiMeta _parent;
        public int size = 40;

        public MoGuiColor Color;

        public TypographySettings titleSettings;
        public TypographySettings buttonSettings;


        public HeaderMeta(MoGuiMeta parent)
        {
            _parent = parent;
            titleSettings = new TypographySettings(18, FontStyle.Bold, TextAnchor.MiddleLeft, _parent.Colors.Text.Color, _parent.font);
            buttonSettings = new TypographySettings(18, FontStyle.Bold, TextAnchor.MiddleCenter, _parent.Colors.Text.Color, _parent.font);
            Color = _parent.Colors.Header;
        }

        public HeaderMeta Size(int _size)
        {
            size = _size;
            return this;
        }
        public int titleFontSize = 18;

        public HeaderMeta TitleFontSize(int _size)
        {
            titleFontSize = _size;
            return this;
        }
        public int btFontSize = 18;

        public HeaderMeta BtFontSize(int _size)
        {
            btFontSize = _size;
            return this;
        }
        public Color background;
        public HeaderMeta Background(Color _color)
        {
            background = _color;
            return this;
        }

        public Color hideColor = new Color(0.75f, 0.25f, 0.25f, 1f);
        public HeaderMeta HhideColor(Color _color)
        {
            hideColor = _color;
            return this;
        }

        public Color minColor;
        public HeaderMeta MinColor(Color _color)
        {
            minColor = _color;
            return this;
        }
    }

}
