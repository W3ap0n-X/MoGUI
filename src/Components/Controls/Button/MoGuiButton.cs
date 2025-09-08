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

        public bool interactible
        {
            get { return Obj.GetComponent<Button>().interactable; }
            set { Obj.GetComponent<Button>().interactable = value; }
        }

        public MoGuiButton(MoGuiMeta meta, string name, Func<object> onUpdateAction, Action onClickAction) : base(meta, name)
        {
            
            Obj = CreateButton(onClickAction);
            AddText("ButtonText", onUpdateAction);
        }

        public MoGuiButton(MoGuiMeta meta, string name, string text, Action onClickAction) : base(meta, name)
        {
            Obj = CreateButton(onClickAction);
            AddText("ButtonText", text);
        }

        public MoGuiButton(MoGuiMeta meta, string name, MoCaButton args) : base(meta, name, args)
        {
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

            minWidth = Meta.Button.sizing.minWidth;
            minHeight = Meta.Button.sizing.minHeight;
            if (Meta.Button.sizing.preferredWidth != null) { preferredWidth = (float)Meta.Button.sizing.preferredWidth; }
            if (Meta.Button.sizing.preferredHeight != null) { preferredHeight = (float)Meta.Button.sizing.preferredHeight; }
            flexibleWidth = Meta.Button.sizing.flexibleWidth ?? 0;
            flexibleHeight = Meta.Button.sizing.flexibleHeight ?? 0;
        }

        public GameObject CreateButton(Action onClickAction)
        {
            GameObject buttonObject = new GameObject(PluginName + "_" + Name + "_" + "Button");

            background = buttonObject.AddComponent<Image>();
            background.color = Meta.Button.background.Color;

            AddLayoutElement(buttonObject);
            SetLayout();

            Button buttonComponent = buttonObject.AddComponent<Button>();
            buttonComponent.onClick.AddListener(() => onClickAction?.Invoke());

            ColorBlock stateColors = buttonComponent.colors;
            stateColors.highlightedColor = Meta.Button.background.Tint;
            stateColors.disabledColor = Meta.Button.background.Shade;
            
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
                Text = new MoGuiTxt(Meta, Name + "_" + label, text:text, Meta.Button.labelSettings);
                Text.Obj.transform.SetParent(Obj.transform, false);
                Text.minHeight = minHeight - (2 * Meta.Margin);
                Text.minWidth = minWidth - (2 * Meta.Margin);
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
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction, Meta.Button.labelSettings);
                Text.Obj.transform.SetParent(Obj.transform, false);
                Text.minHeight = minHeight - (2* Meta.Margin);
                Text.minWidth = minWidth - (2 * Meta.Margin);
            }

        }


        public override void Update() => Text.Update();

        public void Background(Color _color)
        {
            background.color = _color;
        }
    }

}
