using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


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
            Obj = CreatePanel();
            Container.transform.SetParent(Obj.transform, false);
            
            if (topLevel)
            {
                DragHandle = Obj.AddComponent<DraggableHandle>();
                DragHandle.BindParent(Panel.Obj);
                AddTitleText("HeaderText", PluginName + " - " + Panel.Title ?? Name);
                AddXButton("MinGui", "␣", () => MinGui());
                
                var hide = AddXButton("HideGui", "╳", () => ShowGui(false));
                GetButton("HideGui").Background(Meta.Panel.Header.hideColor);
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
            panelImage.color = Meta.Panel.Header.Color.Color;
            RectTransform panelRect = panelObject.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 1);
            panelRect.anchorMax = new Vector2(1, 1);
            panelRect.offsetMin = new Vector2(0, -Meta.Panel.Header.size);
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
            headerLayoutGroup.padding = new RectOffset(Meta.Margin, Meta.Margin, Meta.Margin, Meta.Margin);
            headerLayoutGroup.spacing = Meta.Margin;
            
            RectTransform layoutRect = layoutObject.GetComponent<RectTransform>();
            layoutRect.anchorMin = Vector2.zero;
            layoutRect.anchorMax = Vector2.one;
            layoutRect.offsetMin = new Vector2(0, 0);
            layoutRect.offsetMax = new Vector2(0, 0);
            return layoutObject;
        }

        public override void SetLayout()
        {
            minHeight = Meta.Panel.Header.size;
            minWidth = 100;
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

        public MoGuiButton AddXButton(string label, string text, Action onUpdateAction)
        {
            MoGuiMeta btMeta = new MoGuiMeta(Meta, "headerButtons");
            btMeta.Button.labelSettings = Meta.Panel.Header.buttonSettings;
            MoGuiButton newTxt;
            if (Components.ContainsKey(label))
            {
                newTxt = (MoGuiButton)Components[label];
                newTxt.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                newTxt = new MoGuiButton(btMeta, Name + "_" + label, text, onUpdateAction);
                newTxt.Obj.transform.SetParent(Container.transform, false);

                var width = Meta.Panel.Header.size - 2 * Meta.Margin;
                var height = Meta.Panel.Header.size - 2 * Meta.Margin;

                newTxt.minWidth = width;
                newTxt.minHeight = height;
                
                newTxt.preferredHeight = width;
                newTxt.preferredWidth = width;

                newTxt.Text.minWidth = width - 2 * Meta.Margin;
                newTxt.Text.minHeight = width - 2 * Meta.Margin;

                newTxt.Text.preferredHeight = width - 2 * Meta.Margin;
                newTxt.Text.preferredWidth = width - 2 * Meta.Margin;

                newTxt.flexibleWidth = 0.1f;
                newTxt.Text.flexibleWidth = 0.1f;
                Components.Add(label, newTxt);
            }
            return newTxt;
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
                newTxt = new MoGuiTxt(Meta, Name + "_" + label, text:text , Meta.Panel.Header.titleSettings);
                newTxt.Obj.transform.SetParent(Container.transform, false);
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
            
            resizeLayoutElement.minWidth = Meta.Panel.Header.size - 2 * Meta.Margin;
            resizeLayoutElement.minHeight = Meta.Panel.Header.size - 2 * Meta.Margin;

            Text dragSymbol = resizeIconObject.AddComponent<Text>();
            dragSymbol.text = "❏";
            dragSymbol.font = Meta.Panel.Header.buttonSettings.FontFace;
            dragSymbol.fontSize = Meta.Panel.Header.buttonSettings.FontSize;
            dragSymbol.color = Meta.Panel.Header.buttonSettings.FontColor;
            dragSymbol.alignment = Meta.Panel.Header.buttonSettings.Alignment;

            ResizableUI resizeHandle = resizeIconObject.AddComponent<ResizableUI>();
            resizeHandle.BindParent(Panel.Obj);
        }

    }

    public class HeaderMeta
    {
        public int size = 40;

        public MoGuiColor Color;

        public TypographySettings titleSettings = new TypographySettings(18, 1, FontStyle.Bold, TextAnchor.MiddleLeft, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
        public TypographySettings buttonSettings = new TypographySettings(18, 1, FontStyle.Bold, TextAnchor.MiddleCenter, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);
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

        public Color hideColor = MoGuiMeta.DefaultHeaderExitColor;
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
