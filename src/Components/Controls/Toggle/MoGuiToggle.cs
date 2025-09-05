using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGuiToggle : MoGuiControl
    {
        protected Action<bool> OnClickAction;
        public MoGuiTxt Label;
        protected Func<bool> boundValue;
        protected bool _value;
        ToggleType ToggleType = ToggleType.checkbox;

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

        public MoGuiToggle(MoGuiMeta meta, string name, Func<bool> value, Func<object> text, Action<bool> onClickAction, ToggleType type = ToggleType.checkbox) : base(meta, name)
        {
            ToggleType = type;
            boundValue = value;
            Init(text, onClickAction);

        }

        public MoGuiToggle(MoGuiMeta meta, string name, bool value, Func<object> text, Action<bool> onClickAction, ToggleType type = ToggleType.checkbox) : base(meta, name)
        {
            ToggleType = type;
            _value = value;

            Init(text, onClickAction);


        }

        

        public MoGuiToggle(MoGuiMeta meta, string name, MoCaToggle args) : base(meta, name)
        {
            ToggleType = args.ToggleType;
            if (args.boundValue != null)
            {
                boundValue = args.boundValue;
            }
            else
            {
                _value = args._value;
            }

            Init(args.Text, args.OnClickAction);


        }

        void Init(Func<object> text, Action<bool> onClickAction)
        {
            if (ToggleType == ToggleType.button)
            {
                Obj = CreateToggle(onClickAction);
                AddText("ToggleTxt", text);
            }
            else
            {
                switch (Meta.Toggle.labelPlacement)
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
            
        }

        public override void _Init()
        {
            Container = CreateContainer(Meta.Toggle.orientation);
        }

        public override void SetLayout()
        {
            if (ToggleType == ToggleType.button) 
            {
                minWidth = Meta.Toggle.buttonSize.minWidth;
                minHeight = Meta.Toggle.buttonSize.minHeight;
                if (Meta.Toggle.buttonSize.preferredWidth != null) { preferredWidth = (float)Meta.Toggle.buttonSize.preferredWidth; }
                if (Meta.Toggle.buttonSize.preferredHeight != null) { preferredHeight = (float)Meta.Toggle.buttonSize.preferredHeight; }
                flexibleWidth = Meta.Toggle.buttonSize.flexibleWidth ?? 0;
                flexibleHeight = Meta.Toggle.buttonSize.flexibleHeight ?? 0;
            }
            else
            {
                minWidth = Meta.Toggle.checkBoxSize.minWidth;
                minHeight = Meta.Toggle.checkBoxSize.minHeight;
                if (Meta.Toggle.checkBoxSize.preferredWidth != null) { preferredWidth = (float)Meta.Toggle.checkBoxSize.preferredWidth; }
                if (Meta.Toggle.checkBoxSize.preferredHeight != null) { preferredHeight = (float)Meta.Toggle.checkBoxSize.preferredHeight; }
                flexibleWidth = Meta.Toggle.checkBoxSize.flexibleWidth ?? 0;
                flexibleHeight = Meta.Toggle.checkBoxSize.flexibleHeight ?? 0;
            }
                
        }
        public GameObject CreateToggle(Action<bool> onClickAction)
        {

            GameObject toggleObject = new GameObject(PluginName + "_" + Name + "_" + "Toggle");
            toggleObject.transform.SetParent(Container.transform, false);

            LayoutElement layoutElement = toggleObject.AddComponent<LayoutElement>();
            

            AddLayoutElement(toggleObject);
            SetLayout();

            Toggle toggleComponent = toggleObject.AddComponent<Toggle>();

            GameObject backgroundObject = new GameObject(PluginName + "_" + Name + "_" + "ToggleBackground");
            backgroundObject.transform.SetParent(toggleObject.transform, false);
            RectTransform backgroundRect = backgroundObject.AddComponent<RectTransform>();

            Image backgroundImage = backgroundObject.AddComponent<Image>();
            backgroundImage.color = Meta.Toggle.background;

            GameObject checkmarkObject = new GameObject(PluginName + "_" + Name + "_" + "ToggleCheckmark");
            checkmarkObject.transform.SetParent(backgroundObject.transform, false);
            RectTransform checkmarkRect = checkmarkObject.AddComponent<RectTransform>();
            if (ToggleType == ToggleType.button)
            {
                backgroundRect.anchorMin = new Vector2(0, 0);
                backgroundRect.anchorMax = new Vector2(1, 1);
                backgroundRect.offsetMin = new Vector2(0, 0);
                backgroundRect.offsetMax = new Vector2(0, 0);

                checkmarkRect.anchorMin = new Vector2(0, 0);
                checkmarkRect.anchorMax = new Vector2(1, 1);
                checkmarkRect.offsetMin = new Vector2(2, 2);
                checkmarkRect.offsetMax = new Vector2(-2, -2);
            }
            else
            {
                backgroundRect.anchorMin = new Vector2(0.4f, 0.4f);
                backgroundRect.anchorMax = new Vector2(0.6f, 0.6f);
                backgroundRect.offsetMin = new Vector2(-5, -5);
                backgroundRect.offsetMax = new Vector2(5, 5);

                checkmarkRect.anchorMin = new Vector2(0, 0);
                checkmarkRect.anchorMax = new Vector2(1, 1);
                checkmarkRect.offsetMin = new Vector2(2, 2);
                checkmarkRect.offsetMax = new Vector2(-2, -2);
            }

            Image checkmarkImage = checkmarkObject.AddComponent<Image>();
            checkmarkImage.color = Meta.Toggle.checkBox;

            toggleComponent.graphic = checkmarkImage;
            toggleComponent.isOn = Value;
            OnClickAction = onClickAction;

            toggleComponent.onValueChanged.AddListener(_OnClickAction);

            return toggleObject;
        }



        public void _OnClickAction(bool state)
        {
            if(OnClickAction != null)
            {
                OnClickAction(state);
            }
            
        }

        public void AddText(string label, object text)
        {
            if (ToggleType == ToggleType.button)
            {
                if (Label != null)
                {
                    Label.Update(text);
                    Label.Obj.transform.SetParent(Obj.transform, false);
                }
                else
                {
                    Label = new MoGuiTxt(Meta, Name + "_" + label, text:text, Meta.Toggle.labelSettings);
                    Label.Obj.transform.SetParent(Obj.transform, false);
                    RectTransform labelRect = Label.Obj.GetComponent<RectTransform>();
                    labelRect.anchoredPosition = new Vector2(0, 0);
                    labelRect.anchorMin = new Vector2(0, 0);
                    labelRect.anchorMax = new Vector2(1, 1);
                    labelRect.offsetMin = new Vector2(0, 0);
                    labelRect.offsetMax = new Vector2(0, 0);
                    Label.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                }
            }
            else
            {
                if (Label != null)
                {
                    Label.Update(text);
                    Label.Obj.transform.SetParent(Container.transform, false);
                }
                else
                {
                    Label = new MoGuiTxt(Meta, Name + "_" + label, text: text, Meta.Toggle.labelSettings);
                    Label.Obj.transform.SetParent(Container.transform, false);
                    Label.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                }
            }
            

        }
        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (ToggleType == ToggleType.button)
            {
                if (Label != null)
                {
                    Label.Update(onUpdateAction);
                    Label.Obj.transform.SetParent(Obj.transform, false);
                }
                else
                {
                    Label = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction, Meta.Toggle.labelSettings);
                    Label.Obj.transform.SetParent(Obj.transform, false);
                    RectTransform labelRect = Label.Obj.GetComponent<RectTransform>();
                    labelRect.anchoredPosition = new Vector2(0, 0);
                    labelRect.anchorMin = new Vector2(0, 0);
                    labelRect.anchorMax = new Vector2(1, 1);
                    labelRect.offsetMin = new Vector2(0, 0);
                    labelRect.offsetMax = new Vector2(0, 0);
                    Label.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                }
            }
            else
            {
                if (Label != null)
                {
                    Label.Update(onUpdateAction);
                    Label.Obj.transform.SetParent(Container.transform, false);
                }
                else
                {
                    Label = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction, Meta.Toggle.labelSettings);
                    Label.Obj.transform.SetParent(Container.transform, false);
                    Label.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                }
            }
            

        }
        public override void Update()
        {
            if (Label != null)
            {
                Label.Update();
            }
            if (boundValue != null) {
                Obj.GetComponent<Toggle>().isOn = Value;
            } else
            {
                Value = Obj.GetComponent<Toggle>().isOn;
            }
            
        }
    }

    

}
