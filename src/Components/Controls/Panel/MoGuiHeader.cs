using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace MoGUI
{
    
    public class MoGuiHeader : MoGuiPanel
    {
        private GameObject Canvas;
        private DraggableHandle DragHandle;


        public MoGuiHeader(MoGuiMeta meta, GameObject canvas, MoGuiPanel panel, string name, bool topLevel) : base(meta, name, panel, topLevel)
        {
            
            Canvas = canvas;
            
        }

        public override void _Init()
        {
            Container = CreateContainer();
        }

        public override void Init(bool topLevel)
        {
            Meta.PanelColor.Color = Meta.HeaderColor;

            Meta.FontSize = Meta.HeaderFontSize;
            Meta.ButtonFontSize = Meta.HeaderExitFontSize;
            Obj = CreatePanel();
            Container.transform.SetParent(Obj.transform, false);
            if (topLevel)
            {
                DragHandle = Obj.AddComponent<DraggableHandle>();
                DragHandle.BindParent(Panel.Obj);
                AddTitleText("HeaderText", PluginName + " - " + Panel.Title ?? Name);
                AddXButton("MinGui", "␣", () => MinGui());
                Meta.ButtonColor = Meta.HeaderExitColor;
                AddXButton("HideGui", "╳", () => ShowGui(false));
                GetButton("HideGui").Background(MoGui.TestMeta.Panel.Header.hideColor);
                AddResize();
            } else
            {
                AddTitleText("HeaderText", Panel.Title ?? Name);
                AddXButton("MinGui", "␣", () => MinGui());
            }
                is_init = true;
        }


        public override GameObject CreatePanel()
        {
            var panelObject = new GameObject(PluginName + "_" + Name + "_" + "HeaderPanel");

            Image panelImage = panelObject.AddComponent<Image>();
            panelImage.color = MoGui.TestMeta.Panel.background.Shade;
            RectTransform panelRect = panelObject.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 1);
            panelRect.anchorMax = new Vector2(1, 1);
            panelRect.offsetMin = new Vector2(0, -MoGui.TestMeta.Panel.Header.size);
            panelRect.offsetMax = new Vector2(0, 0);


            AddLayoutElement(panelObject);
            SetLayout();
            

            return panelObject;
        }

        public override GameObject CreateContainer()
        {

            GameObject layoutObject = new GameObject(PluginName + "_" + Name + "_" + "HeaderContainer");

            HorizontalLayoutGroup headerLayoutGroup = layoutObject.AddComponent<HorizontalLayoutGroup>();
            headerLayoutGroup.childControlWidth = true;
            headerLayoutGroup.childControlHeight = true;
            headerLayoutGroup.childForceExpandHeight = false;
            headerLayoutGroup.childForceExpandWidth = false;
            headerLayoutGroup.padding = new RectOffset(MoGui.TestMeta.Margin, MoGui.TestMeta.Margin, MoGui.TestMeta.Margin, MoGui.TestMeta.Margin);
            headerLayoutGroup.spacing = MoGui.TestMeta.Margin;

            
            RectTransform layoutRect = layoutObject.GetComponent<RectTransform>();
            layoutRect.anchorMin = Vector2.zero;
            layoutRect.anchorMax = Vector2.one;
            layoutRect.offsetMin = new Vector2(0, 0);
            layoutRect.offsetMax = new Vector2(0, 0);
            return layoutObject;

        }

        public override void SetLayout()
        {
            minHeight = MoGui.TestMeta.Panel.Header.size;
            minWidth = 100;
            //preferredWidth = 250;
            flexibleWidth = 1;
        }

        new public void ShowGui(bool show)
        {
            Canvas.SetActive(show);
        }

        public void MinGui()
        {
            Panel.Container.SetActive(Panel.Container.activeSelf ? false : true);
        }

        public void AddXButton(string label, string text, Action onUpdateAction)
        {
            MoGuiButton newTxt;
            if (Components.ContainsKey(label))
            {
                newTxt = (MoGuiButton)Components[label];

                newTxt.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                newTxt = new MoGuiButton(Meta, Name + "_" + label, text, onUpdateAction);
                newTxt.Obj.transform.SetParent(Container.transform, false);

                var width = MoGui.TestMeta.Panel.Header.size - 2 * MoGui.TestMeta.Margin;
                var height = MoGui.TestMeta.Panel.Header.size - 2 * MoGui.TestMeta.Margin;

                newTxt.minWidth = width;
                newTxt.minHeight = height;
                
                newTxt.preferredHeight = width;
                newTxt.preferredWidth = width;

                newTxt.Text.minWidth = width - 2 * MoGui.TestMeta.Margin;
                newTxt.Text.minHeight = width - 2 * MoGui.TestMeta.Margin;

                newTxt.Text.preferredHeight = width - 2 * MoGui.TestMeta.Margin;
                newTxt.Text.preferredWidth = width - 2 * MoGui.TestMeta.Margin;

                newTxt.Text.Text.fontSize = MoGui.TestMeta.Panel.Header.btFontSize;
                newTxt.flexibleWidth = 0.1f;
                newTxt.Text.flexibleWidth = 0.1f;
                Components.Add(label, newTxt);

            }

        }

        public void AddTitleText(string label, object text)
        {
            MoGuiTxt newTxt;
            if (Components.ContainsKey(label))
            {
                newTxt = (MoGuiTxt)Components[label];
                newTxt.Update(text);
                newTxt.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                newTxt = new MoGuiTxt(Meta, Name + "_" + label, text);
                newTxt.Obj.transform.SetParent(Container.transform, false);
                newTxt.Text.alignment = TextAnchor.MiddleLeft;
                newTxt.Text.fontSize = MoGui.TestMeta.Panel.Header.titleFontSize;
                LayoutElement titleLayoutElement = newTxt.Obj.AddComponent<LayoutElement>();
                titleLayoutElement.flexibleWidth = 1;
                Components.Add(label, newTxt);
            }

        }


        public void AddResize()
        {
            GameObject resizeIconObject = new GameObject("ResizeIcon");
            resizeIconObject.transform.SetParent(Container.transform, false);
            

            LayoutElement resizeLayoutElement = resizeIconObject.AddComponent<LayoutElement>();
            
            resizeLayoutElement.minWidth = MoGui.TestMeta.Panel.Header.size - 2 * MoGui.TestMeta.Margin;
            resizeLayoutElement.minHeight = MoGui.TestMeta.Panel.Header.size - 2 * MoGui.TestMeta.Margin;

            Text dragSymbol = resizeIconObject.AddComponent<Text>();
            dragSymbol.text = "❏";
            dragSymbol.font = Meta.Font;
            dragSymbol.fontSize = Meta.HeaderExitFontSize;
            dragSymbol.color = Meta.FontColor;
            dragSymbol.alignment = TextAnchor.MiddleCenter;

            ResizableUI resizeHandle = resizeIconObject.AddComponent<ResizableUI>();
            resizeHandle.BindParent(Panel.Obj);
        }

    }

    public class HeaderMeta
    {
        public int size = 40;

        public MoGuiColor Color;

        public HeaderMeta(MoGuiColor color)
        {
            Color = new MoGuiColor(color.Shade);
        }

        public HeaderMeta(Color color)
        {
            Color = new MoGuiColor(new MoGuiColor(color).Shade);
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

        public Color hideColor = GuiMeta.DefaultHeaderExitColor;
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
