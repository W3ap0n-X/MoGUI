using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Linq;

namespace MoGUI
{
    public class MoGui 
    {
        public GameObject Canvas;
        public MoGuiPanel Main;
        public string PluginName;
        public MoGuiMeta Meta;

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

        public T Copy<T>()
        {
            Debug.Log("Name: " + ClassName + " copied");
            return (T)this.MemberwiseClone();
        }
    }

}
