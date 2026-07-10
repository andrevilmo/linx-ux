using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;


namespace Linx.Tools
{
    public delegate void SingleMethodHandler();
    public delegate void BasicMethodHandler(object sender);
    public delegate void DomainsHandler(string domain, Dictionary<string, string> domainValues);
    public delegate void EventSetParameterHandler(string name, string value);
    public delegate void MultimediaRepositoryHandler(List<MultimediaRepository> result, object userToken);
    public delegate void ChangeEditModeHandler(Boolean edit, String PaneTitle, String PaneName);

    #region Business Interfaces
    public interface IActivity
    {
        bool IsBusy { get; set; }
    }
    public interface IBusinessContextControl : IActivity
    {
        ToolbarStatus ControlStatus { get; set; }
        IEnumerable TopDataView { get; }
        void SendMessage(string message, object data, ref bool cancel);
    }
    #endregion

    #region Stream Extensions

    public static class StreamExtension
    {
        public static Byte[] ToArray(this Stream st)
        {
            byte[] buffer = new byte[st.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = st.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }

    #endregion

    #region Multimedia Class Extension

    public class MultimediaConfiguration
    {
        public Guid DocumentUID { get; set; }
        public int KeyID { get; set; }
        public Guid KeyUID { get; set; }
        public String TableName { get; set; }
        public String CheckSUMThumb { get; set; }
        public String CheckSUMContent { get; set; }
        public String TipoDocumento { get; set; }
        public String TipoExtensao { get; set; }
        public Boolean Uploaded { get; set; }
        public Boolean Saved { get; set; }
        public Boolean Deleted { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
    }

    public class MultimediaRepository
    {
        public Guid UidDocument { get; set; }
        public String NomeTabela { get; set; }
        public int IdChave { get; set; }
        public Guid UidChave { get; set; }
        public Byte[] Thumbnail { get; set; }
        public Byte[] Content { get; set; }
        public String Description { get; set; }
        public int Ordem { get; set; }
        public string Url { get; set; }
        public String TipoDocumento { get; set; }
        public String TipoExtensao { get; set; }
    }

    #endregion

    #region DataContext Suport
    public partial class DataContextSuport : INotifyPropertyChanged
    {
        private IList dataSource;
        public IList DataSource
        {
            get { return dataSource; }
            set
            {
                if (dataSource != value)
                {
                    dataSource = value;
                    this.OnPropertyChanged("DataSource");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    #endregion

    #region Linx Attributes

    [System.AttributeUsage(System.AttributeTargets.All)]
    public class FunctionalPoint : System.Attribute
    {
        private string functionName;
        public string FunctionName { get { return functionName; } set { functionName = value; } }

        public FunctionalPoint(string functionName)
        {
            this.functionName = functionName;
        }
    }
    #endregion Linx Attributes

    #region Assistant Interfaces

    /// <summary>
    /// Interface with all main module functions.
    /// </summary>
    public partial interface IApplicationMainModule
    {
        void SetActivity(bool isActive);
        IActivity GetCurrentActivity();
        void RefreshControlStatus();
        ToolbarStatus GetCurrentControlStatus();
        string GetCurrentHeader();
        String GetCurrentPaneName();
        EconomicGroup GetCurrentEconomicGroup();
         object GetCurrentDomainContext();
        IEnumerable GetCurrentData();
        void ShowGridOrDataFormLink(Boolean IsVisible);
        void GetUserInfoByName(string userName, Action<System.Nullable<Guid>, string> callback);

        void ShowSpecialSearch(Guid uidObjeto, object toolBar);
        void ShowCustomSearch(List<object> lstDataObjects);

        void CloseActivePanes();
        void UnLoad();
        string GetServerAddress();
        event ChangeEditModeHandler OnEditModeChandged;
        void InEditMode(Boolean edit);
        void SetPropertyValue(object entity, string propertyName, string parameterName);
        void SetBusinessValues(object entity);
        void ShowReportViewer(string serializedEntitySearch);
        string GetCurrentTransaction();
        void UpdateKpiInfo(KpiInfo kpi, Action action);
        void SetBusinessUidObjeto(object toolBar, string moduleName);
        //void GetParameters(Action<Dictionary<string, string>> callback, List<ParameterRequestInfo> parameters);
        string GetGlobalParameter(string parameterName);
        Dictionary<string, string> GetMediaDocumentTypes();

        //Economic Group
        List<EconomicGroup> EconomicGroups { get; }


        //Grid Size                                                 
        UInt16 GetMaxSizeGrid();
        string GetSizeGridTitle(string sizeGridKey, UInt16 position);

        //Multimedias
        event MultimediaRepositoryHandler MultimediaRepositoryResult;
        void GetMultimediaData(String TableName, int KeyId, Guid KeyUid, Guid DocumentUid, bool bringThumb, bool bringContent, object userToken);
        void SaveStructuralMultimediaByCache(Object UIorIDKeyTemp, Object UIorIDRealKey, String TableName, Action<object> callback);
        void RemoveMultimediaByKey(Object UIorIDRealKey, String TableName);
        void RemoveMultimediaByKey(Guid docId);
        void RejectChangesMultimediaByKey(Object UIorIDRealKey, String TableName, EMultimediaCancelAction CancelType);
        void UploadMultimediaContent(MultimediaConfiguration element, byte[] content, Action<MultimediaConfiguration, Exception> resultBehavior);
        void RunUploadMultimediaQueue();
        void CheckPendantMedias();

        //Report viewer
        event EventHandler GetReportParametersCompleted;
        event EventHandler GetTelerikReportListCompleted;
        event EventHandler GetRSReportListCompleted;
        void GetReportParameters();
        void GetTelerikReportList();
        void GetRSReportList();

        //Security
        void GetAspNetUser(string userName, Action<string> callback);
    }

    public partial interface IUserRegistration
    {
        void GetUser(string userName, Action<string> callback);
        void ShowChangePasswordForm(Action<bool> callback);
    }

    #endregion Assistant Interfaces

    #region Suport Classes To Mutimedias

    /// <summary>
    /// Interface for multimidia managers.
    /// </summary>
    public delegate void EventGetMultiMediaHandler(List<DataMultimedia> list);
    public delegate void EventGetTableIdHandler(int value);
    public delegate void EventContainsMultiMediaHandler(bool value);

    public partial interface IDataMultimediaManager
    {

        event EventGetMultiMediaHandler GetMultiMediaCompleted;
        event EventGetTableIdHandler GetTableIdCompleted;
        event EventContainsMultiMediaHandler ContainsMultiMediaCompleted;


        void GetMultiMediaByKey(int keyId, int tableId, EMultimediaSize sizeType, EMultimediaViewType viewType);
        void GetMultiMediaByUKey(Guid keyUid, int tableId, EMultimediaSize sizeType, EMultimediaViewType viewType);
        void GetResizedMultiMediaByKey(int keyId, int tableId, int width, int height);
        void GetResizedMultiMediaByUKey(Guid keyId, int tableId, int width, int height);
        void GetMultiMediaDocumentUID(List<Guid> documentsUid, int tableId, EMultimediaSize sizeType, EMultimediaViewType viewType);
        void GetTableIdByTableName(String tableName);
        void GetMultimidiaImageByUid(Guid keyId);

        void GetMultiMediaDocumentType(byte documentType, int keyId, int tableId);
        void AddDocument(int keyId, String tableName, SingleMethodHandler afterAdd, string info);
        void AddDocument(Guid keyUid, String tableName, SingleMethodHandler afterAdd, string info);
        void AlterDocument(int keyId, String tableName, SingleMethodHandler afterAlter, string info);
        void AlterDocument(Guid keyUid, String tableName, SingleMethodHandler afterAlter, string info);
        void Contains(String tableName);
    }


    /// <summary>
    /// Class to store multimedias.
    /// </summary>
    public partial class DataMultimedia
    {
        public Int32 TableId { get; set; }
        public System.String Description { get; set; }
        public System.IO.Stream Thumbnail { get; set; }
        public Int32 KeyId { get; set; }
        public System.IO.Stream Content { get; set; }
        public System.Guid KeyUid { get; set; }
        public Int16 Order { get; set; }
        public Byte DocumentType { get; set; }
        public System.String Url { get; set; }
        public System.Guid KeyUidDocument { get; set; }
    }

    #endregion Suport Classes To Mutimedias

    #region Assistant Structures

    /// <summary>
    /// Support for Visual State Manager By BO.
    /// </summary>
    public enum VisualStateRule
    {
        None, Collapsed, Visible, Editable, ReadOnly
    }

    /// <summary>
    /// Support for Authorization.
    /// </summary>
    public enum AuthorizationType
    {
        Query, Update, Insert, Delete
    }

    /// <summary>
    /// Structure with dynamic layouts.
    /// </summary>
    public enum UILayouts { GridLayout, ColumnsLayout, LeftGridLayout_RightColumnsLayout, TopGridLayout_BottomColumnsLayout, RightGridLayout_LeftColumnsLayout, BottomGridLayout_TopColumnsLayout, Default }

    /// <summary>
    /// Structure with button actions.
    /// </summary>
    public enum ActionButton
    {
        New, Edit, Delete, Save, Cancel, Clear, Search, Print, Refresh, First, Prior, Next, Last, SpecialSearch, FirstPage, PriorPage, NextPage, LastPage
    }

    /// <summary>
    /// Structure with toolbar status.
    /// </summary>
    public enum ToolbarStatus
    {
        None, Editing, Searching, Clearing, Validating
    }

    public enum EMultimediaSize
    {
        Both, Original, Thumbnail, MultimediaUidOnly
    };

    public enum EMultimediaViewType
    {
        Default, FirstOnly, AllCollection
    };

    public enum EMultimediaCancelAction
    {
        Added, Updated, Deleted
    };

    public enum ReportType
    {
        Telerik, ReportingServices
    };

    /// <summary>
    /// Informations of one control category.
    /// </summary>
    public struct ControlCategoryInfo
    {
        public string Name;
        public string Type;
        public string Prefix;

        public ControlCategoryInfo(string name, string type, string prefix)
        {
            Name = name;
            Type = type;
            Prefix = prefix;
        }
    }

    #endregion Assistant Structures

    #region String Extender

    /// <summary>
    /// Author: Alessandro Araújo
    /// Date: 19/08/2008
    /// Class Description: 
    ///     Class that extends the String class with others special methods.
    /// </summary>
    public static class StringExtension
    {

        public static string RemoveNullDefinition(this string dataType)
        {
            if (!dataType.IsNullOrEmpty())
            {
                if (dataType.Contains("Nullable<"))
                    dataType = dataType.Extract("Nullable<", ">");
                if (dataType.Contains("?"))
                    dataType = dataType.Replace("?", "");
                dataType = ("." + dataType).Right(".").ToLower();
            }

            return dataType;
        }

        public static string GetRelativePath(this string path1, string path2)
        {
            string result = String.Empty;
            string intersection = path1.GetIntersectionPath(path2);

            if (!intersection.IsNullOrEmpty())
            {
                string relative1 = (path1.Length > intersection.Length ? path1.Right(path1.Length - intersection.Length).Trim() : "");
                string relative2 = (path2.Length > intersection.Length ? path2.Right(path2.Length - intersection.Length).Trim() : "");
                if (relative1.Length > 0 && relative1.Right(1)[0] == Path.DirectorySeparatorChar)
                    relative1 = relative1.Left(relative1.Length - 1);
                if (relative2.Length > 0 && relative2.Right(1)[0] == Path.DirectorySeparatorChar)
                    relative2 = relative2.Left(relative2.Length - 1);

                for (int index = 0; index <= relative1.Occurs(Path.DirectorySeparatorChar.ToString()); index++)
                {
                    result = Path.Combine(result, "..");
                }

                result = Path.Combine(result, relative2);
            }
            return result;
        }

        public static string GetIntersectionPath(this string path1, string path2)
        {
            string intersection = String.Empty;

            if (!path1.IsNullOrEmpty() && !path2.IsNullOrEmpty())
            {
                string[] path1Parts = path1.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                string[] path2Parts = path2.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

                for (int index = 0; index < path1Parts.Length; index++)
                {
                    if (index < path2Parts.Length && path1Parts[index].ToUpper() == path2Parts[index].ToUpper())
                        intersection += path1Parts[index] + "\\";
                    else
                        break;
                }
            }

            return intersection;
        }


        [DebuggerStepThrough]
        public static string ToCamelCase(this string value)
        {
            if (!value.IsNullOrEmpty())
                return value.Left(1).ToLower() + value.Right(value.Length - 1);
            else
                return value;
        }

        public static string ConvertToString(this Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                stream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        public static string ToUrlMediaThumb(this string value, int applicativeId, int usabilityId)
        {
            return value.IsNullOrEmpty() ? value : value.Replace("-media/", String.Format("-thumb/{0}/{1}/", applicativeId, usabilityId));
        }

        public static Stream ConvertToStream(this string source)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(source);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static Dictionary<string, string> ConvertToDictionary(string valueFlat, string delimiter = ",", string startDictionary = "[", string delimiterDictionary = ":", string endDictionary = "]")
        {
            var list = new Dictionary<string, string>();
            var splittedData = valueFlat.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var _item in splittedData)
            {
                var item = !string.IsNullOrEmpty(startDictionary) && !string.IsNullOrEmpty(endDictionary) ? _item.Extract(startDictionary, endDictionary) : _item;
                var item_splitted = item.Split(new string[] { delimiterDictionary }, StringSplitOptions.RemoveEmptyEntries);

                list.Add(item_splitted[0], item_splitted[1]);
            }

            return list;
        }

        /// <summary>
        /// Special trim to remove this chars: "\r", "\n", "\t", " "
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SpecialTrim(this string value)
        {
            if (value.IsNullOrEmpty())
                return "";

            int startPosition = 0, endPosition = value.Length - 1;
            for (int position = 0; position < value.Length; position++)
            {
                if (value[position].ToString().InList("\r", "\n", "\t", " "))
                    startPosition++;
                else
                    break;
            }

            for (int position = (value.Length - 1); position >= 0; position--)
            {
                if (value[position].ToString().InList("\r", "\n", "\t", " "))
                    endPosition--;
                else
                    break;
            }

            if (endPosition <= startPosition)
                return "";
            else
                return value.Substring(startPosition, (endPosition - startPosition + 1));
        }

        /// <summary>
        /// Transalate this string value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string Translate(this string value)
        {
            return value;
        }

        /// <summary>
        /// Transalate this string value.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="culture">The Culture</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string Translate(this string value, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        /// <summary>
        /// If the first char is alpha.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string value)
        {
            if (value.IsNullOrEmpty())
                return false;

            for (int position = 0; position < value.Length; position++)
            {
                if (!(value[position].ToString() == "-" || value[position].ToString() == "]" ||
                    System.Text.RegularExpressions.Regex.IsMatch(value[position].ToString(), "^[A-Za-z]$") ||
                    System.Text.RegularExpressions.Regex.IsMatch(value[position].ToString(), @"^[,.\/""'|[^~`´!@#$%¨&*)(_+=   °ºª§]$çÇ") ||
                    System.Text.RegularExpressions.Regex.IsMatch(value[position].ToString(), "^[0-9]$")
                    ))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// If the first char is numeric.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string value)
        {
            if (value.IsNullOrEmpty())
                return false;

            for (int position = 0; position < value.Length; position++)
            {
                if (position == 0 && value[position].ToString().InList("-", "+"))
                    continue;

                if (value[position].ToString().InList(",", "."))
                    continue;

                if (!System.Text.RegularExpressions.Regex.IsMatch(value[position].ToString(), "^[0-9]$"))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Conver String to MemoryStream.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MemoryStream Base64ToStream(this string value)
        {
            //Create MemoryStream            
            byte[] buffer = Convert.FromBase64String(value);

            return new MemoryStream(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Get characters on the left
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string Left(this string value, int length)
        {
            if (length < 0)
                return "";
            else
                return value.Substring(0, length);
        }

        /// <summary>
        /// Get characters on the left of search parameter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string Left(this string value, string search)
        {
            int length = value.IndexOf(search);

            if (length < 0)
                return "";
            else
                return value.Substring(0, length);
        }


        /// <summary>
        /// Get characters on the right
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string Right(this string value, int length)
        {
            if (value.Length <= length)
                return "";
            else
                return value.Substring(value.Length - length);
        }

        /// <summary>
        /// Get characters on the right of search parameter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string Right(this string value, string search)
        {
            int length = -1;
            while (value.IndexOf(search, length + 1) >= 0)
                length = value.IndexOf(search, length + 1);


            if ((length < 0) || ((length + search.Length) >= value.Length))
                return "";
            else
                return value.Substring(length + search.Length);
        }

        /// <summary>
        /// How many times one expression occurs 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="searchExpression"></param>
        /// <returns></returns>
        public static int Occurs(this string value, string searchExpression)
        {
            int occurs = 0, indexPosition = 0;

            indexPosition = value.IndexOf(searchExpression, indexPosition, StringComparison.CurrentCultureIgnoreCase);
            while (indexPosition >= 0)
            {
                occurs++;
                indexPosition = value.IndexOf(searchExpression, indexPosition + searchExpression.Length, StringComparison.CurrentCultureIgnoreCase);
            }

            return occurs;
        }

        /// <summary>
        /// Return the index of expression on that occurrence
        /// </summary>
        /// <param name="value"></param>
        /// <param name="searchExpression"></param>
        /// <returns></returns>
        public static int IndexOfByOccurs(this string value, string searchExpression, int occurrence)
        {
            int occurs = 0, indexPosition = 0;
            bool generated = false;

            indexPosition = value.IndexOf(searchExpression, indexPosition, StringComparison.CurrentCultureIgnoreCase);
            while (indexPosition >= 0)
            {
                occurs++;

                if (occurs == occurrence)
                {
                    generated = true;
                    break;
                }

                indexPosition = value.IndexOf(searchExpression, indexPosition + searchExpression.Length, StringComparison.CurrentCultureIgnoreCase);
            }

            return (generated ? indexPosition : -1);
        }

        /// <summary>
        /// Extract the expression between one value and other
        /// </summary>
        /// <param name="value"></param>
        /// <param name="searchBegin"></param>
        /// <param name="searchEnd"></param>
        /// <returns></returns>
        public static string Extract(this string value, string searchBegin, string searchEnd)
        {
            int indexStart, indexEnd;

            indexStart = value.IndexOf(searchBegin, StringComparison.CurrentCultureIgnoreCase);

            if (indexStart < 0)
                return "";

            indexEnd = value.IndexOf(searchEnd, indexStart + searchBegin.Length, StringComparison.CurrentCultureIgnoreCase);

            if (indexEnd < 0)
                return "";

            if (!(indexStart >= 0 && indexEnd >= 0 && indexEnd > indexStart))
                return "";

            return value.Substring(indexStart + searchBegin.Length, indexEnd - indexStart - searchBegin.Length);

        }

        /// <summary>
        /// Extract the expression between one value and other, considering the occurrence
        /// </summary>
        /// <param name="value"></param>
        /// <param name="searchBegin"></param>
        /// <param name="searchEnd"></param>
        /// <returns></returns>
        public static string Extract(this string value, string searchBegin, string searchEnd, int occurrence)
        {
            int indexStart, indexEnd;
            int occurs = 0;
            string result = "";

            indexStart = value.IndexOf(searchBegin, StringComparison.CurrentCultureIgnoreCase);

            if (indexStart < 0)
                return "";

            indexEnd = value.IndexOf(searchEnd, indexStart + searchBegin.Length, StringComparison.CurrentCultureIgnoreCase);

            if (indexEnd < 0)
                return "";

            while ((indexStart >= 0 && indexEnd >= 0 && indexEnd > indexStart))
            {
                occurs++;
                if (occurrence == occurs)
                {
                    result = value.Substring(indexStart + searchBegin.Length, indexEnd - indexStart - searchBegin.Length);
                    break;
                }

                indexStart = value.IndexOf(searchBegin, indexEnd + searchEnd.Length, StringComparison.CurrentCultureIgnoreCase);

                if (indexStart < 0)
                    break;

                indexEnd = value.IndexOf(searchEnd, indexStart + searchBegin.Length, StringComparison.CurrentCultureIgnoreCase);

                if (indexEnd < 0)
                    break;
            }

            return result;

        }

        /// <summary>
        /// Indicates if the value is contained in a list
        /// </summary>
        /// <param name="value"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool InList(this string value, params string[] list)
        {
            foreach (string element in list)
            {
                if (value == element)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Proper the words of one string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Proper(this string value)
        {
            string properRerurn = "";

            if (value.Equals(value.ToUpper(), StringComparison.CurrentCulture) || value.Equals(value.ToLower(), StringComparison.CurrentCulture))
            {
                for (int index = 0; index < value.Length; index++)
                {
                    if (index == 0 || value[index - 1] == ' ' || value[index - 1] == '_')
                        properRerurn = properRerurn + value[index].ToString().ToUpper();
                    else
                        properRerurn = properRerurn + value[index].ToString().ToLower();
                }
            }
            else
                properRerurn = value;


            return properRerurn;
        }

        public static string PrepareName(this string s)
        {
            return s.Replace("_", " ").Proper().Replace(" ", "").RemoveDiacritics();
        }

        /// <summary>
        /// Remove Diacritics
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String RemoveDiacritics(this String s)
        {
            if (s == null) return s;
            String normalizedString = s.Normalize(System.Text.NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Decode Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string DecodeUrlString(this string url, bool recursive = false)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url && recursive)
                url = newUrl;
            return newUrl;
        }
    }

    #endregion String Extender

    #region Property Definitions
    public class PropertyDefinitions
    {
        private string name = String.Empty;
        public string Name { get { return name; } set { name = value; } }
        private string caption = String.Empty;
        public string Caption { get { return caption; } set { caption = value; } }
        private string group = String.Empty;
        public string Group { get { return group; } set { group = value; } }
        private string dataType = String.Empty;
        public string DataType { get { return dataType; } set { dataType = value; } }
        private string fullDataType = String.Empty;
        public string FullDataType { get { return fullDataType; } set { fullDataType = value; } }
        private string objectClass = String.Empty;
        public string ObjectClass { get { return objectClass; } set { objectClass = value; } }
        private string connectedField = String.Empty;
        public string ConnectedField { get { return connectedField; } set { connectedField = value; } }
        private string validationValues = String.Empty;
        public string ValidationValues { get { return validationValues; } set { validationValues = value; } }
        private string defaultValue = String.Empty;
        public string DefaultValue { get { return defaultValue; } set { defaultValue = value; } }
        private string domain = String.Empty;
        public string Domain { get { return domain; } set { domain = value; } }
        private string kpiName = String.Empty;
        public string KpiName { get { return kpiName; } set { kpiName = value; } }
        private string kpiRelatedAttribute = String.Empty;
        public string KpiRelatedAttribute { get { return kpiRelatedAttribute; } set { kpiRelatedAttribute = value; } }
        private string recordTypeName = String.Empty;
        public string RecordTypeName { get { return recordTypeName; } set { recordTypeName = value; } }
        private string aggregationFunction = String.Empty;
        public string AggregationFunction { get { return aggregationFunction; } set { aggregationFunction = value; } }
        private string filterDataKey = String.Empty;
        public string FilterDataKey { get { return filterDataKey; } set { filterDataKey = value; } }
        public bool IsPK { get; set; }

        public int Order { get; set; }
        public string Precision { get; set; }
        public bool IsEditableData { get; set; }
        public bool IsBrowsable { get; set; }
        public bool IsPrincipal { get; set; }
        public string Mask { get; set; }
        public string MaskType { get; set; }
        public string DataFormat { get; set; }
        public bool IsMeasure { get; set; }


        //Lookup Binding Configuration
        public string LookUpName { get; set; }
        public string LookUpTitle { get; set; }
        public string LookUpQuery { get; set; } //{data}.executeLookUpProduto]
        public string LookUpDisplayColumns { get; set; } //{ IdProduto : 'Id', NomeProduto : 'Produto' }
        public string LookUpColumns { get; set; } //{ IdProduto: true, NomeProduto : true }
        public string LookUpFinalize { get; set; } //{data}..finalizeLookUpProduto
        //End Lookup Binding Configuration


        public PropertyDefinitions()
            : base()
        {
        }

        public int GetPrecisionDecimals()
        {
            int result = 0;

            if (!this.Precision.IsNullOrEmpty())
            {
                if (this.Precision.Contains(":"))
                    result = int.Parse(this.Precision.Right(":"));
                else
                {
                    decimal precision = (!this.Precision.IsNullOrEmpty() ? decimal.Parse(this.Precision) / 10 : 0);
                    result = (int)(10 * (precision - ((int)precision)));
                }
            }

            return result;
        }

        public PropertyDefinitions(string name, string caption, int order, string group, string dataType, string precision, bool isEditableData, bool isBrowsable, string objectClass, string connectedField, string validationValues, string defaultValue, string domain, string recordTypeName)
        {
            this.SetProperties(name, caption, order, group, dataType, precision, isEditableData, isBrowsable, objectClass, connectedField, validationValues, defaultValue, domain, recordTypeName, "", false);
        }

        public PropertyDefinitions(string name, string caption, int order, string group, string dataType, string precision, bool isEditableData, bool isBrowsable, string objectClass, string connectedField, string validationValues, string defaultValue, string domain, string recordTypeName, string aggregationFunction, bool isPrincipal)
        {
            this.SetProperties(name, caption, order, group, dataType, precision, isEditableData, isBrowsable, objectClass, connectedField, validationValues, defaultValue, domain, recordTypeName, aggregationFunction, isPrincipal);
        }

        private void SetProperties(string name, string caption, int order, string group, string dataType, string precision, bool isEditableData, bool isBrowsable, string objectClass, string connectedField, string validationValues, string defaultValue, string domain, string recordTypeName, string aggregationFunction, bool isPrincipal)
        {
            Name = name;
            Caption = caption;
            Order = order;
            Group = group;
            DataType = dataType;
            Precision = precision;
            IsEditableData = isEditableData;
            IsBrowsable = isBrowsable;
            ObjectClass = objectClass;
            ConnectedField = connectedField;
            ValidationValues = validationValues;
            DefaultValue = defaultValue;
            Domain = domain;
            RecordTypeName = recordTypeName;
            IsPrincipal = isPrincipal;
        }

        public override string ToString()
        {
            return string.Format("[{0}]: {1}", name, caption);
        }
    }
    #endregion

    #region Object Extension
    /// <summary>
    /// Author: Alessandro Araújo
    /// Date: 22/08/2008
    /// Class Description: 
    ///     Extensions method for the object class.
    /// </summary>
    public static class ObjectExtension
    {
        public static bool IsILinx(this PropertyInfo pInfo)
        {
            return pInfo.DeclaringType.IsILinx(pInfo.Name);
        }

        public static bool IsILinx(this Type type, string propertyName)
        {
            return propertyName == "ID_LINX" && type.GetInterfaces().Any(e => e.Name == "ILinx");
        }

        public static bool IsIGpecon(this PropertyInfo pInfo)
        {
            return pInfo.DeclaringType.IsIGpecon(pInfo.Name);
        }

        public static bool IsIGpecon(this Type type, string propertyName)
        {
            return propertyName == "ID_GPECON" && type.GetInterfaces().Any(e => e.Name == "IGpecon");
        }

        public static double GetValue(this double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                return 0D;

            return value;
        }
        /// <summary>
        /// Validate the value is not null and contained in the Dictionary
        /// </summary>
        /// <param name="pdicValidater">Validater Dictionary</param>
        /// <param name="pstrValue">The string to be validated</param>
        /// <returns>Indicate if the value is contained in the Dictionary</returns>
        public static bool Validate(this Dictionary<string, string> value, string search)
        {
            bool validated = true;

            if (string.IsNullOrWhiteSpace(search))
                validated = false;
            else if (!value.ContainsKey(search))
                validated = false;

            return validated;
        }

        /// <summary>
        /// Throw an exception with environment informations.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="connectionEnv"></param>
        /// <param name="connectionReal"></param>
        /// <param name="idLinx"></param>
        /// <param name="idGpecon"></param>
        /// <param name="securityHelper"></param>
        /// <returns></returns>
        public static String ThrowConnectionInfo(this Exception source, string connectionName, string bmName, string connectionEnv, string connectionReal, int idLinx, int idGpecon, ISecurityHelper securityHelper)
        {
            string message = source.GetCompleteMessage();
            message += "\r\nBM Name: " + bmName;
            message += "\r\nConnection Environment: " + connectionEnv;
            message += "\r\nConnection Real: " + connectionReal;
            message += "\r\nEnvironment details: ";
            message += "\r\nIdLinx: " + (idLinx == 0 && securityHelper != null ? securityHelper.GetCurrentIdLinx(connectionName) : idLinx);
            message += "\r\nIdGpecon: " + (idGpecon == 0 && securityHelper != null ? securityHelper.GetCurrentIdGpecon(null) : idGpecon);
            if (securityHelper != null)
            {
                message += "\r\nCurrentUserName: " + securityHelper.GetCurrentUserName();
                message += "\r\nCurrentEnvironmentId: " + securityHelper.GetCurrentEnvironmentId();
                message += "\r\nCurrentAccessGroupId: " + securityHelper.GetCurrentAccessGroupId();
                message += "\r\nCurrentApplicationId: " + securityHelper.GetCurrentApplicationId();
                message += "\r\nApplicativeId: " + securityHelper.GetCurrentApplicativeId();
                message += "\r\nTransactionInfo: " + securityHelper.GetTransactionInfo();

                var relatedEnvInfo = securityHelper.GetRelatedEnvironmentInfo();
                if (relatedEnvInfo != null && relatedEnvInfo.Count > 0)
                {
                    message += "\r\nRelated Environment Info: ";
                    foreach (var relatedEnv in relatedEnvInfo)
                        message += "\r\n" + relatedEnv.Key + ": " + relatedEnv.Value;
                }
            }

            throw new Exception(message);
        }

        /// <summary>
        /// Concatenate all messages
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startMessage"></param>
        /// <returns></returns>
        public static String GetCompleteMessage(this Exception source, string startMessage = null)
        {
            String errorResult = String.Empty;
            foreach (string message in source.GetMessages(startMessage))
            {
                errorResult += (errorResult.IsNullOrEmpty() ? String.Empty : "\r\n") + message;
            }

            return errorResult;
        }

        /// <summary>
        /// Get all messages as a list
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startMessage"></param>
        /// <returns></returns>
        public static List<string> GetMessages(this Exception source, string startMessage = null)
        {
            List<string> messages = new List<string>();

            if (!startMessage.IsNullOrEmpty())
                messages.Add(startMessage);

            Action<Exception> concat = null;
            concat = (e) =>
            {
                if (e != null)
                {
                    messages.Add(e.Message.Replace("An error occurred while executing the command definition.", "").Replace("See the inner exception for details.", "").TrimEnd());
                    concat(e.InnerException);
                }
            };

            concat(source);

            return messages;
        }


        /// <summary>
        /// Get all properties marked with KeyAttribute mark.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetKeyProperties(Type type)
        {
            List<string> keys = new List<string>();

            foreach (var prop in ObjectExtension.GetFunctionalPoints(type, String.Empty, true, false))
            {
                if (Linx.Tools.ObjectExtension.ExistsAttributeOnProperty(type, prop.Name, typeof(KeyAttribute)))
                    keys.Add(prop.Name);
            }

            return keys;
        }

        /// <summary>
        /// Test the type of a reference by string. 
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsTypeOf(this object reference, string typeName)
        {
            return (
                        reference.GetType().Name == typeName ||
                        reference.GetType().GetTypeInfo().BaseType.Name == typeName ||
                        reference.GetType().GetInterfaces().Where(e => e.Name == typeName).Count() > 0
                    );
        }

        /// <summary>
        /// Test the base type
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBaseTypeOf(this Type reference, Type type)
        {
            Type baseType = type.GetTypeInfo().BaseType;
            while (baseType != null && baseType.Name != "Object")
            {
                if (reference.FullName == baseType.FullName)
                    return true;

                baseType = baseType.GetTypeInfo().BaseType;
            }

            return false;
        }


        /// <summary>
        /// Test if reference is not null or empty, if not null executes func isNotNullAction, else print text isNullText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        /// <param name="isNotNullAction"></param>
        /// <param name="isNullText"></param>
        /// <returns></returns>
        public static string IifIsNotNull<T>(this T reference, Func<T, string> isNotNullAction, string isNullText = "")
        {
            return !reference.IsNullOrEmpty() ? isNotNullAction(reference) : isNullText;
        }

        // <summary>
        /// Returns whether int contains any of the specified keywords as a value.
        /// </summary>
        /// <param name="value">Value of the String</param>
        /// <param name="keywords">Array with keywords</param>
        /// <returns>True when value contains any of the specified keywords</returns>
        public static bool In<T>(this T value, params T[] keywords) where T : struct
        {
            return keywords.Any((s) => value.Equals(s));
        }

        /// <summary>
        /// Cloner by memory 
        /// </summary>
        /// <param name="entityObject"></param>
        /// <returns></returns>
        public static object CloneSerializing(this object entityObject)
        {
            object clonedObject;
            try
            {
                DataContractSerializer datContractSer = new DataContractSerializer(entityObject.GetType());
                MemoryStream memoryStream = new MemoryStream();
                datContractSer.WriteObject(memoryStream, entityObject);
                memoryStream.Position = 0;
                clonedObject = (object)datContractSer.ReadObject(memoryStream);
            }
            catch
            {
                clonedObject = null;
            }
            return clonedObject;
        }


        /// <summary>
        /// Get control categories.
        /// </summary>
        /// <param name="assembyName"></param>
        /// <returns></returns>
        public static List<ControlCategoryInfo> GetControlCategories(string assembyName)
        {
            return GetControlCategories(assembyName, "");
        }

        /// <summary>
        /// Get control categories.
        /// </summary>
        /// <param name="assembyName"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static List<ControlCategoryInfo> GetControlCategories(string assembyName, string controlName)
        {
            List<ControlCategoryInfo> listInfo = new List<ControlCategoryInfo>();

            try
            {
                System.ComponentModel.CategoryAttribute attribute;
                Assembly assembly = Assembly.Load(new AssemblyName(assembyName));
                Type[] types = (controlName.IsNullOrEmpty() ? assembly.GetTypes() : assembly.GetTypes().Where(e => e.Name == controlName).ToArray());
                foreach (Type item in types)
                {
                    var listCategory = item.GetTypeInfo().GetCustomAttributes(typeof(System.ComponentModel.CategoryAttribute), false).ToList();
                    if (listCategory.Count > 0)
                    {
                        attribute = listCategory[0] as System.ComponentModel.CategoryAttribute;
                        if (attribute != null)
                            listInfo.Add(new ControlCategoryInfo(item.Name, attribute.Category.Extract("Type(", ")"), attribute.Category.Extract("Prefix(", ")")));
                    }
                }
            }
            catch { }

            return listInfo;
        }

        public static string GetFunctionalPointOfType(Type type, string functionName)
        {
            string funcContent = "";

            foreach (var attr in type.GetTypeInfo().GetCustomAttributes(typeof(FunctionalPoint), false))
            {
                funcContent = ((string)attr.GetPropertyValue("FunctionName")).Extract(functionName + "[", "]");
            }

            return funcContent;
        }

        public static List<PropertyDefinitions> GetFunctionalPoints(Type type, bool getExtendedFilters = false, bool forceAll = false)
        {
            return GetFunctionalPoints(type, "", forceAll, getExtendedFilters);
        }

        public static Type GetElement(this Type type)
        {
            Type elementType = null;

            elementType = type.GetGenericArguments().FirstOrDefault();

            return elementType;
        }

        public static object GetPropertyOfAttributeType(Type type, string propertyName, Type attributeType, string attributePropertyName)
        {
            var member = type.GetProperties().Where(e => e.Name == propertyName).FirstOrDefault();
            if (member != null)
            {
                foreach (var attribute in member.GetCustomAttributes(attributeType, true))
                {
                    return attribute.GetPropertyValue(attributePropertyName);
                }
            }
            return null;
        }

        public static object GetPropertyOfAttributeType(PropertyInfo member, Type attributeType, string attributePropertyName)
        {
            if (member != null)
            {
                foreach (var attribute in member.GetCustomAttributes(attributeType, true))
                {
                    return attribute.GetPropertyValue(attributePropertyName);
                }
            }
            return null;
        }

        public static bool ExistsAttributeOnProperty(Type type, string propertyName, Type attributeType)
        {
            var member = type.GetProperties().Where(e => e.Name == propertyName).FirstOrDefault();
            if (!member.IsNull())
                return (member.GetCustomAttributes(attributeType, true).Count() > 0);
            else
                return false;
        }

        public static bool ExistsAttributeOnType(Type type, Type attributeType)
        {
            return (type.GetTypeInfo().GetCustomAttributes(attributeType, true).Count() > 0);
        }

        public static List<PropertyDefinitions> GetFunctionalPoints(Type type, string propertyName, bool getExtendedFilters = false)
        {
            return GetFunctionalPoints(type, propertyName, false, getExtendedFilters);
        }

        public static List<PropertyDefinitions> GetFunctionalPoints(Type type, string propertyName, bool forceAll, bool getExtendedFilters = false)
        {
            List<PropertyDefinitions> dependences = new List<PropertyDefinitions>();
            List<PropertyDefinitions> result = new List<PropertyDefinitions>();
            string fPoint, dataType;
            PropertyDefinitions propDef;
            object value;

            List<string> baseNames = new List<string>();
            Type baseType = type.GetTypeInfo().BaseType;
            while (baseType != null && baseType.Name != "Entity")
            {
                baseNames.Add(baseType.Name);
                baseType = baseType.GetTypeInfo().BaseType;
            }

            foreach (PropertyInfo member in (propertyName.IsNullOrEmpty() ? type.GetProperties().Where(e => e.DeclaringType.Name == type.Name || baseNames.Contains(e.DeclaringType.Name)).ToArray() : new PropertyInfo[] { type.GetProperty(propertyName) }))
            {
                if (member.IsNullOrEmpty())
                    continue;

                //Get Data Type
                if (member.PropertyType.Name == "Nullable`1")
                    dataType = member.PropertyType.FullName.Extract("System.Nullable`1[[System.", ",");
                else
                    dataType = member.PropertyType.Name;

                if (dataType.IsNullOrEmpty())
                    continue;

                value = GetPropertyOfAttributeType(member, typeof(DisplayAttribute), "AutoGenerateField");
                if (value.IsNull())
                    continue;

                propDef = new PropertyDefinitions();
                propDef.Name = member.Name;
                propDef.DataType = dataType;
                propDef.FullDataType = member.PropertyType.FullName;
                propDef.Caption = propDef.Name;
                propDef.IsBrowsable = (bool)value;
                propDef.IsPK = Linx.Tools.ObjectExtension.ExistsAttributeOnProperty(type, propDef.Name, typeof(KeyAttribute));


                if (!forceAll && !propDef.IsBrowsable && propertyName.IsNullOrEmpty())
                    continue;

                value = GetPropertyOfAttributeType(member, typeof(EditableAttribute), "AllowEdit");
                if (!value.IsNull())
                    propDef.IsEditableData = (bool)value;

                value = GetPropertyOfAttributeType(member, typeof(DisplayAttribute), "Name");
                if (!value.IsNullOrEmpty())
                    propDef.Caption = (string)value;

                value = GetPropertyOfAttributeType(member, typeof(DisplayAttribute), "GroupName");
                if (!value.IsNull())
                    propDef.Group = (string)value;

                value = GetPropertyOfAttributeType(member, typeof(DisplayAttribute), "Order");
                if (!value.IsNull())
                    propDef.Order = (int)value;


                //Get functional point
                value = (GetPropertyOfAttributeType(member, typeof(FunctionalPoint), "FunctionName") as string);
                if (!value.IsNullOrEmpty())
                {
                    fPoint = (string)value;
                    propDef.DefaultValue = fPoint.Extract("DefaultValue[", "]");
                    propDef.Domain = fPoint.Extract("DomainName[", "]");
                    propDef.KpiName = fPoint.Extract("KpiName[", "]");
                    propDef.KpiRelatedAttribute = fPoint.Extract("KpiRelatedAttribute[", "]");
                    propDef.ObjectClass = fPoint.Extract("ObjectClass[", "]");
                    propDef.AggregationFunction = fPoint.Extract("AggregationFunction[", "]");
                    if (!fPoint.Extract("Precision[", "]").IsNullOrEmpty())
                        propDef.Precision = fPoint.Extract("Precision[", "]");
                    if (!propDef.ObjectClass.IsNullOrEmpty() && propDef.ObjectClass.Length >= 4 && propDef.ObjectClass.Left(4) != "Linx") //Add Linx prefix
                        propDef.ObjectClass = "Linx" + propDef.ObjectClass;
                    propDef.ConnectedField = fPoint.Extract("ConnectedField[", "]");
                    if (!fPoint.Extract("IsEditable[", "]").IsNullOrEmpty())
                        propDef.IsEditableData = bool.Parse(fPoint.Extract("IsEditable[", "]"));
                    propDef.FilterDataKey = fPoint.Extract("FilterDataKey[", "]");
                    if (propDef.FilterDataKey.IsNullOrEmpty())
                        propDef.FilterDataKey = propDef.Name;
                    propDef.Mask = fPoint.Extract("Mask[", "]");
                    propDef.MaskType = fPoint.Extract("MaskType[", "]");
                    propDef.DataFormat = fPoint.Extract("DataFormatString[", "]");
                    propDef.IsMeasure = fPoint.Extract("IsMeasure[", "]") == "true";

                    //Lookup Binding Configuration
                    propDef.LookUpName = fPoint.Extract("LookUpName[", "]");
                    propDef.LookUpTitle = fPoint.Extract("LookUpTitle[", "]");
                    propDef.LookUpQuery = fPoint.Extract("LookUpQuery[", "]");
                    propDef.LookUpDisplayColumns = fPoint.Extract("LookUpDisplayColumns[", "]");
                    propDef.LookUpColumns = fPoint.Extract("LookUpColumns[", "]");
                    propDef.LookUpFinalize = fPoint.Extract("LookUpFinalize[", "]");
                    //End Lookup Binding Configuration

                }

                dependences.Add(propDef);
            }

            //Add to result ordered
            result.AddRange(dependences.OrderBy(e => e.Caption));

            if (getExtendedFilters)
            {
                MethodInfo methodInfo = type.GetMethod("GetExtendedFilterDefinitions", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                if (!methodInfo.IsNull())
                {
                    List<PropertyDefinitions> extFilters = methodInfo.Invoke(null, new object[] { }) as List<PropertyDefinitions>;
                    if (extFilters != null)
                        result.AddRange(extFilters.Where(e => dependences.Where(d => d.FilterDataKey == e.FilterDataKey).Count() == 0).OrderBy(e => e.Caption));
                }
            }

            return result;
        }


        public static bool IsBrowsable(Type type, string propertyName)
        {
            bool isBrowsable = true;

            var fPoints = GetFunctionalPoints(type, propertyName);
            if (fPoints.Count > 0)
                isBrowsable = fPoints[0].IsBrowsable;

            return isBrowsable;
        }

        [DebuggerStepThrough]
        public static bool IsNullOrEmpty(this object instance)
        {
            bool isNullOrEmpty = false;

            try
            {
                if (instance == null)
                    isNullOrEmpty = true;
                else
                {
                    string typeName = instance.GetType().Name.ToLower();
                    switch (typeName)
                    {
                        case "string":
                            isNullOrEmpty = String.IsNullOrEmpty(((String)instance));
                            break;
                        case "char":
                            isNullOrEmpty = (!((System.Nullable<char>)instance).HasValue || ((char)instance) == ' ');
                            break;
                        case "byte":
                            isNullOrEmpty = (!((System.Nullable<byte>)instance).HasValue || ((byte)instance) == 0);
                            break;
                        case "int16":
                            isNullOrEmpty = (!((System.Nullable<System.Int16>)instance).HasValue || ((System.Int16)instance) == 0);
                            break;
                        case "int32":
                            isNullOrEmpty = (!((System.Nullable<System.Int32>)instance).HasValue || ((System.Int32)instance) == 0);
                            break;
                        case "int64":
                            isNullOrEmpty = (!((System.Nullable<System.Int64>)instance).HasValue || ((System.Int64)instance) == 0);
                            break;
                        case "sbyte":
                            isNullOrEmpty = (!((System.Nullable<sbyte>)instance).HasValue || ((sbyte)instance) == 0);
                            break;
                        case "uint16":
                            isNullOrEmpty = (!((System.Nullable<System.UInt16>)instance).HasValue || ((System.UInt16)instance) == 0);
                            break;
                        case "uint32":
                            isNullOrEmpty = (!((System.Nullable<System.UInt32>)instance).HasValue || ((System.UInt32)instance) == 0);
                            break;
                        case "uint64":
                            isNullOrEmpty = (!((System.Nullable<System.UInt64>)instance).HasValue || ((System.UInt64)instance) == 0);
                            break;
                        case "single":
                            isNullOrEmpty = (!((System.Nullable<System.Single>)instance).HasValue || ((System.Single)instance) == 0);
                            break;
                        case "double":
                            isNullOrEmpty = (!((System.Nullable<System.Double>)instance).HasValue || ((System.Double)instance) == 0);
                            break;
                        case "decimal":
                            isNullOrEmpty = (!((System.Nullable<System.Decimal>)instance).HasValue || ((System.Decimal)instance) == 0);
                            break;
                        case "datetime":
                            isNullOrEmpty = (!((System.Nullable<DateTime>)instance).HasValue || ((DateTime)instance) == (new DateTime()));
                            break;
                        case "guid":
                            isNullOrEmpty = (!((System.Nullable<Guid>)instance).HasValue || ((Guid)instance) == Guid.Empty);
                            break;
                        case "bool":
                            isNullOrEmpty = (!((System.Nullable<bool>)instance).HasValue || ((bool)instance) == false);
                            break;
                        case "boolean":
                            isNullOrEmpty = (!((System.Nullable<bool>)instance).HasValue || ((bool)instance) == false);
                            break;
                        default:
                            break;
                    }
                }

            }
            catch
            {
                isNullOrEmpty = false;
            }

            return isNullOrEmpty;
        }

        [DebuggerStepThrough]
        public static bool IsNull(this object instance)
        {
            bool isNull = false;

            try
            {
                if (instance == null)
                    isNull = true;
                else
                {
                    string typeName = instance.GetType().Name.ToLower();
                    switch (typeName)
                    {
                        case "char":
                            isNull = (!((System.Nullable<char>)instance).HasValue);
                            break;
                        case "byte":
                            isNull = (!((System.Nullable<byte>)instance).HasValue);
                            break;
                        case "int16":
                            isNull = (!((System.Nullable<System.Int16>)instance).HasValue);
                            break;
                        case "int32":
                            isNull = (!((System.Nullable<System.Int32>)instance).HasValue);
                            break;
                        case "int64":
                            isNull = (!((System.Nullable<System.Int64>)instance).HasValue);
                            break;
                        case "sbyte":
                            isNull = (!((System.Nullable<sbyte>)instance).HasValue);
                            break;
                        case "uint16":
                            isNull = (!((System.Nullable<System.UInt16>)instance).HasValue);
                            break;
                        case "uint32":
                            isNull = (!((System.Nullable<System.UInt32>)instance).HasValue);
                            break;
                        case "uint64":
                            isNull = (!((System.Nullable<System.UInt64>)instance).HasValue);
                            break;
                        case "single":
                            isNull = (!((System.Nullable<System.Single>)instance).HasValue);
                            break;
                        case "double":
                            isNull = (!((System.Nullable<System.Double>)instance).HasValue);
                            break;
                        case "decimal":
                            isNull = (!((System.Nullable<System.Decimal>)instance).HasValue);
                            break;
                        case "datetime":
                            isNull = (!((System.Nullable<DateTime>)instance).HasValue);
                            break;
                        case "guid":
                            isNull = (!((System.Nullable<Guid>)instance).HasValue);
                            break;
                        default:
                            break;
                    }
                }

            }
            catch
            {
                isNull = false;
            }

            return isNull;
        }

        public static void ClearProperties(this object instance)
        {
            try
            {
                foreach (PropertyInfo property in instance.GetType().GetProperties())
                {

                    if (property.PropertyType.Name == "Nullable`1")
                        ((Object)instance).SetPropertyValue(property.Name, null);

                    if (property.PropertyType.Name.ToLower() == "string")
                        ((Object)instance).SetPropertyValue(property.Name, String.Empty);

                    if (property.PropertyType.Name.ToLower() == "char")
                        ((Object)instance).SetPropertyValue(property.Name, ' ');

                    if (property.PropertyType.Name.ToLower().InList(new string[] { "byte", "int16", "int32", "int64", "sbyte", "uint16", "uint32", "uint64", "single", "double", "decimal" }))
                        ((Object)instance).SetPropertyValue(property.Name, Convert.ChangeType(0, property.PropertyType, null));

                    if (property.PropertyType.Name.ToLower() == "datetime")
                        ((Object)instance).SetPropertyValue(property.Name, new DateTime());

                    if (property.PropertyType.Name.ToLower() == "guid")
                        ((Object)instance).SetPropertyValue(property.Name, Guid.Empty);
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// Get the field value from one instance.
        /// </summary>
        /// <param name="objectRef"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetFieldValue(this object objectRef, string fieldName)
        {
            FieldInfo field = objectRef.GetType().GetField(fieldName);

            return field.GetValue(objectRef);

        }

        /// <summary>
        /// Set the field value to one instance.
        /// </summary>
        /// <param name="objectRef"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public static void SetFieldValue(this object objectRef, string fieldName, object value)
        {
            try
            {
                if ((value != null) && (value.GetType().FullName != "System.DBNull"))
                {
                    FieldInfo field = objectRef.GetType().GetField(fieldName);
                    field.SetValue(objectRef, value);
                }
            }
            catch { }
        }

        /// <summary>
        /// Get the property value from one instance.
        /// </summary>
        /// <param name="objectRef"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object objectRef, string propertyName)
        {
            object propertyValue = null;

            try
            {
                PropertyInfo property = objectRef.GetType().GetProperty(propertyName);
                if (property != null)
                    propertyValue = property.GetValue(objectRef, null);
            }
            catch
            {
                propertyValue = null;
            }

            return propertyValue;
        }

        public static bool ExistsProperty(this object objectRef, string propertyName)
        {
            return (objectRef != null && !propertyName.IsNullOrEmpty() && objectRef.GetType().GetProperty(propertyName) != null);
        }

        /// <summary>
        /// Set the property value from to instance.
        /// </summary>
        /// <param name="objectRef"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(this object objectRef, string propertyName, object value)
        {
            try
            {
                PropertyInfo property = objectRef.GetType().GetProperty(propertyName);
                if (property != null && property.GetSetMethod() != null)
                {
                    Type propType = (property.PropertyType.Name == "Nullable`1" ? Type.GetType(property.PropertyType.FullName.Extract("System.Nullable`1[[", ",")) : property.PropertyType);
                    if (propType != null)
                    {
                        if (value is IConvertible && value != null)
                            property.SetValue(objectRef, Convert.ChangeType(value, propType, null), null);
                        else
                            property.SetValue(objectRef, value, null);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Invoke this method
        /// </summary>
        /// <param name="objectRef">Object reference</param>
        /// <param name="methodName">Method Name</param>
        /// <param name="values">Params passed to the method</param>
        public static object InvokeMethod(this object objectRef, string methodName, params object[] values)
        {
            return objectRef.GetType().GetMethod(methodName).Invoke(objectRef, values);
        }


        /// <summary>
        /// Copy all properties from sourceRef to objectRef.
        /// </summary>
        /// <param name="objectRef"></param>
        /// <param name="sourceRef"></param>
        public static void CopyFrom(this object objectRef, object sourceRef)
        {
            var propertiesFrom = sourceRef.GetType().GetProperties().ToLookup(p => p.Name, p => p);
            PropertyInfo[] propertiesTo = objectRef.GetType().GetProperties();
            PropertyInfo propertyF;

            foreach (PropertyInfo propertyT in propertiesTo)
            {
                if (propertiesFrom.Contains(propertyT.Name) && propertyT.GetSetMethod() != null)
                {
                    propertyF = propertiesFrom[propertyT.Name].ToArray()[0];
                    if ((propertyT.PropertyType.Name == "Nullable`1" ? propertyT.PropertyType.GetElement() : propertyT.PropertyType) == (propertyF.PropertyType.Name == "Nullable`1" ? propertyF.PropertyType.GetElement() : propertyF.PropertyType) && propertyT.PropertyType.FullName != "System.DBNull")
                    {
                        objectRef.SetPropertyValue(propertyT.Name, sourceRef.GetPropertyValue(propertyT.Name));
                    }
                }

            }
        }

        /// <summary>
        /// Copy all properties from entitySearchList to objectRef.
        /// </summary>
        /// <param name="objectRef"></param>
        /// <param name="entitySearchList"></param>
        public static void CopyFromSearch(this object objectRef, List<EntitySearch> entitySearchList)
        {
            if (entitySearchList == null || entitySearchList.Count == 0)
                return;

            var refSearchList = entitySearchList.Where(e => e.EntityName == objectRef.GetType().Name);
            PropertyInfo[] propertiesTo = objectRef.GetType().GetProperties();

            foreach (PropertyInfo propertyT in propertiesTo)
            {
                if (propertyT.GetSetMethod() != null)
                {
                    if (propertyT.PropertyType.FullName != "System.DBNull")
                    {
                        foreach (var es in refSearchList)
                        {
                            var value = es.GetExpressionValue(propertyT.Name);
                            if (value != null)
                            {
                                objectRef.SetPropertyValue(propertyT.Name, value);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Copy only data properties from sourceRef to objectRef.
        /// </summary>
        /// <param name="objectRef"></param>
        /// <param name="sourceRef"></param>
        public static void CopyInstanceFrom(this object objectRef, object sourceRef)
        {
            var propertiesFrom = sourceRef.GetType().GetProperties().ToLookup(p => p.Name, p => p);
            PropertyInfo[] propertiesTo = objectRef.GetType().GetProperties().ToArray();
            PropertyInfo propertyF;

            foreach (PropertyInfo propertyT in propertiesTo)
            {
                if (propertiesFrom.Contains(propertyT.Name) && propertyT.GetSetMethod() != null)
                {
                    propertyF = propertiesFrom[propertyT.Name].ToArray()[0];

                    string propertyType = propertyF.PropertyType.FullName.Contains("System.Nullable") ? propertyF.PropertyType.FullName.Extract("[[", ",") : propertyF.PropertyType.Name;
                    propertyType = propertyType.Replace("System.", "").ToLower();

                    if ((propertyT.PropertyType.Name == "Nullable`1" ? propertyT.PropertyType.GetElement() : propertyT.PropertyType) == (propertyF.PropertyType.Name == "Nullable`1" ? propertyF.PropertyType.GetElement() : propertyF.PropertyType) && propertyT.PropertyType.FullName != "System.DBNull"
                        && (propertyF.PropertyType.GetTypeInfo().IsEnum || propertyT.PropertyType.GetTypeInfo().IsPrimitive || propertyF.PropertyType.GetTypeInfo().IsPrimitive || propertyType.InList(new string[] { "string", "decimal", "guid", "datetime" })))
                    {
                        objectRef.SetPropertyValue(propertyT.Name, sourceRef.GetPropertyValue(propertyT.Name));
                    }
                }

            }
        }

        /// <summary>
        /// Check if all common properties are equal.
        /// </summary>
        /// <param name="objectRef"></param>
        /// <param name="sourceRef"></param>
        public static bool EqualsInstanceFrom(this object objectRef, object sourceRef, params string[] excluded)
        {
            var propertiesFrom = sourceRef.GetType().GetProperties().ToLookup(p => p.Name, p => p);
            PropertyInfo[] propertiesTo = objectRef.GetType().GetProperties().ToArray();
            PropertyInfo propertyF;

            foreach (PropertyInfo propertyT in propertiesTo)
            {
                if (!excluded.Contains(propertyT.Name) && propertiesFrom.Contains(propertyT.Name) && propertyT.GetSetMethod() != null)
                {
                    propertyF = propertiesFrom[propertyT.Name].ToArray()[0];

                    string propertyType = propertyF.PropertyType.FullName.Contains("System.Nullable") ? propertyF.PropertyType.FullName.Extract("[[", ",") : propertyF.PropertyType.Name;
                    propertyType = propertyType.Replace("System.", "").ToLower();

                    if ((propertyT.PropertyType.Name == "Nullable`1" ? propertyT.PropertyType.GetElement() : propertyT.PropertyType) == (propertyF.PropertyType.Name == "Nullable`1" ? propertyF.PropertyType.GetElement() : propertyF.PropertyType) && propertyT.PropertyType.FullName != "System.DBNull"
                        && (propertyF.PropertyType.GetTypeInfo().IsEnum || propertyF.PropertyType.GetTypeInfo().IsPrimitive || propertyType.InList(new string[] { "string", "decimal", "guid", "datetime" })))
                    {
                        if (!sourceRef.GetPropertyValue(propertyT.Name).Equals(objectRef.GetPropertyValue(propertyT.Name)))
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Get a hierarchical structure and returns all items flat list
        /// </summary>
        /// <typeparam name="T">Type base</typeparam>
        /// <typeparam name="K">Type Node</typeparam>
        /// <param name="parent">Parent</param>
        /// <param name="children">property that contains collection items of type <typeparamref name="T"/> </param>
        /// <returns></returns>
        public static IEnumerable<T> GetFlattenHierarchical<T, K>(K parent, Func<K, IEnumerable<T>> children)
                where K : T
        {
            yield return parent;
            foreach (T relative in children(parent).SelectMany(e => e is K ? GetFlattenHierarchical((K)e, children) : new List<T> { e }))
                yield return relative;
        }
    }
    #endregion

    #region IEnumerableExtensions
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> SelectAllMany<T>(this IEnumerable<T> collection, Func<T, IEnumerable<T>> selector)
        {
            foreach (T o in collection)
            {
                foreach (T t in selector(o).SelectAllMany<T>(selector))
                    yield return t;

                yield return o;
            }
        }

        public static void Foreach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

        public static void RemoveAction<T>(this ICollection<T> list, Func<T, bool> predicateToRemove)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (predicateToRemove(list.ElementAt(i)))
                    list.Remove(list.ElementAt(i));
            }
        }
    }
    #endregion
    
    #region CustomSearch

    public class PredefinedFilter
    {
        public PredefinedFilter(string condition, string description, bool hasValue, string dataType)
        {
            Condition = condition;
            Description = description;
            HasValue = hasValue;
            DataType = dataType;
        }

        public string Condition { get; set; }
        public string Description { get; set; }
        public bool HasValue { get; set; }
        public string DataType { get; set; }

        public static string GetPredefinedValueDescription(string predefinedValue, int? counter = null)
        {
            string description = string.Empty;
            string parameter = counter.IsNull() ? "x" : counter.ToString();

            switch (predefinedValue)
            {
                case "CurrentYear":
                    description = "Ano corrente".Translate();
                    break;

                case "LastYear":
                    description = "Ano anterior".Translate();
                    break;

                case "2YearsAgo":
                    description = "Há dois anos atrás".Translate();
                    break;

                case "CurrentTrimester":
                    description = "Este trimestre".Translate();
                    break;

                case "LastTrimester":
                    description = "Trimestre anterior".Translate();
                    break;

                case "CurrentMonth":
                    description = "Este mês".Translate();
                    break;

                case "LastMonth":
                    description = "Mês anterior".Translate();
                    break;

                case "2MonthsAgo":
                    description = "Há dois meses atrás".Translate();
                    break;

                case "CurrentWeek":
                    description = "Esta semana".Translate();
                    break;

                case "LastWeek":
                    description = "Semana anterior".Translate();
                    break;

                case "2WeeksAgo":
                    description = "Há duas semanas atrás".Translate();
                    break;

                case "CurrentDate":
                    description = "Hoje".Translate();
                    break;

                case "MonthToDate":
                    description = "Mês até hoje".Translate();
                    break;

                case "YearToDate":
                    description = "Ano até hoje".Translate();
                    break;

                case "XDays":
                    description = string.Format("({0}) {1}", parameter, "dias".Translate());
                    break;

                case "XWeeks":
                    description = string.Format("({0}) {1}", parameter, "semanas".Translate());
                    break;

                case "XMonths":
                    description = string.Format("({0}) {1}", parameter, "meses".Translate());
                    break;

                case "XYears":
                    description = string.Format("({0}) {1}", parameter, "anos".Translate());
                    break;

                case "XDaysToDate":
                    description = string.Format("({0}) {1}", parameter, "dias até hoje".Translate());
                    break;

                case "XWeeksToDate":
                    description = string.Format("({0}) {1}", parameter, "semanas até semana atual".Translate());
                    break;

                case "XYearsToDate":
                    description = string.Format("({0}) {1}", parameter, "anos até hoje".Translate());
                    break;

                case "XMonthsToDate":
                    description = string.Format("({0}) {1}", parameter, "meses até hoje".Translate());
                    break;

                case "Null":
                    description = "Nulo".Translate();
                    break;

                case "Empty":
                    description = "Vazio".Translate();
                    break;
            }

            return description;
        }

        public static List<PredefinedFilter> LoadPredefinedFilters(string currentDataType)
        {
            List<PredefinedFilter> predefinedFilters = new List<PredefinedFilter>();

            //if (currentDataType != "Boolean")
            //    predefinedFilters.Add(new PredefinedFilter("Null", GetPredefinedValueDescription("Null"), false));

            if (currentDataType == "All" || currentDataType == "String")
                predefinedFilters.Add(new PredefinedFilter("Empty", GetPredefinedValueDescription("Empty"), false, "String"));

            if (currentDataType == "All" || currentDataType == "DateTime")
            {
                predefinedFilters.Add(new PredefinedFilter("CurrentYear", GetPredefinedValueDescription("CurrentYear"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("LastYear", GetPredefinedValueDescription("LastYear"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("2YearsAgo", GetPredefinedValueDescription("2YearsAgo"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("CurrentTrimester", GetPredefinedValueDescription("CurrentTrimester"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("LastTrimester", GetPredefinedValueDescription("LastTrimester"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("CurrentMonth", GetPredefinedValueDescription("CurrentMonth"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("LastMonth", GetPredefinedValueDescription("LastMonth"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("2MonthsAgo", GetPredefinedValueDescription("2MonthsAgo"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("CurrentWeek", GetPredefinedValueDescription("CurrentWeek"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("LastWeek", GetPredefinedValueDescription("LastWeek"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("2WeeksAgo", GetPredefinedValueDescription("2WeeksAgo"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("CurrentDate", GetPredefinedValueDescription("CurrentDate"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("MonthToDate", GetPredefinedValueDescription("MonthToDate"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("YearToDate", GetPredefinedValueDescription("YearToDate"), false, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("XDays", GetPredefinedValueDescription("XDays"), true, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("XWeeks", GetPredefinedValueDescription("XWeeks"), true, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("XMonths", GetPredefinedValueDescription("XMonths"), true, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("XYears", GetPredefinedValueDescription("XYears"), true, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("XDaysToDate", GetPredefinedValueDescription("XDaysToDate"), true, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("XWeeksToDate", GetPredefinedValueDescription("XWeeksToDate"), true, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("XMonthsToDate", GetPredefinedValueDescription("XMonthsToDate"), true, "DateTime"));
                predefinedFilters.Add(new PredefinedFilter("XYearsToDate", GetPredefinedValueDescription("XYearsToDate"), true, "DateTime"));
            }
            return predefinedFilters;
        }
    }

    #endregion

    # region Support Class for Report Viewer
    public class LxReportItem
    {
        public LxReportItem(string description, string fullName, string name, ReportType reportType)
        {
            Description = description;
            FullName = fullName;
            Name = name;
            ReportType = reportType;
        }

        public string Description { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public ReportType ReportType { get; set; }
    }
    #endregion

    #region TransactionMenu

    /// <summary>
    /// Tree node of transaction.
    /// </summary>
    public class TransactionMenu
    {
        private Guid uidModuloMenu;
        private Guid uidTransacaoMenu;
        private string nodeDescription;
        private int ordemNavegacao;
        private string classeNome;
        private string descObjeto;
        private Guid uidObjeto;
        private bool acessoTotal;
        private bool pesquisar;
        private bool incluir;
        private bool alterar;
        private bool excluir;
        private bool pesquisaEspecial;
        private bool imprimir;
        private bool exportar;
        private bool criarRelatorio;
        private bool criarPesquisa;
        private bool layout;
        private string layoutName;
        private Guid? uidlayout;

        public TransactionMenu() { }

        public TransactionMenu(Guid uidModuloMenu, Guid uidTransacaoMenu, string nodeDescription, int ordemNavegacao, string classeNome, string descObjeto, Guid uidObjeto, bool acessoTotal, bool pesquisar,
            bool incluir, bool alterar, bool excluir, bool pesquisaEspecial, bool imprimir, bool exportar, bool criarRelatorio, bool criarPesquisa, bool layout)
        {
            UidModuloMenu = uidModuloMenu;
            UidTransacaoMenu = uidTransacaoMenu;
            NodeDescription = nodeDescription;
            OrdemNavegacao = ordemNavegacao;
            ClasseNome = classeNome;
            DescObjeto = descObjeto;
            UidObjeto = uidObjeto;
            AcessoTotal = acessoTotal;
            Pesquisar = pesquisar;
            Incluir = incluir;
            Alterar = alterar;
            Excluir = excluir;
            PesquisaEspecial = pesquisaEspecial;
            Imprimir = imprimir;
            Exportar = exportar;
            CriarRelatorio = criarRelatorio;
            CriarPesquisa = criarPesquisa;
            Layout = layout;

        }

        public TransactionMenu(Guid uidModuloMenu, Guid uidTransacaoMenu, string nodeDescription, int ordemNavegacao, string classeNome, string descObjeto, Guid uidObjeto, bool acessoTotal, bool pesquisar,
            bool incluir, bool alterar, bool excluir, bool pesquisaEspecial, bool imprimir, bool exportar, bool criarRelatorio, bool criarPesquisa, bool layout, Guid? uidlayout, string layoutName) :
            this(uidModuloMenu, uidTransacaoMenu, nodeDescription, ordemNavegacao, classeNome, descObjeto, uidObjeto, acessoTotal, pesquisar, incluir, alterar, excluir, pesquisaEspecial, imprimir, exportar, criarRelatorio, criarPesquisa, layout)
        {
            this.UidLayout = uidlayout;
            this.LayoutName = layoutName;
        }

        public Guid UidModuloMenu { get { return uidModuloMenu; } set { uidModuloMenu = value; } }
        public Guid UidTransacaoMenu { get { return uidTransacaoMenu; } set { uidTransacaoMenu = value; } }
        public string NodeDescription { get { return nodeDescription; } set { nodeDescription = value; } }
        public int OrdemNavegacao { get { return ordemNavegacao; } set { ordemNavegacao = value; } }
        public string ClasseNome { get { return classeNome; } set { classeNome = value; } }
        public string DescObjeto { get { return descObjeto; } set { descObjeto = value; } }
        public Guid UidObjeto { get { return uidObjeto; } set { uidObjeto = value; } }
        public bool AcessoTotal { get { return acessoTotal; } set { acessoTotal = value; } }
        public bool Pesquisar { get { return pesquisar; } set { pesquisar = value; } }
        public bool Incluir { get { return incluir; } set { incluir = value; } }
        public bool Alterar { get { return alterar; } set { alterar = value; } }
        public bool Excluir { get { return excluir; } set { excluir = value; } }
        public bool PesquisaEspecial { get { return pesquisaEspecial; } set { pesquisaEspecial = value; } }
        public bool Imprimir { get { return imprimir; } set { imprimir = value; } }
        public bool Exportar { get { return exportar; } set { exportar = value; } }
        public bool CriarRelatorio { get { return criarRelatorio; } set { criarRelatorio = value; } }
        public bool CriarPesquisa { get { return criarPesquisa; } set { criarPesquisa = value; } }
        public bool Layout { get { return layout; } set { layout = value; } }
        public string LayoutName { get { return layoutName; } set { layoutName = value; } }
        public Guid? UidLayout { get { return uidlayout; } set { uidlayout = value; } }
        public bool SearchOnLoad { get; set; }

        public TransactionMenu Clone()
        {
            return new TransactionMenu(uidModuloMenu, uidTransacaoMenu, nodeDescription, ordemNavegacao, classeNome, descObjeto,
                                       uidObjeto, acessoTotal, pesquisar, incluir, alterar, excluir, pesquisaEspecial, imprimir,
                                       exportar, criarRelatorio, criarPesquisa, layout, uidlayout, layoutName);
        }
    }

    #endregion
}


