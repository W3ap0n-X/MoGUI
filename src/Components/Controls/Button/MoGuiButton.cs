using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGuiButton : MoGuiControl
    {
        public MoGuiTxt Text;
        public Image background;


        public MoGuiButton(MoGuiMeta meta, string name, Func<object> onUpdateAction, Action onClickAction) : base(meta, name)
        {
            Meta.FontSize = Meta.ButtonFontSize;
            
            Obj = CreateButton(onClickAction);
            AddText("ButtonText", onUpdateAction);
        }

        public MoGuiButton(MoGuiMeta meta, string name, string text, Action onClickAction) : base(meta, name)
        {
            Meta.FontSize = Meta.ButtonFontSize;
            Obj = CreateButton(onClickAction);
            AddText("ButtonText", text);
        }

        public MoGuiButton(MoGuiMeta meta, string name, MoCaButton args) : base(meta, name)
        {
            Meta.FontSize = Meta.ButtonFontSize;
            Obj = CreateButton(args.OnClickAction);

            if (args.Text != null)
            {
                AddText("ButtonText", args.Text);
            }
            else
            {
                AddText("ButtonText", Name);
            }
        }

        public override void SetLayout()
        {
            minWidth = Meta.ButtonSize.x;
            minHeight = Meta.ButtonSize.y;
            //preferredWidth = Meta.ButtonSize.z;
            //preferredHeight = Meta.ButtonSize.w;
            flexibleHeight = 1;
            flexibleWidth = 1;
        }

        //public override void _Init()
        //{
        //    Container = CreateContainer();
        //}
        public GameObject CreateButton(Action onClickAction)
        {
            GameObject buttonObject = new GameObject(PluginName + "_" + Name + "_" + "Button");

            background = buttonObject.AddComponent<Image>();
            background.color = MoGui.TestMeta.Button.background.Color;

            AddLayoutElement(buttonObject);
            SetLayout();

            Button buttonComponent = buttonObject.AddComponent<Button>();
            buttonComponent.onClick.AddListener(() => onClickAction?.Invoke());
            
            //buttonObject.transform.SetParent(Container.transform, false);
            return buttonObject;
        }

        public GameObject CreateButton(Action onClickAction, Vector2 size, Vector2 pos)
        {
            GameObject buttonObject = CreateButton(onClickAction);
            RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
            buttonRect.sizeDelta = size;
            buttonRect.anchoredPosition = pos;

            return buttonObject;
        }

        public void AddText(string label, object text)
        {
            if (Text != null)
            {
                Text.Update(text);
                Text.Obj.transform.SetParent(Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, text);
                Text.Obj.transform.SetParent(Obj.transform, false);
                //HorizontalLayoutGroup layoutGroup = Text.Container.GetComponent<HorizontalLayoutGroup>();
                //layoutGroup.childForceExpandHeight = true;
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                Text.minHeight = minHeight - (2 * MoGui.TestMeta.Margin);
                Text.minWidth = minWidth - (2 * MoGui.TestMeta.Margin);
            }

        }

        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (Text != null)
            {
                Text.Update(onUpdateAction());

                Text.Obj.transform.SetParent(Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Text.Obj.transform.SetParent(Obj.transform, false);
                //HorizontalLayoutGroup layoutGroup = Text.Container.GetComponent<HorizontalLayoutGroup>();
                //layoutGroup.childForceExpandHeight = true;
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                Text.minHeight = minHeight - (2* MoGui.TestMeta.Margin);
                Text.minWidth = minWidth - (2 * MoGui.TestMeta.Margin);
            }

        }


        public override void Update() => Text.Update();

        public void Background(Color _color)
        {
            background.color = _color;
        }
    }

    public class MoCaButton : MoGCArgs
    {

        public MoCaButton(Func<object> text,
            Action onClickAction,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiButton), meta, text: text)
        {
            OnClickAction = onClickAction;
        }

        public MoCaButton(object text,
            Action onClickAction,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiButton), meta, text: text)
        {
            OnClickAction = onClickAction;
        }
    }

    public class ButtonMeta : ControlMeta
    {
        Font font;
        int fontsize;
        public MoGuiColor background = new MoGuiColor(GuiMeta.DefaultPanelColor.Tint);
        // Vector2 minSize;
        // Vector2 Size;

        public ButtonMeta(string name) : base(name) { }

        public ButtonMeta Background(Color _color)
        {
            background = new MoGuiColor(_color);
            return this;
        }

        public ButtonMeta Background(MoGuiColor _color)
        {
            background = _color;
            return this;
        }
    }
}
