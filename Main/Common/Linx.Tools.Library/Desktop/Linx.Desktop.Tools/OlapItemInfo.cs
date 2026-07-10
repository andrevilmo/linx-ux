using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public class OlapItemInfo
    {
        #region Properties

        public string DisplayName { get; set; }
        public string Name { get; set; }
        #region OlapItemType

        private OlapItemEnum _OlapItemType = OlapItemEnum.None;
        public OlapItemEnum OlapItemType
        {
            get { return _OlapItemType; }
            set { _OlapItemType = value; }
        }

        #endregion
        #region DataTypeNumber

        private ushort _DataTypeNumber;
        public ushort DataTypeNumber
        {
            get { return _DataTypeNumber; }
            set
            {
                _DataTypeNumber = value;
                SetDataType(_DataTypeNumber);
            }
        }

        #endregion
        public Type DataType { get; set; }
        public string UniqueName { get; set; }
        public string GroupName { get; set; }
        public ushort NumericPrecision { get; set; }
        public string Description { get; set; }
        public string MeasureFormula { get; set; }

        #endregion

        #region Constructor

        public OlapItemInfo()
        {

        }

        #endregion

        #region Override Methods

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var objRef = obj as OlapItemInfo;


            return this.UniqueName == objRef.UniqueName;
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", this.Name, this.DataType.Name);
        }

        #endregion

        #region Private methods

        private void SetDataType(int datatypeNumber)
        {
            //SELECT DATA_TYPE, TYPE_NAME, COLUMN_SIZE, IS_FIXEDLENGTH FROM $SYSTEM.DBSCHEMA_PROVIDER_TYPES

            switch (datatypeNumber)
            {
                case 2: //2	        SHORT	    2	        TRUE
                    DataType = typeof(short);
                    break;
                case 3: //3	        LONG	    4	        TRUE
                case 20: //3	        LONG	    20	        TRUE
                    DataType = typeof(long);
                    break;
                case 4://4	        FLOAT	    4	        TRUE
                case 5://5	        DOUBLE	    8	        TRUE
                    DataType = typeof(double);
                    break;
                case 6: //6	        CURRENCY	8	        TRUE
                    DataType = typeof(decimal);
                    break;
                case 7://7	        DATE	    4	        TRUE
                    DataType = typeof(DateTime);
                    break;
                case 8: //8	        BSTR	    256	        FALSE
                case 129: //129	    CHAR	    256	        FALSE
                case 130:  //130	WCHAR	    256	        FALSE
                    DataType = typeof(string);
                    break;
                case 18:  //18	    USHORT	    2	        TRUE
                    DataType = typeof(ushort);
                    break;
                case 19: //19	    ULONG	    4	        TRUE
                    DataType = typeof(ulong);
                    break;
                case 12: //12	    VARIANT	    16	        TRUE
                    DataType = typeof(object);
                    break;
                case 11:
                    DataType = typeof(bool);
                    break;

                default:
                    DataType = typeof(object);
                    break;
            }
        }

        #endregion
    }

    public enum OlapItemEnum { Measure, Dimension, Kpi, DimensionProperty, None }
}
