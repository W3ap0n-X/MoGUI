using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Linq;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
            switch (TxtElement)
            {
                default:
                    Text.font = Settings.FontFace;
                    Text.fontSize = Settings.FontSize;
                    Text.color = Settings.FontColor;
                    Text.fontStyle = Settings.Style;
                    Text.alignment = Settings.Alignment;
                    break;
            }
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
