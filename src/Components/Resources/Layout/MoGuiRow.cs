using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Linq;

namespace MoGUI
{
    public class MoGuiRow : MoGuiLayoutBrick
    {
        public Dictionary<string, MoGuiCol> Columns;
        private MoGuiPanel _parent;
        public MoGuiRow(MoGuiPanel parent, string name, MoGuiMeta meta = null) : base(name)
        {
            _parent = parent;
            if (meta == null)
            {
                Meta = _parent.Meta;
            } else
            {
                Meta = meta;
            }

            Columns = new Dictionary<string, MoGuiCol>();
            Obj = Create(name);
            AddLayoutElement(Obj);
            SetLayout();
            SetParentRect(_parent.Container.GetComponent<RectTransform>());
        }
        protected override GameObject Create(string name)
        {
            GameObject layoutObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "Row_" + name);
            HorizontalLayoutGroup layoutGroup = layoutObject.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.padding = new RectOffset(Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin);
            layoutGroup.spacing = Meta.TxtMargin;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;


            if (MoGUIManager._LayoutDebug)
            {
                Image bg = layoutObject.AddComponent<Image>();
                bg.color = MoGUIManager._LayoutDebugRowColor;
            }
            if (MoGui.TestMeta.Rows.background != null)
            {
                Image bg = layoutObject.AddComponent<Image>();
                bg.color = (Color)MoGui.TestMeta.Rows.background;
            }

            return layoutObject;
        }

        public MoGuiCol AddColumn(string name)
        {
            MoGuiCol newCol = new MoGuiCol(this, name);
            newCol.Obj.transform.SetParent(Obj.transform);
            Columns.Add(name, newCol);
            return newCol;
        }

        public MoGuiCol GetCol(string columnName)
        {
            MoGuiCol column;
            if (Columns.ContainsKey(columnName))
            {
                column = Columns[columnName];
            }
            else
            {
                column = AddColumn(columnName);
            }
            return column;
        }

        public override void SetLayout()
        {
            minHeight = 10;
            minWidth = 10;
            flexibleHeight = 1;
            flexibleWidth = 1;
        }

        public override void Update()
        {
            UpdateLayout();
            foreach (var item in Columns)
            {
                item.Value.Update();
            }
        }
    }

    public class RowMeta : LayoutMeta
    {

        public RowMeta(string name) : base(name)
        {
            MinSize(GuiMeta.DefaultRowMinSize);
            FlexSize(GuiMeta.DefaultRowFlex);
        }

    }

}
