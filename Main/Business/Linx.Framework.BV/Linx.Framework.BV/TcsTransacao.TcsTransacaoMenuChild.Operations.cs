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
    public partial class TcsTransacaoMenuChild
	{
        public string GetDescModuloMenu()
        {
            if (!this._DescModuloMenu.IsNullOrEmpty() || this.UidModuloMenu.IsNullOrEmpty())
                return this._DescModuloMenu;

            return this.Values(1).ToString();
        }

        public Guid GetUidModulo()
        {
            if (!this._UidModulo.IsNullOrEmpty() || this.UidModuloMenu.IsNullOrEmpty())
                return this._UidModulo;

            return Guid.Parse(this.Values(2).ToString());
        }

        public string GetDescModulo()
        {
            if (!this._DescModulo.IsNullOrEmpty() || this.UidModuloMenu.IsNullOrEmpty())
                return this._DescModulo;

            return this.Values(3).ToString();
        }

        private object Values(int valueType)
        {
            if (this._DescModuloMenu.IsNullOrEmpty() && !this.UidModuloMenu.IsNullOrEmpty())
            {
                TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();

                //TcsModuloMenuAutorizacao
                var tcsModuloMenuAutorizacao =
                    (from result in ds.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => i.UidModuloMenu == this.UidModuloMenu)
                     select new 
                     {
                         DescModuloMenu = result.DescModuloMenu,
                         UidModulo = result.UidModulo,
                         DescModulo = result.DescModulo
                     }).FirstOrDefault();

                if (!tcsModuloMenuAutorizacao.IsNull())
                {
                    this._DescModuloMenu = tcsModuloMenuAutorizacao.DescModuloMenu;
                    this._UidModulo = tcsModuloMenuAutorizacao.UidModulo;
                    this._DescModulo = tcsModuloMenuAutorizacao.DescModulo;
                }
                else
                {
                    TcsModulo.TcsModuloDomainService dsModulo = new TcsModulo.TcsModuloDomainService();
                    var tcsModuloMenu =
                        (from result in dsModulo.GetTcsModuloMenuNoAssociations().Where(i => i.UidModuloMenu == this.UidModuloMenu)
                         select new
                         {
                             DescModuloMenu = result.DescModuloMenu,
                             UidModulo = result.UidModulo,
                         }).FirstOrDefault();

                    this._DescModuloMenu = tcsModuloMenu.DescModuloMenu;
                    this._UidModulo = tcsModuloMenu.UidModulo;

                    if (!this.UidModulo.IsNull())
                    {
                        this.DescModulo = (from result in dsModulo.GetTcsModuloNoAssociations().Where(i => i.UidModulo == this.UidModulo)
                                           select result.DescModulo).FirstOrDefault();
                    }
                }
            }

            object value = string.Empty;

            switch (valueType)
            {
                case 1:
                    value = this._DescModuloMenu ?? String.Empty;
                    break;

                case 2:
                    value = this._UidModulo;
                    break;

                case 3:
                    value = this._DescModulo ?? String.Empty;
                    break;
            }

            return value;
        }
    }
}
