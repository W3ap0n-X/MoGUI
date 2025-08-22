using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGuiTxt : MoGuiControl
    {
        public Text Text;
        string rawText = "<text>";

        public MoGuiTxt(MoGuiMeta meta, string name, Func<object> onUpdateAction) : base(meta, name)
        {

            OnUpdateAction = onUpdateAction;
            Obj = CreateText(OnUpdateAction().ToString());
        }

        public MoGuiTxt(MoGuiMeta meta, string name, object text) : base(meta, name)
        {
            Obj = CreateText(text.ToString());
        }

        public GameObject CreateText(string text = null)
        {
            var textObject = new GameObject(PluginName + "_" + Name + "_" + "Text");
            textObject.transform.SetParent(Container.transform, false);

            Text = textObject.AddComponent<Text>();
            Text.text = text != null ? text : rawText;
            Text.font = Meta.Font;
            Text.fontSize = Meta.FontSize;
            Text.color = Meta.FontColor;
            return textObject;
        }

        public GameObject CreateText(object text, Vector2 size, Vector2 pos)
        {
            rawText = text.ToString();
            var textObject = CreateText();
            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.sizeDelta = size;
            textRect.anchoredPosition = pos;
            return textObject;
        }

        public void UpdateText(object val)
        {
            Text.text = val.ToString();
        }

        public override void UpdateText()
        {
            if (OnUpdateAction != null)
            {
                UpdateText(OnUpdateAction());
            }

        }

        public void SetFontSize(int size)
        {
            Text.fontSize = size;
        }

    }

    public class MoCaText : MoGCArgs
    {
        public new string Text;
        

        public MoCaText(Func<object> text,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta )
        {
            OnUpdateAction = text;
        }

        public MoCaText(string text,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta)
        {
            Text = text;
        }
    }
}
