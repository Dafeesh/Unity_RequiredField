# Required Field Attribute
A simple fix to check if a field is assigned a value.
Useful if you use [SerializeField]/public fields to reference other components and objects.

# How to use
-Place 'RequiredField.cs' in your active Unity project.
-Attribute any field you want to be checked in the editor.
-If a required field is null the Editor will edit Play mode and display errors in the console.
