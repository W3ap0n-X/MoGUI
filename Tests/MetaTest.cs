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
    public class MetaTest : MonoBehaviour
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
            GUI = new MoGui("MetaTest", new Vector2(600, 400), Vector2.zero);
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
            row0.SetPreferredWidthPercentage(100);
            var col0 = Panel.AddCol(row0,"col0");
            col0.SetPreferredWidthPercentage(100);
            Panel.AddControl("row0", "col0", "TestText-title", new MoCaText("title", TextElement.title));
            Panel.AddControl("row0", "col0", "TestText-h1", new MoCaText("h1", TextElement.h1));
            Panel.AddControl("row0", "col0", "TestText-h2", new MoCaText("h2", TextElement.h2));
            Panel.AddControl("row0", "col0", "TestText-h3", new MoCaText("h3", TextElement.h3));
            Panel.AddControl("row0", "col0", "TestText-h4", new MoCaText("h4", TextElement.h4));
            Panel.AddControl("row0", "col0", "TestText-h5", new MoCaText("h5", TextElement.h5));
            Panel.AddControl("row0", "col0", "TestText-h6", new MoCaText("h6", TextElement.h6));
            Panel.AddControl("row0", "col0", "TestText-label", new MoCaText("label", TextElement.label));
            Panel.AddControl("row0", "col0", "TestText-small", new MoCaText("small", TextElement.small));
            Panel.AddControl("row0", "col0", "TestText-text", new MoCaText("text", TextElement.text));
            ui_init = true;
        }

    }
}


