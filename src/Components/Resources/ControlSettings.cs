using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGuiColor
    {

        Color _base;
        public Func<Color> BoundBase;
        public Action<Color> BoundOut;

        public Color Raw
        {
            get
            {
                if (BoundBase != null)
                {
                    Color color = BoundBase();
                    return new Color(color.r, color.g, color.b, 1); ;

                }
                else
                {
                    return new Color(_base.r, _base.g, _base.b, 1);
                }
            }
            set
            {
                if (BoundOut != null)
                {
                    BoundOut(new Color(value.r, value.g, value.b, A));

                }
                else
                {
                    _base = new Color(value.r, value.g, value.b, A);
                }

            }
        }
        public Color Color
        {
            get
            {
                if (BoundBase != null)
                {
                    return BoundBase();

                }
                else
                {
                    return _base;
                }
            }
            set
            {
                if (BoundOut != null)
                {
                    BoundOut(value);

                }
                else
                {
                    _base = value;
                }

            }
        }

        public Color ShadeRaw
        {
            get
            {
                Color color = Shade;
                return new Color(color.r, color.g, color.b, 1);
            }
        }
        public Color Shade
        {
            get
            {
                Color shade = MutateColor(Color, Luminance2 <= 0 ? DarkFactor : DarkFactor * Factor);
                if (A <= 0)
                {
                    shade.a = Factor;
                }
                return shade;
            }
        }

        public Color TintRaw
        {
            get
            {
                Color color = Tint;
                return new Color(color.r, color.g, color.b, 1);
            }
        }
        public Color Tint
        {
            get
            {
                Color tint = MutateColor(Color, Luminance2 <= 0 ? Factor : Factor * -1);
                if (A <= 0)
                {
                    tint.a = Factor;
                }
                return tint;
            }
        }


        public float Factor;
        float? darkFactor = null;

        public float Luminance
        {
            get { return 0.2126f * R + 0.7152f * G + 0.0722f * B; }
        }


        public float Contrast
        {
            get { return MathF.Abs(Luminance - 0.5f) + MathF.Abs(A - 0.5f); }
        }
        public float ContrastS
        {
            get { return MathF.Abs(Tv - 0.5f) + MathF.Abs(A - 0.5f); }
        }
        public float ContrastT
        {
            get { return MathF.Abs(Tv3 - 0.5f) + MathF.Abs(A - 0.5f); }
        }

        public float Luminance2
        {
            get { return (Luminance - 0.5f) * 2; }
        }

        public float LBShade
        {
            get { return (Tv - 0.5f) * 2; }
        }

        public float LBTint
        {
            get { return (Tv3 - 0.5f) * 2; }
        }

        public float Tv
        {
            get { return 0.2126f * Shade.r + 0.7152f * Shade.g + 0.0722f * Shade.b; }
        }

        public float Tv3
        {
            get { return 0.2126f * Tint.r + 0.7152f * Tint.g + 0.0722f * Tint.b; }
        }

        public float Tv2
        {
            get
            {
                // Calculate the luminance of the current color
                //float currentLuminance = (0.2126f * R + 0.7152f * G + 0.0722f * B);

                // Calculate the luminance of the background color
                // This assumes you have a way to get the parent's color.
                // Placeholder, you need to get this from your parent meta
                //float parentLuminance = 0.5f;

                // The values must be the lighter and darker of the two colors
                float L1 = Mathf.Max(Tv, Luminance);
                float L2 = Mathf.Min(Tv, Luminance);

                return (L1 + 0.05f) / (L2 + 0.05f);
            }
        }

        public float Tv4
        {
            get
            {
                // Calculate the luminance of the current color
                //float currentLuminance = (0.2126f * R + 0.7152f * G + 0.0722f * B);

                // Calculate the luminance of the background color
                // This assumes you have a way to get the parent's color.
                // Placeholder, you need to get this from your parent meta
                //float parentLuminance = 0.5f;

                // The values must be the lighter and darker of the two colors
                float L1 = Mathf.Max(Tv3, Luminance);
                float L2 = Mathf.Min(Tv3, Luminance);

                return (L1 + 0.05f) / (L2 + 0.05f);
            }
        }

        public float Tv5
        {
            get
            {
                // Calculate the luminance of the current color
                //float currentLuminance = (0.2126f * R + 0.7152f * G + 0.0722f * B);

                // Calculate the luminance of the background color
                // This assumes you have a way to get the parent's color.
                // Placeholder, you need to get this from your parent meta
                //float parentLuminance = 0.5f;

                // The values must be the lighter and darker of the two colors
                float L1 = Mathf.Max(Tv, Tv3);
                float L2 = Mathf.Min(Tv, Tv3);

                return (L1 + 0.05f) / (L2 + 0.05f);
            }
        }


        public float DarkFactor
        {
            get
            {
                if (darkFactor != null && float.TryParse(darkFactor.ToString(), out float dfactor))
                { return dfactor; }
                else { return Factor * -1; }
            }
            set { darkFactor = value; }
        }

        public float R
        {
            get => Color.r;
            set { Color = new Color(value, Color.g, Color.b, Color.a); }
        }
        public float G
        {
            get => Color.g;
            set { Color = new Color(Color.r, value, Color.b, Color.a); }
        }
        public float B
        {
            get => Color.b;
            set { Color = new Color(Color.r, Color.g, value, Color.a); }
        }
        public float A
        {
            get => Color.a;
            set { Color = new Color(Color.r, Color.g, Color.b, value); }
        }

        public MoGuiColor(Color color, float factor = 0.5f, float? darkfactor = null)
        {
            BoundBase = () => color;
            BoundOut = (val) => color = val;
            darkFactor = darkfactor * -1;
            Factor = factor;
        }

        public MoGuiColor(ColorWrapper color, float factor = 0.5f, float? darkfactor = null)
        {
            BoundBase = () => color.Color;
            darkFactor = darkfactor * -1;
            Factor = factor;
        }

        public MoGuiColor(Func<Color> color, float factor = 0.5f, float? darkfactor = null)
        {
            BoundBase = color;
            darkFactor = darkfactor * -1;
            Factor = factor;
        }


        public void setRange(float factor, float? darkfactor = null)
        {
            Factor = factor;
            darkFactor = darkfactor * -1;
        }

        public Color MutateColor(Color color, float factor)
        {
            float r = MutateColorPart(color.r, factor);
            float g = MutateColorPart(color.g, factor);
            float b = MutateColorPart(color.b, factor);
            return new Color(r, g, b, color.a);
        }

        public float MutateColorPart(float value, float factor)
        {
            if (factor >= 0)
            {
                return value + (1 - value) * factor;
            }
            else
            {
                return value * (1 + factor);

            }
        }

    }

    public class ColorWrapper
    {
        public Color Color;

        public ColorWrapper(Color color)
        {
            this.Color = color;
        }

        public float R 
        {
            get => Color.r;  
            set { Color.r = value; }
        }
        public float G
        {
            get => Color.g;
            set { Color.g = value; }
        }
        public float B
        {
            get => Color.b;
            set { Color.b = value; }
        }
        public float A
        {
            get => Color.a;
            set { Color.a = value; }
        }
    }

    public enum ControlOrientation
    {
        horizontal,
        vertical
    }

    public enum ControlLabelPlacement
    {
        none,
        before,
        after
    }

    public enum TextElement
    {
        text,
        title,
        h1,
        h2,
        h3,
        h4,
        h5,
        h6,
        label,
        small
    }

    public enum SliderDirection
    {
        horizontal, 
        vertical
    }

    public enum PanelStartingState
    {
        show,
        minimize,
        hide
    }

    //public enum TextStyle
    //{
    //    none,
    //    bold,
    //    italic,
    //    underline
    //}

    public enum ToggleType
    {
        checkbox,
        button
    }

    public enum ThemeKey
    {
        none,
        primary,
        secondary,
        
    }


    public struct SizeSettings
    {
        public float minWidth;
        public float minHeight;
        public float? flexibleHeight;
        public float? flexibleWidth;
        public float? preferredWidth;
        public float? preferredHeight;

        public SizeSettings(float minW, float minH, float? flexW = null, float? flexH = null, float? prefW = null, float? prefH = null)
        {
            minWidth = minW;
            minHeight = minH;
            flexibleWidth = flexW;
            flexibleHeight = flexH;
            preferredWidth = prefW;
            preferredHeight = prefH;
        }

        public SizeSettings SetMin(Vector2 sizes)
        {
            minWidth = sizes.x;
            minHeight = sizes.y;
            return this;
        }

        public SizeSettings SetFlex(Vector2 sizes)
        {
            flexibleWidth = sizes.x;
            flexibleHeight = sizes.y;
            return this;
        }

        public SizeSettings SetPref(Vector2 sizes)
        {
            preferredWidth = sizes.x;
            preferredHeight = sizes.y;
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
                FontFace = UnityEngine.Font.CreateDynamicFontFromOSFont(fontFace, 24);
            }
            else
            {
                FontFace = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 24);
            }
        }

        public TypographySettings(float size, FontStyle style, TextAnchor alignment, Color color, string fontFace)
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

    public struct GuiColorSet
    {
        public MoGuiColor Panel;
        public MoGuiColor Header;
        public MoGuiColor Text;
        public MoGuiColor Control;

        public GuiColorSet(Color? baseColor = null)
        {

            Debug.Log("GuiColorSet: " + (baseColor ?? new Color(0.2f, 0.2f, 0.2f, 0.8f)).ToString());

            Panel = new MoGuiColor((baseColor ?? new Color(0.2f, 0.2f, 0.2f, 0.8f)));


            if (Panel.Luminance2 <= 0f)
            {
                Panel.setRange(0.25f, 0.4f);
                Text = new MoGuiColor(Color.white);
                Header = new MoGuiColor(Panel.Shade);
                Control = new MoGuiColor(Panel.TintRaw);
            }
            else
            {
                Panel.setRange(0.25f, 0.4f);
                Text = new MoGuiColor(Color.black);
                Header = new MoGuiColor(Panel.Shade);
                Control = new MoGuiColor(Panel.TintRaw);
            }
        }

        public GuiColorSet(MoGuiColor panel, MoGuiColor text, MoGuiColor header = null)
        {

            Panel = panel;
            Text = text;
            Header = header ?? new MoGuiColor(panel.Shade);
            Control = new MoGuiColor(panel.Tint);
        }

        public GuiColorSet(Color panel, Color text, Color? header = null)
        {

            Panel = new MoGuiColor(panel);
            Text = new MoGuiColor(text);
            Header = new MoGuiColor(header ?? Panel.Shade);
            Control = new MoGuiColor(Panel.Tint);
        }

        public GuiColorSet(GuiColorSet set)
        {
            Panel = set.Panel;
            Text = set.Text;
            Header = set.Header;
            Control = set.Control;
        }

        public GuiColorSet(MoGuiColor panel)
        {

            Panel = panel;

            if (Panel.Luminance2 <= 0f)
            {
                Text = new MoGuiColor(Color.white);
                Header = new MoGuiColor(Panel.Shade);
                Control = new MoGuiColor(Panel.TintRaw);
            }
            else
            {
                Text = new MoGuiColor(Color.black);
                Header = new MoGuiColor(Panel.Shade);
                Control = new MoGuiColor(Panel.TintRaw);
            }
        }

    }
}
