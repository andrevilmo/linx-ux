using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;

namespace Linx.Framework.BV.Multimidia
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class MediaElement
	{
        public string GetExtension()
        {
            var values = Linx.Framework.Domains.BM.Domains.TipoExtensao.GetNames();
            if (values.ContainsKey(this.ExtensionType.ToString()))
                return values[this.ExtensionType.ToString()];
            else return "JPG";
        }

        public string GetTipoDocumento()
        {
            var values = Linx.Framework.Domains.BM.Domains.TipoDocumento.GetValues();
            if (values.ContainsKey(this.LxTipoDocumento.ToString()))
                return values[this.LxTipoDocumento.ToString()];
            else return String.Empty;
        }
    }
}
