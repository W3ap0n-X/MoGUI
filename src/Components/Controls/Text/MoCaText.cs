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
        public Dictionary<string, float> elemSizeMultiplier = new Dictionary<string, float>()
        {
            { "text",  1 },
            { "title", 2 },
            { "h1", 1.8f},
            { "h2", 1.6f },
            { "h3", 1.5f },
            { "h4", 1.4f },
            { "h5", 1.3f },
            { "h6", 1.2f },
            { "label" ,1 },
            { "small", 0.8f }
        };

        public Dictionary<string, TypographySettings> elementSettings;

        private void CreateTypographySet()
        {
            elementSettings = new Dictionary<string, TypographySettings>()
            {
                { "text", new TypographySettings( elemSizeMultiplier["text"] * _parent.fontSize, FontStyle.Normal, TextAnchor.UpperLeft, _parent.Colors.Text.Color) },
                { "title", new TypographySettings(elemSizeMultiplier["title"] * _parent.fontSize, FontStyle.Bold, TextAnchor.MiddleCenter, _parent.Colors.Text.Color) },
                { "h1", new TypographySettings(elemSizeMultiplier["h1"] * _parent.fontSize, FontStyle.Bold, TextAnchor.LowerCenter, _parent.Colors.Text.Color) },
                { "h2", new TypographySettings(elemSizeMultiplier["h2"] * _parent.fontSize, FontStyle.Bold, TextAnchor.UpperLeft, _parent.Colors.Text.Color) },
                { "h3", new TypographySettings(elemSizeMultiplier["h3"] * _parent.fontSize, FontStyle.Bold, TextAnchor.UpperLeft, _parent.Colors.Text.Color) },
                { "h4", new TypographySettings(elemSizeMultiplier["h4"] * _parent.fontSize, FontStyle.Normal, TextAnchor.UpperLeft, _parent.Colors.Text.Color) },
                { "h5", new TypographySettings(elemSizeMultiplier["h5"] * _parent.fontSize, FontStyle.Bold, TextAnchor.UpperLeft, _parent.Colors.Text.Color) },
                { "h6", new TypographySettings(elemSizeMultiplier["h6"] * _parent.fontSize, FontStyle.Normal, TextAnchor.UpperLeft, _parent.Colors.Text.Color) },
                { "label" ,new TypographySettings(elemSizeMultiplier["label"] * _parent.fontSize, FontStyle.Bold, TextAnchor.UpperLeft, _parent.Colors.Text.Color) },
                { "small", new TypographySettings(elemSizeMultiplier["small"] * _parent.fontSize, FontStyle.Italic, TextAnchor.UpperLeft, _parent.Colors.Text.Color) }
            };
        }

        private TextElement elemFromString(string _element)
        {
            switch (_element)
            {

                case "h1":
                    return TextElement.h1;

                case "h2":
                    return TextElement.h2;

                case "h3":
                    return TextElement.h3;

                case "h4":
                    return TextElement.h4;
                    

                case "h5":
                    return TextElement.h5;
                    

                case "h6":
                    return TextElement.h6;
                    

                case "label":
                    return TextElement.label;
                    

                case "small":
                    return TextElement.small;
                    

                case "title":
                    return  TextElement.title;
                    

                case "text":
                default:
                    return TextElement.text;
                    
            }
        }

        public int fontSize = 14;
        public TypographyMeta(MoGuiMeta parent, string name) : base(parent, name)
        {
            CreateTypographySet();
            Sizing(_sizeSettings);
            Defaults();
        }

        public void Defaults()
        {
            foreach (var item in elementSettings)
            {
                var elementMeta = new TextMeta(item.Value, item.Key);
                Type.Add(elementMeta.element, elementMeta);
            }
        }

        public TypographyMeta FontSizes(int _fontsize)
        {
            fontSize = _fontsize;
            foreach (var item in elementSettings)
            {
                TypographySettings current = elementSettings[item.Key];
                TypographySettings newSetting = new TypographySettings(elemSizeMultiplier[item.Key] * _fontsize, current.Style, current.Alignment, current.FontColor, current.FontFace);
                SetElementSetting(item.Key, newSetting);
                Type[elemFromString(item.Key)].settings = newSetting;
            }
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

        public void setElemSizeMultiplier(string element, float _elemSizeMultiplier)
        {
            elemSizeMultiplier[element] = _elemSizeMultiplier;
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

    }

    public struct TypographySettings
    {
        public int FontSize;
        public FontStyle Style;
        public TextAnchor Alignment;
        public Font FontFace;
        public Color FontColor;

        public TypographySettings(int size, FontStyle style, TextAnchor alignment, Color color, Font fontFace = null)
        {
            FontSize = size;
            Style = style;
            Alignment = alignment;
            FontFace = fontFace ?? UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 24);
            FontColor = color;
            
        }

        public TypographySettings(int size, FontStyle style, TextAnchor alignment, Color color, string fontFace)
        {
            FontSize = size;
            Style = style;
            Alignment = alignment;
            FontColor = color;

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
                FontFace = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 24);
            }
        }

        public TypographySettings( float size, FontStyle style, TextAnchor alignment, Color color, string fontFace)
        {
            FontSize = Mathf.RoundToInt(size);
            Style = style;
            Alignment = alignment;
            FontColor = color;

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
                FontFace = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 24);
            }

        }

        public TypographySettings(float size, FontStyle style, TextAnchor alignment, Color color, Font fontFace = null)
        {
            
            FontSize = Mathf.RoundToInt(size);
            Style = style;
            Alignment = alignment;
            FontColor = color;
            FontFace = fontFace ?? UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 24);
        }

    }

    public class TextMeta
    {
        public TypographySettings settings;

        public TextMeta(TypographySettings settings, TextElement element)
        {
            this.settings = settings;
            this.element = element;
        }

        public TextMeta( Font font, int fontSize, Color fontColor, TextElement element, TextAnchor textAnchor, FontStyle textStyle)
        {
            this.settings = new TypographySettings(fontSize, textStyle, textAnchor, fontColor, font);
            this.element = element;
        }

        public TextMeta(TypographySettings settings, string element)
        {
            this.settings = settings;
            Element(element);
        }

        public TextMeta(Font font, int fontSize, Color fontColor, string element, TextAnchor textAnchor, FontStyle textStyle)
        {
            this.settings = new TypographySettings(fontSize, textStyle, textAnchor, fontColor, font);
            Element(element);
        }

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
