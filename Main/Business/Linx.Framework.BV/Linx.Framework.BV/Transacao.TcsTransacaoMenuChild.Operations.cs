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
using Linx.Framework.BV.Modulo;
using Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV.Transacao
{

    ////////////////////////////////////////////////////////////////////////////
    //////////////////////// Business Operations Definition ////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsTransacaoMenuChild
    {
        public string GetDescModuloMenu()
        {
            if (!this._DescModuloMenu.IsNullOrEmpty() || this.IdModuloMenu.IsNullOrEmpty())
                return this._DescModuloMenu;

            return this.Values(1).ToString();
        }

        public Int64 GetIdModulo()
        {
            if (!this._IdModulo.IsNullOrEmpty() || this.IdModuloMenu.IsNullOrEmpty())
                return this._IdModulo;

            return Convert.ToInt64(this.Values(2).ToString());
        }

        public string GetDescModulo()
        {
            if (!this._DescModulo.IsNullOrEmpty() || this.IdModuloMenu.IsNullOrEmpty())
                return this._DescModulo;

            return this.Values(3).ToString();
        }

        public string GetDescAplicativo()
        {
            if (!this._DescAplicativo.IsNullOrEmpty() || this.DescAplicativo.IsNullOrEmpty())
                return this._DescAplicativo;

            return this.Values(4).ToString();
        }

        private object Values(int valueType)
        {
            if (this._DescModuloMenu.IsNullOrEmpty() && !this.IdModuloMenu.IsNullOrEmpty())
            {
                ModuloAutorizacao.ModuloAutorizacaoDomainService ModuloAutorizacaoDs = new ModuloAutorizacao.ModuloAutorizacaoDomainService();

                //TcsModuloMenuAutorizacao
                var tcsModuloMenuAutorizacao =
                    (from result in ModuloAutorizacaoDs.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => i.IdModuloMenu == this.IdModuloMenu)
                     select new
                     {
                         DescModuloMenu = result.DescModuloMenu,
                         IdModulo = result.IdModulo,
                         DescModulo = result.DescModulo,
                         DescAplicativo = result.DescricaoAplicativo
                     }).FirstOrDefault();

                if (!tcsModuloMenuAutorizacao.IsNull())
                {
                    this._DescModuloMenu = tcsModuloMenuAutorizacao.DescModuloMenu;
                    this._IdModulo = tcsModuloMenuAutorizacao.IdModulo;
                    this._DescModulo = tcsModuloMenuAutorizacao.DescModulo;
                    this._DescAplicativo = tcsModuloMenuAutorizacao.DescAplicativo;
                }
                else
                {
                    ModuloDomainService dsModulo = new ModuloDomainService();
                    var tcsModuloMenu =
                        (from result in dsModulo.GetTcsModuloMenuNoAssociations().Where(i => i.IdModuloMenu == this.IdModuloMenu)
                         select new
                         {
                             DescModuloMenu = result.DescModuloMenu,
                             IdModulo = result.IdModulo,
                         }).FirstOrDefault();

                        this._DescModuloMenu = tcsModuloMenu.DescModuloMenu;
                        this._IdModulo = tcsModuloMenu.IdModulo;

                    if (!this.IdModulo.IsNull())
                    {
                        var modulo = (from result in dsModulo.GetTcsModuloNoAssociations().Where(i => i.IdModulo == this.IdModulo)
                                      select new
                                      {
                                          DescModulo = result.DescModulo,
                                          IdTcsAplicativo = result.IdTcsAplicativo
                                      }).FirstOrDefault();

                        this._DescModulo = modulo.DescModulo;
                        this._DescAplicativo = Utils.GetDescAplicativo(modulo.IdTcsAplicativo);
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
                    value = this._IdModulo;
                    break;

                case 3:
                    value = this._DescModulo ?? String.Empty;
                    break;

                case 4:
                    value = this._DescAplicativo ?? String.Empty;
                    break;
            }

            return value;
        }


    }
}
