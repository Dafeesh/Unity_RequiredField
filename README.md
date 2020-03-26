# Required Field Attribute
An attribute to require that a field is assigned a value.

Useful if you use [SerializeField]/public fields to reference other components and objects.

### How to use
-Import 'RequiredField.cs' into your active Unity project.

-Attribute onto any field(s) you want.

-If a required field is null, and the Editor enters Play mode, it will exit Play mode and loudly error.
