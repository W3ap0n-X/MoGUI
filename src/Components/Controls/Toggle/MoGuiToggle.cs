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
        //ToggleType ToggleType = ToggleType.checkbox;

        

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
            Init(text, onClickAction);

        }

        public MoGuiToggle(MoGuiMeta meta, string name, bool value, Func<object> text, Action<bool> onClickAction) : base(meta, name)
        {
            _value = value;

            Init(text, onClickAction);


        }

        protected MoGuiToggle(MoGuiMeta meta, string name) : base(meta, name)
        {
           


        }

        

        public MoGuiToggle(MoGuiMeta meta, string name, MoCaToggle args) : base(meta, name)
        {
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

        public override void SetLayout()
        {
            minWidth = Meta.ToggleSize.x;
            minHeight = Meta.ToggleSize.y;
            preferredWidth = Meta.ToggleSize.z;
            preferredHeight = Meta.ToggleSize.w;
            flexibleWidth = 0;
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
            backgroundRect.anchorMin = new Vector2(0.4f, 0.4f);
            backgroundRect.anchorMax = new Vector2(0.6f, 0.6f);
            backgroundRect.offsetMin = new Vector2(-5, -5);
            backgroundRect.offsetMax = new Vector2(5, 5);

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
            if(OnClickAction != null)
            {
                OnClickAction(state);
            }
            
        }

        public void AddText(string label, object text)
        {
            if (Label != null)
            {
                Label.Update(text);
                Label.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                Label = new MoGuiTxt(Meta, Name + "_" + label, text);
                Label.Obj.transform.SetParent(Container.transform, false);
                Label.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }
        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (Label != null)
            {
                Label.Update(onUpdateAction);
                Label.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                Label = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Label.Obj.transform.SetParent(Container.transform, false);
                Label.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
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

    public class MoGuiToggleBt : MoGuiToggle
    {

        public MoGuiToggleBt(MoGuiMeta meta, string name, Func<bool> value, Func<object> text, Action<bool> onClickAction) : base(meta, name)
        {
            boundValue = value;
            Init(text, onClickAction);

        }
        public MoGuiToggleBt(MoGuiMeta meta, string name, MoCaToggleBT args) : base(meta, name)
        {
            if(args.boundValue != null)
            {
                boundValue = args.boundValue;
            } else
            {
                _value = args._value;
            }
            
            Init(args.Text, args.OnClickAction);

        }



        void Init(Func<object> text, Action<bool> onClickAction)
        {
            Obj = CreateToggle(onClickAction);
            AddText("ToggleTxt", text);
        }

        public override void SetLayout()
        {
            minWidth = Meta.ButtonSize.x;
            minHeight = Meta.ButtonSize.y;
            //preferredWidth = Meta.ButtonSize.z;
            //preferredHeight = Meta.ButtonSize.w;
            flexibleWidth = 1;
        }

        public new GameObject CreateToggle(Action<bool> onClickAction)
        {

            GameObject toggleObject = new GameObject(PluginName + "_" + Name + "_" + "ToggleBT");
            toggleObject.transform.SetParent(Container.transform, false);

            AddLayoutElement(toggleObject);
            SetLayout();

            Toggle toggleComponent = toggleObject.AddComponent<Toggle>();

            

            GameObject backgroundObject = new GameObject(PluginName + "_" + Name + "_" + "ToggleBackground");
            backgroundObject.transform.SetParent(toggleObject.transform, false);
            RectTransform backgroundRect = backgroundObject.AddComponent<RectTransform>();
            backgroundRect.anchorMin = new Vector2(0, 0);
            backgroundRect.anchorMax = new Vector2(1, 1);
            backgroundRect.offsetMin = new Vector2(0, 0);
            backgroundRect.offsetMax = new Vector2(0, 0);

            

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

            //GameObject textObject = new GameObject(PluginName + "_" + Name + "_" + "TEST");
            //textObject.transform.SetParent(toggleObject.transform, false);
            //RectTransform textBackgroundRect = textObject.AddComponent<RectTransform>();
            //textBackgroundRect.anchorMin = new Vector2(0, 0);
            //textBackgroundRect.anchorMax = new Vector2(1, 1);
            //textBackgroundRect.offsetMin = new Vector2(0, 0);
            //textBackgroundRect.offsetMax = new Vector2(0, 0);
            //Text textComponent = textObject.AddComponent<Text>();
            //textComponent.font = Meta.Font;
            //textComponent.fontSize = Meta.FontSize;
            //textComponent.color = Meta.FontColor;
            

            toggleComponent.onValueChanged.AddListener(_OnClickAction);

            return toggleObject;
        }

        public new void AddText(string label, Func<object> onUpdateAction)
        {
            if (Label != null)
            {
                Label.Update(onUpdateAction);
                Label.Obj.transform.SetParent(Obj.transform, false);
            }
            else
            {
                Label = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Label.Obj.transform.SetParent(Obj.transform, false);
                //var labelLayout = Label.Container.GetComponent<HorizontalOrVerticalLayoutGroup>();
                //labelLayout.childAlignment = TextAnchor.MiddleCenter;
                RectTransform labelRect = Label.Obj.GetComponent<RectTransform>();
                labelRect.anchoredPosition = new Vector2(0, 0);
                labelRect.anchorMin = new Vector2(0, 0);
                labelRect.anchorMax = new Vector2(1, 1);
                labelRect.offsetMin = new Vector2(0, 0);
                labelRect.offsetMax = new Vector2(0, 0);
                Label.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            }

        }
    }

    public class MoCaToggleBT : MoGCArgs
    {
        public new Action<bool> OnClickAction;


        public Func<bool> boundValue;
        public bool _value;

        public MoCaToggleBT(
        bool? value = null,
        Func<bool> boundValue = null,
        Action<bool> onClickAction = null,
        string text = null,
        Func<object> boundText = null,
        MoGuiMeta meta = null
    ) : base(typeof(MoGuiToggleBt), meta)
        {
            if (boundValue != null)
            {
                this.boundValue = boundValue;
            }
            else if (value.HasValue)
            {
                _value = value.Value;
            }

            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }

            if (onClickAction != null)
            {
                OnClickAction = onClickAction;
            }
        }
    }

    public class MoCaToggle : MoGCArgs
    {
        
        public new Action<bool> OnClickAction;


        public Func<bool> boundValue;
        public bool _value;

        public MoCaToggle(
        bool? value = null,
        Func<bool> boundValue = null,
        Action<bool> onClickAction = null,
        string text = null,
        Func<object> boundText = null,
        MoGuiMeta meta = null
    ) : base(typeof(MoGuiToggle), meta)
        {
            if (boundValue != null)
            {
                this.boundValue = boundValue;
            }
            else if (value.HasValue)
            {
                _value = value.Value;
            }

            if (boundText != null)
            {
                Text = boundText;
            }
            else if (text != null)
            {
                Text = () => text;
            }

            if (onClickAction != null)
            {
                OnClickAction = onClickAction;
            }
        }
    }

}
