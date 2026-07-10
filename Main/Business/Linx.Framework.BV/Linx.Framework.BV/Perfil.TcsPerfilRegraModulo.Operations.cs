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
using Linx.Framework.ControleSistema.BM;

namespace Linx.Framework.BV.Perfil
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsPerfilRegraModulo
	{
        public string GetDescModulo()
        {
            if (!this._DescModulo.IsNullOrEmpty() || this.IdModulo.IsNullOrEmpty())
                return this._DescModulo;

            return this.Values(2);
        }

        public string GetDescAplicativo()
        {
            if (!this._DescAplicativo.IsNullOrEmpty() || this.IdModulo.IsNullOrEmpty())
                return this._DescAplicativo;

            return this.Values(1);
        }

        private string Values(int valueOption)
        {
            string value = string.Empty;

            if (this._DescModulo.IsNullOrEmpty() && !this.IdModulo.IsNullOrEmpty())
            {
                string descriptions = Utils.GetDescModulo(this.IdModulo);
                this._DescModulo = descriptions.Extract("[", "]");
                this._DescAplicativo = descriptions.Extract(",[", "]");
            }

            switch (valueOption)
            {
                case 1:
                    value = this._DescAplicativo;
                    break;

                case 2:
                    value = this._DescModulo;
                    break;
            }
            return value;
        }

    }
}
