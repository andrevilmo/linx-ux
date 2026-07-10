//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This sample is licensed under the MICROSOFT VISUAL STUDIO 2010
//    VISUALIZATION AND MODELING SOFTWARE DEVELOPMENT KIT license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************

Before you start: this sample was created with Visual Studio 2010 but is compatible with the current version of Visual Studio. Screenshots and some references may be slightly inaccurate. 


1. Install the VSIX in the main instance of VS (Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension.vsix) by double-clicking on it

How to test:
------------
2. Open a regular instance of VS and unfold a new designer (named MyLanguage, extension .myLanguage, namespace MyCompany)

3. Open the DslDefinition.dsl

4. Right click on the design surface (not on a shape), and choose the "Enable Modelbus" command
	a dialog apprears that let you choose if you want to expose your model to the modelbus, and/or enable modelbus consumption. Choose both

5. Transform all templates
	Verify that:
	- there is no template transformation error


6. Build
	Verify that:
	- there is no build error
	- a new project "ModelBusAdapter" was created
	- its Assembly name and Default namespace properties are computed from the ones of the Dsl project.
	- if the Dsl was signed, the ModelBusAdapter is as well, with the same Key
	
	- the DslPackage project references the ModelBusAdapter project
	- source.extension.tt was updated to take into account the new project (as a MEF extension)

	- the Dsl project references Microsoft.VisualStudio.Modeling.Sdk.Integration.10.dll
	- In the DslDefinition's Dsl explorer, the domain types contain ModelBusReference
	- the GeneratedCode folder now contains the "ModelBusReferenceSerialization.tt" file


7. Right click again on the design surface (not on a shape), and choose the "Enable Modelbus" command
	Verify that:
		- the dialog apprears 
		- if youc click both options and press Ok, nothing happens (the modelbus is already enabled)


7. In the DslDefinition, in a domain class add a new DomainProperty

9. Change the type of the DomainProperty to be ModelBusReference

10. Right click on the property and choose "Edit ModelBusReference specific properties"
	a dialog appears.

11. In the dialog set:
	- reference kind to a model element
	- Caption to "Choose one of my models
	- filter string to "My models|*.myLanguage"
	- Model Element type restriction to the fully qualified name of one of the classes of the model (for instance MyCompany.MyLanguage.ClassType
	Click Ok


12. Right click again on the property and choose "Edit ModelBusReference specific properties"
	the dialog appears.
	Verify that it contains the same information as you have entered

13. Transform all templates

14 Run without debugging
	Verifiy that:
		- the added reference attribute can be edited with the picker UI, the caption in the open file dialog is right, the type of files is right, and you can only
		  pick the right kind of domain classes
		- save the sample file
		- reopen it: the modelbus reference was saved