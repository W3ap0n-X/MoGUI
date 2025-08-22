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

        public GameObject CreateButton(Action onClickAction)
        {
            GameObject buttonObject = new GameObject(PluginName + "_" + Name + "_" + "Button");

            Image buttonImage = buttonObject.AddComponent<Image>();
            buttonImage.color = Meta.ButtonColor;

            LayoutElement layoutElement = buttonObject.AddComponent<LayoutElement>();
            layoutElement.minWidth = Meta.ButtonSize.x;
            layoutElement.minHeight = Meta.ButtonSize.y;
            layoutElement.preferredWidth = Meta.ButtonSize.z;
            layoutElement.preferredHeight = Meta.ButtonSize.w;

            Button buttonComponent = buttonObject.AddComponent<Button>();
            buttonComponent.onClick.AddListener(() => onClickAction?.Invoke());

            buttonObject.transform.SetParent(Container.transform, false);
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
                Text.UpdateText(text);
                Text.Container.transform.SetParent(Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, text);
                Text.Container.transform.SetParent(Obj.transform, false);
                HorizontalLayoutGroup layoutGroup = Text.Container.GetComponent<HorizontalLayoutGroup>();
                layoutGroup.childForceExpandHeight = true;
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            }

        }

        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (Text != null)
            {
                Text.UpdateText(onUpdateAction());

                Text.Container.transform.SetParent(Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Text.Container.transform.SetParent(Obj.transform, false);
                HorizontalLayoutGroup layoutGroup = Text.Container.GetComponent<HorizontalLayoutGroup>();
                layoutGroup.childForceExpandHeight = true;
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            }

        }


        public override void UpdateText() => Text.UpdateText();
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
}
