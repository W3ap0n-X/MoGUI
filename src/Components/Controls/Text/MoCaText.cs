using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

    public class MoCaText : MoGCArgs
    {
        public new string Text;
        public TextElement Element;
        public TypographySettings? Settings;

        public MoCaText(Func<object> onUpdateAction,
            TextElement element = TextElement.text,
            TypographySettings? settings = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta)
        {
            Element = element;
            OnUpdateAction = onUpdateAction;
            Settings = settings;

        }

        public MoCaText(string text,
            TextElement element = TextElement.text,
            TypographySettings? settings = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta)
        {
            Element = element;
            Text = text;
            Settings = settings;

        }
    }

    public class TypographyMeta : ControlMeta
    {
        private SizeSettings _sizeSettings = new SizeSettings(20,20, 1,1);
        public Dictionary<TextElement, TextMeta> Type = new Dictionary<TextElement, TextMeta>();
        public Dictionary<string, TypographySettings> elementSettings = new Dictionary<string, TypographySettings>()
        {
            { "text", new TypographySettings( 1, FontStyle.Normal, TextAnchor.UpperLeft) },
            { "title", new TypographySettings(2, FontStyle.Bold, TextAnchor.MiddleCenter) },
            { "h1", new TypographySettings(1.8f, FontStyle.Bold, TextAnchor.LowerCenter) },
            { "h2", new TypographySettings(1.6f, FontStyle.Bold, TextAnchor.UpperLeft) },
            { "h3", new TypographySettings(1.5f, FontStyle.Bold, TextAnchor.UpperLeft) },
            { "h4", new TypographySettings(1.4f, FontStyle.Normal, TextAnchor.UpperLeft) },
            { "h5", new TypographySettings(1.3f, FontStyle.Bold, TextAnchor.UpperLeft) },
            { "h6", new TypographySettings(1.2f, FontStyle.Normal, TextAnchor.UpperLeft) },
            { "label" ,new TypographySettings(1, FontStyle.Bold, TextAnchor.UpperLeft) },
            { "small", new TypographySettings(0.8f, FontStyle.Italic, TextAnchor.UpperLeft) }
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

        public TypographyMeta SetElementSettings(Dictionary<string, TypographySettings> settings)
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

        public TypographyMeta SetElementSetting(string element, TypographySettings settings)
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
                elementSettings[element] = new TypographySettings( sizeFactor, style, alignment);
            }
            else
            {
                elementSettings.Add(element, new TypographySettings( sizeFactor, style, alignment));
            }
            return this;
        }
    }

    public struct TypographySettings
    {
        public float FontSizeFactor;
        public int FontSize;
        public FontStyle Style;
        public TextAnchor Alignment;
        public Font FontFace;
        public Color FontColor;

        public TypographySettings(int size, float sizeFactor, FontStyle style, TextAnchor alignment, Font fontFace = null, Color? color = null)
        {
            FontSize = size;
            FontSizeFactor = sizeFactor;
            Style = style;
            Alignment = alignment;
            if(fontFace != null)
            {
                FontFace = fontFace;
            } else
            {
                FontFace = MoGuiMeta.DefaultFont;
            }
            if(color != null)
            {
                FontColor = (Color)color;
            } else
            {
                FontColor = MoGuiMeta.DefaultFontColor.Color;
            }
            
        }

        public TypographySettings(int size, float sizeFactor, FontStyle style, TextAnchor alignment, string fontFace, Color? color = null)
        {
            FontSize = size;
            FontSizeFactor = sizeFactor;
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
                FontFace = MoGuiMeta.DefaultFont;
            }
            if (color != null)
            {
                FontColor = (Color)color;
            }
            else
            {
                FontColor = MoGuiMeta.DefaultFontColor.Color;
            }
        }

        public TypographySettings( float sizeFactor, FontStyle style, TextAnchor alignment, string fontFace, Color? color = null)
        {
            FontSize = MoGuiMeta.DefaultFontSize;
            FontSizeFactor = sizeFactor;
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
                FontFace = UnityEngine.Font.CreateDynamicFontFromOSFont(fontFace, 24);
            }
            else
            {
                FontFace = MoGuiMeta.DefaultFont;
            }
            if (color != null)
            {
                FontColor = (Color)color;
            }
            else
            {
                FontColor = MoGuiMeta.DefaultFontColor.Color;
            }
        }

        public TypographySettings(float sizeFactor, FontStyle style, TextAnchor alignment, Font fontFace = null, Color? color = null)
        {
            FontSize = MoGuiMeta.DefaultFontSize;
            FontSizeFactor = sizeFactor;
            Style = style;
            Alignment = alignment;

            if(fontFace != null)
            {
                FontFace = fontFace;
            } else
            {
                FontFace = MoGuiMeta.DefaultFont;
            }
            if (color != null)
            {
                FontColor = (Color)color;
            }
            else
            {
                FontColor = MoGuiMeta.DefaultFontColor.Color;
            }
        }

    }

    public class TextMeta
    {
        public TypographySettings settings = new TypographySettings(MoGuiMeta.DefaultFontSize, 1, FontStyle.Normal, TextAnchor.UpperLeft, MoGuiMeta.DefaultFont, MoGuiMeta.DefaultFontColor.Color);

        public Font font
        {
            get { return settings.FontFace; }
            set { settings.FontFace = value; }
        }
        public int fontSize
        {
            get { return settings.FontSize; }
            set { settings.FontSize = value; }
        }
        public Color fontColor
        {
            get { return settings.FontColor; }
            set { settings.FontColor = value; }
        }
        
        public TextElement element = TextElement.text;
        public TextAnchor textAnchor
        {
            get { return settings.Alignment; }
            set { settings.Alignment = value; }
        }
        public FontStyle textStyle
        {
            get { return settings.Style; }
            set { settings.Style = value; }
        }

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
                font = MoGuiMeta.DefaultFont;
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
