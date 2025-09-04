using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace MoGUI
{
    public class MoGuiDDL : MoGuiControl
    {
        private Dropdown _dropdown;
        private List<string> _baseOptions;
        private Dictionary<string, int> _valOptions;
        public KeyValuePair<string,object> Selected;
        string Type;

        public MoGuiDDL(MoGuiMeta meta, string name, Dictionary<string, int> options, Action<object> onValueChanged, string type)
            : base(meta, name)
        {
            Type = type;
            OnEditAction = onValueChanged;
            
            _valOptions = options;
            _baseOptions = new List<string>();
            foreach (var option in _valOptions)
            {
                _baseOptions.Add(option.Key);
            }
            Obj = CreateDropdown();
        }

        public MoGuiDDL(MoGuiMeta meta, string name, List<string> options, Action<object> onValueChanged, string type)
            : base(meta, name)
        {
            Type = type;
            OnEditAction = onValueChanged;
            
            _baseOptions = options;

            Obj = CreateDropdown();

        }



        public MoGuiDDL(MoGuiMeta meta, string name, MoCaDDL args)
            : base(args.Meta ?? meta, name)
        {
            Type = args.ValType;
            
            if (args.OnEditAction != null) 
            {
                OnEditAction = args.OnEditAction;
            }

            if (args.DDLBoundOptions != null)
            {
                _valOptions = args.DDLBoundOptions;
                _baseOptions = new List<string>();
                foreach (var option in _valOptions)
                {
                    _baseOptions.Add(option.Key);
                }
            }
            else 
            {
                _baseOptions = args.DDLOptions;
            }


                

            Obj = CreateDropdown();

        }

        public override void _Init()
        {
            Container = CreateContainer();
        }

        public override void SetLayout()
        {
            minWidth = Meta.DDLSize.x;
            minHeight = Meta.DDLSize.y;
            //preferredWidth = Meta.DDLSize.z;
            //preferredHeight = Meta.DDLSize.w;
            flexibleHeight = 1;
            flexibleWidth = 1;
        }

        private GameObject CreateDropdown()
        {
            GameObject dropdownObject = new GameObject(PluginName + "_" + Name + "_Dropdown");
            dropdownObject.transform.SetParent(Container.transform, false);

            _dropdown = dropdownObject.AddComponent<Dropdown>();
            Image image = dropdownObject.AddComponent<Image>();
            image.color = Meta.DDLButtonColor;

            AddLayoutElement(dropdownObject);
            SetLayout();
            

            GameObject captionTextObject = new GameObject(PluginName + "_" + Name + "_DropdownLabel");
            captionTextObject.transform.SetParent(dropdownObject.transform, false);

            RectTransform captionRect = captionTextObject.AddComponent<RectTransform>();
            captionRect.anchorMin = new Vector2(0, 0);
            captionRect.anchorMax = new Vector2(1, 1);
            captionRect.offsetMin = new Vector2(10, 0);
            captionRect.offsetMax = new Vector2(-10, 0);

            Text captionText = captionTextObject.AddComponent<Text>();
            captionText.alignment = TextAnchor.MiddleLeft;
            captionText.font = Meta.DDLFont;
            captionText.color = Meta.DDLFontColor;
            captionText.fontSize = Meta.DDLFontSize;
            captionText.text = "Select...";
            _dropdown.captionText = captionText;

            GameObject arrowObject = new GameObject(PluginName + "_" + Name + "_DropdownLabelArrow");

            arrowObject.transform.SetParent(dropdownObject.transform, false);


            Text dragSymbol = arrowObject.AddComponent<Text>();
            dragSymbol.text = "︾";
            dragSymbol.font = Meta.DDLFont;
            dragSymbol.fontSize = Meta.DDLFontSize;
            dragSymbol.color = Meta.DDLFontColor;
            dragSymbol.alignment = TextAnchor.MiddleCenter;

            RectTransform arrowRect = arrowObject.GetComponent<RectTransform>();
            arrowRect.anchorMin = new Vector2(1, 0.5f);
            arrowRect.anchorMax = new Vector2(1, 0.5f);
            arrowRect.pivot = new Vector2(1, 0.5f);
            arrowRect.sizeDelta = new Vector2(20, 20);
            arrowRect.anchoredPosition = new Vector2(-10, 0);



            GameObject templateObject = new GameObject(PluginName + "_" + Name + "_DropdownTemplate");
            templateObject.transform.SetParent(dropdownObject.transform, false);
            RectTransform templateRect = templateObject.AddComponent<RectTransform>();
            
            templateRect.anchorMin = new Vector2(0, 0);
            templateRect.anchorMax = new Vector2(1, 0);
            templateRect.pivot = new Vector2(0.5f, 1f);
            templateRect.offsetMin = new Vector2(0, -150);
            templateRect.offsetMax = new Vector2(0, 0);
            templateObject.AddComponent<CanvasGroup>().alpha = 0;
            templateObject.AddComponent<Image>().color = Meta.DDLListColor;
            templateObject.SetActive(false);
            _dropdown.template = templateRect;



            GameObject viewportObject = new GameObject(PluginName + "_" + Name + "_DropdownViewport");
            viewportObject.transform.SetParent(templateObject.transform, false);
            RectTransform viewportRect = viewportObject.AddComponent<RectTransform>();

            viewportRect.anchorMin = Vector2.zero;
            viewportRect.anchorMax = Vector2.one;
            viewportRect.offsetMin = Vector2.zero;
            viewportRect.offsetMax = Vector2.zero;

            Image viewportImage = viewportObject.AddComponent<Image>();
            viewportImage.color = Meta.DDLListColor;
            Mask viewportMask = viewportObject.AddComponent<Mask>();
            viewportMask.showMaskGraphic = true;





            GameObject contentObject = new GameObject(PluginName + "_" + Name + "_DropdownContent");
            contentObject.transform.SetParent(viewportObject.transform, false);
            RectTransform contentRect = contentObject.AddComponent<RectTransform>();

            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = new Vector2(1, 1);
            contentRect.pivot = new Vector2(0.5f, 1f);
            contentRect.sizeDelta = new Vector2(0, 0); 

            contentObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            VerticalLayoutGroup contentLayout = contentObject.AddComponent<VerticalLayoutGroup>();
            contentLayout.childForceExpandHeight = false;
            contentLayout.childControlHeight = true;
            contentLayout.childControlWidth = true;

            ScrollRect scrollRect = templateObject.AddComponent<ScrollRect>();
            scrollRect.content = contentRect;
            scrollRect.viewport = viewportRect;
            scrollRect.horizontal = false;
            scrollRect.scrollSensitivity = 30f;

            GameObject itemObject = new GameObject(PluginName + "_" + Name + "_DropdownItem");
            itemObject.transform.SetParent(contentObject.transform, false);
            Image itemImage = itemObject.AddComponent<Image>();
            itemImage.color = Color.clear;
            Toggle itemToggle = itemObject.AddComponent<Toggle>();
            itemToggle.targetGraphic = itemImage;
            LayoutElement itemLayoutElement = itemObject.AddComponent<LayoutElement>();
            itemLayoutElement.minHeight = 25;

            GameObject itemLabelObject = new GameObject(PluginName + "_" + Name + "_DropdownLabel");
            itemLabelObject.transform.SetParent(itemObject.transform, false);
            RectTransform itemLabelRect = itemLabelObject.AddComponent<RectTransform>();
            itemLabelRect.anchorMin = Vector2.zero;
            itemLabelRect.anchorMax = Vector2.one;
            itemLabelRect.offsetMin = new Vector2(5, 0);
            itemLabelRect.offsetMax = new Vector2(-25, 0);
            Text itemLabel = itemLabelObject.AddComponent<Text>();
            itemLabel.alignment = TextAnchor.MiddleLeft;
            itemLabel.font = Meta.DDLListFont;
            itemLabel.fontSize = Meta.DDLFontSize;
            itemLabel.color = Meta.DDLListItemFontColor;

            GameObject checkmarkObject = new GameObject(PluginName + "_" + Name + "_DropdownCheckmark");
            checkmarkObject.transform.SetParent(itemObject.transform, false);
            Image checkmarkImage = checkmarkObject.AddComponent<Image>();
            checkmarkImage.color = new Color(0.8f, 0.8f, 0.8f, 0.4f);
            checkmarkImage.rectTransform.anchorMin = new Vector2(0, 0);
            checkmarkImage.rectTransform.anchorMax = new Vector2(1, 1);
            checkmarkImage.rectTransform.offsetMin = new Vector2(0, 0); 
            checkmarkImage.rectTransform.offsetMax = new Vector2(0, 0);
            itemToggle.graphic = checkmarkImage;

            _dropdown.itemText = itemLabel;
            AddOptions();
            _dropdown.onValueChanged.AddListener((value) => _OnEditAction(value));

            return dropdownObject;
        }

        public void AddOptions()
        {

            _dropdown.AddOptions(_baseOptions);
            _dropdown.value = 0;
            Selected = GetSelected(0);

        }

        public void _OnEditAction(object value)
        {
            Selected = GetSelected(int.Parse(value.ToString()));
            if(OnEditAction != null)
            {
                switch (Type)
                {
                    case "int":
                        if (int.TryParse(Selected.Value.ToString(), out int intVal))
                        {
                            OnEditAction(intVal);
                        }
                        break;

                    case "float":
                        if (float.TryParse(Selected.Value.ToString(), out float floatVal))
                        {
                            OnEditAction(floatVal);
                        }
                        break;

                    default:

                        OnEditAction(Selected.Key);
                        break;
                }
            }

            
            
        }

        public KeyValuePair<string,object> GetSelected(int selectedOption)
        {
            
            string optionKey = _baseOptions[selectedOption];
            int selectedVal;
            if(_valOptions != null)
            {
                selectedVal = _valOptions[optionKey];
            }else
            {
                selectedVal = selectedOption;
            }


            return new KeyValuePair<string, object>(optionKey, selectedVal);
        }

        public override void Update()
        {
            Selected = GetSelected(_dropdown.value);
        }
    }

    public class MoCaDDL : MoGCArgs
    {

        public List<string> DDLOptions;
        public Dictionary<string, int> DDLBoundOptions = null;
        public MoCaDDL(List<string> dDLOptions,
            Action<object> onEditAction = null,
            string valType = "none",
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiDDL), meta, valType: valType)
        {
            if (onEditAction != null) { OnEditAction = onEditAction; }
            DDLOptions = dDLOptions;
        }

        public MoCaDDL(Dictionary<string, int> dDLBoundOptions,
            Action<object> onEditAction = null,
            string valType = "none",
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiDDL), meta, valType: valType)
        {
            if (onEditAction != null) { OnEditAction = onEditAction; }

            DDLBoundOptions = dDLBoundOptions;
        }

    }

    public class DDLMeta : ControlMeta
    {


        public MoGuiColor Color = GuiMeta.DefaultPanelColor;
        public MoGuiColor textColor = GuiMeta.DefaultFontColor;
        public DDLMeta(string name) : base(name) { }


    }
}