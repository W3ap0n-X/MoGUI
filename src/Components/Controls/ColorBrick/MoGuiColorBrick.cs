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

        public MoGuiColorBrick(MoGuiMeta meta, string name, MoCaColor args) : base(meta, name)
        {
            BoundValue = args.Value;
            Meta.FontSize = Meta.ButtonFontSize;
            Obj = CreateBrick();
            
        }


        //public override void _Init()
        //{
        //    Container = CreateContainer();
        //}
        public GameObject CreateBrick()
        {
            GameObject buttonObject = new GameObject(PluginName + "_" + Name + "_" + "Button");

            Brick = buttonObject.AddComponent<Image>();
            Brick.color = Value;

            LayoutElement layoutElement = buttonObject.AddComponent<LayoutElement>();
            layoutElement.minWidth = Meta.ButtonSize.x;
            layoutElement.minHeight = Meta.ButtonSize.y;
            layoutElement.preferredWidth = Meta.ButtonSize.z;
            layoutElement.preferredHeight = Meta.ButtonSize.w;
            layoutElement.flexibleWidth = 1;
            layoutElement.flexibleHeight = 1;


            //buttonObject.transform.SetParent(Container.transform, false);
            return buttonObject;
        }

        public void AddText(string label, object text)
        {
            if (Text != null)
            {
                Text.Update(text);
                Text.Obj.transform.SetParent(Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, text);
                Text.Obj.transform.SetParent(Obj.transform, false);
                //HorizontalLayoutGroup layoutGroup = Text.Container.GetComponent<HorizontalLayoutGroup>();
                //layoutGroup.childForceExpandHeight = true;
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            }

        }

        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (Text != null)
            {
                Text.Update(onUpdateAction());

                Text.Obj.transform.SetParent(Obj.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Text.Obj.transform.SetParent(Obj.transform, false);
                //HorizontalLayoutGroup layoutGroup = Text.Container.GetComponent<HorizontalLayoutGroup>();
                //layoutGroup.childForceExpandHeight = true;
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            }

        }

        public override void SetLayout()
        {

        }
        public override void Update() 
        {
            Brick.color = Value;
        }
    }

    public class MoCaColor : MoGCArgs
    {
        public Func<Color> Value;
        public MoCaColor(Color value,
            //Func<object> text,
            //Action onClickAction,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiColorBrick), meta)
        {
            Value = () => value;
        }

        public MoCaColor(Func<Color> value,
            //Func<object> text,
            //Action onClickAction,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiColorBrick), meta)
        {
            Value = value;
        }

    }
}
