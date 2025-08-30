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
            Panel.AddControl("row0", "col1", "BackGroundColorTitle", new MoCaText("BackGround Color"));
            BuildColorPicker(Panel, PanelColor, "row0", "col1", "PanelColor");
            Panel.AddControl("row0", "col2", "HeaderColorTitle", new MoCaText("Header Color"));
            BuildColorPicker(Panel, HeaderColor, "row0", "col2", "HeaderColor");
            //Panel.AddControl("row0", "col2", "HeaderColor", new MoCaColor(() => HeaderColor));
            //Panel.AddControl("row0", "col2", "HeaderColor-R", new MoCaSlider(colorSlideRange, (value) => HeaderColor.r = (float)value, () => HeaderColor.r, () => "HeaderColor.r=" + HeaderColor.r.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-G", new MoCaSlider(colorSlideRange, (value) => HeaderColor.g = (float)value, () => HeaderColor.g, () => "HeaderColor.g=" + HeaderColor.g.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-B", new MoCaSlider(colorSlideRange, (value) => HeaderColor.b = (float)value, () => HeaderColor.b, () => "HeaderColor.b=" + HeaderColor.b.ToString("f2"), "float"));
            //Panel.AddControl("row0", "col2", "HeaderColor-A", new MoCaSlider(colorSlideRange, (value) => HeaderColor.a = (float)value, () => HeaderColor.a, () => "HeaderColor.a=" + HeaderColor.a.ToString("f2"), "float"));

            Panel.AddControl("row1", "col0", "RunTestGui", new MoCaButton(() => "Run Color Test", () => RunTestGui()));

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
            var PickerPanel = Panel.AddPanel( row, col, name, new MoCaPanel(false));
            PickerPanel.Meta.TxtMargin = 0;
            
            PickerPanel.Meta.SliderDirection = SliderDirection.vertical;
            PickerPanel.Meta.SliderLabelPlacement = ControlLabelPlacement.none;
            PickerPanel.Meta.InputLabelPlacement = ControlLabelPlacement.none;

            VerticalLayoutGroup layoutGroup = PickerPanel.Container.GetComponent<VerticalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;
            LayoutElement layoutElement = PickerPanel.Container.GetComponent<LayoutElement>();
            layoutElement.preferredWidth = 200;
            layoutElement.preferredHeight = 380;
            PickerPanel.AddControl("0", "0", name + "_preview",new MoCaColor(() => color.Color));

            PickerPanel.Meta.SliderSize.z = 60;
            var rSlider = PickerPanel.AddSlider("sliders", "r", name + "-R", new MoCaSlider(colorSlideRange, (value) => color.R = (float)value, () => color.R, () => color.R.ToString("f2"), "float", meta: PickerPanel.Meta));



            var gSlider = PickerPanel.AddSlider("sliders", "g", name + "-G", new MoCaSlider(colorSlideRange, (value) => color.G = (float)value, () => color.G, () => color.G.ToString("f2"), "float"));

            var bSlider = PickerPanel.AddSlider("sliders", "b", name + "-B", new MoCaSlider(colorSlideRange, (value) => color.B = (float)value, () => color.B, () => color.B.ToString("f2"), "float"));
            var aSlider = PickerPanel.AddSlider("sliders", "a", name + "-A", new MoCaSlider(colorSlideRange, (value) => color.A = (float)value, () => color.A, () => color.A.ToString("f2"), "float"));

            
            rSlider.BoundFillColor = () => new Color(rSlider.Value, 0, 0, aSlider.Value);
            rSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            gSlider.BoundFillColor = () => new Color(0, gSlider.Value,  0, aSlider.Value);
            gSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            bSlider.BoundFillColor = () => new Color(0, 0, bSlider.Value, aSlider.Value);
            bSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            aSlider.BoundFillColor = () => new Color(1,1,1, aSlider.Value);
            aSlider.Container.GetComponent<VerticalLayoutGroup>().padding.bottom = 12;
            PickerPanel.Meta.InputFontSize = 11;
            PickerPanel.AddControl("output", "0", name + "-Output", new MoCaInput(color.Color.ToString(), onUpdateAction: () => "new Color(" + color.R.ToString("f2") + "f, " + color.G.ToString("f2") + "f, " + color.B.ToString("f2") + "f," + color.A.ToString("f2") + "f)", text:"A", valType:"none"));
            var outputRowRect = PickerPanel.GetRow("output").Obj.GetComponent<RectTransform>();
            outputRowRect.anchoredPosition = new Vector2(0, 0);
            outputRowRect.anchorMax = Vector2.one;
            outputRowRect.anchorMin = Vector2.zero;

            PickerPanel.Meta.InputFontSize = 9;
            PickerPanel.Meta.InputSize.w = 20;
            PickerPanel.AddControl("sliders", "r", name + "-R_Input", new MoCaInput((value) => color.R = (float)value, () => color.R, "R", "float", meta: PickerPanel.Meta));
            PickerPanel.AddControl("sliders", "g", name + "-G_Input", new MoCaInput((value) => color.G = (float)value, () => color.G, "G", "float"));
            PickerPanel.AddControl("sliders", "b", name + "-B_Input", new MoCaInput((value) => color.B = (float)value, () => color.B, "B", "float"));
            PickerPanel.AddControl("sliders", "a", name + "-A_Input", new MoCaInput((value) => color.A = (float)value, () => color.A, "A", "float"));


            var sliderRow = PickerPanel.GetRow("sliders").Obj.GetComponent<HorizontalLayoutGroup>();
            sliderRow.padding = new RectOffset(3, 3, 5, 5);
            sliderRow.spacing = 5;
        }
    }
}
