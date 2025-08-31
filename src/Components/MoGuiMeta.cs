using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


namespace MoGUI
{
    public class MoGuiMeta
    {
        public static int DefaultTxtMargin = 5;
        public static int DefaultFontSize = 14;
        public static Font DefaultFont = Font.CreateDynamicFontFromOSFont("Arial", 24);
        public static Color DefaultFontColor = Color.white;
        public static float DefaultHeaderSize = 40;
        public static int DefaultHeaderFontSize = 18;
        public static int DefaultHeaderExitFontSize = 28;
        public static Color DefaultHeaderColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        public static Color DefaultHeaderExitColor = new Color(0.75f, 0.25f, 0.25f, 1f);
        public static Color DefaultPanelColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        public static Color DefaultButtonColor = new Color(0.3f, 0.3f, 0.3f, 1f);
        public static Color DefaultInputColor = new Color(0.1f, 0.1f, 0.1f, 1f);
        public static Color DefaultInputPlaceholderFontColor = new Color(0.6f, 0.6f, 0.6f, 0.8f);
        public static Color DefaultToggleColor = new Color(0.1f, 0.1f, 0.1f, 1f);
        public static Color DefaultToggleCheckColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        public static ControlOrientation DefaultOrientation = ControlOrientation.horizontal;
        public static ControlLabelPlacement DefaultLabelPlacement = ControlLabelPlacement.after;
        public static ControlOrientation DefaultToggleOrientation = ControlOrientation.horizontal;
        public static ControlLabelPlacement DefaultToggleLabelPlacement = ControlLabelPlacement.after;
        public static ControlOrientation DefaultSliderOrientation = ControlOrientation.vertical;
        public static ControlLabelPlacement DefaultSliderLabelPlacement = ControlLabelPlacement.before;
        public static ControlOrientation DefaultInputOrientation = ControlOrientation.horizontal;
        public static ControlLabelPlacement DefaultInputLabelPlacement = ControlLabelPlacement.before;
        public static Vector4 DefaultSliderSize = new Vector4(50, 25, 100, 40);
        public static Vector4 DefaultToggleSize = new Vector4(15, 15, 20, 20);
        public static Vector4 DefaultButtonSize = new Vector4(60, 30, 100, 40);
        public static Vector4 DefaultInputSize = new Vector4(20, 20, 80, 30);
        public static SliderDirection DefaultSliderDirection = SliderDirection.horizontal;
        public static TextAnchor DefaultTxtAnchor = TextAnchor.MiddleLeft;


        // Info
        public string PluginName;
        public string Name;
        // Panel
        public Color PanelColor;
        // Header
        public int HeaderFontSize;
        public int HeaderExitFontSize;
        public float HeaderSize;
        public Color HeaderColor;
        public Color HeaderExitColor;
        public Color HeaderFontColor;
        // Text
        public int TxtMargin;
        public TextAnchor TxtAnchor;
        public int FontSize;
        public Font Font;
        public Color FontColor;
        // Button
        public int ButtonFontSize;
        public Font ButtonFont;
        public Color ButtonColor;
        public Color ButtonFontColor;
        public Vector4 ButtonSize;

        // Toggle
        public Color ToggleColor;
        public Color ToggleCheckColor;
        public ControlOrientation ToggleOrientation;
        public ControlLabelPlacement ToggleLabelPlacement;
        public Vector4 ToggleSize;

        // Slider
        public Color SliderHandleColor;
        public Color SliderTrackColor;
        public Color SliderFillColor;
        public ControlOrientation SliderOrientation;
        public ControlLabelPlacement SliderLabelPlacement;
        public Vector4 SliderSize;
        public SliderDirection SliderDirection;
        // Input
        public int InputFontSize;
        public Font InputFont;
        public Color InputColor;
        public Color InputFontColor;
        public Color InputPlaceholderFontColor;
        public ControlOrientation InputOrientation;
        public ControlLabelPlacement InputLabelPlacement;
        public Vector4 InputSize;
        // DDL
        public int DDLFontSize;
        public int DDLListFontSize;
        public Font DDLFont;
        public Font DDLListFont;
        public Color DDLButtonColor;
        public Color DDLFontColor;
        public Color DDLListColor;
        public Color DDLListItemFontColor;
        public Vector4 DDLSize;
        // Other
        public ControlOrientation Orientation;
        public ControlLabelPlacement LabelPlacement;

        public ControlOrientation SelectorOrientation;

        public MoGuiMeta(string pluginName, string name
            , int? txtMargin = null
            , TextAnchor? txtAnchor = null
            , int? fontSize = null
            , int? inputFontSize = null
            , int? headerFontSize = null
            , int? headerexitFontSize = null
            , int? buttonFontSize = null
            , int? dDLFontSize = null
            , int? dDLListFontSize = null
            , float? headerSize = null
            , Color? fontColor = null
            , Color? headerColor = null
            , Color? headerFontColor = null
            , Color? headerExitColor = null
            , Color? panelColor = null
            , Color? inputColor = null
            , Color? inputFontColor = null
            , Color? inputPlaceholderFontColor = null
            , Color? buttonColor = null
            , Color? buttonFontColor = null
            , Color? toggleColor = null
            , Color? toggleCheckColor = null
            , Color? sliderHandleColor = null
            , Color? sliderTrackColor = null
            , Color? sliderFillColor = null
            , Color? dDLButtonColor = null
            , Color? dDLFontColor = null
            , Color? dDLListColor = null
            , Color? dDLListItemFontColor = null
            , Font font = null
            , Font inputFont = null
            , Font buttonFont = null
            , Font dDLFont = null
            , Font dDLListFont = null
            , ControlOrientation? orientation = null
            , ControlOrientation? sliderOrientation = null
            , ControlOrientation? inputOrientation = null
            , ControlOrientation? toggleOrientation = null
            , ControlOrientation? selectorOrientation = null
            , ControlLabelPlacement? labelPlacement = null
            , ControlLabelPlacement? sliderLabelPlacement = null
            , ControlLabelPlacement? inputLabelPlacement = null
            , ControlLabelPlacement? toggleLabelPlacement = null
            , Vector4? buttonSize = null
            , Vector4? toggleSize = null
            , Vector4? sliderSize = null
            , Vector4? inputSize = null
            , Vector4? dDLSize = null
            , SliderDirection? sliderDirection = null
        )
        {
            PluginName = pluginName;
            Name = name;

            TxtMargin = txtMargin ?? DefaultTxtMargin;
            FontSize = fontSize ?? DefaultFontSize;
            TxtAnchor = txtAnchor ?? DefaultTxtAnchor;
            Font = font ?? DefaultFont;
            FontColor = fontColor ?? DefaultFontColor;
            Orientation = orientation ?? DefaultOrientation;
            LabelPlacement = labelPlacement ?? DefaultLabelPlacement;

            PanelColor = panelColor ?? DefaultPanelColor;
            HeaderFontSize = headerFontSize ?? DefaultHeaderFontSize;
            HeaderExitFontSize = headerexitFontSize ?? DefaultHeaderExitFontSize;
            HeaderSize = headerSize ?? DefaultHeaderSize;
            HeaderColor = headerColor ?? DefaultHeaderColor;
            HeaderExitColor = headerExitColor ?? DefaultHeaderExitColor;
            HeaderFontColor = headerFontColor ?? DefaultFontColor;

            ButtonFontSize = buttonFontSize ?? DefaultFontSize;
            ButtonFont = buttonFont ?? DefaultFont;
            ButtonColor = buttonColor ?? DefaultButtonColor;
            ButtonFontColor = buttonFontColor ?? DefaultFontColor;
            ButtonSize = buttonSize ?? DefaultButtonSize;

            ToggleColor = toggleColor ?? DefaultToggleColor;
            ToggleOrientation = toggleOrientation ?? DefaultToggleOrientation;
            ToggleLabelPlacement = toggleLabelPlacement ?? DefaultToggleLabelPlacement;
            ToggleCheckColor = toggleCheckColor ?? DefaultToggleCheckColor;
            ToggleSize = toggleSize ?? DefaultToggleSize;

            SliderHandleColor = sliderHandleColor ?? DefaultToggleCheckColor;
            SliderTrackColor = sliderTrackColor ?? DefaultInputColor;
            SliderFillColor = sliderFillColor ?? DefaultInputColor;
            SliderOrientation = sliderOrientation ?? DefaultSliderOrientation;
            SliderLabelPlacement = sliderLabelPlacement ?? DefaultSliderLabelPlacement;
            SliderSize = sliderSize ?? DefaultSliderSize;
            SliderDirection = sliderDirection ?? DefaultSliderDirection;

            InputFont = inputFont ?? DefaultFont;
            InputFontSize = inputFontSize ?? DefaultFontSize;
            InputColor = inputColor ?? DefaultInputColor;
            InputFontColor = inputFontColor ?? DefaultFontColor;
            InputPlaceholderFontColor = inputPlaceholderFontColor ?? DefaultInputPlaceholderFontColor;
            InputOrientation = inputOrientation ?? DefaultInputOrientation;
            InputLabelPlacement = inputLabelPlacement ?? DefaultInputLabelPlacement;
            InputSize = inputSize ?? DefaultInputSize;

            DDLFont = dDLFont ?? DefaultFont;
            DDLListFont = dDLListFont ?? DefaultFont;
            DDLFontSize = dDLFontSize ?? DefaultFontSize;
            DDLListFontSize = dDLListFontSize ?? DefaultFontSize;
            DDLButtonColor = dDLButtonColor ?? DefaultButtonColor;
            DDLFontColor = dDLFontColor ?? DefaultFontColor;
            DDLListColor = dDLListColor ?? DefaultInputColor;
            DDLListItemFontColor = dDLListItemFontColor ?? DefaultFontColor;
            DDLSize = dDLSize ?? DefaultButtonSize;

            SelectorOrientation = selectorOrientation ?? DefaultSliderOrientation;

        }

        public MoGuiMeta(MoGuiMeta meta, string name
            , int? txtMargin = null
            , TextAnchor? txtAnchor = null
            , int? fontSize = null
            , int? inputFontSize = null
            , int? buttonFontSize = null
            , int? headerFontSize = null
            , int? headerexitFontSize = null
            , int? dDLFontSize = null
            , int? dDLListFontSize = null
            , float? headerSize = null
            , Color? fontColor = null
            , Color? headerColor = null
            , Color? headerExitColor = null
            , Color? headerFontColor = null
            , Color? panelColor = null
            , Color? inputColor = null
            , Color? inputFontColor = null
            , Color? inputPlaceholderFontColor = null
            , Color? buttonColor = null
            , Color? buttonFontColor = null
            , Color? toggleColor = null
            , Color? toggleCheckColor = null
            , Color? sliderHandleColor = null
            , Color? sliderTrackColor = null
            , Color? sliderFillColor = null
            , Color? dDLButtonColor = null
            , Color? dDLFontColor = null
            , Color? dDLListColor = null
            , Color? dDLListItemFontColor = null
            , Font font = null
            , Font inputFont = null
            , Font buttonFont = null
            , Font dDLFont = null
            , Font dDLListFont = null
            , ControlOrientation? orientation = null
            , ControlLabelPlacement? labelPlacement = null
            , ControlOrientation? sliderOrientation = null
            , ControlOrientation? inputOrientation = null
            , ControlOrientation? toggleOrientation = null
            , ControlOrientation? selectorOrientation = null
            , ControlLabelPlacement? sliderLabelPlacement = null
            , ControlLabelPlacement? inputLabelPlacement = null
            , ControlLabelPlacement? toggleLabelPlacement = null
            , Vector4? buttonSize = null
            , Vector4? toggleSize = null
            , Vector4? sliderSize = null
            , Vector4? inputSize = null
            , Vector4? dDLSize = null
            , SliderDirection? sliderDirection = null
        )
        {
            PluginName = meta.PluginName;
            Name = name;

            TxtMargin = txtMargin ?? meta.TxtMargin;
            TxtAnchor = txtAnchor ?? meta.TxtAnchor;
            FontSize = fontSize ?? meta.FontSize;
            FontColor = fontColor ?? meta.FontColor;
            Font = font ?? meta.Font;
            LabelPlacement = labelPlacement ?? meta.LabelPlacement;
            Orientation = orientation ?? meta.Orientation;

            PanelColor = panelColor ?? meta.PanelColor;
            HeaderFontSize = headerFontSize ?? meta.HeaderFontSize;
            HeaderExitFontSize = headerexitFontSize ?? meta.HeaderExitFontSize;
            HeaderSize = headerSize ?? meta.HeaderSize;
            HeaderColor = headerColor ?? meta.HeaderColor;
            HeaderExitColor = headerExitColor ?? meta.HeaderExitColor;
            HeaderFontColor = headerFontColor ?? meta.HeaderFontColor;

            ButtonFont = buttonFont ?? meta.ButtonFont;
            ButtonSize = buttonSize ?? meta.ButtonSize;
            ButtonColor = buttonColor ?? meta.ButtonColor;
            ButtonFontColor = buttonFontColor ?? meta.FontColor;
            ButtonFontSize = buttonFontSize ?? meta.FontSize;

            ToggleSize = toggleSize ?? meta.ToggleSize;
            ToggleColor = toggleColor ?? meta.ToggleColor;
            ToggleCheckColor = toggleCheckColor ?? meta.ToggleCheckColor;
            ToggleOrientation = toggleOrientation ?? meta.ToggleOrientation;

            SliderSize = sliderSize ?? meta.SliderSize;
            SliderHandleColor = sliderHandleColor ?? meta.SliderHandleColor;
            SliderTrackColor = sliderTrackColor ?? meta.SliderTrackColor;
            SliderFillColor = sliderFillColor ?? meta.SliderFillColor;
            SliderOrientation = sliderOrientation ?? meta.SliderOrientation;
            SliderLabelPlacement = sliderLabelPlacement ?? meta.SliderLabelPlacement;
            SliderDirection = sliderDirection ?? meta.SliderDirection;

            InputColor = inputColor ?? meta.InputColor;
            InputFontColor = inputFontColor ?? meta.FontColor;
            InputPlaceholderFontColor = inputPlaceholderFontColor ?? meta.InputPlaceholderFontColor;
            InputSize = inputSize ?? meta.InputSize;
            InputLabelPlacement = inputLabelPlacement ?? meta.InputLabelPlacement;
            ToggleLabelPlacement = toggleLabelPlacement ?? meta.ToggleLabelPlacement;
            InputOrientation = inputOrientation ?? meta.InputOrientation;
            InputFont = inputFont ?? meta.InputFont;
            InputFontSize = inputFontSize ?? meta.InputFontSize;

            DDLFont = dDLFont ?? meta.DDLFont;
            DDLListFont = dDLListFont ?? meta.DDLListFont;
            DDLFontSize = dDLFontSize ?? meta.DDLFontSize;
            DDLListFontSize = dDLListFontSize ?? meta.DDLListFontSize;
            DDLButtonColor = dDLButtonColor ?? meta.DDLButtonColor;
            DDLFontColor = dDLFontColor ?? meta.DDLFontColor;
            DDLListColor = dDLListColor ?? meta.DDLListColor;
            DDLListItemFontColor = dDLListItemFontColor ?? meta.DDLListItemFontColor;
            DDLSize = dDLSize ?? meta.DDLSize;

            SelectorOrientation = selectorOrientation ?? meta.SelectorOrientation;
        }
    }

    public static class DefaultHeaderMeta
    {
        public static float DefaultHeaderSize = 40;
        public static int DefaultHeaderFontSize = 18;
        public static int DefaultHeaderExitFontSize = 28;
        public static Color DefaultHeaderColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        public static Color DefaultHeaderExitColor = new Color(0.75f, 0.25f, 0.25f, 1f);
        // Minimize
        // hide button
        // header bar
        // title text
        // resize handle
    }

    public class MoGuiFont
    {
        public int FontSize { get; set; }
        public Font Font { get; set; }
        public Color FontColor { get; set; }
        public int TextMargin { get; set; }
        public TextAlignment TextAlignment { get; set; }
        public TextStyle TextStyle { get; set; }
    }

    public class MoGuiTheme
    {
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Color Background { get; set; }
        public Color Accent { get; set; }
        // ...
    }

    public class MoGuiColor
    {

        Color _base;
        public Func<Color> BoundBase;
        public Color Base 
        { 
            get 
            {
                if (BoundBase != null) 
                {
                    return BoundBase();
                
                } else
                {
                    return _base;
                }
            }
            set { _base = value; }
        }
        public Color Shade
        {
            get
            {
                return MutateColor(Base, DarkFactor);
            }
        }
        public Color Tint
        {
            get
            {
                return MutateColor(Base, Factor);
            }
        }



        public float Factor;
        float? darkFactor = null;


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

        


        public MoGuiColor(Color color, float factor = 0.5f, float? darkfactor = null)
        {
            BoundBase = () => color;
            darkFactor = darkfactor;
            Factor = factor;
        }

        public MoGuiColor(ColorWrapper color, float factor = 0.5f, float? darkfactor = null)
        {
            BoundBase = () => color.Color;
            darkFactor = darkfactor;
            Factor = factor;
        }

        public MoGuiColor(Func<Color> color, float factor = 0.5f, float? darkfactor = null)
        {
            BoundBase = color;
            darkFactor = darkfactor;
            Factor = factor;
        }


        public Color MutateColor( Color color, float factor)
        {
            float r = MutateColorPart(color.r , factor);
            float g = MutateColorPart(color.g, factor);
            float b = MutateColorPart(color.b, factor);
            return new Color(r,g,b,color.a);
        }

        public float MutateColorPart(float value, float factor)
        {
            if(factor >= 0)
            {
                return value + (1 - value) * factor;
            } else
            {
                return value * (1 + factor);

            }
        }

    }
}
