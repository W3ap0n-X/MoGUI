using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


namespace MoGUI
{
    public class OMoGuiMeta
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
        public static Vector4 DefaultInputSize = new Vector4(20, 30, 80, 40);
        public static SliderDirection DefaultSliderDirection = SliderDirection.horizontal;
        public static TextAnchor DefaultTxtAnchor = TextAnchor.MiddleLeft;
    }


    public class MoGuiMeta
    {
        public static Vector2 DefaultTextMinSize = new Vector4(20, 20);
        public static Vector2 DefaultColMinSize = new Vector4(10, 10);
        public static Vector2 DefaultColFlex = new Vector4(1, 1);
        public static Vector2 DefaultRowMinSize = new Vector4(10, 10);
        public static Vector2 DefaultRowFlex = new Vector4(1, 1);
        public static Font DefaultFont = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 24);
        public static int DefaultFontSize = 14;
        public static int DefaultMargin = 5;
        public static MoGuiColor DefaultPanelColor = new MoGuiColor(new Color(0.2f, 0.2f, 0.2f, 0.8f), 0.25f, 0.4f);
        public static MoGuiColor DefaultFontColor = new MoGuiColor(Color.white);
        public static Color DefaultHeaderExitColor = new Color(0.75f, 0.25f, 0.25f, 1f);


        public Font font = DefaultFont;
        public int fontSize = DefaultFontSize;
        public MoGuiColor fontColor = DefaultFontColor;
        public Vector2 TextMinSize = DefaultTextMinSize;
        public MoGuiColor PanelColor = DefaultPanelColor;
        public int Margin = DefaultMargin;
        public ControlOrientation orientation = ControlOrientation.horizontal;

        public string Name;
        public string PluginName;
        public MoGuiMeta(string pluginName, string name)
        {
            PluginName = pluginName;
            Name = name;
            SetPanel(name + "-Panel");
            SetRows(name + "-Row");
            SetCols(name + "-Col");
            SetButton(name + "-Button");
            SetTypography(name + "-Text");
            SetToggle(name + "-Toggle");
            SetColorBlock(name + "-ColorBlock");
            SetInput(name + "-Input");
            SetSlider(name + "-Slider");
            SetSelector(name + "-Selector");
            SetDDL(name + "-DDL");

            Debug.Log("Name: " + name + "\n" + this.ToString());
        }

        public MoGuiMeta(MoGuiMeta meta, string name)
        {
            
            
            PluginName = meta.PluginName;
            Name = name;
            SetPanel(meta.Panel);
            SetRows(meta.Rows);
            SetCols(meta.Cols);
            SetButton(meta.Button);
            SetTypography(meta.Text);
            SetToggle(meta.Toggle);
            SetColorBlock(meta.ColorBlock);
            SetInput(meta.Input);
            SetSlider(meta.Slider);
            SetSelector(meta.Selector);
            SetDDL(meta.DDL);

            Debug.Log("Name: " + name + "\n" + this.ToString());
        }

        public PanelMeta Panel;
        public MoGuiMeta SetPanel(string name)
        {
            Panel = new PanelMeta(name);
            return this;
        }
        public MoGuiMeta SetPanel(PanelMeta meta)
        {
            Panel = meta;
            return this;
        }

        public RowMeta Rows;

        public MoGuiMeta SetRows(string name)
        {
            Rows = new RowMeta(name);
            return this;
        }
        public MoGuiMeta SetRows(RowMeta meta)
        {
            Rows = meta.Copy<RowMeta>();
            return this;
        }

        public ColMeta Cols;

        public MoGuiMeta SetCols(string name)
        {
            Cols = new ColMeta(name);
            return this;
        }
        public MoGuiMeta SetCols(ColMeta meta)
        {
            Cols = meta.Copy<ColMeta>();
            return this;
        }

        public TypographyMeta Text;

        public MoGuiMeta SetTypography(string name)
        {
            Text = new TypographyMeta(name);
            return this;
        }
        public MoGuiMeta SetTypography(TypographyMeta meta)
        {
            Text = meta.Copy<TypographyMeta>();
            return this;
        }

        public ButtonMeta Button;

        public MoGuiMeta SetButton(string name)
        {
            Button = new ButtonMeta(name);
            return this;
        }
        public MoGuiMeta SetButton(ButtonMeta meta)
        {
            Button = meta.Copy<ButtonMeta>();
            return this;
        }

        public ToggleMeta Toggle;

        public MoGuiMeta SetToggle(string name)
        {
            Toggle = new ToggleMeta(name);
            return this;
        }
        public MoGuiMeta SetToggle(ToggleMeta meta)
        {
            Toggle = meta.Copy<ToggleMeta>();
            return this;
        }

        public ColorBlockMeta ColorBlock;

        public MoGuiMeta SetColorBlock(string name)
        {
            ColorBlock = new ColorBlockMeta(name);
            return this;
        }
        public MoGuiMeta SetColorBlock(ColorBlockMeta meta)
        {
            ColorBlock = meta.Copy<ColorBlockMeta>();
            return this;
        }

        public InputMeta Input;

        public MoGuiMeta SetInput(string name)
        {
            Input = new InputMeta(name);
            return this;
        }
        public MoGuiMeta SetInput(InputMeta meta)
        {
            Input = meta.Copy<InputMeta>();
            return this;
        }

        public SliderMeta Slider;

        public MoGuiMeta SetSlider(string name)
        {
            Slider = new SliderMeta(name);
            return this;
        }
        public MoGuiMeta SetSlider(SliderMeta meta)
        {
            Slider = meta.Copy<SliderMeta>();
            return this;
        }

        public SelectorMeta Selector;

        public MoGuiMeta SetSelector(string name)
        {
            Selector = new SelectorMeta(name);
            return this;
        }
        public MoGuiMeta SetSelector(SelectorMeta meta)
        {
            Selector = meta.Copy<SelectorMeta>();
            return this;
        }

        public DDLMeta DDL;

        public MoGuiMeta SetDDL(string name)
        {
            DDL = new DDLMeta(name);
            return this;
        }
        public MoGuiMeta SetDDL(DDLMeta meta)
        {
            DDL = meta.Copy<DDLMeta>();
            return this;
        }

        public MoGuiMeta Copy()
        {
            return (MoGuiMeta)this.MemberwiseClone();
        }
    }

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
                
                } else
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
                return MutateColor(Color, DarkFactor);
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
                return MutateColor(Color, Factor);
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

        public float R
        {
            get => Color.r;
            set { Color = new Color(value, Color.g, Color.b, Color.a); }
        }
        public float G
        {
            get => Color.g;
            set { Color = new Color( Color.r, value, Color.b, Color.a); }
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
