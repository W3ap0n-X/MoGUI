using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace MoGUI
{
    public abstract class MoGuiControl
    {

        public GameObject Obj;
        public GameObject Container;
        public LayoutWrapper LoElement;
        public string PluginName;
        public string Name;
        public bool is_init;
        public Vector2 Size;
        public Vector2 Pos;
        public MoGuiMeta Meta;

        public ControlOrientation Orientation = ControlOrientation.horizontal;
        public ControlLabelPlacement LabelPlacement = ControlLabelPlacement.after;

        public Action<object> OnEditAction;
        public Func<object> OnUpdateAction;

        public MoGuiControl(string pluginName, string name)
        {
            PluginName = pluginName;
            Name = name;
            Meta = new MoGuiMeta(pluginName, name);
            _Init();

        }

        public MoGuiControl(MoGuiMeta meta, string name)
        {
            Name = name;
            Meta = new MoGuiMeta(meta, name);
            PluginName = Meta.PluginName;
            _Init();

        }

        public MoGuiControl(string pluginName, string name, Vector2 size)
        {
            PluginName = pluginName;
            Name = name;
            Size = size;
            Meta = new MoGuiMeta(pluginName, name);
            _Init();
        }

        public MoGuiControl(string pluginName, string name, Vector2 size, Vector2 pos)
        {

            PluginName = pluginName;
            Name = name;
            Pos = pos;
            Size = size;
            Meta = new MoGuiMeta(pluginName, name);
            _Init();
        }

        public MoGuiControl(MoGuiMeta meta, string name, Vector2 size, Vector2 pos)
        {

            Meta = new MoGuiMeta(meta, name);
            PluginName = Meta.PluginName;
            Name = Meta.Name;
            Pos = pos;
            Size = size;
            _Init();
        }

        public abstract void Update();


        public virtual void _Init()
        {
            
        }

        public virtual GameObject CreateContainer(ControlOrientation orientation)
        {
            GameObject layoutObject = new GameObject(PluginName + "_" + Name + "_" + "Container");
            if (orientation == ControlOrientation.vertical)
            {
                VerticalLayoutGroup layoutGroup = layoutObject.AddComponent<VerticalLayoutGroup>();
                layoutGroup.padding = new RectOffset(Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin);

                layoutGroup.spacing = Meta.TxtMargin;
                layoutGroup.childForceExpandWidth = false;
                layoutGroup.childForceExpandHeight = false;
            }
            else
            {
                HorizontalLayoutGroup layoutGroup = layoutObject.AddComponent<HorizontalLayoutGroup>();
                layoutGroup.padding = new RectOffset(Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin);

                layoutGroup.spacing = Meta.TxtMargin;
                layoutGroup.childForceExpandWidth = false;
                layoutGroup.childForceExpandHeight = false;
            }


            return layoutObject;
        }
        public virtual GameObject CreateContainer()
        {
            GameObject layoutObject = this.CreateContainer(Meta.Orientation);
            return layoutObject;
        }

        protected void AddLayoutElement(GameObject obj)
        {
            LoElement = new LayoutWrapper(obj.AddComponent<LayoutElement>());
        }

        public abstract void SetLayout();

        public float minHeight { get => LoElement.minHeight; set { LoElement.minHeight = value; } }
        public float minWidth { get => LoElement.minWidth; set { LoElement.minWidth = value; } }
        public float flexibleWidth { get => LoElement.flexibleWidth; set { LoElement.flexibleWidth = value; } }
        public float flexibleHeight { get => LoElement.flexibleHeight; set { LoElement.flexibleHeight = value; } }
        public float preferredWidth { get => LoElement.preferredWidth; set { LoElement.preferredWidth = value; } }
        public float preferredHeight { get => LoElement.preferredHeight; set { LoElement.preferredHeight = value; } }
    }

}
