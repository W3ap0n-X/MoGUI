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
            layoutGroup.padding = new RectOffset(Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin);
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.spacing = Meta.TxtMargin;

            if (MoGUIManager._LayoutDebug)
            {
                Image bg = layoutObject.AddComponent<Image>();
                bg.color = MoGUIManager._LayoutDebugColColor;
            }

            if (MoGui.TestMeta.Cols.background != null)
            {
                Image bg = layoutObject.AddComponent<Image>();
                bg.color = (Color)MoGui.TestMeta.Cols.background;
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
            MinSize(GuiMeta.DefaultColMinSize);
            FlexSize(GuiMeta.DefaultColFlex);
        }

    }

    public abstract class MoGuiLayoutBrick
    {
        public GameObject Obj;
        public LayoutWrapper LoElement;
        public MoGuiMeta Meta;
        public string Name { get; private set; }
        public MoGuiLayoutBrick(string name)
        {
            Name = name;
        }

        protected void AddLayoutElement(GameObject obj)
        {
            LoElement = new LayoutWrapper(obj.AddComponent<LayoutElement>());
        }

        protected abstract GameObject Create(string name);

        public abstract void SetLayout();

        public float minHeight { get => LoElement.minHeight; set { LoElement.minHeight = value; } }
        public float minWidth { get => LoElement.minWidth; set { LoElement.minWidth = value; } }
        public float flexibleWidth { get => LoElement.flexibleWidth; set { LoElement.flexibleWidth = value; } }
        public float flexibleHeight { get => LoElement.flexibleHeight; set { LoElement.flexibleHeight = value; } }
        public float preferredWidth { get => LoElement.preferredWidth; set { LoElement.preferredWidth = value; } }
        public float preferredHeight { get => LoElement.preferredHeight; set { LoElement.preferredHeight = value; } }

        public float preferredWidthPercentage;
        public float preferredHeightPercentage;
        private RectTransform parentRect;
        protected void SetParentRect(RectTransform parent)
        {
            parentRect = parent;
        }

        public void SetPreferredWidthPercentage(float percentage)
        {
            preferredWidthPercentage = percentage;
        }
        public void SetPreferredHeightPercentage(float percentage)
        {
            preferredHeightPercentage = percentage;
        }

        public void UpdateLayout()
        {
            if (preferredWidthPercentage > 0 && parentRect != null)
            {
                preferredWidth = (parentRect.rect.width - (Meta.TxtMargin*2)) * (preferredWidthPercentage / 100f);
            }
            if (preferredHeightPercentage > 0 && parentRect != null)
            {
                preferredHeight = (parentRect.rect.height - (Meta.TxtMargin * 2)) * (preferredHeightPercentage / 100f);
            }
        }

        public virtual void Update()
        {
            UpdateLayout();
        }
    }

    public class LayoutWrapper
    {
        LayoutElement Element;
        public float minHeight { get => Element.minHeight; set { Element.minHeight = value; } }
        public float minWidth { get => Element.minWidth; set { Element.minWidth = value; } }
        public float flexibleWidth { get => Element.flexibleWidth; set { Element.flexibleWidth = value; } }
        public float flexibleHeight { get => Element.flexibleHeight; set { Element.flexibleHeight = value; } }
        public float preferredWidth { get => Element.preferredWidth; set { Element.preferredWidth = value; } }
        public float preferredHeight { get => Element.preferredHeight; set { Element.preferredHeight = value; } }

        public LayoutWrapper(LayoutElement element)
        {
            Element = element;
        }
    }

    public class SizeWrapper
    {
        RectTransform Rect;
        public Vector2 anchorMin { get => Rect.anchorMin; set { Rect.anchorMin = value; } }
        public Vector2 anchorMax { get => Rect.anchorMax; set { Rect.anchorMax = value; } }
        public Vector2 offsetMin { get => Rect.offsetMin; set { Rect.offsetMin = value; } }
        public Vector2 offsetMax { get => Rect.offsetMax; set { Rect.offsetMax = value; } }

        public Vector2 sizeDelta { get => Rect.sizeDelta; set { Rect.sizeDelta = value; } }
        public Vector2 anchoredPosition { get => Rect.anchoredPosition; set { Rect.anchoredPosition = value; } }
        public Vector2 pivot { get => Rect.pivot; set { Rect.pivot = value; } }

        public SizeWrapper(RectTransform rect)
        {
            Rect = rect;
        }
    }

    public class LayoutMeta : BlockMeta
    {

        public Color? background = null;
        TextAnchor childAlignment = TextAnchor.UpperLeft;
        public LayoutMeta(string name) : base(name) 
        {

        }
        public LayoutMeta Alignment(TextAnchor _alignment)
        {
            childAlignment = _alignment;
            return this;
        }

        public LayoutMeta Background(Color? _color = null)
        {
            background = _color;
            return this;
        }

    }

    public class BlockMeta : ComponentMeta
    {
        
        public Vector2 minSize;
        public Vector2 size;
        public Vector2 Flex;

        public BlockMeta(string name) : base(name) { }



        public BlockMeta Size(Vector2 _size)
        {
            size = _size;
            return this;
        }

        public BlockMeta MinSize(Vector2 _size)
        {
            minSize = _size;
            return this;
        }
        public BlockMeta FlexSize(Vector2 _size)
        {
            Flex = _size;
            return this;
        }

        
    }
}
