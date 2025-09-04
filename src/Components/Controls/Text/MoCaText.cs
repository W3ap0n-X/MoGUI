using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Linq;

namespace MoGUI
{

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
