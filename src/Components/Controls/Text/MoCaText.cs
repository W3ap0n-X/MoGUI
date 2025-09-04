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
        ) : base(typeof(MoGuiTxt), meta)
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
        private SizeSettings _sizeSettings = new SizeSettings(20,20, 1,1);
        public Dictionary<TextElement, TextMeta> Type = new Dictionary<TextElement, TextMeta>();
        public Dictionary<string, TypographyDefaults> elementSettings = new Dictionary<string, TypographyDefaults>()
        {
            { "text", new TypographyDefaults(1, FontStyle.Normal, TextAnchor.UpperLeft) },
            { "title", new TypographyDefaults(2, FontStyle.Bold, TextAnchor.MiddleCenter) },
            { "h1", new TypographyDefaults(1.8f, FontStyle.Bold, TextAnchor.LowerCenter) },
            { "h2", new TypographyDefaults(1.6f, FontStyle.Bold, TextAnchor.UpperLeft) },
            { "h3", new TypographyDefaults(1.5f, FontStyle.Bold, TextAnchor.UpperLeft) },
            { "h4", new TypographyDefaults(1.4f, FontStyle.Normal, TextAnchor.UpperLeft) },
            { "h5", new TypographyDefaults(1.3f, FontStyle.Bold, TextAnchor.UpperLeft) },
            { "h6", new TypographyDefaults(1.2f, FontStyle.Normal, TextAnchor.UpperLeft) },
            { "label" ,new TypographyDefaults(1, FontStyle.Bold, TextAnchor.UpperLeft) },
            { "small", new TypographyDefaults(0.8f, FontStyle.Bold, TextAnchor.UpperLeft) }
        };


        public int fontSize = 14;
        public TypographyMeta(string name) : base(name)
        {
            Sizing(_sizeSettings);
            Defaults();
        }

        public void Defaults()
        {
            foreach (var item in elementSettings)
            {
                var thing = new TextMeta().Element(item.Key)
                    .FontSize(fontSize * item.Value.FontSizeFactor)
                    .Style(item.Value.Style)
                    .Anchor(item.Value.Alignment)
                    .Font(item.Value.FontFace)
                    .FontColor(item.Value.FontColor);
                Type.Add(thing.element, thing);
            }
        }

        public TypographyMeta FontSize(int _fontsize)
        {
            fontSize = _fontsize;
            return this;
        }

        public TypographyMeta SetElementSettings(Dictionary<string, TypographyDefaults> settings)
        {
            foreach(var item in settings)
            {
                if (elementSettings.ContainsKey(item.Key))
                {
                    elementSettings[item.Key] = item.Value;
                } else
                {
                    elementSettings.Add(item.Key, item.Value);
                }
                
            }
            return this;
        }

        public TypographyMeta SetElementSetting(string element, TypographyDefaults settings)
        {

            if (elementSettings.ContainsKey(element))
            {
                elementSettings[element] = settings;
            }
            else
            {
                elementSettings.Add(element, settings);
            }
            return this;
        }

        public TypographyMeta SetElementSetting(string element, float sizeFactor, FontStyle style, TextAnchor alignment)
        {

            if (elementSettings.ContainsKey(element))
            {
                elementSettings[element] = new TypographyDefaults(sizeFactor, style, alignment);
            }
            else
            {
                elementSettings.Add(element, new TypographyDefaults(sizeFactor, style, alignment));
            }
            return this;
        }
    }

    public struct TypographyDefaults
    {
        public float FontSizeFactor;
        public FontStyle Style;
        public TextAnchor Alignment;
        public Font FontFace;
        public Color FontColor;

        public TypographyDefaults(float size, FontStyle style, TextAnchor alignment, Font fontFace = null, Color? color = null)
        {
            FontSizeFactor = size;
            Style = style;
            Alignment = alignment;
            if(fontFace != null)
            {
                FontFace = fontFace;
            } else
            {
                FontFace = GuiMeta.DefaultFont;
            }
            if(color != null)
            {
                FontColor = (Color)color;
            } else
            {
                FontColor = GuiMeta.DefaultFontColor.Color;
            }
            
        }

        public TypographyDefaults(float size, FontStyle style, TextAnchor alignment, string fontFace, Color? color = null)
        {
            FontSizeFactor = size;
            Style = style;
            Alignment = alignment;


            bool isvalid = false;
            foreach (var item in UnityEngine.Font.GetOSInstalledFontNames())
            {
                if (item == fontFace)
                {
                    isvalid = true; break;
                }
            }
            if (isvalid)
            {
                FontFace =  UnityEngine.Font.CreateDynamicFontFromOSFont(fontFace, 24);
            }
            else
            {
                FontFace = GuiMeta.DefaultFont;
            }
            if (color != null)
            {
                FontColor = (Color)color;
            }
            else
            {
                FontColor = GuiMeta.DefaultFontColor.Color;
            }
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

        public TextMeta FontSize(float _fontsize)
        {
            fontSize = Mathf.RoundToInt(_fontsize);
            return this;
        }

        public TextMeta FontColor(Color _fontColor)
        {
            fontColor = _fontColor;
            return this;
        }

    }
}
