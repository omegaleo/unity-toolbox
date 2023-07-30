# OmegaLeo.Toolbox
## InstancedBehavior`1

### InstancedBehavior<T>
Create an instanced MonoBehaviour of type T which can than be called by using <ClassName>.instance

## DirectoryHelper

Helper class for any directory related methods.
DirectoryHelper

### RecursiveCopy
Method to copy recursively all files and folders from one path to another.
**Path ** -  The target path we want to recursively copy
**CopyToRoot ** -  The root path we want to copy into

## ArrayExtensions

Set of extension methods to help with Arrays.

### Next
Method to obtain the next object in the array by passing the current index.
Note: Idea obtained from TaroDev's video on things to do in Unity - https://youtu.be/Ic5ux-tpkCE?t=302
**CurrentIndex ** -  The index we're currently on inside the array passed as reference so we can automatically assign to it the next index

## ColoredHeaderAttribute

### ColoredHeaderAttribute
Attribute made specifically to implement fully custom colored headers in the inspector window.
**Title ** -  Title to be displayed in the header
**Thickness ** -  Thickness of the header in integer
**Padding ** -  Inner padding of the header text
**TextColor ** -  Color of the text
**BackgroundColor ** -  Color for the background of the header
**IconPath ** -  Path to the icon inside the project(can also be Unity Editor Built

