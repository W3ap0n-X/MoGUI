using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoGUI;
using UnityEngine.EventSystems;
using System;

namespace MoGUI.Tests
{
    /* This file is added as a component to a game object in the unity editor to do tests of controls and layouts
 * Vesrion: 0.0.1.0
 * 
 * In this test I am creating two windows, both will have the same content but with different styles.
 */
    public class ControlTest : MonoBehaviour
    {
        // Main Gui object
        public MoGui GUI;
        // Keycode used to show the gui if it is closed
        public KeyCode KeyCode;
        // Metadata for the second window
        MoGuiMeta Win2Meta;
        // flag to check if the Gui has been built.
        bool ui_init = false;


        // Start is called before the first frame update
        void Start()
        {
            // Set up meta for secondary window using different fonts and base colors
            //Win2Meta = new MoGuiMeta("ControlTest", "Window 2",
            //    font: Font.CreateDynamicFontFromOSFont("Times New Roman", 20),
            //    panelColor: new Color(1, 1, 1, 0.7f),
            //    headerColor: new Color(0.3f, 0.3f, 0.8f, 1),
            //    headerFontColor: new Color(0.8f, 0.8f, 0.8f, 1),
            //    fontColor: Color.black,
            //    buttonColor: new Color(0.5f, 0.5f, 0.8f, 1),
            //    toggleColor: new Color(0.9f, 0.9f, 0.9f),
            //    toggleCheckColor: new Color(0.4f, 0.4f, 0.4f),
            //    inputColor: new Color(0.9f, 0.9f, 0.9f)
            //);
            // Create the base Gui
            GUI = new MoGui("ControlTest", new Vector2(600, 400), Vector2.zero);
            GUI.Canvas.transform.SetParent(gameObject.transform, false);
            // Create the secondary window
            //GUI.AddPanel(Win2Meta, "Window 2", new Vector2(450, 300), Vector2.zero);
        }

        // Update is called once per frame
        void Update()
        {
            // Check if the Gui has been built yet, if not, build it
            if (!ui_init)
            {
                // Setup content on RootPanel
                BuildUI(GUI.Main);
                // Setup content on secondary window
                //BuildUI(GUI.Panels["Window 2"]);
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
                // since both panels contain an instance of this control, we weill grab the one from the root panel and use this to ensure that a control dependent variable is tied specifically to the correct control
                if (((MoGuiPanel)GUI.Main.Components["TestPanel0"]).Components.ContainsKey("TestToggle5"))
                {
                    bool5Toggle = (MoGuiToggle)((MoGuiPanel)GUI.Main.Components["TestPanel0"]).Components["TestToggle5"];
                }
            }

        }

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
        public void BuildUI(MoGuiPanel rootPanel)
        {
            // Create Panel for Toggles
            var TestPanel0 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel0", new MoCaPanel(true, "Toggles"));
            BuildToggles(TestPanel0);
            // Create Panel for Sliders
            var TestPanel2 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel2", new MoCaPanel(true, "Sliders"));
            BuildSliders(TestPanel2);
            // Create Panel for Inputs
            var TestPanel3 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel3", new MoCaPanel(true, "Inputs"));
            BuildInputs(TestPanel3);
            // Create Panel for DropDownLists
            var TestPanel4 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel4", new MoCaPanel(true, "DropDown Lists"));
            BuildDDLs(TestPanel4);
            // Create Panel for Selectors
            var TestPanel5 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel5", new MoCaPanel(true, "Select Groups"));
            BuildSelectGroups(TestPanel5);

            // Create a non collapsing panel with several text controls to see how the layout reacts
            var TestPanel1 = (MoGuiPanel)rootPanel.AddControl("row0", "col1", "TestPanel1", new MoCaPanel(false));
            TestPanel1.AddScrollArea();
            var oneCol0 = TestPanel1.GetCol("row0", "col0");
            oneCol0.preferredWidth = 100;
            TestPanel1.AddControl("row0", "col0", "TestText", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col0", "TestText0", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col0", "TestText1", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col0", "TestText2", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col0", "TestText3", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col0", "TestText4", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col1", "1TestText", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col1", "1TestText0", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col1", "1TestText1", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col1", "1TestText2", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col1", "1TestText3", new MoCaText("testing"));
            TestPanel1.AddControl("row0", "col1", "1TestText4", new MoCaText("testing"));

            var TestPanel6 = (MoGuiPanel)rootPanel.AddControl("row0", "col1", "TestPanel6", new MoCaPanel(false));

            TestPanel6.AddControl("row0", "col0", "6TestText", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col0", "6TestText0", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col0", "6TestText1", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col0", "6TestText2", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col0", "6TestText3", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col0", "6TestText4", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col1", "61TestText", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col1", "61TestText0", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col1", "61TestText1", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col1", "61TestText2", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col1", "61TestText3", new MoCaText("testing"));
            TestPanel6.AddControl("row0", "col1", "61TestText4", new MoCaText("testing"));

            // Create Text for button testing, this is intended to prove that the button is working and that it's text is updating successfully
            rootPanel.AddControl("row0", "col1", "TestText0", new MoCaText(() => "bool0=" + bool0));
            // Create the button with a bound boolean that will toggle the state when clicked.
            rootPanel.AddControl("row0", "col1", "TestButton0", new MoCaButton(() => "bool0=" + bool0, () => bool0 = bool0 ? false : true));

            // Once all panels are built set flag to true
            ui_init = true;
        }

        // Used for button test, will toggle state on button press
        bool bool0;

        // Used for toggle tests
        bool bool1;
        bool bool2;
        bool bool3;
        bool bool4;

        // Simplifier for testing binding a variable to a control
        MoGuiToggle bool5Toggle;
        // Binding a variable to a control rather than binding a control to a variable??
        bool bool5
        {

            get
            {
                if (bool5Toggle != null) { return bool5Toggle.Value; }
                else
                {
                    return false;
                }
            }
        }

        /* BuildToggles
         * Testing the capability and functionality of Toggles and how they can be used.
         * This takes a pannel and adds one row and two columns
         *  - col1 contains several toggles to test their functionality
         *  - col2 contains text controls used to monitor the bound values of the toggles as well as test the independent toggle values
         */
        void BuildToggles(MoGuiPanel Panel)
        {
            // Add toggle with bound value
            var TestToggle1 = (MoGuiToggle)Panel.AddControl("row1", "col1", "TestToggle1", new MoCaToggle(boundValue: () => bool1, onClickAction: (val) => bool1 = val, boundText: () => "bool1=" + bool1));
            // Add togglebutton with bound value
            Panel.AddControl("row1", "col1", "ToggleBt0", new MoCaToggle(boundValue: () => bool4, onClickAction: (val) => bool4 = val, boundText: () => "bool4=" + bool4, toggleType: ToggleType.button));
            // Add toggle with with Bound Value that will not be saved as a variable to test if it can be called from panel with name 
            Panel.AddControl("row1", "col1", "TestToggle2", new MoCaToggle(boundValue: () => bool2, onClickAction: (val) => bool2 = val, boundText: () => "bool2=" + bool2));
            // Add toggle with bound value to be used for read test
            var TestToggle3 = (MoGuiToggle)Panel.AddControl("row1", "col1", "TestToggle3", new MoCaToggle(boundValue: () => bool3, onClickAction: (val) => bool3 = val, boundText: () => "bool3=" + bool3));
            // add toggle with default value and no bound text
            Panel.AddControl("row1", "col1", "TestToggle4", new MoCaToggle(false, text: "TestToggle4"));
            // add toggle using variable as default value and binding it's text to the variable Used to test setting avariable based on a control
            var TestToggle5 = (MoGuiToggle)Panel.AddControl("row1", "col1", "TestToggle5", new MoCaToggle(bool5, boundText: () => "bool5=" + bool5));
            // add toggle that is "read-only" with bound variable
            Panel.AddControl("row1", "col1", "TestToggle6", new MoCaToggle(boundValue: () => bool2, boundText: () => "bool2=" + bool2));
            // add toggle that is "read-only" with bound toggle
            Panel.AddControl("row1", "col1", "TestToggle7", new MoCaToggle(boundValue: () => ((MoGuiToggle)Panel.Components["ToggleBt0"]).Value, boundText: () => "ToggleBt0: " + ((MoGuiToggle)Panel.Components["ToggleBt0"]).Value));

            // Text displays to verify functionality and logical continuity.
            Panel.AddControl("row1", "col2", "TestText1", new MoCaText(() => "bool1=" + bool1));
            Panel.AddControl("row1", "col2", "TestText2", new MoCaText(() => "bool2=" + bool2));
            Panel.AddControl("row1", "col2", "TestText3", new MoCaText(() => "bool3=" + bool3));
            Panel.AddControl("row1", "col2", "TestText4", new MoCaText(() => "bool4=" + bool4));
            Panel.AddControl("row1", "col2", "TestText8", new MoCaText(() => "bool5=" + bool5));
            Panel.AddControl("row1", "col2", "TestText5", new MoCaText(() => "TestToggle1.Value=" + TestToggle1.Value));
            Panel.AddControl("row1", "col2", "TestText6", new MoCaText(() => "TestToggle2.Value=" + ((MoGuiToggle)Panel.Components["TestToggle2"]).Value));
            Panel.AddControl("row1", "col2", "TestText7", new MoCaText(() => "TestToggle3.Value=" + TestToggle3.Value));
            Panel.AddControl("row1", "col2", "TestText9", new MoCaText(() => "TestToggle4.Value=" + ((MoGuiToggle)Panel.Components["TestToggle4"]).Value));
            Panel.AddControl("row1", "col2", "TestText11", new MoCaText(() => "TestToggle5.Value=" + ((MoGuiToggle)Panel.Components["TestToggle5"]).Value));
            Panel.AddControl("row1", "col2", "TestText10", new MoCaText(() => "TestToggle6.Value=" + ((MoGuiToggle)Panel.Components["TestToggle6"]).Value));
            Panel.AddControl("row1", "col2", "TestText12", new MoCaText(() => "TestToggle7.Value=" + ((MoGuiToggle)Panel.Components["TestToggle7"]).Value));
        }

        // Used for slider tests
        Vector2 testRange = new Vector2(0, 100);
        Vector2 testRange0 = new Vector2(-100, 100);
        float float0 = 1;
        float float1;
        float float2;
        float float3;

        int int0;
        int int1 = 0;
        int int2 = 1;
        /* BuildSliders
         * Testing the capability and functionality of Sliders and how they can be used.
         * This takes panel and will add one row with two columns.
         *  - col0 will contain sliders for testing float values
         *  - col1 will contain sliders for testing int values
         */
        void BuildSliders(MoGuiPanel Panel)
        {
            // standard slider tied to a float value 0-100
            Panel.AddControl("row3", "col0", "TestSlider0", new MoCaSlider(testRange, (value) => float0 = (float)value, () => float0, () => "float0=" + float0, "float"));
            // standard slider tied to a float value -100-100
            Panel.AddControl("row3", "col0", "TestSlider1", new MoCaSlider(testRange0, (value) => float1 = (float)value, () => float1, () => "float1=" + float1, "float"));

            // Create Slider With bound min and max using the values froom previous sliders
            Panel.AddControl("row3", "col0", "TestSlider2",
                new MoCaSlider(
                    testRange,
                    (value) => float2 = (float)value,
                    () => float2,
                    () => "float2=" + float2,
                    "float",
                    () => float1,
                    () => float0
                ));

            // Use Slider as bound min and max Parameter of Slider should have the same effect as above
            Panel.AddControl("row3", "col0", "TestSlider3",
                new MoCaSlider(
                    testRange,
                    (value) => float3 = (float)value,
                    () => float3,
                    () => "float3=" + float3,
                    "float",
                    () => ((MoGuiSlider)Panel.Components["TestSlider1"]).Value,
                    () => ((MoGuiSlider)Panel.Components["TestSlider0"]).Value
                ));

            // Create Sliders Within Slider as bound min and max Parameter of Slider, also try ints, should look the same as above but with no decimals
            Panel.AddControl("row3", "col1", "TestSlider4",
                new MoCaSlider(
                    new Vector2(
                        ((MoGuiSlider)Panel.AddControl("row3", "col1", "TestSlider5", new MoCaSlider(testRange0, (value) => int1 = (int)value, () => int1, () => "int1=" + int1, "int"))).Value,
                        ((MoGuiSlider)Panel.AddControl("row3", "col1", "TestSlider6", new MoCaSlider(testRange0, (value) => int2 = (int)value, () => int2, () => "int2=" + int2, "int"))).Value
                    ),
                    (value) => int0 = (int)value,
                    () => int0,
                    () => "int0=" + int0,
                    "int",
                    () => ((MoGuiSlider)Panel.Components["TestSlider5"]).Value,
                    () => ((MoGuiSlider)Panel.Components["TestSlider6"]).Value
                ));

            // create vertical slider
            Panel.AddControl("row4", "col0", "TestSlider7", new MoCaSlider(testRange, (value) => float0 = (float)value, () => float0, () => "float0=" + float0, "float", direction: ControlOrientation.vertical));
            Panel.AddControl("row4", "col1", "TestSlider8", new MoCaSlider(testRange0, (value) => float1 = (float)value, () => float1, () => "float1=" + float1, "float", direction: ControlOrientation.vertical));
            Panel.AddControl("row4", "col3", "TestSlider9", new MoCaSlider(testRange, (value) => float0 = (float)value, () => float0, () => "float0=" + float0, "float", direction: ControlOrientation.vertical));
            Panel.AddControl("row4", "col4", "TestSlider10", new MoCaSlider(testRange0, (value) => float1 = (float)value, () => float1, () => "float1=" + float1, "float", direction: ControlOrientation.vertical));
        }

        // used for input tests
        int input0;
        float input1;
        string input2 = "text";
        /* BuildInputs
         * Testing the capability and functionality of Inputs and how they can be used.
         * This takes panel and will add three rows
         *  - row 0 contains two columns
         *      - col0 contains unbound inputs
         *      - col1 contains text controls to display the value entered into the above inputs
         *  - row 1 is a text component that should later be used to test out headings
         *  - row 2 is the same as row 0 but will use bound variables
         */
        void BuildInputs(MoGuiPanel Panel)
        {
            // Create three basic inputs with no bound variables to test placeholder text and types/type validation
            var TestInput3 = (MoGuiInput)Panel.AddControl("row0", "col0", "TestInput3", new MoCaInput(null, "none", "string Input"));
            var TestInput4 = (MoGuiInput)Panel.AddControl("row0", "col0", "TestInput4", new MoCaInput(null, "float", "float Input"));
            var TestInput5 = (MoGuiInput)Panel.AddControl("row0", "col0", "TestInput5", new MoCaInput(null, "int", "int Input"));
            // These will display the values of the above inputs
            Panel.AddControl("row0", "col1", "TestInputText3", new MoCaText(() => "TestInput3: " + TestInput3.Value));
            Panel.AddControl("row0", "col1", "TestInputText4", new MoCaText(() => "TestInput4: " + TestInput4.Value));
            Panel.AddControl("row0", "col1", "TestInputText5", new MoCaText(() => "TestInput5: " + TestInput5.Value));

            Panel.AddControl("row1", "col0", "TestInputText1", new MoCaText("Bound variable Inputs"));
            // test input with bound int variable
            var TestInput0 = (MoGuiInput)Panel.AddControl("row2", "col0", "TestInput0", new MoCaInput((val) => input0 = (int)val, () => input0, () => "Input: int " + input0, "int"));
            // test input with bound float variable
            Panel.AddControl("row2", "col0", "TestInput1", new MoCaInput((val) => input1 = (float)val, () => input1, () => "Input: float " + input1, "float"));
            // test input with bound string variable
            Panel.AddControl("row2", "col0", "TestInput2", new MoCaInput((val) => input2 = (string)val, () => input2, () => "Input: string \"" + input2 + "\""));

            // These will display the values of the above inputs and test calling from panel
            Panel.AddControl("row2", "col1", "TestInput0Text0", new MoCaText(() => "Value of TestInput0 is " + TestInput0.Value));
            Panel.AddControl("row2", "col1", "TestInput1Text0", new MoCaText(() => "Value of TestInput1 is " + ((MoGuiInput)Panel.Components["TestInput1"]).Value));
            Panel.AddControl("row2", "col1", "TestInput2Text0", new MoCaText(() => "Value of TestInput2 is " + ((MoGuiInput)Panel.Components["TestInput2"]).Value));
        }

        // Used for DDL Tests
        string Option0 = "Option 0";
        string Option1 = "Option 0";
        string Option2 = "Option 0";
        string Option3 = "Option 0";
        List<string> list0 = new List<string>() { "Option 1", "Option 2", "Option 3" };
        List<string> list2 = new List<string>() { "Option 1", "Option 2", "Option 3" };

        Dictionary<string, int> list1 = new Dictionary<string, int>() { { "Option 1", 11 }, { "Option 2", 12 }, { "Option 3", 13 } };
        Dictionary<string, int> list3 = new Dictionary<string, int>() { { "Option 1", 21 }, { "Option 2", 22 }, { "Option 3", 23 } };

        /* BuildDDLs
         * Testing the capability and functionality of DDLs and how they can be used.
         * This takes panel and will add several pairs of rows each will test a different ddl and have the following:
         *  - the first row will contain text controls to test logic and bound variables
         *  - second row will contain the dropdown being tested
         */
        void BuildDDLs(MoGuiPanel Panel)
        {
            // defined first so that text displays above ddl
            Panel.AddControl("row1", "col0", "TestDropdown0Text0", new MoCaText(() => "Option0 is \"" + Option0 + "\""));
            // Test standard ddl with standard list and bound output
            var DDL0 = (MoGuiDDL)Panel.AddControl("row2", "col0", "DDL0", new MoCaDDL(list0, (val) => Option0 = val.ToString(), valType: "string"));
            // get string value of selected option
            Panel.AddControl("row1", "col1", "TestDropdown0Text", new MoCaText(() => DDL0.Selected.Key + " is selected"));
            // get numeric value of selected option
            Panel.AddControl("row1", "col2", "TestDropdown0Int", new MoCaText(() => DDL0.Selected.Value + " is selected"));

            // defined first so that text displays above ddl
            Panel.AddControl("row3", "col0", "TestDropdown1Text0", new MoCaText(() => "Option1 is \"" + Option1 + "\""));
            // Test standard ddl with dictionary and bound output
            var DDL1 = (MoGuiDDL)Panel.AddControl("row4", "col0", "DDL1", new MoCaDDL(list1, (val) => Option1 = val.ToString(), "string"));
            // get string value of selected option
            Panel.AddControl("row3", "col1", "TestDropdown1Text", new MoCaText(() => DDL1.Selected.Key + " is selected"));
            // get numeric value of selected option
            Panel.AddControl("row3", "col2", "TestDropdown1Int", new MoCaText(() => DDL1.Selected.Value + " is selected"));

            // defined first so that text displays above ddl
            Panel.AddControl("row5", "col0", "TestDropdown2Text0", new MoCaText(() => "Option2 is \"" + Option2 + "\""));
            // Test standard ddl with standard list
            var DDL2 = (MoGuiDDL)Panel.AddControl("row6", "col0", "DDL2", new MoCaDDL(list2));
            // get string value of selected option
            Panel.AddControl("row5", "col1", "TestDropdown2Text", new MoCaText(() => DDL2.Selected.Key + " is selected"));
            // get numeric value of selected option
            Panel.AddControl("row5", "col2", "TestDropdown2Int", new MoCaText(() => DDL2.Selected.Value + " is selected"));

            // defined first so that text displays above ddl
            Panel.AddControl("row7", "col0", "TestDropdown3Text0", new MoCaText(() => "Option3 is \"" + Option3 + "\""));
            // Test standard ddl with dictionary
            var DDL3 = (MoGuiDDL)Panel.AddControl("row8", "col0", "DDL3", new MoCaDDL(list3));
            // get string value of selected option
            Panel.AddControl("row7", "col1", "TestDropdown3Text", new MoCaText(() => DDL3.Selected.Key + " is selected"));
            // get numeric value of selected option
            Panel.AddControl("row7", "col2", "TestDropdown3Int", new MoCaText(() => DDL3.Selected.Value + " is selected"));
        }
        // used for testing Selectors
        public Dictionary<string, object> testOptions = new Dictionary<string, object>() {
        { "1", false },
        { "2", false },
        { "3", false },
        { "4", false },
        { "5", false }
    };
        public Dictionary<string, object> testOptions0 = new Dictionary<string, object>() {
        { "1", 1 },
        { "2", 2 },
        { "3", 3 },
        { "4", 4 },
        { "5", 5 }
    };
        /* BuildSelectGroups
         * Testing the capability and functionality of Selectors and how they can be used.
         * TODO
         */
        void BuildSelectGroups(MoGuiPanel Panel)
        {
            Panel.AddControl("row0", "col0", "TestSelectGroupsText0", new MoCaText("Selectors"));
            var TestSelectGroup0 = (MoGuiSelector)Panel.AddControl("row1", "col0", "TestSelectGroup0", new MoCaSelector(testOptions0, "TestSelectGroup0"));
            var TestSelectGroup1 = (MoGuiSelector)Panel.AddControl("row2", "col0", "TestSelectGroup1", new MoCaSelector(testOptions0, "TestSelectGroup1", direction:ControlOrientation.horizontal));
            Panel.AddControl("row1", "col1", "TestSelectGroup0Text", new MoCaText(() => "TestSelectGroup0.Value: " + TestSelectGroup0.Value));
            Panel.AddControl("row1", "col1", "TestSelectGroup1Text", new MoCaText(() => "TestSelectGroup1.Value: " + TestSelectGroup1.Value));
        }
    }
}


