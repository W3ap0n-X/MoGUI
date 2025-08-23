using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGuiToggle : MoGuiControl
    {
        Action<bool> OnClickAction;
        public MoGuiTxt Text;
        Func<bool> boundValue;
        bool _value;
        public bool Value
        {
            get 
            { 
                if(boundValue != null)
                {
                    return boundValue();
                } else
                {
                    return _value;
                }
            }
            set 
            {

                 _value = value; 

            }
        }

        public MoGuiToggle(MoGuiMeta meta, string name, Func<bool> value, Func<object> text, Action<bool> onClickAction) : base(meta, name)
        {
            boundValue = value;

            switch (Meta.ToggleLabelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("ToggleTxt", text);
                    Obj = CreateToggle(onClickAction);
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateToggle(onClickAction);
                    AddText("ToggleTxt", text);
                    break;
                default:
                    Obj = CreateToggle(onClickAction);
                    break;
            }


        }

        public MoGuiToggle(MoGuiMeta meta, string name, bool value, Func<object> text, Action<bool> onClickAction) : base(meta, name)
        {
            _value = value;

            switch (Meta.ToggleLabelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("ToggleTxt", text);
                    Obj = CreateToggle(onClickAction);
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateToggle(onClickAction);
                    AddText("ToggleTxt", text);
                    break;
                default:
                    Obj = CreateToggle(onClickAction);
                    break;
            }


        }


        public override void _Init()
        {
            Container = CreateContainer(Meta.ToggleOrientation);
        }

        public GameObject CreateToggle(Action<bool> onClickAction)
        {

            GameObject toggleObject = new GameObject(PluginName + "_" + Name + "_" + "Toggle");
            toggleObject.transform.SetParent(Container.transform, false);

            LayoutElement layoutElement = toggleObject.AddComponent<LayoutElement>();
            layoutElement.minWidth = Meta.ToggleSize.x;
            layoutElement.minHeight = Meta.ToggleSize.y;
            layoutElement.preferredWidth = Meta.ToggleSize.z;
            layoutElement.preferredHeight = Meta.ToggleSize.w;
            layoutElement.flexibleWidth = 0;

            Toggle toggleComponent = toggleObject.AddComponent<Toggle>();

            GameObject backgroundObject = new GameObject(PluginName + "_" + Name + "_" + "ToggleBackground");
            backgroundObject.transform.SetParent(toggleObject.transform, false);
            RectTransform backgroundRect = backgroundObject.AddComponent<RectTransform>();
            backgroundRect.anchorMin = new Vector2(0.4f, 0.4f);
            backgroundRect.anchorMax = new Vector2(0.6f, 0.6f);
            backgroundRect.offsetMin = new Vector2(-5, -5);
            backgroundRect.offsetMax = new Vector2(5, 5);

            //backgroundRect.sizeDelta = new Vector2(20, 20);
            Image backgroundImage = backgroundObject.AddComponent<Image>();
            backgroundImage.color = Meta.ToggleColor;

            GameObject checkmarkObject = new GameObject(PluginName + "_" + Name + "_" + "ToggleCheckmark");
            checkmarkObject.transform.SetParent(backgroundObject.transform, false);
            RectTransform checkmarkRect = checkmarkObject.AddComponent<RectTransform>();
            checkmarkRect.anchorMin = new Vector2(0, 0);
            checkmarkRect.anchorMax = new Vector2(1, 1);
            checkmarkRect.offsetMin = new Vector2(2, 2);
            checkmarkRect.offsetMax = new Vector2(-2, -2);

            Image checkmarkImage = checkmarkObject.AddComponent<Image>();
            checkmarkImage.color = Meta.ToggleCheckColor;

            toggleComponent.graphic = checkmarkImage;
            toggleComponent.isOn = Value;
            OnClickAction = onClickAction;

            toggleComponent.onValueChanged.AddListener(_OnClickAction);

            return toggleObject;
        }



        public void _OnClickAction(bool state)
        {
            OnClickAction(state);
        }

        public void AddText(string label, object text)
        {
            if (Text != null)
            {
                Text.Update(text);
                Text.Container.transform.SetParent(Container.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, text);
                Text.Container.transform.SetParent(Container.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }
        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (Text != null)
            {
                Text.Update(onUpdateAction);
                Text.Container.transform.SetParent(Container.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Text.Container.transform.SetParent(Container.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }
        public override void Update()
        {
            Text.Update();
            if (boundValue != null) {
                Obj.GetComponent<Toggle>().isOn = Value;
            } else
            {
                Value = Obj.GetComponent<Toggle>().isOn;
            }
            
        }
    }

    public class MoCaToggle : MoGCArgs
    {
        
        public new Action<bool> OnClickAction;


        public Func<bool> boundValue;
        public bool _value;


        public MoCaToggle(Func<bool> value,
             Action<bool> onClickAction,
             Func<object> text = null,
            MoGuiMeta meta = null
        ) : this(onClickAction, text, meta)
        {
            boundValue = value;
        }

        public MoCaToggle(bool value,
             Action<bool> onClickAction,
             Func<object> text = null,
            MoGuiMeta meta = null
        ) : this(onClickAction, text, meta)
        {
            _value = value;
        }

        public MoCaToggle(Func<bool> value,
             Action<bool> onClickAction,
             string text = null,
            MoGuiMeta meta = null
        ) : this(onClickAction, text, meta)
        {
            boundValue = value;
        }

        public MoCaToggle(bool value,
             Action<bool> onClickAction,
             string text = null,
            MoGuiMeta meta = null
        ) : this( onClickAction, text, meta)
        {
            _value = value;
        }

        MoCaToggle(
             Action<bool> onClickAction,
             Func<object> text,
             
            MoGuiMeta meta
        ) : this(text, meta)
        {
            OnClickAction = onClickAction;
        }

        MoCaToggle(
             Action<bool> onClickAction,
             string text,

            MoGuiMeta meta
        ) : this(text, meta)
        {
            OnClickAction = onClickAction;
        }
        MoCaToggle(
             Func<object> text,

            MoGuiMeta meta
        ) : base(typeof(MoGuiToggle), meta, text: text)
        {
        }

        MoCaToggle(
             string text,

            MoGuiMeta meta
        ) : base(typeof(MoGuiToggle), meta, text:text)
        {
        }
    }

}
