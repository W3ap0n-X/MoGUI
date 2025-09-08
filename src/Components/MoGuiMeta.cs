using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



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
        public static Color DefaultHeaderExitColor = new Color(0.75f, 0.25f, 0.25f, 1f);
        

        public GuiColorSet Colors;

        public Font font = DefaultFont;
        public int fontSize = DefaultFontSize;
        public MoGuiColor fontColor;
        public Vector2 TextMinSize = DefaultTextMinSize;
        public int Margin = DefaultMargin;
        public ControlOrientation orientation = ControlOrientation.horizontal;

        public string Name;
        public string PluginName;

        private void Defaults()
        {
            fontColor = Colors.Text;
        }

        public MoGuiMeta SetMargin(int margin)
        {
            Margin = margin;
            return this;
        }

        public MoGuiMeta(string pluginName, string name, Color? baseColor = null)
        {
            //_baseColor = Color.cyan;

            Colors = new GuiColorSet(baseColor);
            PluginName = pluginName;
            Name = name;

            Defaults();
            Build();



            Debug.Log("Name: " + name + "\n" + this.ToString());
        }

        public MoGuiMeta(string pluginName, string name, GuiColorSet set)
        {
            //_baseColor = Color.cyan;

            Colors = new GuiColorSet(set);
            PluginName = pluginName;
            Name = name;

            Defaults();
            Build();



            Debug.Log("Name: " + name + "\n" + this.ToString());
        }

        public MoGuiMeta(MoGuiMeta meta, string name)
        {
            Colors = new GuiColorSet(meta.Colors);
            PluginName = meta.PluginName;
            Name = name;

            Defaults();
            Build(meta);



            Debug.Log("Name: " + name + "\n" + this.ToString());
        }


        public MoGuiMeta Build(MoGuiMeta meta = null)
        {
            if (meta == null)
            {
                SetPanel(Name + "-Panel");
                SetRows(Name + "-Row");
                SetCols(Name + "-Col");
                SetButton(Name + "-Button");
                SetTypography(Name + "-Text");
                SetToggle(Name + "-Toggle");
                SetColorBlock(Name + "-ColorBlock");
                SetInput(Name + "-Input");
                SetSlider(Name + "-Slider");
                SetSelector(Name + "-Selector");
                SetDDL(Name + "-DDL");
            }
            else 
            {
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
            }
            
            return this;
        }



        public PanelMeta Panel;
        public MoGuiMeta SetPanel(string name)
        {
            Panel = new PanelMeta(this,name);
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
            Rows = new RowMeta(this, name);
            return this;
        }
        public MoGuiMeta SetRows(RowMeta meta)
        {
            Rows = meta.Copy<RowMeta>(this);
            return this;
        }

        public ColMeta Cols;

        public MoGuiMeta SetCols(string name)
        {
            Cols = new ColMeta(this, name);
            return this;
        }
        public MoGuiMeta SetCols(ColMeta meta)
        {
            Cols = meta.Copy<ColMeta>(this);
            return this;
        }

        public TypographyMeta Text;

        public MoGuiMeta SetTypography(string name)
        {
            Text = new TypographyMeta(this, name);
            return this;
        }
        public MoGuiMeta SetTypography(TypographyMeta meta)
        {
            Text = meta.Copy<TypographyMeta>(this);
            return this;
        }

        public ButtonMeta Button;

        public MoGuiMeta SetButton(string name)
        {
            Button = new ButtonMeta(this, name);
            return this;
        }
        public MoGuiMeta SetButton(ButtonMeta meta)
        {
            Button = meta.Copy<ButtonMeta>(this);
            return this;
        }

        public ToggleMeta Toggle;

        public MoGuiMeta SetToggle(string name)
        {
            Toggle = new ToggleMeta(this, name);
            return this;
        }
        public MoGuiMeta SetToggle(ToggleMeta meta)
        {
            Toggle = meta.Copy<ToggleMeta>(this);
            return this;
        }

        public ColorBlockMeta ColorBlock;

        public MoGuiMeta SetColorBlock(string name)
        {
            ColorBlock = new ColorBlockMeta(this, name);
            return this;
        }
        public MoGuiMeta SetColorBlock(ColorBlockMeta meta)
        {
            ColorBlock = meta.Copy<ColorBlockMeta>(this);
            return this;
        }

        public InputMeta Input;

        public MoGuiMeta SetInput(string name)
        {
            Input = new InputMeta(this, name);
            return this;
        }
        public MoGuiMeta SetInput(InputMeta meta)
        {
            Input = meta.Copy<InputMeta>(this);
            return this;
        }

        public SliderMeta Slider;

        public MoGuiMeta SetSlider(string name)
        {
            Slider = new SliderMeta(this, name);
            return this;
        }
        public MoGuiMeta SetSlider(SliderMeta meta)
        {
            Slider = meta.Copy<SliderMeta>(this);
            return this;
        }

        public SelectorMeta Selector;

        public MoGuiMeta SetSelector(string name)
        {
            Selector = new SelectorMeta(this, name);
            return this;
        }
        public MoGuiMeta SetSelector(SelectorMeta meta)
        {
            Selector = meta.Copy<SelectorMeta>(this);
            return this;
        }

        public DDLMeta DDL;

        public MoGuiMeta SetDDL(string name)
        {
            DDL = new DDLMeta(this, name);
            return this;
        }
        public MoGuiMeta SetDDL(DDLMeta meta)
        {
            DDL = meta.Copy<DDLMeta>(this);
            return this;
        }

        public MoGuiMeta Copy()
        {
            return (MoGuiMeta)this.MemberwiseClone();
        }

        public void SetPalette(Color newColor)
        {
            Colors = new GuiColorSet(newColor);
        }

        public void SetPalette(MoGuiColor newColor)
        {
            Colors = new GuiColorSet(newColor);
        }
    }

    
}
