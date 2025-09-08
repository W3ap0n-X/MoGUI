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


        public MoGuiInput(MoGuiMeta meta, string name, MoCaInput args) : base(meta, name, args)
        {
            if (args.OnUpdateAction != null)
            {
                OnUpdateAction = args.OnUpdateAction;
            }
            if (args.OnEditAction != null)
            {
                OnEditAction = args.OnEditAction;
            }
            Type = args.ValType;
            if (args.Value != null)
            {
                Value = args.Value;
            }
            if (args.OnUpdateAction != null)
            {
                Value = args.OnUpdateAction();
            }
            switch (LabelPlacement ?? Meta.Input.labelPlacement)
            {
                case ControlLabelPlacement.before:
                    AddText("InputTxt", args.Text);
                    Obj = CreateInput();
                    break;
                case ControlLabelPlacement.after:
                    Obj = CreateInput();
                    AddText("InputTxt", args.Text);
                    break;
                default:
                    Obj = CreateInput();
                    break;
            }
        }

        public override void SetLayout()
        {
            minWidth = Meta.Input.sizing.minWidth;
            minHeight = Meta.Input.sizing.minHeight;
            if (Meta.Input.sizing.preferredWidth != null) { preferredWidth = (float)Meta.Input.sizing.preferredWidth; }
            if (Meta.Input.sizing.preferredHeight != null) { preferredHeight = (float)Meta.Input.sizing.preferredHeight; }
            flexibleWidth = Meta.Input.sizing.flexibleWidth ?? 0;
            flexibleHeight = Meta.Input.sizing.flexibleHeight ?? 0;
        }

        

        public override void _Init()
        {
            Container = CreateContainer(Orientation ?? Meta.Input.orientation);
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

            AddLayoutElement(inputObject);
            SetLayout();
            RectTransform inputRect = inputObject.GetComponent<RectTransform>();

            inputObject.AddComponent<Image>().color = Meta.Input.background;

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
            placeholderRect.offsetMin = new Vector2(Meta.Margin * 2, Meta.Margin);
            placeholderRect.offsetMax = new Vector2(Meta.Margin * -2, Meta.Margin * -1);

            PlaceHolderText = placeholderObject.AddComponent<Text>();
            PlaceHolderText.alignment = Meta.Input.placeholderSettings.Alignment;

            PlaceHolderText.text = "Enter Value...";

            PlaceHolderText.color = Meta.Input.placeholderSettings.FontColor;
            PlaceHolderText.fontStyle = Meta.Input.placeholderSettings.Style;
            PlaceHolderText.fontSize = Meta.Input.placeholderSettings.FontSize;
            PlaceHolderText.font = Meta.Input.placeholderSettings.FontFace;

            return placeholderObject;
        }

        public GameObject CreateInputText()
        {
            GameObject textObject = new GameObject(PluginName + "_" + Name + "_" + "InputFieldText");
            RectTransform textRect = textObject.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(Meta.Margin *2, Meta.Margin);
            textRect.offsetMax = new Vector2(Meta.Margin * -2, Meta.Margin * -1);
            InputText = textObject.AddComponent<Text>();
            InputText.alignment = Meta.Input.inputSettings.Alignment;

            InputText.color = Meta.Input.inputSettings.FontColor;
            InputText.fontStyle = Meta.Input.inputSettings.Style;
            InputText.fontSize = Meta.Input.inputSettings.FontSize;
            InputText.font = Meta.Input.inputSettings.FontFace;

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
                Text = new MoGuiTxt(Meta, Name + "_" + label, text:text, Meta.Input.labelSettings);
                Text.Obj.transform.SetParent(Container.transform, false);
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
                Text = new MoGuiTxt(Meta, Name + "_" + label, onUpdateAction, Meta.Input.labelSettings);
                Text.Obj.transform.SetParent(Container.transform, false);
            }

        }


        public void _OnEditAction(object value)
        {
            Value = value;
            if (OnEditAction != null)
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
        }
        public override void Update()
        {
            if (Text != null)
            {
                Text.Update();
            }
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
}
