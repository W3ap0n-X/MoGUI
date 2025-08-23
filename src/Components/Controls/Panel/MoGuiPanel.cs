using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml.Linq;

namespace MoGUI
{
    public class MoGuiPanel : MoGuiControl
    {
        MoGuiHeader Header;
        public MoGuiPanel Panel;
        public GameObject Content;

        public Dictionary<string, MoGuiControl> Components = new Dictionary<string, MoGuiControl>();

        public Dictionary<string, MiniUIRow> Rows = new Dictionary<string, MiniUIRow>();

        public MoGuiPanel(MoGuiMeta meta,  string name, GameObject canvas, Vector2 size, Vector2 pos) : base(meta, name,size, pos)
        {
            Init( canvas,  name,  size,  pos, true);
        }

        public MoGuiPanel(MoGuiMeta meta, string name, Vector2 size, Vector2 pos, bool topLevel = false) : base(meta, name, size, pos)
        {
            Init( name, size, pos, topLevel);
        }

        public MoGuiPanel(MoGuiMeta meta, string name, MoGuiPanel owner, bool includeHeader = false) : base(meta, name)
        {
            Panel = owner;
            Init(includeHeader);
        }

        public virtual void Init(GameObject canvas, string name, Vector2 size, Vector2 pos, bool topLevel = false)
        {
            Obj = CreatePanel();
            
            
            if (topLevel)
            {

                RectTransform layoutRect = Container.GetComponent<RectTransform>();

                layoutRect.anchorMin = new Vector2(0, 0);
                layoutRect.anchorMax = new Vector2(1, 1);
                layoutRect.offsetMin = new Vector2(0, 0);
                layoutRect.offsetMax = new Vector2(0, -Meta.HeaderSize);


                Header = new MoGuiHeader(Meta, canvas, this, name, topLevel);
                Header.Obj.transform.SetParent(Obj.transform, false);
                Container.transform.SetParent(Obj.transform, false);
                
                MoGuiScrollArea scrollArea = new MoGuiScrollArea(Meta);
                scrollArea.Obj.transform.SetParent(Container.transform, false);


                Content = scrollArea.Content;
            }
            else
            {
                Container.transform.SetParent(Obj.transform, false);
                Content = Container;
            }
            
            is_init = true;
        }

        public virtual void Init( string name, Vector2 size, Vector2 pos, bool topLevel = false)
        {
            Obj = CreatePanel();


            if (topLevel)
            {

                RectTransform layoutRect = Container.GetComponent<RectTransform>();

                layoutRect.anchorMin = new Vector2(0, 0);
                layoutRect.anchorMax = new Vector2(1, 1);
                layoutRect.offsetMin = new Vector2(0, 0);
                layoutRect.offsetMax = new Vector2(0, -Meta.HeaderSize);


                Header = new MoGuiHeader(Meta, Obj, this, name, topLevel);
                Header.Obj.transform.SetParent(Obj.transform, false);
                Container.transform.SetParent(Obj.transform, false);

                MoGuiScrollArea scrollArea = new MoGuiScrollArea(Meta);
                scrollArea.Obj.transform.SetParent(Container.transform, false);


                Content = scrollArea.Content;
            }
            else 
            {
                Container.transform.SetParent(Obj.transform, false);
                Content = Container;
            }
                
            is_init = true;
        }

        public virtual void Init()
        {
            Obj = CreatePanel();
            Container.transform.SetParent(Obj.transform, false);
            Content = Container;
            is_init = true;
        }
        public virtual void Init(bool includeHeader)
        {
            Obj = CreatePanel();
            

            if (includeHeader)
            {
                

                RectTransform layoutRect = Container.GetComponent<RectTransform>();

                layoutRect.anchorMin = new Vector2(0, 0);
                layoutRect.anchorMax = new Vector2(1, 1);
                layoutRect.offsetMin = new Vector2(0, 0);
                layoutRect.offsetMax = new Vector2(0, -Meta.HeaderSize);

                RectTransform panelRect = Obj.GetComponent<RectTransform>();
                panelRect.anchorMin = new Vector2(0, 0);
                panelRect.anchorMax = new Vector2(1, 1);
                panelRect.offsetMin = new Vector2(0, 0);
                panelRect.offsetMax = new Vector2(0, 0);

                Header = new MoGuiHeader(Meta, Obj, this, Name, false);
                Header.Obj.transform.SetParent(Obj.transform, false);
            }
            VerticalLayoutGroup layoutGroup = Obj.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;
            Container.transform.SetParent(Obj.transform, false);
            Content = Container;
            is_init = true;
        }

        public override GameObject CreateContainer()
        {

            



            GameObject layoutObject = new GameObject(PluginName + "_" + Name + "_" + "DrawContainer");
            VerticalLayoutGroup layoutGroup = layoutObject.AddComponent<VerticalLayoutGroup>();

            Image panelImage = layoutObject.AddComponent<Image>();
            panelImage.color = Meta.PanelColor;

            RectTransform layoutRect = layoutObject.GetComponent<RectTransform>();

            layoutRect.anchorMin = new Vector2(0, 0);
            layoutRect.anchorMax = new Vector2(1, 1);
            layoutRect.offsetMin = new Vector2(0, 0);
            layoutRect.offsetMax = new Vector2(0, 0);
            layoutGroup.spacing = Meta.TxtMargin;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childForceExpandHeight = true;
            return layoutObject;
        }

        public bool IsActive
        {
            get => Obj.activeSelf;
        }

        public bool Minimized
        {
            get => Container.activeSelf ? false : true;
        }

        public void ToggleGui(bool show)
        {
            Obj.SetActive(show);
        }

        public void ToggleMinGui(bool show)
        {
            Obj.SetActive(show);
        }

        public virtual GameObject CreatePanel()
        {
            var panelObject = new GameObject(PluginName + "_" + Name + "_" + "Panel");

            RectTransform panelRect = panelObject.AddComponent<RectTransform>();
            panelRect.sizeDelta = Size;
            panelRect.anchoredPosition = Pos;
            return panelObject;
        }

        

        public GameObject CreateRow(string name)
        {
            GameObject layoutObject = new GameObject(PluginName + "_" + Name + "_" + "Row_" + name);
            HorizontalLayoutGroup layoutGroup = layoutObject.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.padding = new RectOffset(Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin);

            layoutGroup.spacing = Meta.TxtMargin;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
            return layoutObject;
        }

        public void AddRow(string name)
        {
            Rows.Add(name, new MiniUIRow(this, name));
        }

        public GameObject CreateColumn( string row, string name)
        {
            GameObject layoutObject = new GameObject(PluginName + "_" + Name + "_" + row + "_" + "Column_" + name);
            VerticalLayoutGroup layoutGroup = layoutObject.AddComponent<VerticalLayoutGroup>();
            layoutGroup.padding = new RectOffset(Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin);
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.spacing = Meta.TxtMargin;
            return layoutObject;
        }

        public MiniUIRow GetRow(string rowName)
        {
            MiniUIRow row;
            if (Rows.ContainsKey(rowName))
            {
                row = Rows[rowName];
            }
            else
            {
                AddRow(rowName);
                row = Rows[rowName];
            }
            return row;
        }

        public GameObject GetCol(string rowName, string columnName)
        {
            MiniUIRow row = GetRow(rowName);
            GameObject column;
            if (row.Columns.ContainsKey(columnName))
            {
                column = row.Columns[columnName];
            }
            else
            {
                row.AddColumn(columnName);
                column = row.Columns[columnName];
            }
            return column;
        }


        public MoGuiControl AddControl( string row, string col,  string name, MoGCArgs args)
        {

            GameObject column = GetCol(row, col);

            MoGuiControl newComponent;
            if (Components.ContainsKey(name))
            {
                newComponent = Components[name];
                newComponent.Container.transform.SetParent(column.transform, false);
            }
            else
            {
                newComponent = CreateComponent( name, args);
                if (args.Type == typeof(MoGuiPanel))
                {
                    newComponent.Obj.transform.SetParent(column.transform, false);
                } else
                {
                    newComponent.Container.transform.SetParent(column.transform, false);
                }
                    
                Components.Add(name, newComponent);
            }
            return newComponent;
        }

        private MoGuiControl CreateComponent(string name, MoGCArgs args)
        {
            if (args.Type == typeof(MoGuiButton))
            {
                MoCaButton _args = (MoCaButton)args;
                return new MoGuiButton(_args.Meta ?? Meta, Name + "_" + name, _args.Text, _args.OnClickAction);
            }
            else if (args.Type == typeof(MoGuiToggle))
            {
                MoCaToggle _args = (MoCaToggle)args;
                if(_args.boundValue != null)
                {
                    return new MoGuiToggle(_args.Meta ?? Meta, Name + "_" + name, _args.boundValue, _args.Text, _args.OnClickAction);
                } else
                {
                    return new MoGuiToggle(_args.Meta ?? Meta, Name + "_" + name, _args._value, _args.Text, _args.OnClickAction);
                }
                
            }
            else if (args.Type == typeof(MoGuiInput))
            {
                MoCaInput _args = (MoCaInput)args;
                return new MoGuiInput(_args.Meta ?? Meta, Name + "_" + name, _args.Text ,  _args.OnUpdateAction, _args.OnEditAction, _args.ValType);
            }
            else if (args.Type == typeof(MoGuiSlider))
            {
                MoCaSlider _args = (MoCaSlider)args;
                return new MoGuiSlider(_args.Meta ?? Meta, Name + "_" + name, _args.Text, _args.Range, _args.OnUpdateAction, _args.OnEditAction, _args.ValType, _args.BoundMin, _args.BoundMax);
            }
            else if (args.Type == typeof(MoGuiDDL))
            {
                MoCaDDL _args = (MoCaDDL)args;
                if (_args.DDLBoundOptions != null)
                {
                    return new MoGuiDDL(_args.Meta ?? Meta, Name + "_" + name,  _args.DDLBoundOptions , _args.OnEditAction, _args.ValType);
                }
                else
                {
                    return new MoGuiDDL(_args.Meta ?? Meta, Name + "_" + name, _args.DDLOptions, _args.OnEditAction, _args.ValType);
                }
                
            }
            else if(args.Type == typeof(MoGuiTxt))
            {
                MoCaText _args = (MoCaText)args;
                if(_args.OnUpdateAction != null)
                {
                    return new MoGuiTxt(_args.Meta ?? Meta, Name + "_" + name, _args.OnUpdateAction);
                } else
                {
                    return new MoGuiTxt(_args.Meta ?? Meta, Name + "_" + name, _args.Text);
                }
                
            }
            else if (args.Type == typeof(MoGuiPanel))
            {
                MoCaPanel _args = (MoCaPanel)args;
                return new MoGuiPanel(_args.Meta ?? Meta, Name + "_" + name, this, _args.IncludeHeader);
            }
            else
            {
                MoCaText _args = (MoCaText)args;
                return  new MoGuiTxt(_args.Meta ?? Meta, Name + "_" + name, _args.Text);
            }
        }

        public override void Update()
        {
            if (IsActive && !Minimized)
            {
                foreach (var item in Components)
                {
                    item.Value.Update();
                }
            }

        }


    }

    public class MiniUIRow
    {
        public Dictionary<string, GameObject> Columns;
        public GameObject RowContainer;
        private MoGuiPanel _parent;
        public string Name { get; private set; }
        public MiniUIRow(MoGuiPanel parent, string name)
        {
            _parent = parent;
            Name = name;
            Columns = new Dictionary<string, GameObject>();
            RowContainer = CreateRow();
        }

        private GameObject CreateRow()
        {
            GameObject rowObj = _parent.CreateRow(Name);
            rowObj.transform.SetParent(_parent.Content.transform);
            return rowObj;
        }

        private GameObject CreateColumn(string name)
        {
            GameObject newCol = _parent.CreateColumn(Name,name);
            newCol.transform.SetParent(RowContainer.transform);
            return newCol;
        }


        public void AddColumn( string name)
        {
            
            Columns.Add(name, CreateColumn(name));
        }

        public GameObject GetCol( string columnName)
        {
            GameObject column;
            if (Columns.ContainsKey(columnName))
            {
                column = Columns[columnName];
            }
            else
            {
                AddColumn(columnName);
                column = Columns[columnName];
            }
            return column;
        }

    }


    public class MoGuiHeader : MoGuiPanel
    {
        private GameObject Canvas;
        private DraggableHandle DragHandle;


        public MoGuiHeader(MoGuiMeta meta, GameObject canvas, MoGuiPanel panel, string name, bool topLevel) : base(meta, name, panel, topLevel)
        {
            
            Canvas = canvas;
            
        }


        public override void Init(bool topLevel)
        {
            Meta.PanelColor = Meta.HeaderColor;

            Meta.FontSize = Meta.HeaderFontSize;
            Meta.ButtonFontSize = Meta.HeaderExitFontSize;
            Obj = CreatePanel();
            Container.transform.SetParent(Obj.transform, false);
            if (topLevel)
            {
                DragHandle = Obj.AddComponent<DraggableHandle>();
                DragHandle.BindParent(Panel.Obj);
                AddTitleText("HeaderText", PluginName + " - " + Name);
                AddXButton("MinGui", "␣", () => MinGui());
                Meta.ButtonColor = Meta.HeaderExitColor;
                AddXButton("HideGui", "╳", () => ToggleGui(false));
                AddResize();
            } else
            {
                AddTitleText("HeaderText", Name);
                AddXButton("MinGui", "␣", () => MinGui());
            }
                is_init = true;
        }


        public override GameObject CreatePanel()
        {
            var panelObject = new GameObject(PluginName + "_" + Name + "_" + "HeaderPanel");

            Image panelImage = panelObject.AddComponent<Image>();
            panelImage.color = Meta.HeaderColor;
            RectTransform panelRect = panelObject.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 1);
            panelRect.anchorMax = new Vector2(1, 1);
            panelRect.offsetMin = new Vector2(0, -Meta.HeaderSize);
            panelRect.offsetMax = new Vector2(0, 0);

            LayoutElement panelSizer = panelObject.AddComponent<LayoutElement>();
            panelSizer.minHeight = Meta.HeaderSize;
            panelSizer.minWidth = 100;
            panelSizer.preferredWidth = 250;

            return panelObject;
        }

        public override GameObject CreateContainer()
        {

            GameObject layoutObject = new GameObject(PluginName + "_" + Name + "_" + "HeaderContainer");

            HorizontalLayoutGroup headerLayoutGroup = layoutObject.AddComponent<HorizontalLayoutGroup>();
            headerLayoutGroup.childControlWidth = true;
            headerLayoutGroup.childControlHeight = true;
            headerLayoutGroup.childForceExpandHeight = true;
            headerLayoutGroup.childForceExpandWidth = false;


            RectTransform layoutRect = layoutObject.GetComponent<RectTransform>();
            layoutRect.anchorMin = Vector2.zero;
            layoutRect.anchorMax = Vector2.one;
            layoutRect.offsetMin = new Vector2(0, 0);
            layoutRect.offsetMax = new Vector2(0, 0);
            return layoutObject;

        }

        new public void ToggleGui(bool show)
        {
            Canvas.SetActive(show);
        }

        public void MinGui()
        {
            Panel.Container.SetActive(Panel.Container.activeSelf ? false : true);
        }

        public void AddXButton(string label, string text, Action onUpdateAction)
        {
            MoGuiButton newTxt;
            if (Components.ContainsKey(label))
            {
                newTxt = (MoGuiButton)Components[label];

                newTxt.Container.transform.SetParent(Container.transform, false);
            }
            else
            {
                newTxt = new MoGuiButton(Meta, Name + "_" + label, text, onUpdateAction);
                newTxt.Container.transform.SetParent(Container.transform, false);

                LayoutElement buttonContainerLayoutElement = newTxt.Container.AddComponent<LayoutElement>();
                buttonContainerLayoutElement.minWidth = Meta.HeaderSize;
                buttonContainerLayoutElement.minHeight = Meta.HeaderSize;
                buttonContainerLayoutElement.flexibleWidth = 0.1f;
                LayoutElement buttonLayoutElement = newTxt.Container.GetComponent<LayoutElement>();
                buttonLayoutElement.minWidth = Meta.HeaderSize - Meta.TxtMargin;
                buttonLayoutElement.minHeight = Meta.HeaderSize - Meta.TxtMargin;
                buttonLayoutElement.flexibleWidth = 0.1f;
                Components.Add(label, newTxt);

            }

        }

        public void AddTitleText(string label, object text)
        {
            MoGuiTxt newTxt;
            if (Components.ContainsKey(label))
            {
                newTxt = (MoGuiTxt)Components[label];
                newTxt.Update(text);
                newTxt.Container.transform.SetParent(Container.transform, false);
            }
            else
            {
                newTxt = new MoGuiTxt(Meta, Name + "_" + label, text);
                newTxt.Container.transform.SetParent(Container.transform, false);
                newTxt.Text.alignment = TextAnchor.MiddleLeft;
                LayoutElement titleLayoutElement = newTxt.Container.AddComponent<LayoutElement>();
                titleLayoutElement.flexibleWidth = 1;
                Components.Add(label, newTxt);
            }

        }


        public void AddResize()
        {
            GameObject resizeIconObject = new GameObject("ResizeIcon");
            resizeIconObject.transform.SetParent(Container.transform, false);
            

            LayoutElement resizeLayoutElement = resizeIconObject.AddComponent<LayoutElement>();
            
            resizeLayoutElement.minWidth = Meta.HeaderSize;
            resizeLayoutElement.minHeight = Meta.HeaderSize;

            Text dragSymbol = resizeIconObject.AddComponent<Text>();
            dragSymbol.text = "❏";
            dragSymbol.font = Meta.Font;
            dragSymbol.fontSize = Meta.HeaderExitFontSize;
            dragSymbol.color = Meta.FontColor;
            dragSymbol.alignment = TextAnchor.MiddleCenter;

            ResizableUI resizeHandle = resizeIconObject.AddComponent<ResizableUI>();
            resizeHandle.BindParent(Panel.Obj);
        }

    }

    public class MoCaPanel : MoGCArgs
    {
        public bool IncludeHeader;
        public MoCaPanel(bool includeHeader = false,
            MoGuiMeta meta = null
        ) : base(typeof(MoGuiPanel), meta)
        {
            IncludeHeader = includeHeader;
        }
    }

}
