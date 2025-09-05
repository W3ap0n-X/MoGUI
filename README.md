# MoGUI
MoGUI is a lightweight, flexible UI framework for Unity. It provides a clean, code-driven approach to building dynamic and responsive user interfaces.

## Core Principles


### Bound Variable
A bound variable is nothing more than a `Func<object>` used to dynamically retrieve a value. These are defined with Lambda Functions:
```csharp
var some_variable = 1;
Func<object> bound_variable = () => some_variable;
```
In this example, if the value of `some_variable` were to change the bound_variable will detect this and dynamically update.

### Bound Action
This is the inverse of a Bound Variable and is simply an `Action<object>`. This is used to dynamically set a value when triggered:
```csharp
Action<object> bound_action = (newValue) => some_variable = newValue;
```

### Cascading MetaData
Each object associated with MoGUI has it's own Metadata used to define it's styles and default options. 
This metadata is automatically passed down to child components from their parent or new metadata altogether can be provided to a component to define it's styles and defaults.

## Components

### Panel
Panels Are the base layout components and background objects that house all controls used in MoGUI

#### TopLevel Panels
Toplevel Panels are Panels with their own header that can be moved around resized minimized and hidden. 

##### Header
The Header Contains the Panel Title which can be clicked and dragged to reposition the panel on the canvas, as well as the following controls
- minimize button to hide the Panel's Container
- X button to hide the Panel
- Resize Handle 

##### Container
The main Draw Area of the Panel where Components will be rendered. 
Each panel has a container with it's own self contained arbitrary grid structure to automatically size and position any components.

###### Arbitrary Grid System
The grid system is used to group together and manage layout of controls. Controls will be within a column which is contained within a row allowing for complex automated layouts.

- Rows
	Rows are the Horizontal containers used to house columns, a panel maintains a list of rows which in turn maintains a list of it's columns.
- Columns
	Columns are the Vertical containers used to hold groups of controls. 

Rows and columns do not need to be added manually, when adding a control you simply identify both the row and the column with a string.
- If the row isn't fownd in the panel's list of rows, it will be added
	- if the column isn't found in the row's list of columns it will be added.

This allows for an easy to use dynamic layout without having to hassle with managing the grid itself. 

#### RootPanel
TopLevel Panel created by default as the 'main' panel for any MoGUI GUI. For simple uses this can be used as the only panel or as a manager for other panels.

##### Header
The RootPanel Header controls the visibility of the RootPanel as well as sharing the features of other TopLevelPanel Headers.
The X button for the RootPanel will hide the MoGUI Canvas which will hide all associated TopLevelPanels. 

##### Container
The main Draw Area of the RootPanel where Components will be rendered. 
See Toplevel Panels > Containers for more details.

#### SubPanels
These are panels that are nested within a TopLevel Panel used for visual effect and more granular layout control.
##### Header
[TODO] Subpanels have an option to include a header with a title and option to collapse it's container.
##### Container
The main Draw Area of the Panel where Components will be rendered.
See Toplevel Panels > Containers for more details.

#### Controls
Each Panel maintains a list of it's components. Each control owned by a panel must have a unique name.

##### Update Loop
A panel is responsible for updating all of it's controls in a cascading heirarchy. 
The panel checks if it is hidden or minimized and if neitehr then it will invoke the Update loop on each subcontrol.

### Text
Text Controls can be used to display arbitrary text or can display a bound variable. 
Any controls that use Labels or anything that can potentially be ynamic text use this as the base.

### Button
A button is a clickable object that can be used to trigger a Bound Action

#### Label
Each button has a label that is derived from the Text Component. This can be used to display a bound variable on the face of the button.

### Toggle
A button is a clickable object that allows you to manipulate a boolean, setting it to true by checking the box or vise-versa. 
- can be used to control the value of boolean Bound Variables
- can be used as the base variable for other controls or functions

#### Label
Each toggle has a label that is derived from the Text Component. 
This can be used to display a bound variable in the selected label placement for this control.
The default Label placement for toggles is to the right of the checkbox

### Slider
A slider allows you to manipulate a float or int based upon a certain Range
- can be used to control the value of numeric Bound Variables
- can be used as the base variable for other controls or functions

#### Label
Each slider has a label that is derived from the Text Component. 
This can be used to display a bound variable in the selected label placement for this control.
The default Label placement for sliders is above the slider

### Input
An Input is typical input box that allows the user to type in a value.
- can be used to control the value of Bound Variables
- can be used as the base variable for other controls or functions

#### Label
Each Input has a label that is derived from the Text Component. 
This can be used to display a bound variable in the selected label placement for this control.
The default Label placement for Inputs is to the left of the inputbox

#### Update Loop
Inputs can be set up to display boud variables within their inputbox

### DropDownList
A Button that allows you to select a value from a list. Can be supplied a `List<string>` for simple setup or a `Dictionary<string,int>` to be used for dynamic selection.

### Selector
A Unity Toggle Group that ensures only one option from a list is selected

### ColorBrick


## Usage
Coming soon!