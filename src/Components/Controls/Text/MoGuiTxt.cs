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

        TextElement TxtElement = TextElement.text;
        public TypographySettings Settings;

        public MoGuiTxt(MoGuiMeta meta, string name, Func<object> onUpdateAction, TypographySettings? settings = null) : base(meta, name)
        {
            if (settings != null)
            {
                Settings = (TypographySettings)settings;
            }
            else
            {
                Settings = Meta.Text.Type[TxtElement].settings;
            }
            OnUpdateAction = onUpdateAction;
            Obj = CreateText(OnUpdateAction().ToString());
        }

        public MoGuiTxt(MoGuiMeta meta, string name, object text, TypographySettings? settings= null) : base(meta, name)
        {
            if (settings != null)
            {
                Settings = (TypographySettings)settings;
            }
            else
            {
                Settings = Meta.Text.Type[TxtElement].settings;
            }
            Obj = CreateText(text.ToString());
        }

        public MoGuiTxt(MoGuiMeta meta, string name, MoCaText args) : base(meta, name)
        {
            TxtElement = args.Element;
            if (args.Settings != null)
            {
                Settings = (TypographySettings)args.Settings;
            } else
            {
                Settings = Meta.Text.Type[TxtElement].settings;
            }
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
            minWidth = Meta.Text.sizing.minWidth;
            minHeight = Meta.Text.sizing.minHeight;
            if (Meta.Text.sizing.preferredWidth != null) { preferredWidth = (float)Meta.Text.sizing.preferredWidth; }
            if (Meta.Text.sizing.preferredHeight != null) { preferredHeight = (float)Meta.Text.sizing.preferredHeight; }
            flexibleWidth = Meta.Text.sizing.flexibleWidth ?? 0;
            flexibleHeight = Meta.Text.sizing.flexibleHeight ?? 0;

        }

        public GameObject CreateText(string text = null)
        {
            var textObject = new GameObject(PluginName + "_" + Name + "_" + "Text");

            Text = textObject.AddComponent<Text>();
            Text.text = text != null ? text : "";
            setFont();
            RectTransform labelRect = textObject.GetComponent<RectTransform>();
            labelRect.anchoredPosition = new Vector2(0, 0);
            labelRect.anchorMin = new Vector2(0, 0);
            labelRect.anchorMax = new Vector2(1, 1);
            labelRect.offsetMin = new Vector2(Meta.Margin, 0);

            labelRect.offsetMax = new Vector2(-Meta.Margin, 0);

            AddLayoutElement(textObject);
            SetLayout();

            return textObject;
        }

        public void setFont()
        {
            Text.font = Settings.FontFace;
            Text.fontSize = Settings.FontSize;
            Text.color = Settings.FontColor;
            Text.fontStyle = Settings.Style;
            Text.alignment = Settings.Alignment;
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

        public void FontFace(Font font)
        {
            FontSettings(font);
        }

        public void FontFace(string font)
        {
            FontSettings(font);
        }

        public void FontSize(int size)
        {
            FontSettings(Settings.FontFace, fontSize: size);
        }

        public void TextAlignment(TextAnchor alignment)
        {
            FontSettings(Settings.FontFace, alignment: alignment);
        }

        public void FontStyle(FontStyle style)
        {
            FontSettings(Settings.FontFace, style: style);
        }

        public void FontColor(Color color)
        {
            FontSettings(Settings.FontFace, color: color);
        }

        // Change text Element of control
        public void Element(TextElement element)
        {
            TxtElement = element;

        }

        // Same as above but using string shortcut
        public void Element(string element)
        {
            switch (element)
            {

                case "h1":
                    Element(TextElement.h1);
                    break;

                case "h2":
                    Element(TextElement.h2);
                    break;

                case "h3":
                    Element(TextElement.h3);
                    break;

                case "h4":
                    Element(TextElement.h4);
                    break;

                case "h5":
                    Element(TextElement.h5);
                    break;

                case "h6":
                    Element(TextElement.h6);
                    break;

                case "label":
                    Element(TextElement.label);
                    break;

                case "small":
                    Element(TextElement.small);
                    break;

                case "title":
                    Element(TextElement.title);
                    break;

                case "text":
                default:
                    Element( TextElement.text);
                    break;
            }
        }

        public void FontSettings(TypographySettings settings)
        {
            Settings = settings;
            setFont();
        }
        public void FontSettings(Font fontFace = null, int? fontSize = null,  FontStyle? style = null, TextAnchor? alignment = null, Color? color = null )
        {
            FontSettings( new TypographySettings(fontSize ?? Settings.FontSize, style ?? Settings.Style, alignment ?? Settings.Alignment, fontFace ?? Settings.FontFace, color ?? Settings.FontColor) );
        }
        public void FontSettings(string fontFace = null, int? fontSize = null, FontStyle? style = null, TextAnchor? alignment = null, Color? color = null)
        {
            FontSettings(new TypographySettings(fontSize ?? Settings.FontSize, style ?? Settings.Style, alignment ?? Settings.Alignment, fontFace ?? Settings.FontFace.name, color ?? Settings.FontColor));
        }

    }


}
