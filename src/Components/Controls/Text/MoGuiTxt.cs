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

    public class MoCaText : MoGCArgs
    {
        public new string Text;
        public TextElement Element;

        public MoCaText(Func<object> onUpdateAction,
            TextElement element = TextElement.text,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta )
        {
            Element = element;
            OnUpdateAction = onUpdateAction;
        }

        public MoCaText(string text,
            TextElement element = TextElement.text,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta)
        {
            Element = element;
            Text = text;
        }
    }


    public class TypographyMeta : ControlMeta
    {
        public Dictionary<TextElement, TextMeta> Type = new Dictionary<TextElement, TextMeta>();

        public TypographyMeta(string name) : base(name)
        {
            Defaults();
            MinSize(GuiMeta.DefaultTextMinSize);
        }

        public void Defaults()
        {
            var list = new string[]{ "text",
        "title",
        "h1",
        "h2",
        "h3",
        "h4",
        "h5",
        "h6",
        "label",
        "small"};
            foreach (var item in list)
            {
                var thing = new TextMeta().Element(item);
                Type.Add(thing.element, thing);
            }
            
            Type[TextElement.title].FontSize(Type[TextElement.text].fontSize + 14).Style(FontStyle.Bold).Anchor(TextAnchor.MiddleCenter);
            Type[TextElement.h1].FontSize(Type[TextElement.text].fontSize +12).Style(FontStyle.Bold).Anchor(TextAnchor.LowerCenter);
            Type[TextElement.h2].FontSize(Type[TextElement.text].fontSize + 10).Style(FontStyle.Bold).Anchor(TextAnchor.UpperLeft);
            Type[TextElement.h3].FontSize(Type[TextElement.text].fontSize +9).Style(FontStyle.Bold).Anchor(TextAnchor.UpperLeft);
            Type[TextElement.h4].FontSize(Type[TextElement.text].fontSize + 8).Style(FontStyle.Normal).Anchor(TextAnchor.UpperLeft);
            Type[TextElement.h5].FontSize(Type[TextElement.text].fontSize + 6).Style(FontStyle.Bold).Anchor(TextAnchor.UpperLeft);
            Type[TextElement.h6].FontSize(Type[TextElement.text].fontSize + 4).Style(FontStyle.Normal).Anchor(TextAnchor.UpperLeft);
            Type[TextElement.label].Style(FontStyle.Bold);
            Type[TextElement.small].FontSize(Type[TextElement.text].fontSize - 4).Style(FontStyle.Italic);
        }
    }

    public class TextMeta
    {
        
        public Font font = GuiMeta.DefaultFont;
        public int fontSize = GuiMeta.DefaultFontSize;
        public Color fontColor = GuiMeta.DefaultFontColor.Color;
        
        public TextElement element = TextElement.text;
        public TextAnchor textAnchor = TextAnchor.UpperLeft;
        public FontStyle textStyle = FontStyle.Normal;


        public TextMeta Element(string _element)
        {
            switch (_element)
            {

                case "h1":
                    element = TextElement.h1;
                    break;

                case "h2":
                    element = TextElement.h2;
                    break;

                case "h3":
                    element = TextElement.h3;
                    break;

                case "h4":
                    element = TextElement.h4;
                    break;

                case "h5":
                    element = TextElement.h5;
                    break;

                case "h6":
                    element = TextElement.h6;
                    break;

                case "label":
                    element = TextElement.label;
                    break;

                case "small":
                    element = TextElement.small;
                    break;

                case "title":
                    element = TextElement.title;
                    break;

                case "text":
                default:
                    element = TextElement.text;
                    break;
            }
            return this;
        }

        public TextMeta Element(TextElement _element)
        {
            element = _element;
            return this;
        }

        public TextMeta Anchor(TextAnchor _anchor)
        {
            textAnchor = _anchor;
            return this;
        }

        public TextMeta Style(FontStyle _style)
        {
            textStyle = _style;
            return this;
        }
        public TextMeta Font(string _font)
        {
            bool isvalid = false;
            foreach (var item in UnityEngine.Font.GetOSInstalledFontNames())
            {
                if(item == _font)
                {
                    isvalid = true; break;
                }
            }
            if (isvalid)
            {
                font = UnityEngine.Font.CreateDynamicFontFromOSFont(_font, 24);
            }
            else
            {
                font = GuiMeta.DefaultFont;
            }
                return this;
        }

        public TextMeta Font(Font _font)
        {
            font = _font;
            return this;
        }

        public TextMeta FontSize(int _fontsize)
        {
            fontSize = _fontsize;
            return this;
        }

        

        public TextMeta FontColor(Color _fontColor)
        {
            fontColor = _fontColor;
            return this;
        }

    }
}
