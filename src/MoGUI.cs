using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGui 
    {
        GameObject Canvas;
        public MoGuiPanel RootPanel;
        public string PluginName;
        public string Name;
        public MoGuiMeta Meta;

        public Dictionary<string, MoGuiPanel> Panels = new Dictionary<string, MoGuiPanel>();
        public MoGui(string pluginName, string name, Vector2 size, Vector2 pos)
        {
            Meta = new MoGuiMeta(pluginName, name);
            PluginName = pluginName;
            Name = name;
            Canvas = CreateCanvas();
            RootPanel = new MoGuiPanel(Meta,  Name, Canvas, size, pos);
            RootPanel.Obj.transform.SetParent(Canvas.transform, false);
        }

        public MoGui(MoGuiMeta meta, string name, Vector2 size, Vector2 pos)
        {
            
            Meta = new MoGuiMeta(meta, name);
            PluginName = Meta.PluginName;
            Name = name;
            Canvas = CreateCanvas();
            RootPanel = new MoGuiPanel(Meta, Name, Canvas, size, pos);
            RootPanel.Obj.transform.SetParent(Canvas.transform, false);
        }

        public void ToggleGui(bool show)
        {
            Canvas.SetActive(show);
        }

        public bool IsActive
        {
            get => Canvas.activeSelf;
        }

        public GameObject CreateCanvas()
        {
            var canvasObject = new GameObject( PluginName + "_" + Name + "_" + "Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            return canvasObject;
        }

        
        public MoGuiPanel AddPanel(string label, Vector2 size, Vector2 pos, bool topLevel = false)
        {
            MoGuiPanel newPanel;
            if (Panels.ContainsKey(label))
            {
                newPanel = Panels[label];
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
            }
            else
            {
                newPanel = new MoGuiPanel(Meta, Name + "_" + label + "_", size, pos, topLevel);
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
                Panels.Add(label, newPanel);
            }
            return newPanel;
        }

        public MoGuiPanel AddPanel(MoGuiMeta meta, string label, Vector2 size, Vector2 pos, bool topLevel = false)
        {
            MoGuiPanel newPanel;
            if (Panels.ContainsKey(label))
            {
                newPanel = Panels[label];
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
            }
            else
            {
                newPanel = new MoGuiPanel(meta, Name + "_" + label + "_", size, pos, topLevel);
                newPanel.Obj.transform.SetParent(Canvas.transform, false);
                Panels.Add(label, newPanel);
            }
            return newPanel;
        }

        public void Update()
        {
            if (IsActive)
            {
                RootPanel.UpdateText();
                foreach (var item in Panels)
                {
                    item.Value.UpdateText();
                }
            }
        }
        
    }

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

        public virtual void UpdateText()
        {

        }


        public virtual void _Init()
        {
            Container = CreateContainer();
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

    public class DraggableHandle : MonoBehaviour, IDragHandler
    {
        private RectTransform rectTransform;

        private RectTransform rectTransformParent;

        public void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void BindParent(GameObject parent)
        {
            rectTransformParent = parent.GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (rectTransformParent != null)
            {
                rectTransformParent.anchoredPosition += eventData.delta;
            }
            else if (rectTransform != null)
            {
                rectTransform.anchoredPosition += eventData.delta;
            }
            else
            {
                Awake();
            }
        }
    }

    public class ResizableUI : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        private RectTransform rectTransform;
        private RectTransform rectTransformParent;
        private Vector2 originalMousePosition;
        private Vector2 originalPanelSize;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void BindParent(GameObject parent)
        {
            rectTransformParent = parent.GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            originalMousePosition = eventData.position;
            if (rectTransformParent != null)
            {
                originalPanelSize = rectTransformParent.sizeDelta;
            }
            else if (rectTransform != null)
            {
                originalPanelSize = rectTransform.sizeDelta;
            }
            else
            {
                Awake();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 mouseDelta = eventData.position - originalMousePosition;
            Vector2 newSize = originalPanelSize + mouseDelta;

            if (rectTransformParent != null)
            {
                rectTransformParent.sizeDelta = new Vector2(
                Mathf.Max(newSize.x, 250), 
                Mathf.Max(newSize.y, 200)  
            );
            }
            else if (rectTransform != null)
            {
                rectTransform.sizeDelta += newSize;
            }
            else
            {
                Awake();
            }
        }
    }

    public enum ControlOrientation
    {
        horizontal,
        vertical
    }

    public enum ControlLabelPlacement
    {
        none,
        before,
        after
    }

    


    public class MoGuiMeta
    {
        public static int DefaultTxtMargin = 5;
        public static int DefaultFontSize = 14;
        public static Font DefaultFont = Font.CreateDynamicFontFromOSFont("Arial", 24);
        public static Color DefaultFontColor = Color.white;
        public static float DefaultHeaderSize = 40;
        public static int DefaultHeaderFontSize = 18;
        public static int DefaultHeaderExitFontSize = 28;
        public static Color DefaultHeaderColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        public static Color DefaultHeaderExitColor = new Color(0.75f, 0.25f, 0.25f, 1f);
        public static Color DefaultPanelColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        public static Color DefaultButtonColor = new Color(0.3f, 0.3f, 0.3f, 1f);
        public static Color DefaultInputColor = new Color(0.1f, 0.1f, 0.1f, 1f);
        public static Color DefaultInputPlaceholderFontColor = new Color(0.6f, 0.6f, 0.6f, 0.8f);
        public static Color DefaultToggleColor = new Color(0.1f, 0.1f, 0.1f, 1f);
        public static Color DefaultToggleCheckColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        public static ControlOrientation DefaultOrientation = ControlOrientation.horizontal;
        public static ControlLabelPlacement DefaultLabelPlacement = ControlLabelPlacement.after;
        public static ControlOrientation DefaultToggleOrientation = ControlOrientation.horizontal;
        public static ControlLabelPlacement DefaultToggleLabelPlacement = ControlLabelPlacement.after;
        public static ControlOrientation DefaultSliderOrientation = ControlOrientation.vertical;
        public static ControlLabelPlacement DefaultSliderLabelPlacement = ControlLabelPlacement.before;
        public static ControlOrientation DefaultInputOrientation = ControlOrientation.horizontal;
        public static ControlLabelPlacement DefaultInputLabelPlacement = ControlLabelPlacement.before;
        public static Vector4 DefaultSliderSize = new Vector4(50, 25, 100, 40);
        public static Vector4 DefaultToggleSize = new Vector4(15, 15, 20, 20);
        public static Vector4 DefaultButtonSize = new Vector4(60, 30, 100, 40);
        public static Vector4 DefaultInputSize = new Vector4(20, 20, 80, 30);


        // Info
        public string PluginName;
        public string Name;
        // Panel
        public Color PanelColor;
        // Header
        public int HeaderFontSize;
        public int HeaderExitFontSize;
        public float HeaderSize;
        public Color HeaderColor;
        public Color HeaderExitColor;
        // Text
        public int TxtMargin;
        public int FontSize;
        public Font Font;
        public Color FontColor;
        // Button
        public int ButtonFontSize;
        public Font ButtonFont;
        public Color ButtonColor;
        public Color ButtonFontColor;
        public Vector4 ButtonSize;

        // Toggle
        public Color ToggleColor;
        public Color ToggleCheckColor;
        public ControlOrientation ToggleOrientation;
        public ControlLabelPlacement ToggleLabelPlacement;
        public Vector4 ToggleSize;

        // Slider
        public Color SliderHandleColor;
        public Color SliderTrackColor;
        public Color SliderFillColor;
        public ControlOrientation SliderOrientation;
        public ControlLabelPlacement SliderLabelPlacement;
        public Vector4 SliderSize;
        // Input
        public int InputFontSize;
        public Font InputFont;
        public Color InputColor;
        public Color InputFontColor;
        public Color InputPlaceholderFontColor;
        public ControlOrientation InputOrientation;
        public ControlLabelPlacement InputLabelPlacement;
        public Vector4 InputSize;
        // DDL
        public int DDLFontSize;
        public int DDLListFontSize;
        public Font DDLFont;
        public Font DDLListFont;
        public Color DDLButtonColor;
        public Color DDLFontColor;
        public Color DDLListColor;
        public Color DDLListItemFontColor;
        public Vector4 DDLSize;
        // Other
        public ControlOrientation Orientation;
        public ControlLabelPlacement LabelPlacement;

        public MoGuiMeta(string pluginName, string name
            , int? txtMargin = null
            , int? fontSize = null
            , int? inputFontSize = null
            , int? headerFontSize = null
            , int? headerexitFontSize = null
            , int? buttonFontSize = null
            , int? dDLFontSize = null
            , int? dDLListFontSize = null
            , float? headerSize = null
            , Color? fontColor = null
            , Color? headerColor = null
            , Color? headerExitColor = null
            , Color? panelColor = null
            , Color? inputColor = null
            , Color? inputFontColor = null
            , Color? inputPlaceholderFontColor = null
            , Color? buttonColor = null
            , Color? buttonFontColor = null
            , Color? toggleColor = null
            , Color? toggleCheckColor = null
            , Color? sliderHandleColor = null
            , Color? sliderTrackColor = null
            , Color? sliderFillColor = null
            , Color? dDLButtonColor = null
            , Color? dDLFontColor = null
            , Color? dDLListColor = null
            , Color? dDLListItemFontColor = null
            , Font font = null
            , Font inputFont = null
            , Font buttonFont = null
            , Font dDLFont = null
            , Font dDLListFont = null
            , ControlOrientation? orientation = null
            , ControlOrientation? sliderOrientation = null
            , ControlOrientation? inputOrientation = null
            , ControlOrientation? toggleOrientation = null
            , ControlLabelPlacement? labelPlacement = null
            , ControlLabelPlacement? sliderLabelPlacement = null
            , ControlLabelPlacement? inputLabelPlacement = null
            , ControlLabelPlacement? toggleLabelPlacement = null
            , Vector4? buttonSize = null
            , Vector4? toggleSize = null
            , Vector4? sliderSize = null
            , Vector4? inputSize = null
            , Vector4? dDLSize = null
        )
        {
            PluginName = pluginName;
            Name = name;
            TxtMargin = txtMargin ?? DefaultTxtMargin;
            FontSize = fontSize ?? DefaultFontSize;
            HeaderFontSize = headerFontSize ?? DefaultHeaderFontSize;
            HeaderExitFontSize = headerexitFontSize ?? DefaultHeaderExitFontSize;
            InputFontSize = inputFontSize ?? DefaultFontSize;
            ButtonFontSize = buttonFontSize ?? DefaultFontSize;
            HeaderSize = headerSize ?? DefaultHeaderSize;
            HeaderColor = headerColor ?? DefaultHeaderColor;
            HeaderExitColor = headerExitColor ?? DefaultHeaderExitColor;
            Font = font ?? DefaultFont;
            InputFont = inputFont ?? DefaultFont;
            ButtonFont = buttonFont ?? DefaultFont;
            DDLFont = dDLFont ?? DefaultFont;
            DDLListFont = dDLListFont ?? DefaultFont;
            FontColor = fontColor ?? DefaultFontColor;
            PanelColor = panelColor ?? DefaultPanelColor;
            InputColor = inputColor ?? DefaultInputColor;
            InputFontColor = inputFontColor ?? DefaultFontColor;
            InputPlaceholderFontColor = inputPlaceholderFontColor ?? DefaultInputPlaceholderFontColor;
            ButtonColor = buttonColor ?? DefaultButtonColor;
            ButtonFontColor = buttonFontColor ?? DefaultFontColor;
            ToggleColor = toggleColor ?? DefaultToggleColor;
            ToggleCheckColor = toggleCheckColor ?? DefaultToggleCheckColor;
            DDLFontSize = dDLFontSize ?? DefaultFontSize;
            DDLListFontSize = dDLListFontSize ?? DefaultFontSize;
            SliderHandleColor = sliderHandleColor ?? DefaultToggleCheckColor;
            SliderTrackColor = sliderTrackColor ?? DefaultInputColor;
            SliderFillColor = sliderFillColor ?? DefaultInputColor;
            DDLButtonColor = dDLButtonColor ?? DefaultButtonColor;
            DDLFontColor = dDLFontColor ?? DefaultFontColor;
            DDLListColor = dDLListColor ?? DefaultInputColor;
            DDLListItemFontColor = dDLListItemFontColor ?? DefaultFontColor;
            Orientation = orientation ?? DefaultOrientation;
            SliderOrientation = sliderOrientation ?? DefaultSliderOrientation;
            InputOrientation = inputOrientation ?? DefaultInputOrientation;
            ToggleOrientation = toggleOrientation ?? DefaultToggleOrientation;
            LabelPlacement = labelPlacement ?? DefaultLabelPlacement;
            SliderLabelPlacement = sliderLabelPlacement ?? DefaultSliderLabelPlacement;
            InputLabelPlacement = inputLabelPlacement ?? DefaultInputLabelPlacement;
            ToggleLabelPlacement = toggleLabelPlacement ?? DefaultToggleLabelPlacement;
            ButtonSize = buttonSize ?? DefaultButtonSize;
            ToggleSize = toggleSize ?? DefaultToggleSize;
            SliderSize = sliderSize ?? DefaultSliderSize;
            InputSize = inputSize ?? DefaultInputSize;
            DDLSize = dDLSize ?? DefaultButtonSize;

        }

        public MoGuiMeta(MoGuiMeta meta, string name
            , int? txtMargin = null
            , int? fontSize = null
            , int? inputFontSize = null
            , int? buttonFontSize = null
            , int? headerFontSize = null
            , int? headerexitFontSize = null
            , int? dDLFontSize = null
            , int? dDLListFontSize = null
            , float? headerSize = null
            , Color? fontColor = null
            , Color? headerColor = null
            , Color? headerExitColor = null
            , Color? panelColor = null
            , Color? inputColor = null
            , Color? inputFontColor = null
            , Color? inputPlaceholderFontColor = null
            , Color? buttonColor = null
            , Color? buttonFontColor = null
            , Color? toggleColor = null
            , Color? toggleCheckColor = null
            , Color? sliderHandleColor = null
            , Color? sliderTrackColor = null
            , Color? sliderFillColor = null
            , Color? dDLButtonColor = null
            , Color? dDLFontColor = null
            , Color? dDLListColor = null
            , Color? dDLListItemFontColor = null
            , Font font = null
            , Font inputFont = null
            , Font buttonFont = null
            , Font dDLFont = null
            , Font dDLListFont = null
            , ControlOrientation? orientation = null
            , ControlLabelPlacement? labelPlacement = null
            , ControlOrientation? sliderOrientation = null
            , ControlOrientation? inputOrientation = null
            , ControlOrientation? toggleOrientation = null
            , ControlLabelPlacement? sliderLabelPlacement = null
            , ControlLabelPlacement? inputLabelPlacement = null
            , ControlLabelPlacement? toggleLabelPlacement = null
            , Vector4? buttonSize = null
            , Vector4? toggleSize = null
            , Vector4? sliderSize = null
            , Vector4? inputSize = null
            , Vector4? dDLSize = null
        )
        {
            PluginName = meta.PluginName;
            Name = name;
            TxtMargin = txtMargin ?? meta.TxtMargin;
            FontSize = fontSize ?? meta.FontSize;
            InputFontSize = inputFontSize ?? meta.FontSize;
            ButtonFontSize = buttonFontSize ?? meta.FontSize;
            HeaderFontSize = headerFontSize ?? meta.HeaderFontSize;
            HeaderExitFontSize = headerexitFontSize ?? meta.HeaderExitFontSize;
            HeaderSize = headerSize ?? meta.HeaderSize;
            HeaderColor = headerColor ?? meta.HeaderColor;
            HeaderExitColor = headerExitColor ?? meta.HeaderExitColor;
            Font = font ?? meta.Font;
            InputFont = inputFont ?? meta.Font;
            ButtonFont = buttonFont ?? meta.ButtonFont;
            DDLFont = dDLFont ?? meta.DDLFont;
            DDLListFont = dDLListFont ?? meta.DDLListFont;
            FontColor = fontColor ?? meta.FontColor;
            PanelColor = panelColor ?? meta.PanelColor;
            InputColor = inputColor ?? meta.InputColor;
            InputFontColor = inputFontColor ?? meta.FontColor;
            InputPlaceholderFontColor = inputPlaceholderFontColor ?? meta.InputPlaceholderFontColor;
            ButtonColor = buttonColor ?? meta.ButtonColor;
            ButtonFontColor = buttonFontColor ?? meta.FontColor;
            ToggleColor = toggleColor ?? meta.ToggleColor;
            ToggleCheckColor = toggleCheckColor ?? meta.ToggleCheckColor;
            DDLFontSize = dDLFontSize ?? meta.DDLFontSize;
            DDLListFontSize = dDLListFontSize ?? meta.DDLListFontSize;
            SliderHandleColor = sliderHandleColor ?? meta.SliderHandleColor;
            SliderTrackColor = sliderTrackColor ?? meta.SliderTrackColor;
            SliderFillColor = sliderFillColor ?? DefaultInputColor;
            DDLButtonColor = dDLButtonColor ?? meta.DDLButtonColor;
            DDLFontColor = dDLFontColor ?? meta.DDLFontColor;
            DDLListColor = dDLListColor ?? meta.DDLListColor;
            DDLListItemFontColor = dDLListItemFontColor ?? meta.DDLListItemFontColor;
            SliderOrientation = sliderOrientation ?? meta.SliderOrientation;
            InputOrientation = inputOrientation ?? meta.InputOrientation;
            ToggleOrientation = toggleOrientation ?? meta.ToggleOrientation;
            SliderLabelPlacement = sliderLabelPlacement ?? meta.SliderLabelPlacement;
            InputLabelPlacement = inputLabelPlacement ?? meta.InputLabelPlacement;
            ToggleLabelPlacement = toggleLabelPlacement ?? meta.ToggleLabelPlacement;
            LabelPlacement = labelPlacement ?? meta.LabelPlacement;
            Orientation = orientation ?? meta.Orientation;
            ButtonSize = buttonSize ?? meta.ButtonSize;
            ToggleSize = toggleSize ?? meta.ToggleSize;
            SliderSize = sliderSize ?? meta.SliderSize;
            InputSize = inputSize ?? meta.InputSize;
            DDLSize = dDLSize ?? meta.DDLSize;
        }
    }


    

}
