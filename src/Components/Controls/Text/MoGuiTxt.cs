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
        public string Value => Text.text;
        TextElement Element = TextElement.text;

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
            Element = args.Element;
            
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
        }

        public GameObject CreateText(string text = null)
        {
            var textObject = new GameObject(PluginName + "_" + Name + "_" + "Text");
            //textObject.transform.SetParent(Container.transform, false);

            Text = textObject.AddComponent<Text>();
            Text.text = text != null ? text : "";
            //Text.horizontalOverflow = HorizontalWrapMode.Overflow;
            switch (Element)
            {
                //case TextElement.title:
                //    break;
                //case TextElement.h1:
                //    break;
                //case TextElement.h2:
                //    break;
                //case TextElement.h3:
                //    break;
                //case TextElement.h4:
                //    break;
                //case TextElement.h5:
                //    break;
                //case TextElement.label:
                //    break;
                //case TextElement.small:
                //    break;
                //case TextElement.text:
                default:
                    Text.font = Meta.Font;
                    Text.fontSize = Meta.FontSize;
                    Text.color = Meta.FontColor;
                    break;
            }
            RectTransform labelRect = textObject.GetComponent<RectTransform>();
            labelRect.anchoredPosition = new Vector2(0, 0);
            labelRect.anchorMin = new Vector2(0, 0);
            labelRect.anchorMax = new Vector2(1, 1);
            labelRect.offsetMin = new Vector2(Meta.TxtMargin, 0);

            labelRect.offsetMax = new Vector2(-Meta.TxtMargin, 0);

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

    public class MoCaText : MoGCArgs
    {
        public new string Text;
        public TextElement Element;

        public MoCaText(Func<object> onUpdateAction,
            TextElement element = TextElement.text,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta )
        {
            OnUpdateAction = onUpdateAction;
        }

        public MoCaText(string text,
            TextElement element = TextElement.text,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta)
        {
            Text = text;
        }
    }
}
