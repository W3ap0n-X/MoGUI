using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class ColorWrapper
    {
        public Color Color;

        public ColorWrapper(Color color)
        {
            this.Color = color;
        }

        public float R 
        {
            get => Color.r;  
            set { Color.r = value; }
        }
        public float G
        {
            get => Color.g;
            set { Color.g = value; }
        }
        public float B
        {
            get => Color.b;
            set { Color.b = value; }
        }
        public float A
        {
            get => Color.a;
            set { Color.a = value; }
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

    public enum TextElement
    {
        text,
        title,
        h1,
        h2,
        h3,
        h4,
        h5,
        label,
        small
    }

    public enum SliderDirection
    {
        horizontal, 
        vertical
    }

    public enum PanelStartingState
    {
        show,
        minimize,
        hide
    }

    public enum TextStyle
    {
        none,
        bold,
        italic,
        underline
    }

    //public enum ToggleType
    //{
    //    checkbox,
    //    button
    //}

    public enum ThemeKey
    {
        none,
        primary,
        secondary,
        
    }

}
