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
#if !SILVERLIGHT
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
#endif
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Objects.DataClasses;
using Linx.Framework.ControleSistema.BM;

namespace Linx.TCS0101.BO.TcsFiltro
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsFiltro
	{
        public string GetDescObjeto()
        {
            if (!this._DescObjeto.IsNullOrEmpty() || this.UidObjeto.IsNullOrEmpty())
                return this._DescObjeto;

            return this.Values(2).ToString();
        }

        public Byte GetLxTipoObjeto()
        {
            if (!this._LxTipoObjeto.IsNullOrEmpty() || this.UidObjeto.IsNullOrEmpty())
                return this._LxTipoObjeto;

            return Convert.ToByte(this.Values(3));
        }

        private object Values(int valueOption)
        {
            object value = string.Empty;

            if (this._DescObjeto.IsNullOrEmpty() && !this.UidObjeto.IsNullOrEmpty())
            {
                string descriptions = Utils.GetDescObjeto(this.UidObjeto);
                this._DescObjeto = descriptions.Extract("[", "]");
                this.LxTipoObjeto = Convert.ToByte(descriptions.Extract(",[", "]", 2));
            }

            switch (valueOption)
            {
                case 2:
                    value = this._DescObjeto;
                    break;

                case 3:
                    value = this._LxTipoObjeto;
                    break;
            }
            return value;
        }
    }
}
