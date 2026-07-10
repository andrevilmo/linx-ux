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

namespace Linx.TCS0101.BO.TcsTransacao
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsTransacao
	{
        public string GetDescObjeto()
        {
            if (!this._DescObjeto.IsNullOrEmpty() || this.UidObjeto.IsNullOrEmpty())
                return this._DescObjeto;

            return this.Values(2);
        }

        public string GetClasseNomeObjeto()
        {
            if (!this._ClasseNomeObjeto.IsNullOrEmpty() || this.UidObjeto.IsNullOrEmpty())
                return this._ClasseNomeObjeto;

            return this.Values(1);
        }

        private string Values(int valueOption)
        {
            string value = string.Empty;

            if (this._DescObjeto.IsNullOrEmpty() && !this.UidObjeto.IsNullOrEmpty())
            {
                string descriptions = Utils.GetDescObjeto(this.UidObjeto);
                this._DescObjeto = descriptions.Extract("[", "]");
                this._ClasseNomeObjeto = descriptions.Extract(",[", "]");
            }

            switch (valueOption)
            {
                case 1:
                    value = this._ClasseNomeObjeto;
                    break;

                case 2:
                    value = this._DescObjeto;
                    break;
            }
            return value;
        }
    }
}
