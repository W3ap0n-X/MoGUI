using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGui 
    {
        public GameObject Canvas;
        public MoGuiPanel Main;
        public string PluginName;
        public MoGuiMeta Meta;

        public static GuiMeta TestMeta = new GuiMeta("TEST");

        //GuiMeta TestMeta;

        public Dictionary<string, MoGuiPanel> Panels = new Dictionary<string, MoGuiPanel>();
        public MoGui(string pluginName, Vector2 size, Vector2 pos)
        {
            Meta = new MoGuiMeta(pluginName, pluginName);
            Init(size, pos);
        }

        public MoGui(MoGuiMeta meta, string pluginName, Vector2 size, Vector2 pos)
        {
            Meta = new MoGuiMeta(meta, pluginName);
            Init(size, pos);
        }

        void Init(Vector2 size, Vector2 pos)
        {
            //TestMeta = new GuiMeta("TEST");
            PluginName = Meta.PluginName;
            Canvas = CreateCanvas();
            Main = new MoGuiPanel(Meta, "Main", Canvas, size, pos);
            Main.Obj.transform.SetParent(Canvas.transform, false);
            Panels.Add("Main", Main);
        }

        public GameObject CreateCanvas()
        {
            var canvasObject = new GameObject(PluginName + "_" + "Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            return canvasObject;
        } 
        public MoGuiPanel AddPanel(string label, Vector2 size, Vector2 pos)
        {
            return AddPanel(Meta, label, size, pos);
        }

        public MoGuiPanel AddPanel(MoGuiMeta meta, string label, Vector2 size, Vector2 pos)
        {
            MoGuiPanel newPanel;
            if (Panels.ContainsKey(label))
            {
                newPanel = Panels[label];
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
            }
            else
            {
                newPanel = new MoGuiPanel(meta, label, size, pos);
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
                Panels.Add(label, newPanel);
            }
            return newPanel;
        }

        public void Update()
        {
            if (IsActive)
            {
                foreach (var item in Panels)
                {
                    item.Value.Update();
                }
            }
        }

        public void ShowGui(bool show)
        {
            Canvas.SetActive(show);
        }

        public bool IsActive
        {
            get => Canvas.activeSelf;
        }

    }

    public class GuiMeta
    {
        public static Vector2 DefaultTextMinSize = new Vector4(20, 20);
        public static Vector2 DefaultColMinSize = new Vector4(10, 10);
        public static Vector2 DefaultColFlex = new Vector4(1, 1);
        public static Vector2 DefaultRowMinSize = new Vector4(10, 10);
        public static Vector2 DefaultRowFlex = new Vector4(1, 1);
        public static Font DefaultFont = UnityEngine.Font.CreateDynamicFontFromOSFont("Arial", 24);
        public static int DefaultFontSize = 14;
        public static int DefaultMargin = 5;
        public static MoGuiColor DefaultPanelColor = new MoGuiColor(new Color(0.2f, 0.2f, 0.2f, 0.8f), 0.25f,0.4f);
        public static MoGuiColor DefaultFontColor = new MoGuiColor(Color.white);
        public static Color DefaultHeaderExitColor = new Color(0.75f, 0.25f, 0.25f, 1f);


        public Font font = DefaultFont;
        public int fontSize = DefaultFontSize;
        public MoGuiColor fontColor = DefaultFontColor;
        public Vector2 TextMinSize = DefaultTextMinSize;
        public MoGuiColor PanelColor = DefaultPanelColor;
        public int Margin = DefaultMargin;

        public string Name;

        public GuiMeta(string name)
        {
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

        public  PanelMeta Panel;
        public GuiMeta SetPanel(string name)
        {
            Panel = new PanelMeta(name);
            return this;
        }

        public LayoutMeta Rows;

        public GuiMeta SetRows(string name)
        {
            Rows = new LayoutMeta(name);
            return this;
        }

        public LayoutMeta Cols;

        public GuiMeta SetCols(string name)
        {
            Cols = new LayoutMeta(name);
            return this;
        }

        public TypographyMeta Text;

        public GuiMeta SetTypography(string name)
        {
            Text = new TypographyMeta(name);
            return this;
        }

        public ButtonMeta Button;

        public GuiMeta SetButton(string name)
        {
            Button = new ButtonMeta(name);
            return this;
        }

        public ToggleMeta Toggle;

        public GuiMeta SetToggle(string name)
        {
            Toggle = new ToggleMeta(name);
            return this;
        }

        public ColorBlockMeta ColorBlock;

        public GuiMeta SetColorBlock(string name)
        {
            ColorBlock = new ColorBlockMeta(name);
            return this;
        }

        public InputMeta Input;

        public GuiMeta SetInput(string name)
        {
            Input = new InputMeta(name);
            return this;
        }

        public SliderMeta Slider;

        public GuiMeta SetSlider(string name)
        {
            Slider = new SliderMeta(name);
            return this;
        }

        public SelectorMeta Selector;

        public GuiMeta SetSelector(string name)
        {
            Selector = new SelectorMeta(name);
            return this;
        }

        public DDLMeta DDL;

        public GuiMeta SetDDL(string name)
        {
            DDL = new DDLMeta(name);
            return this;
        }
    }

    public abstract class ComponentMeta
    {
        string ClassName;
        int margin;

        public ComponentMeta(string name)
        {
            ClassName = name;
            Debug.Log("Name: " + name + "\n" + this.ToString());
        }

        public ComponentMeta Margin(int _margin)
        {
            margin = _margin;
            return this;
        }

        public ComponentMeta Class(string className)
        {
            ClassName = className;
            return this;
        }

        //public abstract void SetDefaults();
    }

}
