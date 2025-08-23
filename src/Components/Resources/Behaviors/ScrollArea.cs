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

            GameObject viewportObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollViewViewport");
            viewportObject.transform.SetParent(Obj.transform, false);

            viewportObject.AddComponent<Mask>().showMaskGraphic = false;
            viewportObject.AddComponent<Image>().color = new Color(0, 0, 0, 0.5f);

            scrollRect.viewport = viewportObject.GetComponent<RectTransform>();
            scrollRect.viewport.anchorMin = new Vector2(0, 0);
            scrollRect.viewport.anchorMax = new Vector2(1, 1);
            scrollRect.viewport.offsetMin = new Vector2(0, 20);
            scrollRect.viewport.offsetMax = new Vector2(-20, 0);

            GameObject contentObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollViewContent");
            contentObject.transform.SetParent(viewportObject.transform, false);

            contentObject.AddComponent<VerticalLayoutGroup>();
            ContentSizeFitter contentFitter = contentObject.AddComponent<ContentSizeFitter>();
            contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            scrollRect.content = contentObject.GetComponent<RectTransform>();
            scrollRect.content.anchorMin = new Vector2(0, 0);
            scrollRect.content.anchorMax = new Vector2(1, 1);
            scrollRect.content.offsetMin = new Vector2(0, 0);
            scrollRect.content.offsetMax = new Vector2(0, 0);
            scrollRect.scrollSensitivity = 30f;

            GameObject verticalScrollbarObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollViewVerticalScrollbar");
            verticalScrollbarObject.transform.SetParent(Obj.transform, false);

            scrollRect.verticalScrollbar = verticalBar(verticalScrollbarObject);
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;

            GameObject horizontalScrollbarObject = new GameObject(Meta.PluginName + "_" + Meta.Name + "_" + "ScrollViewHorizontalScrollbar");
            horizontalScrollbarObject.transform.SetParent(Obj.transform, false);
            scrollRect.horizontalScrollbar = horizontalBar(horizontalScrollbarObject);
            scrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            return contentObject;
        }
        Scrollbar verticalBar(GameObject parent)
        {
            Scrollbar scrollbar = parent.AddComponent<Scrollbar>();
            RectTransform scrollbarRect = parent.GetComponent<RectTransform>();

            scrollbarRect.anchorMin = new Vector2(1, 0);
            scrollbarRect.anchorMax = new Vector2(1, 1);
            scrollbarRect.pivot = new Vector2(1, 0.5f); 
            scrollbarRect.offsetMin = new Vector2(-10, 0); 
            scrollbarRect.offsetMax = new Vector2(0, 0);  
            scrollbarRect.sizeDelta = new Vector2(10, 0); 
                                                          
            GameObject handleObject = new GameObject("Handle");
            handleObject.transform.SetParent(parent.transform, false);

            Image handleImage = handleObject.AddComponent<Image>();
            handleImage.color = Meta.PanelColor; 

            scrollbar.handleRect = handleObject.GetComponent<RectTransform>();
            scrollbar.handleRect.anchorMin = new Vector2(0, 0);
            scrollbar.handleRect.anchorMax = new Vector2(1, 1);
            scrollbar.handleRect.offsetMin = new Vector2(0, 10);
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
            scrollbarRect.pivot = new Vector2(1, 0.5f); 
            scrollbarRect.offsetMin = new Vector2(0, 10);
            scrollbarRect.offsetMax = new Vector2(0, 0);  
            scrollbarRect.sizeDelta = new Vector2(0, 10);

            GameObject handleObject = new GameObject("Handle");
            handleObject.transform.SetParent(parent.transform, false);

            Image handleImage = handleObject.AddComponent<Image>();
            handleImage.color = Meta.PanelColor;

            scrollbar.handleRect = handleObject.GetComponent<RectTransform>();
            scrollbar.handleRect.anchorMin = new Vector2(0, 0);
            scrollbar.handleRect.anchorMax = new Vector2(1, 1);
            scrollbar.handleRect.offsetMin = new Vector2(-10, 0);
            scrollbar.handleRect.offsetMax = new Vector2(-10, 0);
            scrollbar.direction = Scrollbar.Direction.LeftToRight;
            return scrollbar;
        }
    }

}