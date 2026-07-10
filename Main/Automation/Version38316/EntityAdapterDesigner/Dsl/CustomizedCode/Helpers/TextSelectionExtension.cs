using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using Linx.Tools;
using System.CodeDom;
using EnvDTE80;
using Microsoft.VisualStudio.Modeling;

namespace Linx.EntityAdapterDesigner
{
	public static class TextSelectionExtension
	{
		public static void OpenOperation(this TextSelection selection, GenericOperation operation, string className)
		{
			OpenOperation(selection, operation, className, String.Empty);
		}

		public static void OpenOperation(this TextSelection selection, GenericOperation operation, string className, String insertCommandText)
		{
			try
			{
				selection.StartOfDocument(false);
				if (selection.FindText("partial class " + className, 0))
				{
					TextPoint operationPoint = selection.GetOperationLine(operation);
					if (operationPoint != null)
						selection.MoveToLineAndOffset(operationPoint.Line, operationPoint.LineCharOffset, false);
					else
					{
						selection.AddOperation(operation);
						selection.StartOfDocument(false);
						if (selection.FindText("partial class " + className, 0))
						{
							operationPoint = selection.GetOperationLine(operation);
							if (operationPoint != null)
								selection.MoveToLineAndOffset(operationPoint.Line, operationPoint.LineCharOffset, false);
						}
					}

					if (!insertCommandText.IsNullOrEmpty())
					{
						if (!selection.FindText(insertCommandText))
						{
							selection.LineDown(false, 2);
							selection.NewLine();
							selection.Insert("//Begin Code Block added by DSL");
							selection.NewLine();
							selection.Insert(insertCommandText);
							selection.NewLine();
							selection.Insert("//End Code Block added by DSL");
							selection.NewLine();
						}
					}

					selection.SmartFormat();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		private static TextPoint GetOperationLine(this TextSelection selection, GenericOperation operation)
		{
			try
			{
				CodeFunction function;
				// Retrieve the CodeClass at the insertion point.
				CodeClass classElement =
					(CodeClass)selection.ActivePoint.get_CodeElement(
					vsCMElement.vsCMElementClass);

				foreach (CodeElement element in classElement.Members)
				{
					function = element as CodeFunction;
					if (!function.IsNull())
					{
						if (function.Name == operation.OverloadName)
						{
							foreach (CodeFunction overload in function.Overloads)
							{
								if (!overload.IsNull() && CheckParameters(overload, operation))
								{
									overload.AddAttributes(operation);
									//Adjust description
									if (!operation.Comment.IsNullOrEmpty() && overload.Comment != operation.Comment)
										overload.Comment = operation.Comment;
									if (!operation.DocComment.IsNullOrEmpty() && overload.DocComment != "<doc>\r\n" + operation.DocComment + "\r\n</doc>")
										overload.DocComment = "<doc>\r\n" + operation.DocComment + "\r\n</doc>";
									return overload.StartPoint;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			return null;
		}


        public static Dictionary<string, string[]> GetOperationSignatureInfo(this TextSelection selection, GenericOperation operation)
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();
            List<string> parameters = new List<string>();
            string returnInfo = String.Empty;

            try
            {
                CodeFunction function;
                // Retrieve the CodeClass at the insertion point.
                CodeClass classElement =
                    (CodeClass)selection.ActivePoint.get_CodeElement(
                    vsCMElement.vsCMElementClass);

                foreach (CodeElement element in classElement.Members)
                {
                    function = element as CodeFunction;
                    if (!function.IsNull())
                    {
                        if (function.Name == operation.OverloadName)
                        {
                            foreach (CodeFunction overload in function.Overloads)
                            {
                                if (!overload.IsNull() && CheckParameters(overload, operation))
                                {
                                    returnInfo = (overload.Type.AsFullName.IsNullOrEmpty() ? "void" : overload.Type.AsFullName) + " Result";
                                    foreach (CodeParameter parameter in overload.Parameters)
                                    {
                                        parameters.Add(parameter.Type.AsFullName + " " + parameter.Name);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            if (!returnInfo.IsNullOrEmpty())
                result.Add(returnInfo, parameters.ToArray());

            return result;
        }


		private static bool CheckParameters(this CodeFunction function, GenericOperation operation)
		{
			bool result = true;
			string parameterName, parameterType, parameter, defaultValue;
			CodeParameter2 codeParameter;

			if (!operation.IsUniqueOverload)
			{
				string[] parameters = operation.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
				if (function.Parameters.Count == parameters.Length)
				{
					for (int idxParam = 0; idxParam < parameters.Length; idxParam++)
					{
                        parameter = parameters[idxParam];
                        defaultValue = parameter.Right("=").Trim();
                        if (!defaultValue.IsNullOrEmpty())
                            parameter = parameter.Left("=").Trim();
                        parameterName = parameter.Right(" ").Trim();
                        parameterType = parameter.Left(" " + parameterName).Trim();
						if (parameterType.Length > 4)
						{
							if (parameterType.Left(4).InList("ref ", "out "))
								parameterType = parameterType.Substring(4);
							else if (parameterType.Left(5) == "this ")
								parameterType = parameterType.Substring(5);
						}
						codeParameter = function.Parameters.Item(idxParam + 1) as CodeParameter2;
                        if ((!codeParameter.DefaultValue.IsNullOrEmpty() || !defaultValue.IsNullOrEmpty()) && codeParameter.DefaultValue != defaultValue)
                            codeParameter.DefaultValue = defaultValue;
                        if (result && (codeParameter.IsNull() || !(codeParameter.Name == parameterName && codeParameter.Type.AsString == parameterType)))
                            result = false;
					}
				}
				else result = false;
			}

			return result;
		}

		private static void AddParameters(this CodeFunction function, GenericOperation operation)
		{
            string parameterName, parameterType, prefix, parametersResult = String.Empty, defaultValue, parameter;
			//Add Parameters
			if (!operation.Parameters.IsNullOrEmpty())
			{
				foreach (string parameterElement in operation.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
				{
                    defaultValue = parameterElement.Right("=").Trim();
                    if (!defaultValue.IsNullOrEmpty())
                        parameter = parameterElement.Left("=").Trim();
                    else
                        parameter = parameterElement.Trim();
					parameterName = parameter.Right(" ").Trim();
					parameterType = parameter.Left(" " + parameterName).Trim();
					prefix = String.Empty;
					if (parameterType.Length > 4)
					{
						if (parameterType.Left(4).InList("ref ", "out "))
						{
							prefix = parameterType.Left(4).Trim();
							parameterType = prefix + "____" + parameterType.Substring(4);
						}
						else if (parameterType.Left(5) == "this ")
						{
							prefix = parameterType.Left(5).Trim();
							parameterType = prefix + "____" + parameterType.Substring(5);
						}
					}
                    CodeParameter2 codeParam = function.AddParameter(parameterName, parameterType, -1) as CodeParameter2;

                    if (!defaultValue.IsNullOrEmpty())
                        codeParam.DefaultValue = defaultValue;

                    parametersResult += (parametersResult.IsNullOrEmpty() ? String.Empty : "#") + codeParam.Type.AsString.Replace("____", " ") + " " + codeParam.Name + (defaultValue.IsNullOrEmpty() ? String.Empty : "=" + defaultValue);
				}


				//Update parameters if exists changes.
				if (operation.Parameters != parametersResult)
				{
					using (Transaction transaction =
								operation.Store.TransactionManager.BeginTransaction("Changing parameters."))
					{
						operation.Parameters = parametersResult;
						transaction.Commit();
					}
				}
			}
		}


        private static void AddAttributes(this CodeClass classObj, string attributes)
        {
            if (attributes.IsNullOrEmpty())
                return;

            string attributeName, attributeParams;
            CodeAttribute objAttribute;
            //Getting old attributes
            List<CodeAttribute> oldAttributes = new List<CodeAttribute>();
            foreach (CodeAttribute attrib in classObj.Attributes)
                oldAttributes.Add(attrib);

            //Add new attributes
            if (!attributes.IsNullOrEmpty())
            {
                foreach (string attribute in attributes.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (attribute.Contains("("))
                    {
                        attributeName = attribute.Left("(");
                        attributeParams = attribute.Right(attributeName + "(").Trim();
                        attributeParams = attributeParams.Left(attributeParams.Length - 1);
                        objAttribute = oldAttributes.Where(e => e.Name == attributeName).FirstOrDefault();
                        if (objAttribute == null)
                            classObj.AddAttribute(attributeName, attributeParams);
                        else if (objAttribute.Value != attributeParams) 
                            objAttribute.Value = attributeParams;
                    }
                    else
                    {
                        objAttribute = oldAttributes.Where(e => e.Name == attribute).FirstOrDefault();
                        if (objAttribute == null)
                            classObj.AddAttribute(attribute, String.Empty);
                        else if (!objAttribute.Value.IsNullOrEmpty())
                            objAttribute.Value = String.Empty;
                        
                    }
                }
            }
        }

		private static void AddAttributes(this CodeFunction function, GenericOperation operation)
		{
			string attributeName, attributeParams;
			//Add custom atributes
			string attributes = operation.CustomAttributes;
			if (operation is DomainServiceOperation)
			{
				switch (((DomainServiceOperation)operation).DomainAttribute)
				{
					case DomainAttributeType.IgnoreOperation:
						attributes += (attributes.IsNullOrEmpty() ? String.Empty : "#") + "Ignore";
						break;
					case DomainAttributeType.Invoke:
                        attributes += (attributes.IsNullOrEmpty() ? String.Empty : "#") + "Invoke(HasSideEffects = " + (!((DomainServiceOperation)operation).IsJson).ToString().ToLower() + ")";
						break;
					case DomainAttributeType.Query:
                        attributes += (attributes.IsNullOrEmpty() ? String.Empty : "#") + "Query(HasSideEffects = " + (!((DomainServiceOperation)operation).IsJson).ToString().ToLower() + ")";
						break;
					default:
						break;
				}
			}

            if (operation is WebApiAction)
            {
                WebApiAction apiAction = (WebApiAction)operation;
                string verb = apiAction.HttpVerb.ToString(), routeactionName = (apiAction.RouteActionName == "." ? apiAction.Name : apiAction.RouteActionName);
                //Base route
                if (apiAction.EnableAccessControl && apiAction.WebApiController.EntityAdapterDesignerRoot.EnableAutomaticAuthorization)
                    attributes += (attributes.IsNullOrEmpty() ? String.Empty : "#") + apiAction.WebApiController.Name + "ControllerAuthorize";

                attributes += (attributes.IsNullOrEmpty() ? String.Empty : "#") + "System.Web.Http.Http" + verb.Proper();
                attributes += (attributes.IsNullOrEmpty() ? String.Empty : "#") + "Route(\"" + routeactionName + "\")";
                //If GET, add a route for all parameters
                if (apiAction.EnableRoutesForParameters && apiAction.HttpVerb == HttpRouteAttribute.GET && !apiAction.Parameters.IsNullOrEmpty())
                {
                    string[] parameters = apiAction.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => (e.Contains("=") ? e.Left("=").Trim() : e).Right(" ")).ToArray();
                    foreach (string paramName in parameters)
                    {
                        attributes += (attributes.IsNullOrEmpty() ? String.Empty : "#") + "Route(\"" + routeactionName + (routeactionName.IsNullOrEmpty() ? String.Empty : "/") + paramName + "/{" + paramName + "}" + "\")";
                    }
                }
                
                //Add custom routes
                if (!apiAction.CustomRoutes.IsNullOrEmpty())
                {
                    string[] routes = apiAction.CustomRoutes.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string route in routes)
                    {
                        attributes += (attributes.IsNullOrEmpty() ? String.Empty : "#") + "Route(\"" + route.Replace("./", routeactionName + (routeactionName.IsNullOrEmpty() ? String.Empty : "/")) + "\")";
                    }
                }
            }

            //Getting old attributes
            List<CodeAttribute> oldAttributes = new List<CodeAttribute>();
            foreach (CodeAttribute attrib in function.Attributes)
                oldAttributes.Add(attrib);

            //Add new attributes
			if (!attributes.IsNullOrEmpty())
			{
				foreach (string atribute in attributes.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
				{
                    if (atribute.Contains("("))
                    {
                        attributeName = atribute.Left("(");
                        attributeParams = atribute.Right(attributeName + "(").Trim();
                        attributeParams = attributeParams.Left(attributeParams.Length - 1);
                        if (oldAttributes.Where(e => e.Name == attributeName && e.Value == attributeParams).Count() == 0)
                            function.AddAttribute(attributeName, attributeParams);
                    }
                    else
                    {
                        if (oldAttributes.Where(e => e.Name == atribute && e.Value == String.Empty).Count() == 0)
                            function.AddAttribute(atribute, String.Empty);
                    }
				}
			}
		}

		private static void ReplaceAttribute(this CodeFunction function, string attributeName, string attributeParams)
		{
			CodeAttribute currentAttribute = null;
			//Get the attribute if exists.
			foreach (CodeAttribute attribute in function.Attributes)
			{
				if (attribute.Name == attributeName)
				{
					currentAttribute = attribute;
					break;
				}
			}

			if (!currentAttribute.IsNull())
			{
				if (currentAttribute.Value.Replace(" ", "") != attributeParams.Replace(" ", ""))
					currentAttribute.Value = attributeParams;
			}
			else
				function.AddAttribute(attributeName, attributeParams, -1);
		}


		private static void ReplaceElementsWithoutSupport(this TextSelection selection, int startLine, int endLine, string key)
		{
			selection.MoveTo(startLine, 1, false);
			selection.MoveTo(endLine, 1, true);
			selection.ReplaceText(key + "____", key + " ", 0);
		}

		private static void AddOperation(this TextSelection selection, GenericOperation operation)
		{
			try
			{
				// Retrieve the CodeClass at the insertion point.
				CodeClass classElement =
					(CodeClass)selection.ActivePoint.get_CodeElement(
					vsCMElement.vsCMElementClass);


				// Create a new member function.
				CodeFunction function = classElement.AddFunction(operation.OverloadName,
					vsCMFunction.vsCMFunctionFunction,
					(operation.IsPartial ? "partial____" : "") + operation.ReturnType, -1,
					GetAccess(operation.Access), null);


				//Adjust description
				if (!operation.DocComment.IsNullOrEmpty())
					function.Comment = operation.Comment;
				if (!operation.DocComment.IsNullOrEmpty())
					function.DocComment = "<doc>\r\n" + operation.DocComment + "\r\n</doc>";

				// Set auxiliar informations.
				function.IsShared = operation.IsStatic;
				function.CanOverride = operation.CanOverride;

				//Add attributes and parameters
				function.AddAttributes(operation);
				function.AddParameters(operation);


				//Adjust elemenst without suport
				int startPoint = function.StartPoint.Line, endPoint = function.EndPoint.Line;

				if (operation.IsPartial)
					selection.ReplaceElementsWithoutSupport(startPoint, endPoint, "partial");
				selection.ReplaceElementsWithoutSupport(startPoint, endPoint, "this");
				selection.ReplaceElementsWithoutSupport(startPoint, endPoint, "ref");
				selection.ReplaceElementsWithoutSupport(startPoint, endPoint, "out");

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}


		private static vsCMAccess GetAccess(OperationAccess access)
		{
			vsCMAccess resut = vsCMAccess.vsCMAccessDefault; ;

			switch (access)
			{
				case OperationAccess.AssemblyOrFamily:
					resut = vsCMAccess.vsCMAccessAssemblyOrFamily;
					break;
				case OperationAccess.Default:
					resut = vsCMAccess.vsCMAccessDefault;
					break;
				case OperationAccess.Private:
					resut = vsCMAccess.vsCMAccessPrivate;
					break;
				case OperationAccess.Project:
					resut = vsCMAccess.vsCMAccessProject;
					break;
				case OperationAccess.ProjectOrProtected:
					resut = vsCMAccess.vsCMAccessProjectOrProtected;
					break;
				case OperationAccess.Protected:
					resut = vsCMAccess.vsCMAccessProtected;
					break;
				case OperationAccess.Public:
					resut = vsCMAccess.vsCMAccessPublic;
					break;
				case OperationAccess.WithEvents:
					resut = vsCMAccess.vsCMAccessWithEvents;
					break;
				default:
					break;
			}

			return resut;
		}

        public static void MoveToCodeElement(this TextSelection selection, string className, string elementName, string attributes)
		{
			try
			{
				selection.StartOfDocument(false);
                if (selection.FindText("partial class " + className, 0) || selection.FindText(" interface " + className, 0))
				{
                    TextPoint operationPoint = GetCodeElementLine(selection, elementName, attributes);
					if (operationPoint != null)
						selection.MoveToLineAndOffset(operationPoint.Line, operationPoint.LineCharOffset, false);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        public static void MoveToElementName(this TextSelection selection, string elementName, string attributes)
        {
            try
            {
                selection.StartOfDocument(false);
                if (selection.FindText(elementName, 0))
                {
                    TextPoint operationPoint = GetCodeElementLine(selection, elementName, attributes);
                    if (operationPoint != null)
                        selection.MoveToLineAndOffset(operationPoint.Line, operationPoint.LineCharOffset, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static TextPoint GetCodeElementLine(this TextSelection selection, string elementName, string attributes)
		{
			try
			{
				// Retrieve the CodeClass at the insertion point.
				CodeClass classElement =
					(CodeClass)selection.ActivePoint.get_CodeElement(
					vsCMElement.vsCMElementClass);

                if (classElement != null)
                {
                    classElement.AddAttributes(attributes);

                    if (elementName.IsNullOrEmpty())
                        return classElement.StartPoint;

                    foreach (CodeElement element in classElement.Members)
                    {
                        if (element.Name == elementName)
                        {
                            return element.StartPoint;
                        }
                    }
                }

                // Retrieve the CodeInterface at the insertion point.
                CodeInterface intefaceElement =
                    (CodeInterface)selection.ActivePoint.get_CodeElement(
                    vsCMElement.vsCMElementInterface);

                if (intefaceElement != null)
                {
                    if (elementName.IsNullOrEmpty())
                        return intefaceElement.StartPoint;

                    foreach (CodeElement element in intefaceElement.Members)
                    {
                        if (element.Name == elementName)
                        {
                            return element.StartPoint;
                        }
                    }
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			return null;
		}



	}
}
