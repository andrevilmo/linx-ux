using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Reflection;
using Linx.Tools;
using System.IO;


namespace Linx.Builder.Resources
{
    public class CodeGen
    {

        private static Dictionary<string, string> GetWfNameSpaces()
        {
            return new Dictionary<string, string> 
            {
              { "mva", "clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities" },
              { "lbt", "clr-namespace:Linx.Business.Tools;assembly=Linx.Business.Tools" }, 
              { "ld", "clr-namespace:Linx.Data;assembly=Linx.Data" },
              { "lt", "clr-namespace:Linx.Tools;assembly=Linx.Tools"  },  
              { "mv", "clr-namespace:Microsoft.VisualBasic;assembly=System" },
              { "s", "clr-namespace:System;assembly=mscorlib" },              
              { "sa", "clr-namespace:System.Activities;assembly=System.Activities" },
              { "sad", "clr-namespace:System.Activities.Debugger;assembly=System.Activities" },
              { "scg", "clr-namespace:System.Collections.Generic;assembly=mscorlib" },
              { "sd", "clr-namespace:System.Data;assembly=System.Data" },
              { "sde", "clr-namespace:System.Data.Entity;assembly=System.Data.Entity" },
              { "sl", "clr-namespace:System.Linq;assembly=System.Core" },
              { "ssde", "clr-namespace:System.ServiceModel.DomainServices.EntityFramework;assembly=System.ServiceModel.DomainServices.EntityFramework" },
              { "ssds", "clr-namespace:System.ServiceModel.DomainServices.Server;assembly=System.ServiceModel.DomainServices.Server" },
              { "st", "clr-namespace:System.Text;assembly=mscorlib" }
            };
        }
        

		public static string ReadResourceContent(string resourcePath)
		{
			string body = String.Empty;
			//Read template file
			using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					body = reader.ReadToEnd();
				}
			}

			return body;
		}

        #region Wokflow resources

        //Get Composite Resources
        public static string GetFlowchartActivity(string nameSpace, string className, string startActivity, string filePath, string edmNS, string[] parameters, string callerObjectType)
        {
            string body = ReadResourceContent((startActivity.IsNullOrEmpty() ? "Linx.Builder.Resources.Templates.EmptyFlowchart.txt" : "Linx.Builder.Resources.Templates.FlowchartActivity.txt"));
            //Replace code elements.
            body = body.Replace("#nameSpace#", nameSpace);
            body = body.Replace("#className#", className);            
            body = body.Replace("#filePath#", filePath);
                        

            //Get Namespaces
            Dictionary<string, string> nsList = GetWfNameSpaces();
            nsList.Add("local", String.Format("clr-namespace:{0}", nameSpace));
            nsList.Add("edm", String.Format("clr-namespace:{0};assembly={0}", edmNS));
                       
            List<string> parameterList = new List<string>(parameters);
            if (!callerObjectType.IsNullOrEmpty())
                parameterList.Add(nameSpace + "." + callerObjectType + " Caller");

            //Verify member
            if (!startActivity.IsNullOrEmpty())
            {
                if (parameterList.Count > 0)
                {
                    string members = "<x:Members>", propType, propName, ns, key, fullType, refType;
                    int nsCnt;
                    for (int idx = 0; idx < parameterList.Count; idx++)
                    {
                        refType = String.Empty;
                        fullType = parameterList[idx].Left(" ").Replace(">", "").Trim();
                        nsCnt = 0;
                        foreach (string elementType in fullType.Split(new char[] { '<' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            propType = elementType.Right(".").Trim();
                            ns = (elementType + " ").Left("." + propType + " ").Trim();
                            nsCnt++;
                            var element = nsList.Where(e => (e.Value + ";").Contains(String.Format("clr-namespace:{0};", ns))).FirstOrDefault();
                            if (!element.IsNull() && !element.Value.IsNullOrEmpty())
                                refType += (refType.IsNullOrEmpty() ? "" : "(") + element.Key + ":" + propType;
                            else
                            {                                
                                key = "ns" + (nsCnt).ToString();
                                nsList.Add(key, String.Format("clr-namespace:{0};assembly={0}", ns));
                                refType += (refType.IsNullOrEmpty() ? "" : "(") + key + ":" + propType;
                            }
                        }
                        if (nsCnt > 1)
                            refType += "".PadLeft(nsCnt - 1, ')');
                        propName = parameterList[idx].Right(" ").Trim();
                        propName = propName.Left(1).ToUpper() + propName.Substring(1);
                        members += "\r\n        " + @"<x:Property Name=""" + propName + @""" Type=""InArgument(" + refType + @")"" />";
                        startActivity += @" " + propName + @"=""[" + propName + @"]""";
                    }
                    members += "\r\n  </x:Members>";

                    body = body.Replace("#Members#", members);
                }
                else
                    body = body.Replace("#Members#", String.Empty);
                
                body = body.Replace("#startActivity#", startActivity);
            }

            //Generate NameSpaces
            string nameSpaces = String.Empty;
            foreach (var ns in nsList)
            {
                nameSpaces += "\r\n  " + String.Format("xmlns:{0}=\"{1}\"", ns.Key, ns.Value);
            }
            body = body.Replace("#nameSpaces#", nameSpaces);

            return body;
        }


        public static string GetWorkflowInvoker(string className, bool isCallerStatic, string[] parameters, string indent)
        {
            string body = "\r\n" + indent + "[Ignore]";
            body += "\r\n" + indent + "public " + (isCallerStatic ? "static" : String.Empty) + " void Invoke" + className + "(";
          
            //In Argumets
            string dictionaries = String.Empty, inArguments = String.Empty, propName;
                       
            for (int idx = 0; idx < parameters.Length; idx++)
            {
                propName = parameters[idx].Right(" ").Trim();                
                inArguments += (inArguments.IsNullOrEmpty() ? String.Empty : ", ") + parameters[idx];
                dictionaries += (dictionaries.IsNullOrEmpty() ? "new Dictionary<string, object> { " : ", ") + "{\"" + propName.Left(1).ToUpper() + propName.Substring(1) + "\", " + propName + "}";
            }

            if (!isCallerStatic)
                dictionaries += (dictionaries.IsNullOrEmpty() ? "new Dictionary<string, object> { " : ", ") + "{\"Caller\", this}";

            if (!dictionaries.IsNullOrEmpty())
                dictionaries += " }";

            body += inArguments + ")";
            body += "\r\n" + indent + "{";            
            body += "\r\n" + indent + "      System.Activities.WorkflowInvoker.Invoke(new " + className + "()" + (dictionaries.IsNullOrEmpty() ? String.Empty : ", " + dictionaries) + ");";
            body += "\r\n" + indent + "}";

            return body;
        }

        public static string GetActivity(string nameSpace, string className, string callerObjectType, bool isCallerStatic, string operationName, string[] parameters, string returnType)
        {
            string body = ReadResourceContent("Linx.Builder.Resources.Templates.Activity.txt");
            body = body.Replace("#nameSpace#", nameSpace);
            body = body.Replace("#className#", className);

            //In Argumets
            bool isRef;
            string inArguments = String.Empty, propType, propName, operationCall = operationName + "(", variablesForCall = String.Empty, paramDef ;
            for (int idx = 0; idx < parameters.Length; idx++)
            {
                paramDef = parameters[idx];
                isRef = paramDef.Left(4) == "ref ";
                if (isRef)
                  paramDef = paramDef.Substring(4);
                propType = paramDef.Left(" ").Trim();
                propName = paramDef.Right(" ").Trim();
                propName = propName.Left(1).ToUpper() + propName.Substring(1);
                inArguments += "\r\n        public InArgument<" + propType + "> " + propName + " { get; set; }";
                variablesForCall += "\r\n			var in" + propName + " = " + propName + ".Get(context);";
                operationCall += (idx == 0 ? "" : ", ") + (isRef ? "ref " : String.Empty) + "in" + propName;
            }
            operationCall += ")";

            //Add calller reference
            if (!isCallerStatic)
                inArguments += "\r\n        public InArgument<" + callerObjectType + "> Caller { get; set; }";

            body = body.Replace("#InArguments#", inArguments);

            //Adjust return type
            if (!returnType.IsNullOrEmpty() && returnType.ToLower() != "void")
                body = body.Replace("#ReturnType#", returnType);
            else
            {
                body = body.Replace("<#ReturnType#>", String.Empty);
                body = body.Replace("#ReturnType#", "void");
            }

            body = body.Replace("#VariablesForCall#", variablesForCall);
            body = body.Replace("#OperationCall#", ((!returnType.IsNullOrEmpty() && returnType.ToLower() != "void") ? "return " : String.Empty) + (isCallerStatic ? callerObjectType : "Caller.Get(context)") + "." + operationCall);

            return body;
        }


        public static string[] GetActivityDesigner(string nameSpace, string className, string[] parameters, string returnType, bool isCallerStatic)
        {
            string body = ReadResourceContent("Linx.Builder.Resources.Templates.ActivityDesigner.txt");
            body = body.Replace("#nameSpace#", nameSpace);
            body = body.Replace("#className#", className);

            //Row definitions and In Argumets
            string inArguments = String.Empty, rowDefinitions = String.Empty, propName;

            //Check parameters
            List<string> parameterList = new List<string>(parameters);
            if (!isCallerStatic)
                parameterList.Add(className + " caller");

            for (int idx = 0; idx < parameterList.Count; idx++)
            {
                //Row definitions
                rowDefinitions += @"
            <RowDefinition />";

                //Get in arguments
                propName = parameterList[idx].Right(" ").Trim();
                propName = propName.Left(1).ToUpper() + propName.Substring(1);
                inArguments += GetInArgumentDefinition(propName, idx);
            }            
            body = body.Replace("#InArguments#", inArguments);

            //Row definitions
            rowDefinitions += @"
            <RowDefinition />";
            body = body.Replace("#RowDefinitions#", rowDefinitions);
                        

            //Out Arguments
            if (!returnType.IsNullOrEmpty() && returnType.ToLower() != "void")
                body = body.Replace("#OutArguments#", GetResultArgumentDefinition(parameterList.Count));
            else
                body = body.Replace("#OutArguments#", String.Empty);

            string codeBehind = ReadResourceContent("Linx.Builder.Resources.Templates.ActivityDesignerCodeBehind.txt");
            codeBehind = codeBehind.Replace("#nameSpace#", nameSpace);
            codeBehind = codeBehind.Replace("#className#", className); 

            return new string[] { body, codeBehind };
        }

        private static string GetInArgumentDefinition(string propName, int row)
        {

            return @"
        <TextBlock Margin=""18,2, 0,0"" VerticalAlignment=""Center"" HorizontalAlignment=""Right"" Grid.Row=""" + row.ToString() + @""" FontWeight=""Bold"">" + propName + @":</TextBlock>
        <sapv:ExpressionTextBox Margin=""18,2, 0,0"" Grid.Column=""1"" Grid.Row=""" + row.ToString() + @""" Expression=""{Binding Path=ModelItem." + propName + @", Mode=TwoWay, Converter={StaticResource ArgumentToExpressionConverter}, ConverterParameter=In}"" OwnerActivity=""{Binding Path=ModelItem}"" 
            MinLines=""1"" MaxLines=""1"" MinWidth=""250"" HintText=""&lt;" + propName + @"&gt;""/>";

        }

        private static string GetResultArgumentDefinition(int row)
        {

            return @"
        <TextBlock Margin=""18,12,0,0"" FontWeight=""Bold"" VerticalAlignment=""Center"" HorizontalAlignment=""Right"" Grid.Row=""" + row.ToString() + @""">Result:</TextBlock>
        <sapv:ExpressionTextBox  FontWeight=""Normal"" Margin=""18,12,0,0"" Grid.Column=""2"" Grid.Row=""" + row.ToString() + @""" UseLocationExpression=""True"" Expression=""{Binding Path=ModelItem.Result, Mode=TwoWay, Converter={StaticResource ArgumentToExpressionConverter}, ConverterParameter=Out}"" OwnerActivity=""{Binding Path=ModelItem}""
        MinLines=""1"" MaxLines=""1"" MinWidth=""250"" HintText=""&lt;Result&gt;"" />";

        }

        #endregion

        //Get Composite Resources
        public static string GetCompositeModule(string nameSpace, string moduleName, string moduleClass, bool onDemand)
        {
            string body = ReadResourceContent("Linx.Builder.Resources.Templates.CompositeModuleAutoOrCustom.txt");
			//Replace code elements.
			body = body.Replace("#nameSpace#", nameSpace);
			body = body.Replace("#moduleName#", moduleName);
			body = body.Replace("#onDemand#", onDemand.ToString().ToLower());
			body = body.Replace("#moduleClass#", moduleClass);

			return body;
        }


        public static string[] GetCompositeModuleView(string nameSpace, string moduleClass, string dataNameSpace, string domainContextClassName, string dataClassName, UILayouts currentLayOut, string sortFields, int pageSize, int loadSize, string dataAssemblyName, string specializedType, bool isMaintenanceLookUp, bool loadDetailsOnDemand, bool clearFilterAutomatically, bool removeDataToolbar, bool removeViewSwitch, bool alwaysSearchIfLookUp, bool canClear, bool canSearch, bool canAddNew, bool canEdit, bool canDelete, bool canCustomSearch, bool canPrint, bool canLayout, bool canNavigate)
        {
            //Adjust page size
            if (pageSize < 0)
                pageSize = 0;
            if (loadSize < 0)
                loadSize = 0;

            string body = ReadResourceContent(@"Linx.Builder.Resources.Templates." + (specializedType == "NoData" ? "CompositeModuleViewNoDataAuto.txt" : "CompositeModuleViewAuto.txt"));
			//Replace code elements.
			body = body.Replace("#nameSpace#", nameSpace);
			body = body.Replace("#moduleClass#", moduleClass);
			body = body.Replace("#contextClassName#", domainContextClassName);			
			body = body.Replace("#pageSize#", pageSize.ToString());
			body = body.Replace("#loadSize#", loadSize.ToString());
			body = body.Replace("#dataNameSpace#", dataNameSpace);
			body = body.Replace("#dataAssemblyName#", dataAssemblyName);
            body = body.Replace("#UserControlRef#", "<local:" + moduleClass + @"Control x:Name=""ctrlRef""/>");
                
            //sortFields
            string sortDescriptors = String.Empty;
            foreach (string sort in sortFields.Split(new char[]{ ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] parts = sort.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                    sortDescriptors += String.Format("\r\n							<riaControls:SortDescriptor PropertyPath=\"{0}\" Direction=\"{1}\" />", parts[0], (parts.Length > 1 ? parts[1] : "Ascending"));
            }
            body = body.Replace("#SortDescriptors#", sortDescriptors);
            body = body.Replace("#RemoveDataToolbar#", (removeDataToolbar ? "True" : "False"));
            body = body.Replace("#RemoveViewSwitch#", (removeViewSwitch ? "True" : "False"));


            string codeBehind = ReadResourceContent(@"Linx.Builder.Resources.Templates." + GetResourceBySpecializedType(specializedType));
			//Replace code elements.
			codeBehind = codeBehind.Replace("#nameSpace#", nameSpace);
			codeBehind = codeBehind.Replace("#moduleClass#", moduleClass);
            codeBehind = codeBehind.Replace("#moduleLayout#", "UILayouts." + currentLayOut.ToString());
			codeBehind = codeBehind.Replace("#domainContextClassName#", domainContextClassName);
			codeBehind = codeBehind.Replace("#dataClassName#", dataClassName);
			codeBehind = codeBehind.Replace("#dataNameSpace#", dataNameSpace);
            codeBehind = codeBehind.Replace("#loadDetailsOnDemand#", loadDetailsOnDemand.ToString().ToLower());
            

            //Adjust DataToolbar access
            codeBehind = codeBehind.Replace("#CanClear#", canClear.ToString().ToLower());
            codeBehind = codeBehind.Replace("#CanSearch#", canSearch.ToString().ToLower());
            codeBehind = codeBehind.Replace("#CanAddNew#", canAddNew.ToString().ToLower());
            codeBehind = codeBehind.Replace("#CanEdit#", canEdit.ToString().ToLower());
            codeBehind = codeBehind.Replace("#CanDelete#", canDelete.ToString().ToLower());
            codeBehind = codeBehind.Replace("#CanCustomSearch#", canCustomSearch.ToString().ToLower());
            codeBehind = codeBehind.Replace("#CanPrint#", canPrint.ToString().ToLower());
            codeBehind = codeBehind.Replace("#CanLayout#", canLayout.ToString().ToLower());
            codeBehind = codeBehind.Replace("#CanNavigate#", canNavigate.ToString().ToLower());

            
            if (specializedType == "IsSpecializedFilter")
            {
                codeBehind = codeBehind.Replace("#ClearAutomatically#", clearFilterAutomatically.ToString().ToLower());
            }

            if (specializedType == "IsSpecializedLookUp")            
            {
                codeBehind = codeBehind.Replace("#IsReadOnly#", (!isMaintenanceLookUp).ToString().ToLower());
                codeBehind = codeBehind.Replace("#CanAddNew#", (isMaintenanceLookUp).ToString().ToLower());
                codeBehind = codeBehind.Replace("#CanEdit#", (isMaintenanceLookUp).ToString().ToLower());
                codeBehind = codeBehind.Replace("#LookUpEmptyConditionTest#", (alwaysSearchIfLookUp ? "" : @" && !lookUpValidator.FieldValue.IsNullOrEmpty() && lookUpValidator.FieldValue.ToString() != ""%"""));
            }
			
			return new string[] { body, codeBehind };
        }

        private static string GetResourceBySpecializedType(string specializedType)
        {
            string result = String.Empty; 

            switch (specializedType)
            {
                case "None":
                    result = "CompositeModuleViewCustomCodeBehind.txt";
                    break;
                case "NoData":
                    result = "CompositeModuleViewNoDataCodeBehind.txt";
                    break;
                case "IsSpecializedFilter":
                    result = "CompositeModuleViewCustomCodeBehindForFilter.txt";
                    break;
                case "IsSpecializedLookUp":
                    result = "CompositeModuleViewCustomCodeBehindForLookUp.txt";
                    break;
                default:
                    break;
            }

            return result;
        }
        
        public static string GetMeasureAggregators(string nameSpace, string classesDefinitions)
        {               
            //Get Code Behind
            string codeBehind = ReadResourceContent(@"Linx.Builder.Resources.Templates.MeasureAggregators.txt");

            //Replace code elements.
            codeBehind = codeBehind.Replace("#nameSpace#", nameSpace);
            codeBehind = codeBehind.Replace("#classesDefinitions#", classesDefinitions);

            return codeBehind;
        }

    }
}

