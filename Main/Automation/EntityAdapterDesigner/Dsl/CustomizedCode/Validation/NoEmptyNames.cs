using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling;
using System.Globalization;


namespace Linx.EntityAdapterDesigner
{

    /// <summary>  
    /// Add a hard constraint to StateElement to prevent its "Name" property from being empty.  
    /// </summary>  
    public partial class EntityDataModel
    {
        /// <summary>  
        /// Value handler for the NamedElement.Name domain property.  
        /// </summary>  
        internal sealed partial class NamePropertyHandler : DomainPropertyValueHandler<EntityDataModel, global::System.String>
        {
            protected override void OnValueChanging(EntityDataModel element, string oldValue, string newValue)
            {
                if (!element.Store.InUndoRedoOrRollback)
                {
                    if (string.IsNullOrEmpty(newValue))
                    {
                        throw new ArgumentOutOfRangeException("Name", "Name cannot be empty or null.");
                    }
                }
                base.OnValueChanging(element, oldValue, newValue);
            }
        }
    }


    /// <summary>  
    /// Add a hard constraint to StateElement to prevent its "Name" property from being empty.  
    /// </summary>  
    public partial class EntityAdapter
    {
        /// <summary>  
        /// Value handler for the NamedElement.Name domain property.  
        /// </summary>  
        internal sealed partial class NamePropertyHandler : DomainPropertyValueHandler<EntityAdapter, global::System.String>
        {
            protected override void OnValueChanging(EntityAdapter element, string oldValue, string newValue)
            {
                if (!element.Store.InUndoRedoOrRollback)
                {
                    if (string.IsNullOrEmpty(newValue))
                    {
                        throw new ArgumentOutOfRangeException("Name", "Name cannot be empty or null.");
                    }
                }
                base.OnValueChanging(element, oldValue, newValue);
            }
        }
    }


	/// <summary>  
	/// Add a hard constraint to StateElement to prevent its "Name" property from being empty.  
	/// </summary>  
	public partial class DomainServiceExtension
	{
		/// <summary>  
		/// Value handler for the NamedElement.Name domain property.  
		/// </summary>  
		internal sealed partial class NamePropertyHandler : DomainPropertyValueHandler<DomainServiceExtension, global::System.String>
		{
			protected override void OnValueChanging(DomainServiceExtension element, string oldValue, string newValue)
			{
				if (!element.Store.InUndoRedoOrRollback)
				{
					if (string.IsNullOrEmpty(newValue))
					{
						throw new ArgumentOutOfRangeException("Name", "Name cannot be empty or null.");
					}
				}
				base.OnValueChanging(element, oldValue, newValue);
			}
		}
	}
}