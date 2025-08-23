using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

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

    


    


    

}
