using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Linq;
using System.ComponentModel;

namespace MoGUI
{

    public class MoGuiScrollArea
    {
        MoGuiMeta Meta;
        public GameObject Content;
        public GameObject Obj;
        public MoGuiScrollArea(MoGuiMeta meta)
        {
            Meta = meta;
            Content = CreateViewPort();
        }

        public GameObject CreateViewPort()
        {
            Obj = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollView");
            ScrollRect scrollRect = Obj.AddComponent<ScrollRect>();

            RectTransform scrollViewRect = Obj.GetComponent<RectTransform>();
            scrollViewRect.anchorMin = Vector2.zero;
            scrollViewRect.anchorMax = Vector2.one;
            scrollViewRect.offsetMin = Vector2.zero;
            scrollViewRect.offsetMax = Vector2.zero;

            LayoutElement ObjlayoutElement = Obj.AddComponent<LayoutElement>();
            ObjlayoutElement.minWidth = 100;
            ObjlayoutElement.minHeight = 100;
            //layoutElement.preferredWidth = 100;
            ObjlayoutElement.flexibleWidth = 1;
            ObjlayoutElement.flexibleHeight = 1;

            if (MoGUIManager._LayoutDebug)
            {
                Image SAbg = Obj.AddComponent<Image>();
                SAbg.color = MoGUIManager._LayoutDebugScrollColor;
            }


            GameObject viewportObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollViewViewport");
            viewportObject.transform.SetParent(Obj.transform, false);

            viewportObject.AddComponent<Mask>().showMaskGraphic = false;
            viewportObject.AddComponent<Image>().color = new Color(0, 0, 0, 0.5f);

            scrollRect.viewport = viewportObject.GetComponent<RectTransform>();
            scrollRect.viewport.anchorMin = new Vector2(0, 0);
            scrollRect.viewport.anchorMax = new Vector2(1, 1);
            scrollRect.viewport.offsetMin = new Vector2(0, 20);
            scrollRect.viewport.offsetMax = new Vector2(-20, 0);
            scrollRect.viewport.pivot = new Vector2(0, 1);

            GameObject contentObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollViewContent");
            contentObject.transform.SetParent(viewportObject.transform, false);

            var CoLayout = contentObject.AddComponent<VerticalLayoutGroup>();
            CoLayout.childForceExpandHeight = false;
            CoLayout.childForceExpandWidth = false;
            ContentSizeFitter contentFitter = contentObject.AddComponent<ContentSizeFitter>();
            contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            scrollRect.content = contentObject.GetComponent<RectTransform>();
            
            scrollRect.content.anchorMin = new Vector2(0, 0);
            scrollRect.content.anchorMax = new Vector2(1, 1);
            scrollRect.content.offsetMin = new Vector2(0, 0);
            scrollRect.content.offsetMax = new Vector2(0, 0);
            scrollRect.content.pivot = new Vector2(0, 1);
            scrollRect.scrollSensitivity = 30f;


            if (MoGUIManager._LayoutDebug)
            {
                Image CObg = contentObject.AddComponent<Image>();
                CObg.color = MoGUIManager._LayoutDebugScrollContentColor;
            }
            //LayoutElement layoutElement = contentObject.AddComponent<LayoutElement>();
            //layoutElement.minWidth = 100;
            //layoutElement.minHeight = 100;
            ////layoutElement.preferredWidth = 100;
            //layoutElement.flexibleWidth = 1;
            //layoutElement.flexibleHeight = 1;

            GameObject verticalScrollbarObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollViewVerticalScrollbar");
            verticalScrollbarObject.AddComponent<Image>().color = Meta.PanelColor.Shade;
            verticalScrollbarObject.transform.SetParent(Obj.transform, false);

            scrollRect.verticalScrollbar = verticalBar(verticalScrollbarObject);
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            //scrollRect.verticalScrollbarSpacing = 5;

            GameObject horizontalScrollbarObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollViewHorizontalScrollbar");
            horizontalScrollbarObject.AddComponent<Image>().color = Meta.PanelColor.Shade;
            horizontalScrollbarObject.transform.SetParent(Obj.transform, false);
            scrollRect.horizontalScrollbar = horizontalBar(horizontalScrollbarObject);
            scrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            //scrollRect.horizontalScrollbarSpacing = 5;
            return contentObject;
        }
        Scrollbar verticalBar(GameObject parent)
        {
            Scrollbar scrollbar = parent.AddComponent<Scrollbar>();
            RectTransform scrollbarRect = parent.GetComponent<RectTransform>();

            scrollbarRect.anchorMin = new Vector2(1, 0);
            scrollbarRect.anchorMax = new Vector2(1, 1);
            scrollbarRect.pivot = new Vector2(1, 1); 
            scrollbarRect.offsetMin = new Vector2(-10, 0); 
            scrollbarRect.offsetMax = new Vector2(0, 0);  
            scrollbarRect.sizeDelta = new Vector2(10, 0); 
                                                          
            GameObject handleObject = new GameObject("Handle");
            handleObject.transform.SetParent(parent.transform, false);

            Image handleImage = handleObject.AddComponent<Image>();
            handleImage.color = Meta.PanelColor.Tint; 

            scrollbar.handleRect = handleObject.GetComponent<RectTransform>();
            scrollbar.handleRect.anchorMin = new Vector2(0, 0);
            scrollbar.handleRect.anchorMax = new Vector2(1, 1);
            scrollbar.handleRect.offsetMin = new Vector2(0, 0);
            scrollbar.handleRect.offsetMax = new Vector2(0, 0);
            scrollbar.direction = Scrollbar.Direction.BottomToTop;

            return scrollbar;
        }

        Scrollbar horizontalBar(GameObject parent)
        {
            Scrollbar scrollbar = parent.AddComponent<Scrollbar>();
            RectTransform scrollbarRect = parent.GetComponent<RectTransform>();

            scrollbarRect.anchorMin = new Vector2(0, 0);
            scrollbarRect.anchorMax = new Vector2(1, 0);
            scrollbarRect.pivot = new Vector2(0, 0.5f);
            scrollbarRect.offsetMin = new Vector2(0, 10);
            scrollbarRect.offsetMax = new Vector2(0, 0);  
            scrollbarRect.sizeDelta = new Vector2(0, 10);

            GameObject handleObject = new GameObject("Handle");
            handleObject.transform.SetParent(parent.transform, false);

            Image handleImage = handleObject.AddComponent<Image>();
            handleImage.color = Meta.PanelColor.Tint;

            scrollbar.handleRect = handleObject.GetComponent<RectTransform>();
            scrollbar.handleRect.anchorMin = new Vector2(0, 0);
            scrollbar.handleRect.anchorMax = new Vector2(1, 1);
            scrollbar.handleRect.offsetMin = new Vector2(0, 0);
            scrollbar.handleRect.offsetMax = new Vector2(0, 0);
            scrollbar.direction = Scrollbar.Direction.LeftToRight;
            return scrollbar;
        }
    }

}