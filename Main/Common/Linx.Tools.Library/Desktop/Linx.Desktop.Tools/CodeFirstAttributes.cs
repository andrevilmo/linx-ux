using System;

namespace Linx.Tools
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PrecisionAttribute : Attribute
    {
        #region Properties

        public int Value { get; private set; }

        #endregion

        #region Constructors

        public PrecisionAttribute(int value)
        {
            this.Value = value;
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ScaleAttribute : Attribute
    {
        #region Properties

        public int Value { get; private set; }

        #endregion

        #region Constructors

        public ScaleAttribute(int value)
        {
            this.Value = value;
        }

        #endregion
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BrandDecimalsAttribute : Attribute
    {
        
    }


}
