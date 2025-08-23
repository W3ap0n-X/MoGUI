using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGui 
    {
        GameObject Canvas;
        public MoGuiPanel RootPanel;
        public string PluginName;
        public string Name;
        public MoGuiMeta Meta;

        public Dictionary<string, MoGuiPanel> Panels = new Dictionary<string, MoGuiPanel>();
        public MoGui(string pluginName, string name, Vector2 size, Vector2 pos)
        {
            Meta = new MoGuiMeta(pluginName, name);
            PluginName = pluginName;
            Name = name;
            Canvas = CreateCanvas();
            RootPanel = new MoGuiPanel(Meta,  Name, Canvas, size, pos);
            RootPanel.Obj.transform.SetParent(Canvas.transform, false);
        }

        public MoGui(MoGuiMeta meta, string name, Vector2 size, Vector2 pos)
        {
            
            Meta = new MoGuiMeta(meta, name);
            PluginName = Meta.PluginName;
            Name = name;
            Canvas = CreateCanvas();
            RootPanel = new MoGuiPanel(Meta, Name, Canvas, size, pos);
            RootPanel.Obj.transform.SetParent(Canvas.transform, false);
        }

        public void ToggleGui(bool show)
        {
            Canvas.SetActive(show);
        }

        public bool IsActive
        {
            get => Canvas.activeSelf;
        }

        public GameObject CreateCanvas()
        {
            var canvasObject = new GameObject( PluginName + "_" + Name + "_" + "Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            return canvasObject;
        }

        
        public MoGuiPanel AddPanel(string label, Vector2 size, Vector2 pos, bool topLevel = false)
        {
            MoGuiPanel newPanel;
            if (Panels.ContainsKey(label))
            {
                newPanel = Panels[label];
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
            }
            else
            {
                newPanel = new MoGuiPanel(Meta, label, size, pos, topLevel);
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
                Panels.Add(label, newPanel);
            }
            return newPanel;
        }

        public MoGuiPanel AddPanel(MoGuiMeta meta, string label, Vector2 size, Vector2 pos, bool topLevel = false)
        {
            MoGuiPanel newPanel;
            if (Panels.ContainsKey(label))
            {
                newPanel = Panels[label];
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
            }
            else
            {
                newPanel = new MoGuiPanel(meta, label, size, pos, topLevel);
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
                Panels.Add(label, newPanel);
            }
            return newPanel;
        }

        public void Update()
        {
            if (IsActive)
            {
                RootPanel.Update();
                foreach (var item in Panels)
                {
                    item.Value.Update();
                }
            }
        }
        
    }

}
