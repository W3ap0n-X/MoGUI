using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoGUI;
using UnityEngine;
using UnityEngine.UI;

namespace MoGUI.Tests
{
    public class ThemeTest : MonoBehaviour
    {
        // Main Gui object
        public MoGui GUI;
        // Keycode used to show the gui if it is closed
        public KeyCode KeyCode;
        // Metadata for the second window
        public MoGuiMeta Meta = new MoGuiMeta("ThemeTest", "Main");
        // flag to check if the Gui has been built.
        bool ui_init = false;



        // Start is called before the first frame update
        void Start()
        {
            SetupVariables(Meta);
            Meta.PanelColor = new Color(0.00f, 0.00f, 0.00f, 0.00f);
            
            // Create the base Gui
            GUI = new MoGui(Meta, "ThemeTest", new Vector2(420, 520), Vector2.zero);
            GUI.Canvas.transform.SetParent(gameObject.transform, false);
            Meta.PanelColor = MoGuiMeta.DefaultPanelColor;


        }

        // Update is called once per frame
        void Update()
        {
            // Check if the Gui has been built yet, if not, build it
            if (!ui_init)
            {
                // Setup content on RootPanel
                BuildUI(GUI.Main);
            }
            else
            {
                // If the Gui is Active run Update() on all active gui items
                if (GUI.IsActive)
                {
                    GUI.Update();
                }
                // If the gui is not active await keypress to show gui
                else if (Input.GetKeyDown(KeyCode))
                {
                    GUI.ShowGui(true);
                }
            }

        }

        MoGuiColor TestColor;
        Color testColor = Color.gray;
        MoGuiColor TestColor0;
        Color testColor0 = Color.red;
        MoGuiColor TestColor1;
        Color testColor1 = Color.gray;
        MoGuiColor TestColor2;
        Color testColor2 = Color.gray;

        MoGuiColor TestColor3;


        Vector2 colorSlideRange = new Vector2(0, 1);

        /* BuildUI
         * This is intended to take a panel and add two columns to test horizontal scrolling
         * The second column contains:
         *  - a subpanel with no header and several text items 
         *  - Button test 
         *      - A text control displaying the name and state of a variable
         *      - A button that toggles the aforementioned variable and displays the same text on it's face.
         * The first column will contain several subpanels
         *  - Subpanels are collapsible
         *      - layout should adjust accordingly when these are collapsed or opened
         *  - Each will be used to test  a set of controls corresponding with it's label.
         */
        public void BuildUI(MoGuiPanel Panel)
        {

            

            //Panel.AddControl("row0", "col0", "FontSize", new MoCaInput((val) => FontSize = (int)val, ()=> FontSize, "FontSize", "int"));
            //Panel.AddControl("row0", "col0", "HeaderFontSize", new MoCaInput((val) => HeaderFontSize = (int)val, () => HeaderFontSize, "HeaderFontSize", "int"));
            //Panel.AddControl("row0", "col0", "HeaderSize", new MoCaInput((val) => HeaderSize = (float)val, () => HeaderSize, "HeaderSize", "float"));

            // Proto-color-picker
            //Panel.AddControl("row0", "col1", "PanelColor", new MoCaColor(() => PanelColor.Color));
            //Panel.AddControl("row0", "col1", "PanelColor-R", new MoCaSlider(colorSlideRange, (value) => PanelColor.R = (float)value, () => PanelColor.R, () => "PanelColor.r=" + PanelColor.R.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col1", "PanelColor-G", new MoCaSlider(colorSlideRange, (value) => PanelColor.G = (float)value, () => PanelColor.G, () => "PanelColor.g=" + PanelColor.G.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col1", "PanelColor-B", new MoCaSlider(colorSlideRange, (value) => PanelColor.B = (float)value, () => PanelColor.B, () => "PanelColor.b=" + PanelColor.B.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col1", "PanelColor-A", new MoCaSlider(colorSlideRange, (value) => PanelColor.A = (float)value, () => PanelColor.A, () => "PanelColor.a=" + PanelColor.A.ToString("f2"), "float"));
            
            BuildColorPicker(Panel, PanelColor, "row0", "col1", "PanelColor");
            
            BuildColorPicker(Panel, HeaderColor, "row0", "col2", "HeaderColor");
            //Panel.AddControl("row0", "col2", "HeaderColor", new MoCaColor(() => HeaderColor));
            //Panel.AddControl("row0", "col2", "HeaderColor-R", new MoCaSlider(colorSlideRange, (value) => HeaderColor.r = (float)value, () => HeaderColor.r, () => "HeaderColor.r=" + HeaderColor.r.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-G", new MoCaSlider(colorSlideRange, (value) => HeaderColor.g = (float)value, () => HeaderColor.g, () => "HeaderColor.g=" + HeaderColor.g.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-B", new MoCaSlider(colorSlideRange, (value) => HeaderColor.b = (float)value, () => HeaderColor.b, () => "HeaderColor.b=" + HeaderColor.b.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-A", new MoCaSlider(colorSlideRange, (value) => HeaderColor.a = (float)value, () => HeaderColor.a, () => "HeaderColor.a=" + HeaderColor.a.ToString("f2"), "float"));

            Panel.AddControl("row1", "col0", "RunTestGui", new MoCaButton(() => "Run Color Test", () => RunTestGui()));

            //TestColor = new MoGuiColor(testColor);
            //Panel.AddControl("row2", "col0", "Color-0", new MoCaColor(() => testColor));
            //Panel.AddControl("row2", "col0", "Color-1", new MoCaColor(() => TestColor.Base));
            //Panel.AddControl("row2", "col0", "Color-2", new MoCaColor(() => TestColor.Shade));
            //Panel.AddControl("row2", "col0", "Color-3", new MoCaColor(() => TestColor.Tint));

            //TestColor0 = new MoGuiColor(testColor0);
            //Panel.AddControl("row2", "col1", "Color0-0", new MoCaColor(() => testColor0));
            //Panel.AddControl("row2", "col1", "Color0-1", new MoCaColor(() => TestColor0.Base));
            //Panel.AddControl("row2", "col1", "Color0-2", new MoCaColor(() => TestColor0.Shade));
            //Panel.AddControl("row2", "col1", "Color0-3", new MoCaColor(() => TestColor0.Tint));

            //TestColor1 = new MoGuiColor(testColor1, 0.8f);
            //Panel.AddControl("row2", "col2", "Color1-0", new MoCaColor(() => testColor1));
            //Panel.AddControl("row2", "col2", "Color1-1", new MoCaColor(() => TestColor1.Base));
            //Panel.AddControl("row2", "col2", "Color1-2", new MoCaColor(() => TestColor1.Shade));
            //Panel.AddControl("row2", "col2", "Color1-3", new MoCaColor(() => TestColor1.Tint));

            //TestColor2 = new MoGuiColor(testColor2, 0.3f);
            //Panel.AddControl("row2", "col3", "Color2-0", new MoCaColor(() => testColor2));
            //Panel.AddControl("row2", "col3", "Color2-1", new MoCaColor(() => TestColor2.Base));
            //Panel.AddControl("row2", "col3", "Color2-2", new MoCaColor(() => TestColor2.Shade));
            //Panel.AddControl("row2", "col3", "Color2-3", new MoCaColor(() => TestColor2.Tint));

            //TestColor3 = new MoGuiColor(() => PanelColor.Color, 0.5f);
            //Panel.AddControl("row2", "col4", "Color3-0", new MoCaColor(() => PanelColor.Color));
            //Panel.AddControl("row2", "col4", "Color3-1", new MoCaColor(() => TestColor3.Base));
            //Panel.AddControl("row2", "col4", "Color3-2", new MoCaColor(() => TestColor3.Shade));
            //Panel.AddControl("row2", "col4", "Color3-3", new MoCaColor(() => TestColor3.Tint));


            ui_init = true;
        }

        int FontSize;
        int HeaderFontSize;
        float HeaderSize;
        ColorWrapper PanelColor;
        ColorWrapper HeaderColor;

        public void SetupVariables(MoGuiMeta meta)
        {
            PanelColor = new ColorWrapper(meta.PanelColor);
            HeaderColor = new ColorWrapper(meta.HeaderColor);
            FontSize = meta.FontSize;
            HeaderFontSize = meta.HeaderFontSize;
            HeaderSize = meta.HeaderSize;
            PanelColor.Color = meta.PanelColor;
            HeaderColor.Color = meta.HeaderColor;
        }
        int WindowCount;
        public void RunTestGui()
        {
            var testMeta = new MoGuiMeta("ThemeTest", "Window" + WindowCount,
                fontSize: FontSize,
                headerFontSize: HeaderFontSize,
                headerSize: HeaderSize,
                panelColor: PanelColor.Color,
                headerColor: HeaderColor.Color
            );
            GUI.AddPanel(testMeta, "Window" + WindowCount, new Vector2(450, 300), Vector2.zero);
            WindowCount++;

        }

        public void BuildColorPicker(MoGuiPanel Panel, ColorWrapper color, string row, string col, string name)
        {
            //Panel.Meta.PanelColor = MoGuiMeta.DefaultPanelColor;
            var PickerPanel = Panel.AddPanel( row, col, name, new MoCaPanel(true, name));
            //PickerPanel.AddScrollArea();
            PickerPanel.Meta.TxtMargin = 0;
            PickerPanel.Meta.TxtAnchor = TextAnchor.MiddleCenter;
            PickerPanel.Meta.SliderDirection = SliderDirection.vertical;
            PickerPanel.Meta.SliderLabelPlacement = ControlLabelPlacement.none;
            PickerPanel.Meta.InputLabelPlacement = ControlLabelPlacement.none;

            VerticalLayoutGroup layoutGroup = PickerPanel.Container.GetComponent<VerticalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;
            LayoutElement layoutElement = PickerPanel.Container.GetComponent<LayoutElement>();
            layoutElement.preferredWidth = 280;
            layoutElement.preferredHeight = 380;

            MoGuiColor _color = new MoGuiColor(() => color.Color, 0.5f);
            PickerPanel.AddControl("0", "0", name + "_preview",new MoCaColor(() => _color.Base));
            PickerPanel.AddControl("0", "1", name + "_previewT", new MoCaColor(() => _color.Tint));
            PickerPanel.AddControl("0", "1", name + "_previewS", new MoCaColor(() => _color.Shade));

            PickerPanel.Meta.SliderSize.z = 60;
            PickerPanel.Meta.SliderTrackColor.a = 0.25f;

            var rSlider = PickerPanel.AddSlider("sliders", "r", name + "-R", new MoCaSlider(colorSlideRange, (value) => color.R = (float)value, () => color.R, () => color.R.ToString("f2"), "float", meta: PickerPanel.Meta));



            var gSlider = PickerPanel.AddSlider("sliders", "g", name + "-G", new MoCaSlider(colorSlideRange, (value) => color.G = (float)value, () => color.G, () => color.G.ToString("f2"), "float"));

            var bSlider = PickerPanel.AddSlider("sliders", "b", name + "-B", new MoCaSlider(colorSlideRange, (value) => color.B = (float)value, () => color.B, () => color.B.ToString("f2"), "float"));
            var aSlider = PickerPanel.AddSlider("sliders", "a", name + "-A", new MoCaSlider(colorSlideRange, (value) => color.A = (float)value, () => color.A, () => color.A.ToString("f2"), "float"));
            var lFactorSlider = PickerPanel.AddSlider("sliders", "lFactor", name + "-lFactor", new MoCaSlider(colorSlideRange, (value) => _color.Factor = (float)value, () => _color.Factor, () => _color.Factor.ToString("f2"), "float"));
            var dFactorSlider = PickerPanel.AddSlider("sliders", "dFactor", name + "-dFactor", new MoCaSlider(colorSlideRange, (value) => _color.DarkFactor = (float)value * -1, () => _color.DarkFactor * -1, () => _color.Factor.ToString("f2"), "float"));

            rSlider.BoundFillColor = () => new Color(rSlider.Value, 0, 0, aSlider.Value);
            rSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            gSlider.BoundFillColor = () => new Color(0, gSlider.Value,  0, aSlider.Value);
            gSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            bSlider.BoundFillColor = () => new Color(0, 0, bSlider.Value, aSlider.Value);
            bSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            aSlider.BoundFillColor = () => new Color(1,1,1, aSlider.Value);
            aSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            lFactorSlider.BoundFillColor = () => new Color(0.5f + (lFactorSlider.Value/2), 0.5f + (lFactorSlider.Value / 2), 0.5f + (lFactorSlider.Value / 2), 1);
            lFactorSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            dFactorSlider.BoundFillColor = () => new Color(0.5f - (dFactorSlider.Value/2), 0.5f - (dFactorSlider.Value / 2), 0.5f - (dFactorSlider.Value / 2), 1);
            dFactorSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            PickerPanel.Meta.InputFontSize = 11;
            PickerPanel.AddControl("output", "0", name + "-Output", new MoCaInput(color.Color.ToString(), onUpdateAction: () => "new Color(" + color.R.ToString("f2") + "f, " + color.G.ToString("f2") + "f, " + color.B.ToString("f2") + "f," + color.A.ToString("f2") + "f)", text:"Base", valType:"none"));
            PickerPanel.AddControl("output", "0", name + "-TOutput", new MoCaInput(_color.Tint.ToString(), onUpdateAction: () => "new Color(" + _color.Tint.r.ToString("f2") + "f, " + _color.Tint.g.ToString("f2") + "f, " + _color.Tint.b.ToString("f2") + "f," + _color.Tint.a.ToString("f2") + "f)", text: "Tint", valType: "none"));
            PickerPanel.AddControl("output", "0", name + "-SOutput", new MoCaInput(_color.Shade.ToString(), onUpdateAction: () => "new Color(" + _color.Shade.r.ToString("f2") + "f, " + _color.Shade.g.ToString("f2") + "f, " + _color.Shade.b.ToString("f2") + "f," + _color.Shade.a.ToString("f2") + "f)", text: "Shade", valType: "none"));
            var outputRowRect = PickerPanel.GetRow("output").Obj.GetComponent<RectTransform>();
            outputRowRect.anchoredPosition = new Vector2(0, 0);
            outputRowRect.anchorMax = Vector2.one;
            outputRowRect.anchorMin = Vector2.zero;

            PickerPanel.Meta.InputFontSize = 9;
            PickerPanel.Meta.InputSize.w = 20;
            PickerPanel.AddInput("sliders", "r", name + "-R_Input", new MoCaInput((value) => color.R = (float)value, () => color.R, "R", "float", meta: PickerPanel.Meta));
            PickerPanel.AddInput("sliders", "g", name + "-G_Input", new MoCaInput((value) => color.G = (float)value, () => color.G, "G", "float"));
            PickerPanel.AddInput("sliders", "b", name + "-B_Input", new MoCaInput((value) => color.B = (float)value, () => color.B, "B", "float"));
            PickerPanel.AddInput("sliders", "a", name + "-A_Input", new MoCaInput((value) => color.A = (float)value, () => color.A, "A", "float"));
            PickerPanel.AddInput("sliders", "lFactor", name + "-lFactor_Input", new MoCaInput((value) => _color.Factor = (float)value, () => _color.Factor, "lFactor", "float"));
            PickerPanel.AddInput("sliders", "dFactor", name + "-dFactor_Input", new MoCaInput((value) => _color.DarkFactor = (float)value * -1, () => _color.DarkFactor * -1, "dFactor", "float"));


            var sliderRow = PickerPanel.GetRow("sliders").Obj.GetComponent<HorizontalLayoutGroup>();
            sliderRow.padding = new RectOffset(3, 3, 5, 5);
            sliderRow.spacing = 5;
        }
    }
}
