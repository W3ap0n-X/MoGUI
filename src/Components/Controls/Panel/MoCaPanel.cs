using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace MoGUI
{

    public class MoCaPanel : MoGCArgs
    {
        public bool IncludeHeader;
        public string Title;
        public MoCaPanel(bool includeHeader = false,
            string title = null,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiPanel), meta)
        {
            IncludeHeader = includeHeader;
            if (title != null)
            {
                Title = title;
            }
        }
    }
  

    

}
