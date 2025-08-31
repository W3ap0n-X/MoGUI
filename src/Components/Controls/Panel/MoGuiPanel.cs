using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace MoGUI
{
    public class MoGuiPanel : MoGuiControl
    {
        MoGuiHeader Header;
        public MoGuiPanel Panel;
        public GameObject Content;
        public string Title = null;
        SizeWrapper ContainerSize;
        SizeWrapper PanelSize;

        public Dictionary<string, MoGuiControl> Components = new Dictionary<string, MoGuiControl>();

        public Dictionary<string, MoGuiRow> Rows = new Dictionary<string, MoGuiRow>();

        // Create RootPanel from Main Gui
        public MoGuiPanel(MoGuiMeta meta,  string name, GameObject canvas, Vector2 size, Vector2 pos) : base(meta, name,size, pos)
        {
            Title = name;
            Init( canvas,  name,  size,  pos);
        }

        // Create new toplevel Panel
        public MoGuiPanel(MoGuiMeta meta, string name, Vector2 size, Vector2 pos) : base(meta, name, size, pos)
        {
            Title = name;
            Init( name, size, pos);
        }

        // is only used by the header subclass 
        protected MoGuiPanel(MoGuiMeta meta, string name, MoGuiPanel owner, bool topLevel = false) : base(meta, name)
        {
            Panel = owner;
            Init(topLevel);
        }

        public MoGuiPanel(MoGuiMeta meta, string name, MoGuiPanel owner, MoCaPanel args) : base(meta, name)
        {
            Panel = owner;
            if (args.Title != null) 
            {
                Title = args.Title;
            }
            Init(args.IncludeHeader);
        }
        public override void _Init()
        {
            Container = CreateContainer();
        }

        public virtual void Init(GameObject canvas, string name, Vector2 size, Vector2 pos)
        {
            if(Obj == null)
            {
                Obj = CreatePanel();
            }

            ContainerSize.anchorMin = new Vector2(0, 0);
            ContainerSize.anchorMax = new Vector2(1, 1);
            ContainerSize.offsetMin = new Vector2(0, 0);
            ContainerSize.offsetMax = new Vector2(0, -Meta.HeaderSize);

            Header = new MoGuiHeader(Meta, canvas, this, name, true);
            Header.Obj.transform.SetParent(Obj.transform, false);
            Container.transform.SetParent(Obj.transform, false);

            MoGuiScrollArea scrollArea = new MoGuiScrollArea(Meta);
            scrollArea.Obj.transform.SetParent(Container.transform, false);

            Content = scrollArea.Content;

            PanelSize.pivot = new Vector2(0, 0);
            is_init = true;
        }

        public virtual void Init( string name, Vector2 size, Vector2 pos)
        {
            Obj = CreatePanel();
            Init(Obj, name, size, pos);
        }
        public virtual void Init(bool includeHeader)
        {
            Obj = CreatePanel();
            if (includeHeader)
            {

                ContainerSize.anchorMin = new Vector2(0, 0);
                ContainerSize.anchorMax = new Vector2(1, 1);
                ContainerSize.offsetMin = new Vector2(0, 0);
                ContainerSize.offsetMax = new Vector2(0, -Meta.HeaderSize);

                PanelSize.anchorMin = new Vector2(0, 0);
                PanelSize.anchorMax = new Vector2(1, 1);
                PanelSize.offsetMin = new Vector2(0, 0);
                PanelSize.offsetMax = new Vector2(0, 0);

                Header = new MoGuiHeader(Meta, Obj, this, Name, false);
                Header.Obj.transform.SetParent(Obj.transform, false);
            }
            VerticalLayoutGroup layoutGroup = Obj.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            Container.transform.SetParent(Obj.transform, false);
            Content = Container;
            is_init = true;
        }

        public override GameObject CreateContainer()
        {
            GameObject layoutObject = new GameObject(PluginName + "_" + Name + "_" + "DrawContainer");
            VerticalLayoutGroup layoutGroup = layoutObject.AddComponent<VerticalLayoutGroup>();


            AddLayoutElement(layoutObject);
            SetLayout();
            

            Image panelImage = layoutObject.AddComponent<Image>();
            panelImage.color = Meta.PanelColor.Base;



            ContainerSize = new SizeWrapper(layoutObject.GetComponent<RectTransform>());

            ContainerSize.anchorMin = new Vector2(0, 0);
            ContainerSize.anchorMax = new Vector2(1, 1);
            ContainerSize.offsetMin = new Vector2(0, 0);
            ContainerSize.offsetMax = new Vector2(0, 0);

            layoutGroup.spacing = Meta.TxtMargin;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;
            return layoutObject;
        }

        public void AddScrollArea()
        {
            MoGuiScrollArea scrollArea = new MoGuiScrollArea(Meta);
            scrollArea.Obj.transform.SetParent(Container.transform, false);

            Content = scrollArea.Content;
        }

        public virtual GameObject CreatePanel()
        {
            var panelObject = new GameObject(PluginName + "_" + Name + "_" + "Panel");

            PanelSize = new SizeWrapper(panelObject.AddComponent<RectTransform>());
            PanelSize.sizeDelta = Size;
            PanelSize.anchoredPosition = new Vector2(Pos.x - (Size.x/2), Pos.y - (Size.y / 2));
            return panelObject;
        }

        public override void SetLayout()
        {
            minWidth = 100;
            minHeight = 100;
            //preferredWidth = 100;
            flexibleWidth = 1;
            flexibleHeight = 1;
        }


        public MoGuiRow AddRow(string name)
        {
            MoGuiRow row = new MoGuiRow(this, name);
            row.Obj.transform.SetParent(Content.transform);
            Rows.Add(name, row);
            return row;
        }

        public MoGuiCol AddCol(string row, string name)
        {
            var _row = GetRow(row);
            return _row.AddColumn(name);
        }

        public MoGuiCol AddCol(MoGuiRow row, string name)
        {
            return row.AddColumn(name);
        }


        public MoGuiControl AddControl( string row, string col,  string name, MoGCArgs args)
        {

            MoGuiCol column = GetCol(row, col);

            MoGuiControl newComponent;
            if (Components.ContainsKey(name))
            {
                newComponent = Components[name];
                if (args.Type == typeof(MoGuiPanel))
                {
                    newComponent.Obj.transform.SetParent(column.Obj.transform, false);
                }
                else if (newComponent.Container != null)
                {
                    newComponent.Container.transform.SetParent(column.Obj.transform, false);
                }
                else
                {
                    newComponent.Obj.transform.SetParent(column.Obj.transform, false);
                }
            }
            else
            {
                newComponent = CreateComponent( name, args);
                if (args.Type == typeof(MoGuiPanel))
                {
                    newComponent.Obj.transform.SetParent(column.Obj.transform, false);
                } else if (newComponent.Container != null)
                {
                    newComponent.Container.transform.SetParent(column.Obj.transform, false);
                } else
                {
                    newComponent.Obj.transform.SetParent(column.Obj.transform, false);
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
                return new MoGuiButton(_args.Meta ?? Meta, Name + "_" + name, _args);
            }
            else if (args.Type == typeof(MoGuiToggle))
            {
                MoCaToggle _args = (MoCaToggle)args;
                return new MoGuiToggle(_args.Meta ?? Meta, Name + "_" + name, _args);

            }
            else if (args.Type == typeof(MoGuiToggleBt))
            {
                MoCaToggleBT _args = (MoCaToggleBT)args;
                return new MoGuiToggleBt(_args.Meta ?? Meta, Name + "_" + name, _args);

            }
            else if (args.Type == typeof(MoGuiInput))
            {
                MoCaInput _args = (MoCaInput)args;
                return new MoGuiInput(_args.Meta ?? Meta, Name + "_" + name, _args);
            }
            else if (args.Type == typeof(MoGuiSlider))
            {
                MoCaSlider _args = (MoCaSlider)args;
                return new MoGuiSlider(_args.Meta ?? Meta, Name + "_" + name, _args);
            }
            else if (args.Type == typeof(MoGuiDDL))
            {
                MoCaDDL _args = (MoCaDDL)args;
                return new MoGuiDDL(_args.Meta ?? Meta, Name + "_" + name, _args);

            }
            else if (args.Type == typeof(MoGuiSelector))
            {

                MoCaSelector _args = (MoCaSelector)args;
                
                return new MoGuiSelector(_args.Meta ?? Meta, Name + "_" + name, _args);

            }
            else if(args.Type == typeof(MoGuiTxt))
            {
                MoCaText _args = (MoCaText)args;
                return new MoGuiTxt(_args.Meta ?? Meta, Name + "_" + name, _args);
                
            }
            else if (args.Type == typeof(MoGuiColorBrick))
            {
                MoCaColor _args = (MoCaColor)args;
                return new MoGuiColorBrick(_args.Meta ?? Meta, Name + "_" + name, _args);

            }
            else if (args.Type == typeof(MoGuiPanel))
            {
                MoCaPanel _args = (MoCaPanel)args;
                return new MoGuiPanel(_args.Meta ?? Meta, Name + "_" + name, this, _args);
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
                foreach (var row in Rows)
                {
                    row.Value.Update();
                }
            }

        }

        public MoGuiTxt AddText(string row, string col, string name, MoGCArgs args)
        {
            return AddControl(row, col, name, args) as MoGuiTxt;
        }

        public MoGuiSlider AddSlider(string row, string col, string name, MoGCArgs args)
        {
            return AddControl(row, col, name, args) as MoGuiSlider;
        }
        public MoGuiInput AddInput(string row, string col, string name, MoGCArgs args)
        {
            return AddControl(row, col, name, args) as MoGuiInput;
        }
        public MoGuiToggle AddToggle(string row, string col, string name, MoGCArgs args)
        {
            return AddControl(row, col, name, args) as MoGuiToggle;
        }
        public MoGuiButton AddButton(string row, string col, string name, MoGCArgs args)
        {
            return AddControl(row, col, name, args) as MoGuiButton;
        }
        public MoGuiDDL AddDDL(string row, string col, string name, MoGCArgs args)
        {
            return AddControl(row, col, name, args) as MoGuiDDL;
        }

        public MoGuiPanel AddPanel(string row, string col, string name, MoGCArgs args)
        {
            return AddControl(row, col, name, args) as MoGuiPanel;
        }

        public MoGuiTxt GetText(string name)
        {
            return GetControl(name) as MoGuiTxt;
        }

        public MoGuiSlider GetSlider(string name)
        {
            return GetControl(name) as MoGuiSlider;
        }
        public MoGuiInput GetInput(string name)
        {
            return GetControl(name) as MoGuiInput;
        }
        public MoGuiToggle GetToggle(string name)
        {
            return GetControl(name) as MoGuiToggle;
        }
        public MoGuiButton GetButton(string name)
        {
            return GetControl(name) as MoGuiButton;
        }
        public MoGuiDDL GetDDL(string name)
        {
            return GetControl(name) as MoGuiDDL;
        }
        public MoGuiControl GetControl(string name)
        {
            if (Components.ContainsKey(name))
            {
                return Components[name];
            }
            else { return null; }
        }

        public MoGuiRow GetRow(string rowName)
        {
            MoGuiRow row;
            if (Rows.ContainsKey(rowName))
            {
                row = Rows[rowName];
            }
            else
            {
                row = AddRow(rowName);
            }
            return row;
        }

        public MoGuiCol GetCol(string rowName, string columnName)
        {
            MoGuiRow row = GetRow(rowName);
            MoGuiCol column;
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

        public bool IsActive
        {
            get => Obj.activeSelf;
        }

        public bool Minimized
        {
            get => Container.activeSelf ? false : true;
        }

        public void ShowGui(bool show)
        {
            Obj.SetActive(show);
        }

        public void ToggleMinGui(bool show)
        {
            Obj.SetActive(show);
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

        public override void _Init()
        {
            Container = CreateContainer();
        }

        public override void Init(bool topLevel)
        {
            Meta.PanelColor.Base = Meta.HeaderColor;

            Meta.FontSize = Meta.HeaderFontSize;
            Meta.ButtonFontSize = Meta.HeaderExitFontSize;
            Obj = CreatePanel();
            Container.transform.SetParent(Obj.transform, false);
            if (topLevel)
            {
                DragHandle = Obj.AddComponent<DraggableHandle>();
                DragHandle.BindParent(Panel.Obj);
                AddTitleText("HeaderText", PluginName + " - " + Panel.Title ?? Name);
                AddXButton("MinGui", "␣", () => MinGui());
                Meta.ButtonColor = Meta.HeaderExitColor;
                AddXButton("HideGui", "╳", () => ShowGui(false));
                AddResize();
            } else
            {
                AddTitleText("HeaderText", Panel.Title ?? Name);
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


            AddLayoutElement(panelObject);
            SetLayout();
            

            return panelObject;
        }

        public override GameObject CreateContainer()
        {

            GameObject layoutObject = new GameObject(PluginName + "_" + Name + "_" + "HeaderContainer");

            HorizontalLayoutGroup headerLayoutGroup = layoutObject.AddComponent<HorizontalLayoutGroup>();
            headerLayoutGroup.childControlWidth = true;
            headerLayoutGroup.childControlHeight = true;
            headerLayoutGroup.childForceExpandHeight = false;
            headerLayoutGroup.childForceExpandWidth = false;
            headerLayoutGroup.padding = new RectOffset(Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin, Meta.TxtMargin);
            headerLayoutGroup.spacing = Meta.TxtMargin;

            
            RectTransform layoutRect = layoutObject.GetComponent<RectTransform>();
            layoutRect.anchorMin = Vector2.zero;
            layoutRect.anchorMax = Vector2.one;
            layoutRect.offsetMin = new Vector2(0, 0);
            layoutRect.offsetMax = new Vector2(0, 0);
            return layoutObject;

        }

        public override void SetLayout()
        {
            minHeight = Meta.HeaderSize;
            minWidth = 100;
            //preferredWidth = 250;
            flexibleWidth = 1;
        }

        new public void ShowGui(bool show)
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

                newTxt.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                newTxt = new MoGuiButton(Meta, Name + "_" + label, text, onUpdateAction);
                newTxt.Obj.transform.SetParent(Container.transform, false);

                newTxt.minWidth = Meta.HeaderSize - 2 * Meta.TxtMargin;
                newTxt.minHeight = Meta.HeaderSize - 2 * Meta.TxtMargin;
                newTxt.preferredHeight = newTxt.minHeight;
                newTxt.preferredWidth = newTxt.minWidth;
                newTxt.flexibleWidth = 0.1f;
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
                newTxt.Obj.transform.SetParent(Container.transform, false);
            }
            else
            {
                newTxt = new MoGuiTxt(Meta, Name + "_" + label, text);
                newTxt.Obj.transform.SetParent(Container.transform, false);
                newTxt.Text.alignment = TextAnchor.MiddleLeft;
                LayoutElement titleLayoutElement = newTxt.Obj.AddComponent<LayoutElement>();
                titleLayoutElement.flexibleWidth = 1;
                Components.Add(label, newTxt);
            }

        }


        public void AddResize()
        {
            GameObject resizeIconObject = new GameObject("ResizeIcon");
            resizeIconObject.transform.SetParent(Container.transform, false);
            

            LayoutElement resizeLayoutElement = resizeIconObject.AddComponent<LayoutElement>();
            
            resizeLayoutElement.minWidth = Meta.HeaderSize - 2 * Meta.TxtMargin;
            resizeLayoutElement.minHeight = Meta.HeaderSize - 2 * Meta.TxtMargin;

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
