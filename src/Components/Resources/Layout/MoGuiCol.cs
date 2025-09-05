using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Linq;

namespace MoGUI
{

    public class MoGuiCol : MoGuiLayoutBrick
    {
        private MoGuiRow _parent;
        
        public MoGuiCol(MoGuiRow parent, string name, MoGuiMeta meta = null) : base(name)
        {
            _parent = parent;
            if (meta == null)
            {
                Meta = _parent.Meta;
            }
            else
            {
                Meta = meta;
            }
            Obj = Create(name);
            AddLayoutElement(Obj);
            SetLayout();
            SetParentRect(_parent.Obj.GetComponent<RectTransform>());
        }

        protected override GameObject Create(string name)
        {
            GameObject layoutObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + _parent.Name + "_" + "Column_" + name);
            VerticalLayoutGroup layoutGroup = layoutObject.AddComponent<VerticalLayoutGroup>();
            layoutGroup.padding = new RectOffset(Meta.Margin, Meta.Margin, Meta.Margin, Meta.Margin);
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.spacing = Meta.Margin;
            if (Meta.Cols.background != null)
            {
                Image bg = layoutObject.AddComponent<Image>();
                bg.color = (Color)Meta.Cols.background;
            }
            return layoutObject;
        }

        public override void SetLayout()
        {
            minHeight = 10;
            minWidth = 10;
            flexibleWidth = 1;
            flexibleHeight = 1;
        }

    }

    public class ColMeta : LayoutMeta
    {
        public ColMeta(string name) : base(name)
        {
            MinSize(MoGuiMeta.DefaultColMinSize);
            FlexSize(MoGuiMeta.DefaultColFlex);
        }

    }
    
}
