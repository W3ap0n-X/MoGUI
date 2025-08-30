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
                layoutGroup.childForceExpandWidth = true;
                layoutGroup.childForceExpandHeight = false;
            }
            else
            {
                HorizontalLayoutGroup layoutGroup = layoutObject.AddComponent<HorizontalLayoutGroup>();
                layoutGroup.padding = new RectOffset(Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin);

                layoutGroup.spacing = Meta.TxtMargin;
                layoutGroup.childForceExpandWidth = true;
                layoutGroup.childForceExpandHeight = false;
            }


            return layoutObject;
        }
        public virtual GameObject CreateContainer()
        {
            GameObject layoutObject = this.CreateContainer(Meta.Orientation);
            return layoutObject;
        }
    }

    public abstract class MoGCArgs
    {
        public Action OnClickAction;
        public Func<object> OnUpdateAction;
        public Action<object> OnEditAction;
        public Func<object> Text;
        public string ValType;
        public string Orientation;
        public string LabelPlacement;
        public Type Type;
        public MoGuiMeta Meta;


        public MoGCArgs(Type type,
            MoGuiMeta meta = null,
            Func<object> value = null,
             Action onClickAction = null,
             Func<object> onUpdateAction = null,
             Action<object> onEditAction = null,
             Func<object> text = null,
             string valType = "none"
        )
        {
            Type = type;
            Meta = meta;
            OnClickAction = onClickAction;
            OnUpdateAction = onUpdateAction;
            OnEditAction = onEditAction;
            Text = text;
            ValType = valType;
        }

        public MoGCArgs(Type type, object value = null,
             Action onClickAction = null,
             Func<object> onUpdateAction = null,
             Action<object> onEditAction = null,
             object text = null,
             string valType = null,
            MoGuiMeta meta = null
        )
        {
            Type = type;
            Meta = meta;
            OnClickAction = onClickAction;
            OnUpdateAction = onUpdateAction;
            OnEditAction = onEditAction;
            Text = ConvertString(text);
            ValType = valType;
        }

        public Func<object> ConvertString(object obj)
        {
            return () => obj;
        }
    }
}
