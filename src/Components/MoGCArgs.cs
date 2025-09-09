using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace MoGUI
{

    public abstract class MoGCArgs
    {
        public Action OnClickAction;
        public Func<object> OnUpdateAction;
        public Action<object> OnEditAction;
        public Func<object> Text;
        public string ValType;
        public ControlOrientation? Orientation;
        public ControlLabelPlacement? LabelPlacement;
        public Type Type;
        public MoGuiMeta Meta;


        public MoGCArgs(Type type,
            MoGuiMeta meta = null,
            Func<object> value = null,
             Action onClickAction = null,
             Func<object> onUpdateAction = null,
             Action<object> onEditAction = null,
             Func<object> text = null,
             ControlLabelPlacement? labelPlacement = null,
             ControlOrientation? orientation = null,
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
            LabelPlacement = labelPlacement;
            Orientation = orientation;
        }

        public MoGCArgs(Type type, object value = null,
             Action onClickAction = null,
             Func<object> onUpdateAction = null,
             Action<object> onEditAction = null,
             object text = null,
             string valType = null,
             ControlLabelPlacement? labelPlacement = null,
             ControlOrientation? orientation = null,
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
            LabelPlacement = labelPlacement;
            Orientation = orientation;
        }

        public Func<object> ConvertString(object obj)
        {
            return () => obj;
        }
    }

    public class ControlMeta : BlockMeta
    {

        public ControlOrientation orientation = ControlOrientation.horizontal;
        public ControlLabelPlacement labelPlacement = ControlLabelPlacement.before;
        public ControlMeta(MoGuiMeta parent, string name) : base(parent, name) { }
        


        public ControlMeta Orientation(ControlOrientation controlOrientation)
        {
            orientation = controlOrientation;
            return this;
        }

        public ControlMeta Orientation(string controlOrientation)
        {
            if(controlOrientation == "vertical")
            {
                orientation = ControlOrientation.vertical;
            } else
            {
                orientation = ControlOrientation.horizontal;
            }

                return this;
        }

        public ControlMeta LabelPlacement(ControlLabelPlacement placement)
        {
            labelPlacement = placement;
            return this;
        }

        public ControlMeta LabelPlacement(string placement)
        {
            if (placement == "after")
            {
                labelPlacement = ControlLabelPlacement.after;
            }
            else if( placement == "none")
            {
                labelPlacement = ControlLabelPlacement.none;
            }
            else
            {
                labelPlacement = ControlLabelPlacement.before;
            }

            return this;
        }

    }

}
