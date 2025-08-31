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
    public class LayoutTest : MonoBehaviour
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

            // Create the base Gui
            GUI = new MoGui("LayoutTest", new Vector2(600, 400), Vector2.zero);
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

        public void BuildUI(MoGuiPanel Panel)
        {
            var row0 = Panel.AddRow("row0");
            var row1 = Panel.AddRow("row1");
            Panel.AddRow("row2");
            var row3 = Panel.AddRow("row3");
            var row4 = Panel.AddRow("row4");
            var row5 = Panel.AddRow("row5");

            Panel.AddCol("row2", "0");


            var col0 = row1.AddColumn("0");
            row1.AddColumn("1");

            for (int i = 0; i < 5; i++)
            {
                row0.AddColumn(i.ToString());
            }

            for (int i = 0; i < 10; i++)
            {
                row4.AddColumn(i.ToString());
            }
            
            for (int i = 0; i < 15; i++)
            {
                 row5.AddColumn(i.ToString());
            }

            col0.preferredWidth = 100;
            col0.preferredHeight = 100;
            row5.Columns["2"].preferredWidth = 300;
            row5.Columns["3"].preferredHeight = 100;
            row5.Columns["4"].preferredWidth = 300;

            row3.preferredHeight = 500;
            //// Create Panel for Toggles
            //var TestPanel0 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel0", new MoCaPanel(true, "Toggles"));
            //BuildToggles(TestPanel0);
            //// Create Panel for Sliders
            //var TestPanel2 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel2", new MoCaPanel(true, "Sliders"));
            //BuildSliders(TestPanel2);
            //// Create Panel for Inputs
            //var TestPanel3 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel3", new MoCaPanel(true, "Inputs"));
            //BuildInputs(TestPanel3);
            //// Create Panel for DropDownLists
            //var TestPanel4 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel4", new MoCaPanel(true, "DropDown Lists"));
            //BuildDDLs(TestPanel4);
            //// Create Panel for Selectors
            //var TestPanel5 = (MoGuiPanel)rootPanel.AddControl("row0", "col0", "TestPanel5", new MoCaPanel(true, "Select Groups"));
            //BuildSelectGroups(TestPanel5);

            //// Create a non collapsing panel with several text controls to see how the layout reacts
            //var TestPanel1 = (MoGuiPanel)rootPanel.AddControl("row0", "col1", "TestPanel1", new MoCaPanel(false));
            //TestPanel1.AddScrollArea();
            //TestPanel1.AddControl("row0", "col0", "TestText", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col0", "TestText0", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col0", "TestText1", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col0", "TestText2", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col0", "TestText3", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col0", "TestText4", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col1", "1TestText", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col1", "1TestText0", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col1", "1TestText1", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col1", "1TestText2", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col1", "1TestText3", new MoCaText("testing"));
            //TestPanel1.AddControl("row0", "col1", "1TestText4", new MoCaText("testing"));

            //// Create Text for button testing, this is intended to prove that the button is working and that it's text is updating successfully
            //rootPanel.AddControl("row0", "col1", "TestText0", new MoCaText(() => "bool0=" + bool0));
            //// Create the button with a bound boolean that will toggle the state when clicked.
            //rootPanel.AddControl("row0", "col1", "TestButton0", new MoCaButton(() => "bool0=" + bool0, () => bool0 = bool0 ? false : true));

            //// Once all panels are built set flag to true
            ui_init = true;
        }

    }
}


