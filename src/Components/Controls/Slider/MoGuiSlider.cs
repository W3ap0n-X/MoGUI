using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGuiSlider : MoGuiControl
    {
        string Type;
        MoGuiTxt Text;
        Slider Slider;

        public ControlOrientation Direction;
        public float Value => Slider.value;
        Image Fill;
        public Func<Color> BoundFillColor;

        public Color FillColor
        {
            get
            {
                if(BoundFillColor != null)
                {
                    return BoundFillColor();
                }
                else
                {
                    return Color.clear;
                }
            }
        }

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

            switch (Meta.Slider.labelPlacement)
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
            switch (Meta.Slider.labelPlacement)
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

        public MoGuiSlider(MoGuiMeta meta, string name, MoCaSlider args) : base(meta, name)
        {
            MinValue = args.Range.x;
            MaxValue = args.Range.y;

            Direction = args.Direction ?? Meta.Slider.direction;

            OnUpdateAction = args.OnUpdateAction;
            Type = args.ValType;
            OnEditAction = args.OnEditAction;
            switch (Meta.Slider.labelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("SliderTxt", args.Text);
                    Obj = CreateSlider();
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateSlider();
                    AddText("SliderTxt", args.Text);
                    break;
                default:
                    Obj = CreateSlider();
                    break;
            }

            if (args.BoundMin != null)
            {
                bindMin(args.BoundMin);
            }
            if (args.BoundMax != null)
            {
                bindMax(args.BoundMax);
            }


        }

        public override void _Init()
        {
            Container = CreateContainer(Meta.Slider.orientation);
        }

        public override void SetLayout()
        {
            if (Direction == ControlOrientation.vertical)
            {
                minWidth = Meta.Slider.verticalsizeSettings.minHeight;
                minHeight = Meta.Slider.verticalsizeSettings.minWidth;
                if (Meta.Slider.verticalsizeSettings.preferredWidth != null) { preferredWidth = (float)Meta.Slider.verticalsizeSettings.preferredWidth; }
                if (Meta.Slider.verticalsizeSettings.preferredHeight != null) { preferredHeight = (float)Meta.Slider.verticalsizeSettings.preferredHeight; }
                flexibleWidth = Meta.Slider.verticalsizeSettings.flexibleWidth ?? 0;
                flexibleHeight = Meta.Slider.verticalsizeSettings.flexibleHeight ?? 0;
            } else
            {
                minWidth = Meta.Slider.horizontalizeSettings.minWidth;
                minHeight = Meta.Slider.horizontalizeSettings.minHeight;
                if (Meta.Slider.horizontalizeSettings.preferredWidth != null) { preferredWidth = (float)Meta.Slider.horizontalizeSettings.preferredWidth; }
                if (Meta.Slider.horizontalizeSettings.preferredHeight != null) { preferredHeight = (float)Meta.Slider.horizontalizeSettings.preferredHeight; }
                flexibleWidth = Meta.Slider.horizontalizeSettings.flexibleWidth ?? 0;
                flexibleHeight = Meta.Slider.horizontalizeSettings.flexibleHeight ?? 0;
            }
        }

        public GameObject CreateSlider()
        {
            GameObject sliderObject = new GameObject(PluginName + "_" + Name + "_" + "Slider");
            sliderObject.transform.SetParent(Container.transform, false);
            RectTransform sliderRect = sliderObject.AddComponent<RectTransform>();
            sliderRect.anchorMin = new Vector2(0, 0);
            sliderRect.anchorMax = new Vector2(1, 1);
            


            AddLayoutElement(sliderObject);
            SetLayout();



            Slider = sliderObject.AddComponent<Slider>();
            Slider.minValue = MinValue;
            Slider.maxValue = MaxValue;

            Slider.wholeNumbers = Type == "int" ? true : false;
            
            GameObject backgroundObject = new GameObject(PluginName + "_" + Name + "_" + "SliderBackground");
            backgroundObject.transform.SetParent(sliderObject.transform, false);
            RectTransform backgroundRect = backgroundObject.AddComponent<RectTransform>();

            

            Image backgroundImage = backgroundObject.AddComponent<Image>();
            backgroundImage.color = Meta.Panel.background.Shade;

            GameObject fillAreaObject = new GameObject(PluginName + "_" + Name + "_" + "SliderFillArea");
            fillAreaObject.transform.SetParent(sliderObject.transform, false);
            RectTransform fillAreaRect = fillAreaObject.AddComponent<RectTransform>();
            

            GameObject fillObject = new GameObject(PluginName + "_" + Name + "_" + "SliderFill");
            fillObject.transform.SetParent(fillAreaObject.transform, false);
            RectTransform fillRect = fillObject.AddComponent<RectTransform>();

            Fill = fillObject.AddComponent<Image>();
            Fill.color = FillColor;
            Slider.fillRect = Fill.rectTransform;

            GameObject handleObject = new GameObject(PluginName + "_" + Name + "_" + "SliderHandle");
            handleObject.transform.SetParent(sliderObject.transform, false);
            RectTransform handleRect = handleObject.AddComponent<RectTransform>();
            

            Image handleImage = handleObject.AddComponent<Image>();
            handleImage.color = Meta.Slider.Color.Color;
            Slider.handleRect = handleRect;



            if (Direction == ControlOrientation.vertical)
            {
                
                backgroundRect.anchorMin = Vector2.zero;
                backgroundRect.anchorMax = Vector2.one;

                backgroundRect.offsetMin = new Vector2(10, 0);
                backgroundRect.offsetMax = new Vector2(-10, 0);


                fillAreaRect.anchorMin = Vector2.zero;
                fillAreaRect.anchorMax = Vector2.one;
                fillAreaRect.offsetMin = new Vector2(0, 0);
                fillAreaRect.offsetMax = new Vector2(0, 0);

                fillRect.anchorMin = Vector2.zero;
                fillRect.anchorMax = Vector2.one;
                fillRect.offsetMin = new Vector2(12, 0);
                fillRect.offsetMax = new Vector2(-12, 0);

                handleRect.anchorMin = new Vector2(0.5f, 0);
                handleRect.anchorMax = new Vector2(0.5f,  1);
                handleRect.offsetMin = new Vector2(10, -10);
                handleRect.offsetMax = new Vector2(-10, 10);
                Slider.direction = Slider.Direction.BottomToTop;
            }
            else
            {
                
                backgroundRect.anchorMin = Vector2.zero;
                backgroundRect.anchorMax = Vector2.one;

                backgroundRect.offsetMin = new Vector2(0, 10);
                backgroundRect.offsetMax = new Vector2(0, -10);


                fillAreaRect.anchorMin = Vector2.zero;
                fillAreaRect.anchorMax = Vector2.one;
                fillAreaRect.offsetMin = new Vector2(0, 0);
                fillAreaRect.offsetMax = new Vector2(0, 0);

                fillRect.anchorMin = Vector2.zero;
                fillRect.anchorMax = Vector2.one;
                fillRect.offsetMin = new Vector2(0, 12);
                fillRect.offsetMax = new Vector2(0, -12);

                handleRect.anchorMin = new Vector2(0, 0.5f);
                handleRect.anchorMax = new Vector2(1, 0.5f);
                handleRect.offsetMin = new Vector2(-10, 10);
                handleRect.offsetMax = new Vector2(10, -10);
                Slider.direction = Slider.Direction.LeftToRight;
            }




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
                Text.Update(text);
                Text.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, text:text, Meta.Slider.labelSettings);
                Text.Obj.transform.SetParent(Container.transform, false);
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
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction, Meta.Slider.labelSettings);
                Text.Obj.transform.SetParent(Container.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }

        public override void Update()
        {
            if(Text != null)
            {
                Text.Update();
            }
            
            if (OnUpdateAction != null)
            {
                if (float.TryParse(OnUpdateAction().ToString(), out float updatedValue))
                {
                    Slider.value = updatedValue;
                }
            }
            Fill.color = FillColor;

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


}
