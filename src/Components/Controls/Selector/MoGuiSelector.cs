using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    class MoGuiSelector : MoGuiControl
    {
        string Type;
        MoGuiTxt Text;
        public Dictionary<string, object> Options;
        Dictionary<string, bool> _options = new Dictionary<string, bool>(); 

        List<MoGuiToggle> moGuiToggles = new List<MoGuiToggle>();
        ToggleGroup ToggleGroup;

        public object Value;

        public MoGuiSelector(MoGuiMeta meta, string name, MoCaSelector args) : base(meta, name)
        {
            Options = args.Options;
            switch (Meta.SliderLabelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("SelectorTxt", args.Text);
                    Obj = CreateSelector();
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateSelector();
                    AddText("SelectorTxt", args.Text);
                    break;
                default:
                    Obj = CreateSelector();
                    break;
            }


            foreach (var item in Options)
            {
                _options.Add(item.Key, false);
            }
            foreach (var item in _options)
            {
                MoGuiToggle newToggle = new MoGuiToggle(Meta, Name + "_Option_" + item.Key, () => item.Value, () => item.Key, (val) => _options[item.Key] = val, ToggleType.button);
                newToggle.Obj.GetComponent<Toggle>().group = ToggleGroup;
                newToggle.Container.transform.SetParent(Obj.transform, false);
                moGuiToggles.Add(newToggle);
            }
            GetValue();
        }

        public override void _Init()
        {
            Container = CreateContainer(Meta.SliderOrientation);
        }
        public override void SetLayout()
        {

        }

        public GameObject CreateSelector()
        {
            GameObject selectorObject = new GameObject(PluginName + "_" + Name + "_" + "Selector");
            selectorObject.transform.SetParent(Container.transform, false);
            RectTransform selectorRect = selectorObject.AddComponent<RectTransform>();
            selectorRect.anchorMin = new Vector2(0, 0);
            selectorRect.anchorMax = new Vector2(1, 1);


            ToggleGroup = selectorObject.AddComponent<ToggleGroup>();
            if (Meta.SelectorOrientation == ControlOrientation.vertical)
            {
                VerticalLayoutGroup layoutGroup = selectorObject.AddComponent<VerticalLayoutGroup>();
                layoutGroup.childForceExpandWidth = false;
                layoutGroup.childForceExpandHeight = false;
            }
            else
            {
                HorizontalLayoutGroup layoutGroup = selectorObject.AddComponent<HorizontalLayoutGroup>();
                layoutGroup.childForceExpandWidth = false;
                layoutGroup.childForceExpandHeight = false;
            }


            LayoutElement layoutElement = selectorObject.AddComponent<LayoutElement>();


            

            return selectorObject;
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
                Text = new MoGuiTxt(Meta, Name + "_" + label, text);
                Text.Obj.transform.SetParent(Container.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }

        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (Text != null)
            {
                Text.Update(onUpdateAction);
                Text.Obj.transform.SetParent(Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Text.Obj.transform.SetParent(Container.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }

        void GetValue()
        {
            foreach (var item in _options)
            {
                if (item.Value == true)
                {
                    Value = Options[item.Key];
                }
            }
            
        }

        public override void Update()
        {
            if (Text != null)
            {
                Text.Update();
            }
            if (OnUpdateAction != null)
            {
                
            }

            foreach (var item in moGuiToggles)
            {
                item.Update();
            }
            GetValue();

        }


    }
}
