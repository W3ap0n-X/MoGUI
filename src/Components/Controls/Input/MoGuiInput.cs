using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MoGUI
{
    public class MoGuiInput : MoGuiControl
    {
        MoGuiTxt Text;
        Text InputText;
        Text PlaceHolderText;
        InputField Input;
        public object Value;
        string Type;

        

        public MoGuiInput(MoGuiMeta meta, string name, Func<object> text, Func<object> onUpdateAction, Action<object> onEditAction, string type) : base(meta, name)
        {
            OnUpdateAction = onUpdateAction;
            Type = type;
            OnEditAction = onEditAction;
            switch (Meta.InputLabelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("InputTxt", text);
                    Obj = CreateInput();
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateInput();
                    AddText("InputTxt", text);
                    break;
                default:
                    Obj = CreateInput();
                    break;
            }

        }

        public MoGuiInput(MoGuiMeta meta, string name, object text, Func<object> onUpdateAction, Action<object> onEditAction, string type) : base(meta, name)
        {
            OnUpdateAction = onUpdateAction;
            OnEditAction = onEditAction;

            Type = type;
            switch (Meta.InputLabelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("InputTxt", text);
                    Obj = CreateInput();
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateInput();
                    AddText("InputTxt", text);
                    break;
                default:
                    Obj = CreateInput();
                    break;
            }
        }

        public override void _Init()
        {
            Container = CreateContainer(Meta.InputOrientation);
        }

        public InputField.CharacterValidation getTypeValidation(string type)
        {
            InputField.CharacterValidation Validator;
            switch (type)
            {
                case "long":
                case "int":
                    Validator = InputField.CharacterValidation.Integer;
                    break;
                case "float":
                    Validator = InputField.CharacterValidation.Decimal;
                    break;

                default:
                    Validator = InputField.CharacterValidation.None;
                    break;
            }

            return Validator;
        }

        public InputField.ContentType getType(string type)
        {
            InputField.ContentType Validator;
            switch (type)
            {
                case "long":
                case "int":
                    Validator = InputField.ContentType.IntegerNumber;
                    break;
                case "float":
                    Validator = InputField.ContentType.DecimalNumber;
                    break;

                default:
                    Validator = InputField.ContentType.Standard;
                    break;
            }

            return Validator;
        }

        

        public GameObject CreateInput()
        {
            GameObject inputObject = new GameObject(PluginName + "_" + Name + "_" + "InputField");
            inputObject.transform.SetParent(Container.transform, false);

            LayoutElement layoutElement = inputObject.AddComponent<LayoutElement>();
            layoutElement.minWidth = Meta.InputSize.x;
            layoutElement.minHeight = Meta.InputSize.y;
            layoutElement.preferredWidth = Meta.InputSize.z;
            layoutElement.preferredHeight = Meta.InputSize.w;


            RectTransform inputRect = inputObject.GetComponent<RectTransform>();

            inputObject.AddComponent<Image>().color = Meta.InputColor;

            GameObject textObject = CreateInputText();
            textObject.transform.SetParent(inputObject.transform, false);

            GameObject placeholderObject = CreatePlaceholder();
            placeholderObject.transform.SetParent(inputObject.transform, false);

            Input = inputObject.AddComponent<InputField>();
            Input.textComponent = InputText;
            Input.placeholder = PlaceHolderText;
            Input.characterValidation = getTypeValidation(Type);
            Input.contentType = getType(Type);
            Input.onEndEdit.AddListener((value) => _OnEditAction( value));

            return inputObject;
        }

        public GameObject CreatePlaceholder()
        {
            GameObject placeholderObject = new GameObject(PluginName + "_" + Name + "_" + "InputFieldPlaceholder");
            RectTransform placeholderRect = placeholderObject.AddComponent<RectTransform>();
            placeholderRect.anchorMin = Vector2.zero;
            placeholderRect.anchorMax = Vector2.one;
            placeholderRect.offsetMin = new Vector2(10, 5);
            placeholderRect.offsetMax = new Vector2(-10, -5);

            PlaceHolderText = placeholderObject.AddComponent<Text>();
            PlaceHolderText.alignment = TextAnchor.MiddleLeft;
            PlaceHolderText.text = "Enter Value...";
            PlaceHolderText.color = Meta.InputPlaceholderFontColor;
            PlaceHolderText.fontStyle = FontStyle.Italic;
            PlaceHolderText.fontSize = Meta.InputFontSize;
            PlaceHolderText.font = Meta.InputFont;

            return placeholderObject;
        }

        public GameObject CreateInputText()
        {
            GameObject textObject = new GameObject(PluginName + "_" + Name + "_" + "InputFieldText");
            RectTransform textRect = textObject.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(10, 5);
            textRect.offsetMax = new Vector2(-10, -5);
            InputText = textObject.AddComponent<Text>();
            InputText.alignment = TextAnchor.MiddleLeft;
            InputText.text = OnUpdateAction().ToString() ?? null;
            InputText.color = Meta.InputFontColor;
            InputText.fontSize = Meta.InputFontSize;
            InputText.font = Meta.InputFont;

            return textObject;
        }

        public void AddText(string label, object text)
        {
            if (Text != null)
            {
                Text.Update(text);
                Text.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, text);
                Text.Obj.transform.SetParent(Container.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }
        public void AddText(string label, Func<object> onUpdateAction)
        {
            if (Text != null)
            {
                Text.Update(onUpdateAction);
                Text.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction);
                Text.Obj.transform.SetParent(Container.transform, false);
                Text.Obj.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            }

        }


        public void _OnEditAction(object value)
        {
            switch (Type)
            {
                case "int":
                    if (int.TryParse(value.ToString(), out int intVal))
                    {
                        OnEditAction(intVal);
                    }
                    break;

                case "float":
                    if (float.TryParse(value.ToString(), out float floatVal))
                    {
                        OnEditAction(floatVal);
                    }
                    break;

                case "long":
                    if (long.TryParse(value.ToString(), out long longVal))
                    {
                        OnEditAction(longVal);
                    }
                    break;

                default:
                    OnEditAction(value);
                    break;
            }

        }
        public override void Update()
        {
            Text.Update();
            if (!Input.isFocused)
            {

                if (OnUpdateAction != null)
                {
                    Value = Input.text = InputText.text = OnUpdateAction().ToString();
                }
                
            }
            else
            {
                Value = InputText.text;
            }
                
        }
    }

    public class MoCaInput : MoGCArgs
    {

        public MoCaInput(Action<object> onEditAction,
            Func<object> onUpdateAction,
            Func<object> text,
            string valType = "none",
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiInput), meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {

        }

        public MoCaInput(Action<object> onEditAction,
            Func<object> onUpdateAction,
            object text = null,
            string valType = "none",
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiInput), meta, text: text, onEditAction: onEditAction, onUpdateAction: onUpdateAction, valType: valType)
        {

        }

    }
}
