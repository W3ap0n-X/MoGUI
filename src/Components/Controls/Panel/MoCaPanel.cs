using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


namespace MoGUI
{

    public class MoCaPanel : MoGCArgs
    {
        public bool IncludeHeader;
        public string Title;
        public MoCaPanel(bool includeHeader = false,
            string title = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiPanel), meta)
        {
            IncludeHeader = includeHeader;
            if (title != null)
            {
                Title = title;
            }
        }
    }
    
    public class PanelMeta : ComponentMeta
    {
        public MoGuiColor background;
        public Vector2 size;
        public TextAnchor childAlignment = TextAnchor.UpperLeft;

        public HeaderMeta Header;

        public PanelMeta(MoGuiMeta parent, string name) : base(parent, name) 
        {
            background = _parent.Colors.Panel;
            SetHeader();
        }

        public PanelMeta Alignment(TextAnchor _alignment)
        {
            childAlignment = _alignment;
            return this;
        }

        public PanelMeta Background(Color _color)
        {
            background = new MoGuiColor(_color);
            return this;
        }

        public PanelMeta Background(MoGuiColor _color)
        {
            background = _color;
            return this;
        }

        public PanelMeta Size(Vector2 _size)
        {
            size = _size;
            return this;
        }
        public void SetHeader()
        {
            Header = new HeaderMeta(_parent);
        }

    }

    

}
