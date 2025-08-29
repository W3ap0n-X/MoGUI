using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{


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
