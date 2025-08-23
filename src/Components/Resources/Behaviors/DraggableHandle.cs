using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{

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
    

}
