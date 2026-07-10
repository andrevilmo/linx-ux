using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.ComponentModel;
using System.CodeDom;
using System.Reflection;
using System.Collections;
using System.IO;
using Linx.Tools;
using System.CodeDom.Compiler;
using System.Globalization;


namespace Linx.Builder.Resources
{
    #region:Classes

    public class SerializableFieldNode
    {
        public SerializableFieldNode()
        {
        }

        public SerializableFieldNode(string columnName, string name, string caption, string objectClass, string alias, string datatype,
            int rowCount, int columnCount, int row, int column, Boolean isContainer, Boolean hasLabel, Boolean isDataGrid,
            Double width, double height, string bindingPath, Boolean isEditable, string orientation, string elementDataKey,
            decimal precision)
        {
            ColumnName = columnName;
            Name = name;
            Caption = caption;
            ObjectClass = objectClass;
            Alias = alias;
            Datatype = datatype;
            RowCount = rowCount;
            ColumnCount = columnCount;
            Row = row;
            Column = column;
            IsContainer = isContainer;
            HasLabel = hasLabel;
            IsDataGrid = isDataGrid;
            Items = new List<SerializableFieldNode>();
            ConnectedFields = new List<SerializableFieldNode>();
            Width = width;
            Height = height;
            BindingPath = bindingPath;
            IsEditable = isEditable;
            Orientation = orientation;
            ElementDataKey = elementDataKey;
            Precision = precision;
        }
        public string ColumnName { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string ObjectClass { get; set; }
        public string Alias { get; set; }
        public string Datatype { get; set; }
        public string BindingPath { get; set; }
        public string Orientation { get; set; }
        public string ElementDataKey { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public decimal Precision { get; set; }
        public Double Width { get; set; }
        public Double Height { get; set; }
        public Boolean IsContainer { get; set; }
        public Boolean HasLabel { get; set; }
        public Boolean IsDataGrid { get; set; }
        public Boolean IsEditable { get; set; }
        public List<SerializableFieldNode> Items { get; set; }
        public List<SerializableFieldNode> ConnectedFields { get; set; }
    }

    #endregion

   


    #region Code Manager
    /// <summary>
    /// Author: Alessandro Araújo
    /// Date: 19/08/2008
    /// Class Description: 
    ///     Class to generate code C# dynamically.
    /// </summary>
    public class CodeBuilder
    {
        private CodeCompileUnit targetUnit;
        private CodeTypeDeclaration targetClass;
        private System.CodeDom.CodeNamespace targetNS;

        /// <summary>
        /// The Target Class.
        /// </summary>
        public CodeTypeDeclaration TargetClass
        {
            get { return targetClass; }
        }

        /// <summary>
        /// The Target NameSpace.
        /// </summary>
        public System.CodeDom.CodeNamespace TargetNS
        {
            get { return targetNS; }
        }

        public CodeBuilder(string nameSpace, string className, string[] comments, string[] baseTypes, string[] customAttributes, MemberAttributes attr, bool isInterface)
        {
            this.AddNS(nameSpace);
            this.AddClass(className, comments, baseTypes, customAttributes, attr, isInterface, new string[] { });
        }

        public CodeBuilder(string nameSpace, string className, string[] comments, string[] baseTypes, string[] customAttributes, MemberAttributes attr, bool isInterface, string[] typeParameters)
        {
            this.AddNS(nameSpace);
            this.AddClass(className, comments, baseTypes, customAttributes, attr, isInterface, typeParameters);
        }

        public CodeBuilder(string nameSpace)
        {
            this.AddNS(nameSpace);
        }

        public CodeBuilder() { }

        /// <summary>
        /// Add one name space to the CSharp generator.
        /// </summary>
        /// <param name="nameSpace"></param>
        public void AddNS(string nameSpace)
        {
            //Unit
            targetUnit = new CodeCompileUnit();
            //Name Space
            targetNS = new System.CodeDom.CodeNamespace(nameSpace);
            //Add name space to unit
            targetUnit.Namespaces.Add(targetNS);
        }

        /// <summary>
        /// Add one class to the CSharp generator.
        /// </summary>
        /// <param name="classType"></param>
        public void AddClass(CodeTypeDeclaration clasType)
        {
            targetClass = clasType;

            if (targetNS == null)
                this.AddNS("Generic.NameSpace");

            //Add Class to name space
            targetNS.Types.Add(targetClass);
        }

        /// <summary>
        /// Change type.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object ChangeType(string value, Type type)
        {
            if (type.IsEnum)
                return Enum.Parse(type, value);
            else
                return Convert.ChangeType(value, type);
        }

        /// <summary>
        /// Add one class to the CSharp generator.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="comments"></param>
        /// <param name="baseTypes"></param>
        public void AddClass(string className, string[] comments, string[] baseTypes, string[] customAttributes, MemberAttributes attr, bool isInterface, string[] typeParameters)
        {
            //Class
            targetClass = new CodeTypeDeclaration(className);
            targetClass.IsInterface = isInterface;
            targetClass.IsClass = (!isInterface);
            targetClass.IsPartial = ((attr & MemberAttributes.Static) == 0);
            targetClass.Attributes = attr;

            foreach (string typeParam in typeParameters)
                targetClass.TypeParameters.Add(typeParam);

            //Add custom atttributes.            
            string attrParams, attrType;
            foreach (string attribute in customAttributes)
            {
                attrParams = attribute.Extract("(", ")");
                if (attrParams == "")
                    targetClass.CustomAttributes.Add(new CodeAttributeDeclaration(attribute));
                else
                {
                    List<CodeAttributeArgument> attrArgs = new List<CodeAttributeArgument>();
                    string valueOfParam = attrParams;

                    foreach (string extractedParam in attrParams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (extractedParam.IndexOf("#") > 0)
                        {
                            attrType = extractedParam.Right("#").Trim().Replace("<|>", ",").Replace("<->", "#");
                            valueOfParam = extractedParam.Left("#").Trim();
                        }
                        else
                            attrType = "System.String";

                        if (valueOfParam.IndexOf(":=") > 0)
                        {
                            if (Type.GetType(attrType).IsEnum)
                                attrArgs.Add(new CodeAttributeArgument(valueOfParam.Left(":=").Trim(), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(Type.GetType(attrType)), valueOfParam.Right(":=").Trim())));
                            else
                                attrArgs.Add(new CodeAttributeArgument(valueOfParam.Left(":=").Trim(), new CodePrimitiveExpression(this.ChangeType(valueOfParam.Right(":=").Trim(), Type.GetType(attrType)))));
                        }
                        else
                            attrArgs.Add(new CodeAttributeArgument(new CodePrimitiveExpression(this.ChangeType(valueOfParam, Type.GetType(attrType)))));

                    }

                    targetClass.CustomAttributes.Add(new CodeAttributeDeclaration(attribute.Left("("), attrArgs.ToArray()));
                }
            }


            foreach (string baseType in baseTypes)
            {
                targetClass.BaseTypes.Add(baseType);
            }

            foreach (string comment in comments)
            {
                targetClass.Comments.Add(new CodeCommentStatement(comment));
            }

            if (targetNS == null)
                this.AddNS("Generic.NameSpace");

            //Add Class to name space
            targetNS.Types.Add(targetClass);
        }

        /// <summary>
        /// Add one import to the CSharp generator.
        /// </summary>
        /// <param name="import"></param>
        public void AddImport(string import)
        {
            targetNS.Imports.Add(new CodeNamespaceImport(import));
        }

        /// <summary>
        /// Add one field to the CSharp generator.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attr"></param>
        /// <param name="type"></param>
        /// <param name="comments"></param>
        public void AddField(string name, MemberAttributes attr, string type, string comments, string initExpression, string[] memberAttributes)
        {
            // Declare the field.
            CodeMemberField newField = new CodeMemberField();
            newField.Attributes = attr; // MemberAttributes.Private;
            newField.Name = name;
            newField.Type = new CodeTypeReference(type);
            if (comments != "")
                newField.Comments.Add(new CodeCommentStatement(comments));

            if (initExpression != "")
                newField.InitExpression = new CodeVariableReferenceExpression(initExpression);


            //Add MemberAttributes.
            string attrParams, attrType;
            foreach (string attribute in memberAttributes)
            {
                attrParams = attribute.Extract("(", ")");
                if (attrParams == "")
                    newField.CustomAttributes.Add(new CodeAttributeDeclaration(attribute));
                else
                {
                    List<CodeAttributeArgument> attrArgs = new List<CodeAttributeArgument>();
                    string valueOfParam = attrParams;

                    foreach (string extractedParam in attrParams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (extractedParam.IndexOf("#") > 0)
                        {
                            attrType = extractedParam.Right("#").Trim().Replace("<|>", ",").Replace("<->", "#");
                            valueOfParam = extractedParam.Left("#").Trim();
                        }
                        else
                            attrType = "System.String";

                        if (valueOfParam.IndexOf(":=") > 0)
                            attrArgs.Add(new CodeAttributeArgument(valueOfParam.Left(":=").Trim(), new CodePrimitiveExpression(this.ChangeType(valueOfParam.Right(":=").Trim(), Type.GetType(attrType)))));
                        else
                            attrArgs.Add(new CodeAttributeArgument(new CodePrimitiveExpression(this.ChangeType(valueOfParam, Type.GetType(attrType)))));
                    }

                    newField.CustomAttributes.Add(new CodeAttributeDeclaration(attribute.Left("("), attrArgs.ToArray()));
                }
            }

            //Add Category
            newField.CustomAttributes.Add(new CodeAttributeDeclaration("System.ComponentModel.Category", new CodeAttributeArgument(new CodePrimitiveExpression("Linx Fields"))));

            targetClass.Members.Add(newField);
        }

        /// <summary>
        /// Add one property to the CSharp generator.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attr"></param>
        /// <param name="type"></param>
        /// <param name="comments"></param>
        /// <param name="bodyGet"></param>
        /// <param name="bodySet"></param>
        public void AddProperty(string name, MemberAttributes attr, string type, string comments, string[] bodyGet, string[] bodySet, string[] memberAttributes)
        {
            // Declare the read-only Width property.
            CodeMemberProperty newProperty = new CodeMemberProperty();
            newProperty.Attributes = attr;
            newProperty.Name = name;
            newProperty.Type = new CodeTypeReference(type);
            if (comments != "")
                newProperty.Comments.Add(new CodeCommentStatement(comments));

            newProperty.HasGet = (bodyGet.Length > 0);
            if (newProperty.HasGet)
            {
                // Add Body
                foreach (string code in bodyGet)
                    newProperty.GetStatements.Add(new CodeSnippetStatement(code));

            }

            newProperty.HasSet = (bodySet.Length > 0);
            if (newProperty.HasSet)
            {

                // Add Body
                foreach (string code in bodySet)
                    newProperty.SetStatements.Add(new CodeSnippetStatement(code));

            }

            //Add MemberAttributes.
            string attrParams, attrType;
            foreach (string attribute in memberAttributes)
            {
                attrParams = attribute.Extract("(", ")");
                if (attrParams == "")
                    newProperty.CustomAttributes.Add(new CodeAttributeDeclaration(attribute));
                else
                {
                    List<CodeAttributeArgument> attrArgs = new List<CodeAttributeArgument>();
                    string valueOfParam = attrParams;

                    foreach (string extractedParam in attrParams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (extractedParam.IndexOf("#") > 0)
                        {
                            attrType = extractedParam.Right("#").Trim().Replace("<|>", ",").Replace("<->", "#");
                            valueOfParam = extractedParam.Left("#").Trim();
                        }
                        else
                            attrType = "System.String";

                        if (valueOfParam.IndexOf(":=") > 0)
                            attrArgs.Add(new CodeAttributeArgument(valueOfParam.Left(":=").Trim(), new CodePrimitiveExpression(this.ChangeType(valueOfParam.Right(":=").Trim(), Type.GetType(attrType)))));
                        else
                            attrArgs.Add(new CodeAttributeArgument(new CodePrimitiveExpression(this.ChangeType(valueOfParam, Type.GetType(attrType)))));
                    }

                    newProperty.CustomAttributes.Add(new CodeAttributeDeclaration(attribute.Left("("), attrArgs.ToArray()));
                }
            }

            //Add Category
            newProperty.CustomAttributes.Add(new CodeAttributeDeclaration("System.ComponentModel.Category", new CodeAttributeArgument(new CodePrimitiveExpression("Linx Properties"))));

            targetClass.Members.Add(newProperty);
        }

        /// <summary>
        /// Add one method to the CSharp generator.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attr"></param>
        /// <param name="returnType"></param>
        /// <param name="comments"></param>
        /// <param name="parameters"></param>
        /// <param name="body"></param>
        public void AddMethod(string name, MemberAttributes attr, string returnType, string comments, string[,] parameters, string[] body, string category, string[] memberAttributes)
        {
            // Declaring a ToString method
            CodeMemberMethod newMethod = new CodeMemberMethod();
            newMethod.Attributes = attr;
            newMethod.Name = name;
            newMethod.ReturnType = new CodeTypeReference(returnType);
            if (comments != "")
                newMethod.Comments.Add(new CodeCommentStatement(comments));

            for (int index = 0; index < parameters.Length / 2; index++)
            {
                if (parameters[index, 0] != "")
                    newMethod.Parameters.Add(new CodeParameterDeclarationExpression(parameters[index, 0], parameters[index, 1]));
            }

            // Add Body
            foreach (string code in body)
                newMethod.Statements.Add(new CodeSnippetStatement(code));

            //Add MemberAttributes.
            string attrParams, attrType;
            foreach (string attribute in memberAttributes)
            {
                attrParams = attribute.Extract("(", ")");
                if (attrParams == "")
                    newMethod.CustomAttributes.Add(new CodeAttributeDeclaration(attribute));
                else
                {
                    List<CodeAttributeArgument> attrArgs = new List<CodeAttributeArgument>();
                    string valueOfParam = attrParams;

                    foreach (string extractedParam in attrParams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (extractedParam.IndexOf("#") > 0)
                        {
                            attrType = extractedParam.Right("#").Trim().Replace("<|>", ",").Replace("<->", "#");
                            valueOfParam = extractedParam.Left("#").Trim();
                        }
                        else
                            attrType = "System.String";

                        if (valueOfParam.IndexOf(":=") > 0)
                            attrArgs.Add(new CodeAttributeArgument(valueOfParam.Left(":=").Trim(), new CodePrimitiveExpression(this.ChangeType(valueOfParam.Right(":=").Trim(), Type.GetType(attrType)))));
                        else
                            attrArgs.Add(new CodeAttributeArgument(new CodePrimitiveExpression(this.ChangeType(valueOfParam, Type.GetType(attrType)))));
                    }

                    newMethod.CustomAttributes.Add(new CodeAttributeDeclaration(attribute.Left("("), attrArgs.ToArray()));
                }
            }

            //Add Category
            if (category != "")
                newMethod.CustomAttributes.Add(new CodeAttributeDeclaration("System.ComponentModel.Category", new CodeAttributeArgument(new CodePrimitiveExpression(category))));

            //Add to type         
            targetClass.Members.Add(newMethod);

        }

        /// <summary>
        /// Add Delegate.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attr"></param>
        /// <param name="returnType"></param>
        /// <param name="comments"></param>
        /// <param name="parameters"></param>
        /// <param name="category"></param>
        /// <param name="memberAttributes"></param>
        public void AddDelegate(string name, MemberAttributes attr, string returnType, string comments, string[,] parameters, string category, string[] memberAttributes)
        {
            // Declaring a ToString method
            CodeTypeDelegate newDelegate = new CodeTypeDelegate();
            newDelegate.Attributes = attr;
            newDelegate.Name = name;
            newDelegate.ReturnType = new CodeTypeReference(returnType);
            if (comments != "")
                newDelegate.Comments.Add(new CodeCommentStatement(comments));

            for (int index = 0; index < parameters.Length / 2; index++)
            {
                if (parameters[index, 0] != "")
                    newDelegate.Parameters.Add(new CodeParameterDeclarationExpression(parameters[index, 0], parameters[index, 1]));
            }

            //Add MemberAttributes.
            string attrParams, attrType;
            foreach (string attribute in memberAttributes)
            {
                attrParams = attribute.Extract("(", ")");
                if (attrParams == "")
                    newDelegate.CustomAttributes.Add(new CodeAttributeDeclaration(attribute));
                else
                {
                    List<CodeAttributeArgument> attrArgs = new List<CodeAttributeArgument>();
                    string valueOfParam = attrParams;

                    foreach (string extractedParam in attrParams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (extractedParam.IndexOf("#") > 0)
                        {
                            attrType = extractedParam.Right("#").Trim().Replace("<|>", ",").Replace("<->", "#");
                            valueOfParam = extractedParam.Left("#").Trim();
                        }
                        else
                            attrType = "System.String";

                        if (valueOfParam.IndexOf(":=") > 0)
                            attrArgs.Add(new CodeAttributeArgument(valueOfParam.Left(":=").Trim(), new CodePrimitiveExpression(this.ChangeType(valueOfParam.Right(":=").Trim(), Type.GetType(attrType)))));
                        else
                            attrArgs.Add(new CodeAttributeArgument(new CodePrimitiveExpression(this.ChangeType(valueOfParam, Type.GetType(attrType)))));
                    }

                    newDelegate.CustomAttributes.Add(new CodeAttributeDeclaration(attribute.Left("("), attrArgs.ToArray()));
                }
            }

            //Add Category
            if (category != "")
                newDelegate.CustomAttributes.Add(new CodeAttributeDeclaration("System.ComponentModel.Category", new CodeAttributeArgument(new CodePrimitiveExpression(category))));

            //Add to type         
            targetClass.Members.Add(newDelegate);

        }

        /// <summary>
        /// Add event declaration.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attr"></param>
        /// <param name="type"></param>
        /// <param name="comments"></param>
        /// <param name="memberAttributes"></param>
        public void AddEvent(string name, MemberAttributes attr, string type, string comments, string[] memberAttributes)
        {
            // Declare the field.
            CodeMemberEvent newEvent = new CodeMemberEvent();
            newEvent.Attributes = attr;
            newEvent.Name = name;
            newEvent.Type = new CodeTypeReference(type);
            if (comments != "")
                newEvent.Comments.Add(new CodeCommentStatement(comments));


            //Add MemberAttributes.
            string attrParams, attrType;
            foreach (string attribute in memberAttributes)
            {
                attrParams = attribute.Extract("(", ")");
                if (attrParams == "")
                    newEvent.CustomAttributes.Add(new CodeAttributeDeclaration(attribute));
                else
                {
                    List<CodeAttributeArgument> attrArgs = new List<CodeAttributeArgument>();
                    string valueOfParam = attrParams;

                    foreach (string extractedParam in attrParams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (extractedParam.IndexOf("#") > 0)
                        {
                            attrType = extractedParam.Right("#").Trim().Replace("<|>", ",").Replace("<->", "#");
                            valueOfParam = extractedParam.Left("#").Trim();
                        }
                        else
                            attrType = "System.String";

                        if (valueOfParam.IndexOf(":=") > 0)
                            attrArgs.Add(new CodeAttributeArgument(valueOfParam.Left(":=").Trim(), new CodePrimitiveExpression(this.ChangeType(valueOfParam.Right(":=").Trim(), Type.GetType(attrType)))));
                        else
                            attrArgs.Add(new CodeAttributeArgument(new CodePrimitiveExpression(this.ChangeType(valueOfParam, Type.GetType(attrType)))));
                    }

                    newEvent.CustomAttributes.Add(new CodeAttributeDeclaration(attribute.Left("("), attrArgs.ToArray()));
                }
            }

            //Add Category
            newEvent.CustomAttributes.Add(new CodeAttributeDeclaration("System.ComponentModel.Category", new CodeAttributeArgument(new CodePrimitiveExpression("Linx Events"))));

            targetClass.Members.Add(newEvent);
        }

        /// <summary>
        /// Add one constructor to the CSharp generator.
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="comments"></param>
        /// <param name="parameters"></param>
        /// <param name="body"></param>
        /// <param name="callBase"></param>
        public void AddConstructor(MemberAttributes attr, string comments, string[,] parameters, string[] body, bool callBase)
        {
            if ((attr & MemberAttributes.Static) == MemberAttributes.Static)
            {
                this.AddStaticConstructor(comments, parameters, body);
            }
            else
            {
                // Declare the constructor
                CodeConstructor constructor;
                constructor = new CodeConstructor();

                constructor.Attributes = attr;
                if (comments != "")
                    constructor.Comments.Add(new CodeCommentStatement(comments));

                // Add parameters.
                if (parameters.Length != 0)
                {
                    for (int index = 0; index < parameters.Length / 2; index++)
                    {
                        if (parameters[index, 0] != "")
                        {
                            constructor.Parameters.Add(new CodeParameterDeclarationExpression(parameters[index, 0], parameters[index, 1]));

                            if (callBase)
                                constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(parameters[index, 1]));
                        }
                    }
                }
                else
                {
                    if (callBase)
                        constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression());
                }


                // Add Body
                foreach (string code in body)
                    constructor.Statements.Add(new CodeSnippetStatement(code));

                //Add Category
                constructor.CustomAttributes.Add(new CodeAttributeDeclaration("System.ComponentModel.Category", new CodeAttributeArgument(new CodePrimitiveExpression("Linx Constructors"))));

                //Add to type            
                targetClass.Members.Add(constructor);
            }
        }

        /// <summary>
        /// Add static constructor;
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="parameters"></param>
        /// <param name="body"></param>
        private void AddStaticConstructor(string comments, string[,] parameters, string[] body)
        {
            // Declare the constructor
            CodeTypeConstructor constructor;

            constructor = new CodeTypeConstructor();

            if (comments != "")
                constructor.Comments.Add(new CodeCommentStatement(comments));

            // Add parameters.
            if (parameters.Length != 0)
            {
                for (int index = 0; index < parameters.Length / 2; index++)
                {
                    if (parameters[index, 0] != "")
                    {
                        constructor.Parameters.Add(new CodeParameterDeclarationExpression(parameters[index, 0], parameters[index, 1]));


                    }
                }
            }


            // Add Body
            foreach (string code in body)
                constructor.Statements.Add(new CodeSnippetStatement(code));

            //Add Category
            constructor.CustomAttributes.Add(new CodeAttributeDeclaration("System.ComponentModel.Category", new CodeAttributeArgument(new CodePrimitiveExpression("Linx Constructors"))));

            //Add to type            
            targetClass.Members.Add(constructor);
        }

        /// <summary>
        /// Creator of CSharp Coding.
        /// </summary>
        /// <param name="fileName"></param>
        public void GenerateCSharpCode(string fileName)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(fileName))
            {
                provider.GenerateCodeFromCompileUnit(targetUnit, sourceWriter, options);
            }
        }


    }


    #endregion Code Manager

}
