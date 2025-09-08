using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace MoGUI
{

    public class MoCaText : MoGCArgs
    {
        public new string Text;
        public TextElement Element;
        public TypographySettings? Settings;

        public MoCaText(Func<object> onUpdateAction,
            TextElement element = TextElement.text,
            TypographySettings? settings = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta)
        {
            Element = element;
            OnUpdateAction = onUpdateAction;
            Settings = settings;

        }

        public MoCaText(string text,
            TextElement element = TextElement.text,
            TypographySettings? settings = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiTxt), meta)
        {
            Element = element;
            Text = text;
            Settings = settings;

        }
    }

    
}
