using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    class MoGuiSlider : MoGuiControl
    {
        string Type;
        MoGuiTxt Text;
        Slider Slider;
        public float Value => Slider.value;

        public Vector2 Range
        {
            get
            {
                return new Vector2(MinValue, MaxValue);
            }
            set
            {
                _minValue = value.x;
                _maxValue = value.y;
            }
        }



        float _minValue;
        float _maxValue;

        float MinValue
        {
            get
            {
                if (_boundMin != null)
                {
                    return _boundMin();
                } else
                {
                    return _minValue;
                }
            }
            set
            {
                _minValue = value;
            }
        }
        float MaxValue
        {
            get
            {
                if (_boundMax != null)
                {
                    return _boundMax();
                }
                else
                {
                    return _maxValue;
                }
            }
            set
            {
                _maxValue = value;
            }
        }

        Func<float> _boundMin;
        Func<float> _boundMax;

        public MoGuiSlider(MoGuiMeta meta, string name, Func<object> text, Vector2 range, Func<object> onUpdateAction, Action<object> onEditAction, string type, Func<float> boundMin = null, Func<float> boundMax = null) : base(meta, name)
        {
            MinValue = range.x;
            MaxValue = range.y;
            
            
            OnUpdateAction = onUpdateAction;
            Type = type;
            OnEditAction = onEditAction;

            switch (Meta.SliderLabelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("SliderTxt", text);
                    Obj = CreateSlider();
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateSlider();
                    AddText("SliderTxt", text);
                    break;
                default:
                    Obj = CreateSlider();
                    break;
            }

            if (boundMin != null)
            {
                bindMin(boundMin);
            }
            if (boundMax != null)
            {
                bindMax(boundMax);
            }

        }

        public MoGuiSlider(MoGuiMeta meta, string name, string text, Vector2 range, Func<object> onUpdateAction, Action<object> onEditAction, string type, Func<float> boundMin = null, Func<float> boundMax = null) : base(meta, name)
        {
            MinValue = range.x;
            MaxValue = range.y;
            
            OnUpdateAction = onUpdateAction;
            Type = type;
            OnEditAction = onEditAction;
            switch (Meta.SliderLabelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("SliderTxt", text);
                    Obj = CreateSlider();
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateSlider();
                    AddText("SliderTxt", text);
                    break;
                default:
                    Obj = CreateSlider();
                    break;
            }

            if (boundMin != null)
            {
                bindMin(boundMin);
            }
            if (boundMax != null)
            {
                bindMax(boundMax);
            }


        }

        public override void _Init()
        {
            Container = CreateContainer(Meta.SliderOrientation);
        }

        public GameObject CreateSlider()
        {
            GameObject sliderObject = new GameObject(PluginName + "_" + Name + "_" + "Slider");
            sliderObject.transform.SetParent(Container.transform, false);
            RectTransform sliderRect = sliderObject.AddComponent<RectTransform>();
            sliderRect.anchorMin = new Vector2(0, 0);
            sliderRect.anchorMax = new Vector2(1, 1);
            

            LayoutElement layoutElement = sliderObject.AddComponent<LayoutElement>();
            layoutElement.minWidth = Meta.SliderSize.x;
            layoutElement.minHeight = Meta.SliderSize.y;
            layoutElement.preferredWidth = Meta.SliderSize.z;
            layoutElement.preferredHeight = Meta.SliderSize.w;

            Slider = sliderObject.AddComponent<Slider>();
            Slider.minValue = MinValue;
            Slider.maxValue = MaxValue;

            Slider.wholeNumbers = Type == "int" ? true : false;
            
            GameObject backgroundObject = new GameObject(PluginName + "_" + Name + "_" + "SliderBackground");
            backgroundObject.transform.SetParent(sliderObject.transform, false);
            RectTransform backgroundRect = backgroundObject.AddComponent<RectTransform>();

            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.anchorMax = Vector2.one;

            backgroundRect.offsetMin = new Vector2(0, 10);
            backgroundRect.offsetMax = new Vector2(0, -10);

            Image backgroundImage = backgroundObject.AddComponent<Image>();
            backgroundImage.color = Meta.SliderTrackColor;

            GameObject fillAreaObject = new GameObject(PluginName + "_" + Name + "_" + "SliderFillArea");
            fillAreaObject.transform.SetParent(sliderObject.transform, false);
            RectTransform fillAreaRect = fillAreaObject.AddComponent<RectTransform>();
            fillAreaRect.anchorMin = Vector2.zero;
            fillAreaRect.anchorMax = Vector2.one;
            fillAreaRect.offsetMin = new Vector2(0, 0);
            fillAreaRect.offsetMax = new Vector2(0,0);

            GameObject fillObject = new GameObject(PluginName + "_" + Name + "_" + "SliderFill");
            fillObject.transform.SetParent(fillAreaObject.transform, false);
            RectTransform fillRect = fillObject.AddComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.offsetMin = new Vector2(0, 12);
            fillRect.offsetMax = new Vector2(0, -12);
            Image fillImage = fillObject.AddComponent<Image>();
            fillImage.color = Meta.SliderFillColor;
            Slider.fillRect = fillImage.rectTransform;

            GameObject handleObject = new GameObject(PluginName + "_" + Name + "_" + "SliderHandle");
            handleObject.transform.SetParent(sliderObject.transform, false);
            RectTransform handleRect = handleObject.AddComponent<RectTransform>();
            handleRect.anchorMin = new Vector2(0, 0.5f);
            handleRect.anchorMax = new Vector2(1, 0.5f);
            handleRect.offsetMin = new Vector2(-10, 10); 
            handleRect.offsetMax = new Vector2(10, -10); 

            Image handleImage = handleObject.AddComponent<Image>();
            handleImage.color = Meta.SliderHandleColor;
            Slider.handleRect = handleRect;




            if (float.TryParse(OnUpdateAction().ToString(), out float updatedValue))
            {
                Slider.value = updatedValue;
            }



            Slider.onValueChanged.AddListener((newValue) => _OnEditAction(newValue));

            return sliderObject;
        }

        public void _OnEditAction(object value)
        {
            switch (Type)
            {
                case "int":
                    if (int.TryParse(value.ToString(), out int intVal))
                    {
                        OnEditAction(intVal);
                    }
                    break;

                case "float":
                    if (float.TryParse(value.ToString(), out float floatVal))
                    {
                        OnEditAction(floatVal);
                    }
                    break;

                default:
                    OnEditAction(value);
                    break;
            }
        }

        public void AddText(string label, object text)
        {
            if (Text != null)
            {
                Text.UpdateText(text);
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
                Text.UpdateText(onUpdateAction);
                Text.Container.transform.SetParent(Container.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Text.Container.transform.SetParent(Container.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }

        public override void UpdateText()
        {
            Text.UpdateText();
            if (OnUpdateAction != null)
            {
                if (float.TryParse(OnUpdateAction().ToString(), out float updatedValue))
                {
                    Slider.value = updatedValue;
                }
            }
            Slider.minValue = MinValue;
            Slider.maxValue = MaxValue;

        }


        public void  bindMin(Func<float> minFunc)
        {
            _boundMin = minFunc;
        }

        public void bindMax(Func<float> maxFunc)
        {
            _boundMax = maxFunc;
        }

        public void bindMinMax(Func<float> minFunc, Func<float> maxFunc)
        {
            bindMin(minFunc);
            bindMax(maxFunc);
        }

    }

    public class MoCaSlider : MoGCArgs
    {
        public Vector2 Range;
        public Func<float> BoundMin;
        public Func<float> BoundMax;
        public MoCaSlider(Vector2 range,
            Action<object> onEditAction,
            Func<object> onUpdateAction,
            Func<object> text = null,
            string valType = "none",
            Func<float> boundMin = null,
            Func<float> boundMax = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSlider), meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {
            Range = range;
            if (boundMin != null) { BoundMin = boundMin; }
            if (boundMax != null) { BoundMax = boundMax; }


        }

        public MoCaSlider(Vector2 range,
            Action<object> onEditAction,
            Func<object> onUpdateAction,
            object text = null,
            string valType = "none",
            Func<float> boundMin = null,
            Func<float> boundMax = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiSlider), meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {
            Range = range;
            if (boundMin != null) { BoundMin = boundMin; }
            if (boundMax != null) { BoundMax = boundMax; }
        }


    }
}
