using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGuiColorBrick : MoGuiControl
    {
        public MoGuiTxt Text;
        Image Brick;

        public Func<Color> BoundValue;
        public Color _value;

        public Color Value
        {
            get
            {
                if (BoundValue != null)
                {
                    return BoundValue();
                }
                else
                {
                    return _value;
                }
            }
            set
            {
                _value = value;
            }
        }

        public MoGuiColorBrick(MoGuiMeta meta, string name, MoCaColor args) : base(meta, name, args)
        {
            BoundValue = args.Value;
            if (args.Text != null) { OnUpdateAction = args.Text; }
            Init();




        }


        void Init()
        {
            if(OnUpdateAction != null)
            {
                
                switch (LabelPlacement ?? Meta.ColorBlock.labelPlacement)
                {
                    case ControlLabelPlacement.before:
                        Container = CreateContainer(Orientation ?? Meta.ColorBlock.orientation);
                        AddText("ColorBrickTxt", OnUpdateAction);
                        Obj = CreateBrick();
                        Obj.transform.SetParent(Container.transform, false);
                        break;
                    case ControlLabelPlacement.after:
                        Container = CreateContainer(Orientation ?? Meta.ColorBlock.orientation);
                        Obj = CreateBrick();
                        Obj.transform.SetParent(Container.transform, false);
                        AddText("ColorBrickTxt", OnUpdateAction);
                        break;
                    default:
                        Obj = CreateBrick();
                        AddText("ColorBrickTxt", OnUpdateAction);
                        break;
                }
            } 
            else
            {
                Obj = CreateBrick();
            }

                
        }

        public GameObject CreateBrick()
        {
            GameObject buttonObject = new GameObject(PluginName + "_" + Name + "_" + "ColorBrick");

            Brick = buttonObject.AddComponent<Image>();
            Brick.color = Value;
            AddLayoutElement(buttonObject);
            SetLayout();
            return buttonObject;
        }

        public void AddText(string label, object text)
        {
            if (Text != null)
            {
                Text.Update(text);
                Text.Obj.transform.SetParent(Container!=null ? Container.transform : Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, text:text, Meta.ColorBlock.labelSettings);
                Text.Obj.transform.SetParent(Container != null ? Container.transform : Obj.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            }

        }

        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (Text != null)
            {
                Text.Update(onUpdateAction());

                Text.Obj.transform.SetParent(Container != null ? Container.transform : Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction, Meta.ColorBlock.labelSettings);
                Text.Obj.transform.SetParent(Container != null ? Container.transform : Obj.transform, false);
            }

        }

        public override void SetLayout()
        {
            minWidth = Meta.ColorBlock.sizing.minWidth;
            minHeight = Meta.ColorBlock.sizing.minHeight;
            if (Meta.ColorBlock.sizing.preferredWidth != null) { preferredWidth = (float)Meta.ColorBlock.sizing.preferredWidth; }
            if (Meta.ColorBlock.sizing.preferredHeight != null) { preferredHeight = (float)Meta.ColorBlock.sizing.preferredHeight; }
            flexibleWidth = Meta.ColorBlock.sizing.flexibleWidth ?? 0;
            flexibleHeight = Meta.ColorBlock.sizing.flexibleHeight ?? 0;
        }
        public override void Update() 
        {
            Brick.color = Value;
        }
    }
}
