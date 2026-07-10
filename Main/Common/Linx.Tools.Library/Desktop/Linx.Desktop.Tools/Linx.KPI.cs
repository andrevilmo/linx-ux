using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Linx.Tools
{
    public enum KpiShowType
    {
        Progress = 0,
        Value = 1,
        Description = 2
    }

    public class KpiInfo : INotifyPropertyChanged
    {
        private KpiShowType _showType;
        public KpiShowType ShowType 
        { 
            get { return _showType; }
            set
            {
                if (value != this._showType)
                {
                    this._showType = value;
                    this.OnPropertyChanged("ShowType");
                }
            }
        }

        private bool _started;
        public bool Started
        {
            get { return _started; }
            set
            {
                if (value != this._started)
                {
                    this._started = value;
                    this.OnPropertyChanged("Started");
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != this._description)
                {
                    this._description = value;
                    this.OnPropertyChanged("Description");
                }
            }
        }

        Dictionary<string, KpiRangeItem> ranges;
        public Dictionary<string, KpiRangeItem> Ranges 
        {
            get 
            {
                if (ranges.IsNull())
                    ranges = new Dictionary<string, KpiRangeItem>();
                return ranges;
            } 
        }

        public double GetMinValue()
        { 
            double result = 0;

            if (this.Ranges.Count > 0)
                result = this.Ranges.Values.Min(e => e.StartValue);

            return result;
        }

        public double GetMaxValue()
        {
            double result = 0;

            if (this.Ranges.Count > 0)
                result = this.Ranges.Values.Max(e => e.EndValue);

            return result;
        }


        public KpiRangeItem GetRangeByValue(double value)
        {
            if (Ranges.Count == 0)
                return null;

            var range = Ranges.Where(e => value >= e.Value.StartValue && value <= e.Value.EndValue).FirstOrDefault();

            if (range.IsNull() || range.Value.IsNull())
            {
                if (value < this.GetMinValue())
                    range = Ranges.FirstOrDefault();
                else if (value > this.GetMaxValue())
                    range = Ranges.LastOrDefault();
            }

            if (range.IsNull() || range.Value.IsNull())
                return null;
            else
                return range.Value;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public void ClearEntitySearch()
        {
            KpiRangeItem[] checkedList = this.Ranges.Values.Where(e => e.IsChecked).ToArray();
            if (checkedList.Length == 0)
                return ;

            foreach (KpiRangeItem range in checkedList)
                range.IsChecked = false;
        }

        /// <summary>
        /// Get search for this KPI.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public EntitySearch GetEntitySearch(string type, string entityName, string field)
        {
            KpiRangeItem[] checkedList = this.Ranges.Values.Where(e => e.IsChecked).ToArray();
            if (checkedList.Length == 0)
                return null;

            //Adjust type reference
            if (type.IndexOf(".") >= 0)
                type = type.Right(".").ToLower();
            else
                type = type.ToLower();

            EntitySearch currentDTO = new EntitySearch(entityName);
            foreach (KpiRangeItem range in checkedList)
            {
                if (currentDTO.Expressions.Count > 0)
                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "||"));

                if (range != this.Ranges.Values.First())
                {
                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, field));
                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, ">="));
                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, ConvertValue(range.StartValue, type)));
                }

                if (range != this.Ranges.Values.First() && range != this.Ranges.Values.Last())
                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));

                if (range != this.Ranges.Values.Last())
                {
                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, field));
                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "<="));
                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, ConvertValue(range.EndValue, type)));
                }
            }

            return currentDTO;
        }

        private object ConvertValue(double value, string type)
        {
            object result = (int)0;
            switch (type)
            {
                case "decimal":
                    result = (decimal)value;
                    break;
                case "float":
                    result = (float)value;
                    break;
                case "single":
                    result = (Single)value;
                    break;
                case "int64":
                    result = (Int64)value;
                    break;
                case "int32":
                    result = (Int32)value;
                    break;
                case "int16":
                    result = (Int16)value;
                    break;
                case "uint64":
                    result = (UInt64)value;
                    break;
                case "uint32":
                    result = (UInt32)value;
                    break;
                case "uint16":
                    result = (UInt16)value;
                    break; 
                default:
                    result = value;
                    break;
            }

            return result;
        }
    }


    

    public class KpiRangeItem : INotifyPropertyChanged
    {
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != this._description)
                {
                    this._description = value;
                    this.OnPropertyChanged("Description");
                }
            }
        }

        private double _startValue;
        public double StartValue
        {
            get { return _startValue; }
            set
            {
                if (value != this._startValue)
                {
                    this._startValue = value;
                    this.OnPropertyChanged("StartValue");
                }
            }
        }

        private double _endValue;
        public double EndValue
        {
            get { return _endValue; }
            set
            {
                if (value != this._endValue)
                {
                    this._endValue = value;
                    this.OnPropertyChanged("EndValue");
                }
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value != this._isChecked)
                {
                    this._isChecked = value;
                    this.OnPropertyChanged("IsChecked");
                }
            }
        }

        private byte _alpha;
        public byte Alpha
        {
            get { return _alpha; }
            set
            {
                if (value != this._alpha)
                {
                    this._alpha = value;
                    this.OnPropertyChanged("Alpha");
                    this.Color = "#" + _red.ToString("X2") + _green.ToString("X2") + _blue.ToString("X2");
                }
            }
        }

        private byte _red;
        public byte Red
        {
            get { return _red; }
            set
            {
                if (value != this._red)
                {
                    this._red = value;
                    this.OnPropertyChanged("Red");
                    this.Color = "#" + _red.ToString("X2") + _green.ToString("X2") + _blue.ToString("X2");
                }
            }
        }

        private byte _green;
        public byte Green
        {
            get { return _green; }
            set
            {
                if (value != this._green)
                {
                    this._green = value;
                    this.OnPropertyChanged("Green");
                    this.Color = "#" + _red.ToString("X2") + _green.ToString("X2") + _blue.ToString("X2");
                }
            }
        }

        private byte _blue;
        public byte Blue
        {
            get { return _blue; }
            set
            {
                if (value != this._blue)
                {
                    this._blue = value;
                    this.OnPropertyChanged("Blue");
                    this.Color = "#" + _red.ToString("X2") + _green.ToString("X2") + _blue.ToString("X2");
                }
            }
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                if (value != this._color)
                {
                    this._color = value;
                    this.OnPropertyChanged("Color");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
