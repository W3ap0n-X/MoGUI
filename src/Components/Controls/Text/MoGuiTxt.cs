using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Linq;

namespace MoGUI
{
    public class MoGuiTxt : MoGuiControl
    {
        public Text Text;
        public string Value => Text.text;
        TextElement TxtElement = TextElement.text;

        public MoGuiTxt(MoGuiMeta meta, string name, Func<object> onUpdateAction) : base(meta, name)
        {

            OnUpdateAction = onUpdateAction;
            Obj = CreateText(OnUpdateAction().ToString());
        }

        public MoGuiTxt(MoGuiMeta meta, string name, object text) : base(meta, name)
        {
            Obj = CreateText(text.ToString());
        }

        public MoGuiTxt(MoGuiMeta meta, string name, MoCaText args) : base(meta, name)
        {
            TxtElement = args.Element;
            
            if (args.OnUpdateAction != null)
            {
                OnUpdateAction = args.OnUpdateAction;
                Obj = CreateText(OnUpdateAction().ToString());
            }
            else
            {
                Obj = CreateText(args.Text);
            }

        }

        public override void SetLayout()
        {
            minWidth = 20;
            minHeight = 20;

            flexibleHeight = 1 ; flexibleWidth = 1;
        }

        public GameObject CreateText(string text = null)
        {
            var textObject = new GameObject(PluginName + "_" + Name + "_" + "Text");

            Text = textObject.AddComponent<Text>();
            Text.text = text != null ? text : "";
            switch (TxtElement)
            {
                default:
                    Text.font = MoGui.TestMeta.Text.Type[TxtElement].font;
                    Text.fontSize = MoGui.TestMeta.Text.Type[TxtElement].fontSize;
                    Text.color = MoGui.TestMeta.Text.Type[TxtElement].fontColor;
                    Text.fontStyle = MoGui.TestMeta.Text.Type[TxtElement].textStyle;
                    Text.alignment = MoGui.TestMeta.Text.Type[TxtElement].textAnchor;
                    break;
            }
            RectTransform labelRect = textObject.GetComponent<RectTransform>();
            labelRect.anchoredPosition = new Vector2(0, 0);
            labelRect.anchorMin = new Vector2(0, 0);
            labelRect.anchorMax = new Vector2(1, 1);
            labelRect.offsetMin = new Vector2(MoGui.TestMeta.Margin, 0);

            labelRect.offsetMax = new Vector2(-MoGui.TestMeta.Margin, 0);

            AddLayoutElement(textObject);
            SetLayout();
            //layoutElement.preferredHeight = 380;

            return textObject;
        }

        public void Update(object val)
        {
            Text.text = val.ToString();
        }

        public override void Update()
        {
            if (OnUpdateAction != null)
            {
                Update(OnUpdateAction());
            }

        }

        public void SetFontSize(int size)
        {
            Text.fontSize = size;
        }

    }


}
