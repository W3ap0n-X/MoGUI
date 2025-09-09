using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoGUI;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

namespace MoGUI.Tests
{
    public class ThemeTest : MonoBehaviour
    {
        // Main Gui object
        public MoGui GUI;
        // Keycode used to show the gui if it is closed
        public KeyCode KeyCode;
        // Metadata for the second window
        public MoGuiMeta Meta;
        // flag to check if the Gui has been built.
        bool ui_init = false;



        // Start is called before the first frame update
        void Start()
        {
            Meta = new MoGuiMeta("ThemeTest", "Main");
            SetupVariables(Meta);
            

            // Create the base Gui
            GUI = new MoGui( "ThemeTest", new Vector2(420, 520), Vector2.zero, new Color(0.00f, 0.00f, 0.00f, 0.00f));
            GUI.Canvas.transform.SetParent(gameObject.transform, false);


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
        Vector2 factorSlideRange = new Vector2(-1, 1);

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
            
            BuildColorPicker(Panel, () => Meta.Colors.Panel, "row0", "col1", "PanelColor");
            BuildColorPicker(Panel, () => Meta.Colors.Text, "row0", "col2", "TextColor");
            BuildColorPicker(Panel, () => Meta.Colors.Control, "row0", "col3", "CtlColor");
            BuildColorPicker(Panel, () => Meta.Colors.Header, "row0", "col4", "Header");
            Panel.AddControl("rowB", "col0", "RebuildPalette", new MoCaButton(() => "RebuildPalette", () => RebuildPalette()));
            //TestColor0 = new MoGuiColor(testColor0);
            //Panel.AddControl("row0", "col2", "HeaderColor", new MoCaColor(() => HeaderColor));
            //Panel.AddControl("row0", "col2", "HeaderColor-R", new MoCaSlider(colorSlideRange, (value) => HeaderColor.r = (float)value, () => HeaderColor.r, () => "HeaderColor.r=" + HeaderColor.r.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-G", new MoCaSlider(colorSlideRange, (value) => HeaderColor.g = (float)value, () => HeaderColor.g, () => "HeaderColor.g=" + HeaderColor.g.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-B", new MoCaSlider(colorSlideRange, (value) => HeaderColor.b = (float)value, () => HeaderColor.b, () => "HeaderColor.b=" + HeaderColor.b.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-A", new MoCaSlider(colorSlideRange, (value) => HeaderColor.a = (float)value, () => HeaderColor.a, () => "HeaderColor.a=" + HeaderColor.a.ToString("f2"), "float"));
            Panel.Meta.Input.Orientation(ControlOrientation.vertical);
            Panel.AddInput("rowT", "col0", "LumTest", new MoCaInput(null, () => Meta.Colors.Panel.Luminance.ToString("f4"), () => "Luminance", "none"));
            Panel.AddInput("rowT", "col1", "LumTest2", new MoCaInput(null, () => Meta.Colors.Panel.Luminance2.ToString("f4"), () => "LightBalance", "none"));
            Panel.AddInput("rowT", "col1", "LumTest3", new MoCaInput(null, () => Meta.Colors.Panel.LBShade.ToString("f4"), () => "LightBalShade", "none"));
            Panel.AddInput("rowT", "col1", "LumTest4", new MoCaInput(null, () => Meta.Colors.Panel.LBTint.ToString("f4"), () => "LightBalTint", "none"));
            Panel.AddInput("rowT", "col3", "Contrast", new MoCaInput(null, () => Meta.Colors.Panel.Contrast.ToString("f4"), () => "Contrast", "none"));
            Panel.AddInput("rowT", "col3", "ContrastS", new MoCaInput(null, () => Meta.Colors.Panel.ContrastS.ToString("f4"), () => "ContrastS", "none"));
            Panel.AddInput("rowT", "col3", "ContrastT", new MoCaInput(null, () => Meta.Colors.Panel.ContrastT.ToString("f4"), () => "ContrastT", "none"));
            Panel.AddInput("rowT", "col0", "ShadeLum", new MoCaInput(null, () => Meta.Colors.Panel.Tv.ToString("f4"), () => "ShadeLum", "none"));
            
            Panel.AddInput("rowT", "col4", "SomethingFont0", new MoCaInput(null, () => Meta.Colors.Panel.Tv2.ToString("f4"), () => "SomethingFont0", "none"));
            Panel.AddInput("rowT", "col4", "SomethingFont1", new MoCaInput(null, () => Meta.Colors.Panel.Tv4.ToString("f4"), () => "SomethingFont1", "none"));
            Panel.AddInput("rowT", "col4", "SomethingFont2", new MoCaInput(null, () => Meta.Colors.Panel.Tv5.ToString("f4"), () => "SomethingFont2", "none"));
            

            Panel.AddInput("rowT", "col0", "TintLum", new MoCaInput(null, () => Meta.Colors.Panel.Tv3.ToString("f4"), () => "TintLum", "none"));

            Panel.AddControl("row1", "col0", "RunTestGui", new MoCaButton(() => "Run Color Test", () => RunTestGui()));

            //TestColor = new MoGuiColor(testColor);
            //Panel.AddControl("row2", "col0", "Color-0", new MoCaColor(() => testColor));
            //Panel.AddControl("row2", "col0", "Color-1", new MoCaColor(() => TestColor.Base));
            //Panel.AddControl("row2", "col0", "Color-2", new MoCaColor(() => TestColor.Shade));
            //Panel.AddControl("row2", "col0", "Color-3", new MoCaColor(() => TestColor.Tint));


            //Panel.AddControl("row2", "col1", "Color0-0", new MoCaColor(() => testColor0));
            //Panel.AddControl("row2", "col1", "Color0-1", new MoCaColor(() => TestColor0.Color));
            //Panel.AddControl("row2", "col1", "Color0-2", new MoCaColor(() => TestColor0.Shade));
            //Panel.AddControl("row2", "col1", "Color0-3", new MoCaColor(() => TestColor0.Tint));


            //Panel.AddControl("row2", "col1", "changeColor00", new MoCaButton(() => "Change", () => testColor0 = Color.blue));

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
            //PanelColor = new ColorWrapper(meta.Panel.background.Color);
            //HeaderColor = new ColorWrapper(meta.Panel.Header.background);
            //FontSize = meta.FontSize;
            //HeaderFontSize = meta.HeaderFontSize;
            //HeaderSize = meta.HeaderSize;
            //PanelColor.Color = meta.PanelColor.Color;
            //HeaderColor.Color = meta.HeaderColor;
        }
        int WindowCount;
        public void RunTestGui()
        {
            var testMeta = new MoGuiMeta("ThemeTest", "Window" + WindowCount, Meta.Colors);
            var newPanel = GUI.AddPanel(testMeta, "Window" + WindowCount, new Vector2(440, 320), Vector2.zero);
            
            WindowCount++;
            AddSampleControls(newPanel);

        }
        public void RebuildPalette()
        {
            Meta.SetPalette(Meta.Colors.Panel);
            Meta.Build();

        }

        int int1 = 0;
        public Dictionary<string, object> testOptions0 = new Dictionary<string, object>() {
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 }
        };
        public void AddSampleControls(MoGuiPanel panel)
        {
            panel.AddText("rowA", "col0", "visible", new MoCaText(() => "visibility:" + panel.Meta.Colors.Panel.Contrast.ToString("f4"), TextElement.h1));

            panel.AddText("row", "col", "text", new MoCaText( "Text"));
            panel.AddButton("row", "col", "button", new MoCaButton(() => "Button", () => Debug.Log("boop")));
            panel.AddToggle("row", "col", "toggle", new MoCaToggle(false, text: "toggle"));
            panel.AddSlider("row", "col1", "slider" , new MoCaSlider(new Vector2(0,1), (value) => int1 = (int)value, () => int1, () => "Slider" , "int"));
            panel.AddInput("row", "col1", "input", new MoCaInput(null, "none", "Input"));
            panel.AddDDL("row", "col1", "ddl", new MoCaDDL(new List<string>() { "option" }));
            panel.AddControl("row0", "col", "TestSelectGroup1", new MoCaSelector(testOptions0, "TestSelectGroup1", direction: ControlOrientation.horizontal));
        }


        public void BuildColorPicker(MoGuiPanel Panel, Func<MoGuiColor> color, string row, string col, string name)
        {
            //Panel.Meta.PanelColor = MoGuiMeta.DefaultPanelColor;
            //MoGuiMeta btMeta = new MoGuiMeta(Meta, "headerButtons");
            var PickerPanel = Panel.AddPanel(row, col, name, new MoCaPanel(true, name));
            PickerPanel.Container.GetComponent<VerticalLayoutGroup>().spacing = 0;
            MoGuiMeta btMeta = PickerPanel.Meta.SetMargin(0);
            //Meta.Margin = 0;

            //Meta.Margin = 5;
            //PickerPanel.AddScrollArea();
            //btMeta.Rows = (RowMeta)(new RowMeta(btMeta, "rows").Margin(0).Spacing(0));
            //btMeta.Cols = (ColMeta)(new ColMeta(btMeta, "rows").Margin(0).Spacing(0));

            //PickerPanel.Meta.TxtAnchor = TextAnchor.MiddleCenter;
            //PickerPanel.Meta.SliderDirection = SliderDirection.vertical;
            //PickerPanel.Meta.SliderLabelPlacement = ControlLabelPlacement.none;
            //PickerPanel.Meta.InputLabelPlacement = ControlLabelPlacement.none;

            //VerticalLayoutGroup layoutGroup = PickerPanel.Container.GetComponent<VerticalLayoutGroup>();
            //layoutGroup.childForceExpandHeight = false;
            //LayoutElement layoutElement = PickerPanel.Container.GetComponent<LayoutElement>();
            //layoutElement.preferredWidth = 280;
            //layoutElement.preferredHeight = 380;


            var previewRow = PickerPanel.AddRow("0");
            //previewRow.Obj.GetComponent<HorizontalLayoutGroup>().padding.top = 0;
            //previewRow.Obj.GetComponent<HorizontalLayoutGroup>().padding.bottom = 0;
            //previewRow.Obj.GetComponent<HorizontalLayoutGroup>().padding.left = 0;
            //previewRow.Obj.GetComponent<HorizontalLayoutGroup>().padding.right = 0;

            PickerPanel.AddControl("0", "0", name + "_preview", new MoCaColor(() => color().Color, meta: btMeta));
            PickerPanel.AddControl("0", "0", name + "_previewRaw", new MoCaColor(() => color().Raw, meta: btMeta));
            PickerPanel.AddControl("0", "1", name + "_previewT", new MoCaColor(() => color().Tint, meta: btMeta));
            
            PickerPanel.AddControl("0", "1", name + "_previewS", new MoCaColor(() => color().Shade, meta: btMeta));
            PickerPanel.AddControl("0", "1", name + "_previewTRaw", new MoCaColor(() => color().TintRaw, meta: btMeta));
            PickerPanel.AddControl("0", "1", name + "_previewSRaw", new MoCaColor(() => color().ShadeRaw, meta: btMeta));


            //previewRow.GetCol("0")
            //PickerPanel.Meta.SliderSize.z = 60;
            //PickerPanel.Meta.SliderTrackColor.a = 0.25f;



            btMeta.Slider.Orientation(ControlOrientation.vertical);
            btMeta.Slider.LabelPlacement(ControlLabelPlacement.none);
            btMeta.Slider.Size(new Vector2(25, 60));
            btMeta.Input.LabelPlacement(ControlLabelPlacement.none);
            var rSlider = PickerPanel.AddSlider("sliders", "r", name + "-R", new MoCaSlider(colorSlideRange, (value) => color().R = (float)value, () => color().R, () => color().R.ToString("f2"), "float", meta: btMeta, direction: ControlOrientation.vertical));



            var gSlider = PickerPanel.AddSlider("sliders", "g", name + "-G", new MoCaSlider(colorSlideRange, (value) => color().G = (float)value, () => color().G, () => color().G.ToString("f2"), "float", meta: btMeta, direction: ControlOrientation.vertical));

            var bSlider = PickerPanel.AddSlider("sliders", "b", name + "-B", new MoCaSlider(colorSlideRange, (value) => color().B = (float)value, () => color().B, () => color().B.ToString("f2"), "float", meta: btMeta, direction: ControlOrientation.vertical));
            var aSlider = PickerPanel.AddSlider("sliders", "a", name + "-A", new MoCaSlider(colorSlideRange, (value) => color().A = (float)value, () => color().A, () => color().A.ToString("f2"), "float", meta: btMeta, direction: ControlOrientation.vertical));
            var lFactorSlider = PickerPanel.AddSlider("sliders", "lFactor", name + "-lFactor", new MoCaSlider(factorSlideRange, (value) => color().Factor = (float)value, () => color().Factor, () => color().Factor.ToString("f2"), "float", meta: btMeta, direction: ControlOrientation.vertical));
            var dFactorSlider = PickerPanel.AddSlider("sliders", "dFactor", name + "-dFactor", new MoCaSlider(factorSlideRange, (value) => color().DarkFactor = (float)value * -1, () => color().DarkFactor * -1, () => color().Factor.ToString("f2"), "float", meta: btMeta, direction: ControlOrientation.vertical));
            
            rSlider.BoundFillColor = () => new Color(rSlider.Value, 0, 0, aSlider.Value);
            rSlider.Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding.bottom = 12;
            gSlider.BoundFillColor = () => new Color(0, gSlider.Value, 0, aSlider.Value);
            gSlider.Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding.bottom = 12;
            bSlider.BoundFillColor = () => new Color(0, 0, bSlider.Value, aSlider.Value);
            bSlider.Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding.bottom = 12;
            aSlider.BoundFillColor = () => new Color(1, 1, 1, aSlider.Value);
            aSlider.Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding.bottom = 12;
            lFactorSlider.BoundFillColor = () => new Color(1 - (lFactorSlider.Value / 2), 1 - (lFactorSlider.Value / 2), 1 - (lFactorSlider.Value / 2), 1);
            lFactorSlider.Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding.bottom = 12;
            dFactorSlider.BoundFillColor = () => new Color(1 + (dFactorSlider.Value / 2), 1 + (dFactorSlider.Value / 2), 1 + (dFactorSlider.Value / 2), 1);
            dFactorSlider.Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding.bottom = 12;

            btMeta.Input.inputSettings.FontSize = 11;
            PickerPanel.AddControl("output", "0", name + "-Output", new MoCaInput(color().Color.ToString(), onUpdateAction: () => "new Color(" + color().R.ToString("f2") + "f, " + color().G.ToString("f2") + "f, " + color().B.ToString("f2") + "f," + color().A.ToString("f2") + "f)", text: "Base", valType: "none", meta: btMeta));
            PickerPanel.AddControl("output", "0", name + "-TOutput", new MoCaInput(color().Tint.ToString(), onUpdateAction: () => "new Color(" + color().Tint.r.ToString("f2") + "f, " + color().Tint.g.ToString("f2") + "f, " + color().Tint.b.ToString("f2") + "f," + color().Tint.a.ToString("f2") + "f)", text: "Tint", valType: "none", meta: btMeta));
            PickerPanel.AddControl("output", "0", name + "-SOutput", new MoCaInput(color().Shade.ToString(), onUpdateAction: () => "new Color(" + color().Shade.r.ToString("f2") + "f, " + color().Shade.g.ToString("f2") + "f, " + color().Shade.b.ToString("f2") + "f," + color().Shade.a.ToString("f2") + "f)", text: "Shade", valType: "none", meta: btMeta));
            //var outputRowRect = PickerPanel.GetRow("output").Obj.GetComponent<RectTransform>();
            //outputRowRect.anchoredPosition = new Vector2(0, 0);
            //outputRowRect.anchorMax = Vector2.one;
            //outputRowRect.anchorMin = Vector2.zero;

            //PickerPanel.Meta.InputFontSize = 9;
            btMeta.Input.inputSettings.FontSize = 9;
            btMeta.Input.sizing.minWidth = 12;
            btMeta.Input.sizing.preferredWidth = 15;
            btMeta.Input.sizing.preferredHeight = 15;
            //PickerPanel.Meta.InputSize.w = 20;
            PickerPanel.AddInput("sliders", "r", name + "-R_Input", new MoCaInput((value) => color().R = (float)value, () => color().R, "R", "float", meta: btMeta)).Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding = new RectOffset(0, 0, 0, 0);
            PickerPanel.AddInput("sliders", "g", name + "-G_Input", new MoCaInput((value) => color().G = (float)value, () => color().G, "G", "float", meta: btMeta)).Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding = new RectOffset(0, 0, 0, 0);
            PickerPanel.AddInput("sliders", "b", name + "-B_Input", new MoCaInput((value) => color().B = (float)value, () => color().B, "B", "float", meta: btMeta)).Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding = new RectOffset(0, 0, 0, 0);
            PickerPanel.AddInput("sliders", "a", name + "-A_Input", new MoCaInput((value) => color().A = (float)value, () => color().A, "A", "float", meta: btMeta)).Container.GetComponent<HorizontalOrVerticalLayoutGroup>().padding = new RectOffset(0,0,0,0);
            PickerPanel.AddInput("sliders", "lFactor", name + "-lFactor_Input", new MoCaInput((value) => color().Factor = (float)value, () => color().Factor, "lFactor", "float", meta: btMeta));
            PickerPanel.AddInput("sliders", "dFactor", name + "-dFactor_Input", new MoCaInput((value) => color().DarkFactor = (float)value * -1, () => color().DarkFactor * -1, "dFactor", "float", meta: btMeta));


            var sliderRow = PickerPanel.GetRow("sliders").Obj.GetComponent<HorizontalLayoutGroup>();
            sliderRow.padding = new RectOffset(3, 3, 5, 5);
            sliderRow.spacing = 0;
        }
    }
}
