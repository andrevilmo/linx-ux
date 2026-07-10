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

namespace Linx.TCS0101.BO.TcsUsuario
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsUsuarioRegraTransacao
	{
        public string GetDescTransacao()
        {
            if (!this._DescTransacao.IsNullOrEmpty() || this.UidTransacao.IsNullOrEmpty())
                return this._DescTransacao;

            return this.Values(2);
        }

        public string GetClasseNome()
        {
            if (!this._ClasseNome.IsNullOrEmpty() || this.UidTransacao.IsNullOrEmpty())
                return this._ClasseNome;

            return this.Values(1);
        }

        private string Values(int valueOption)
        {
            string value = string.Empty;

            if (this._DescTransacao.IsNullOrEmpty() && !this.UidTransacao.IsNullOrEmpty())
            {
                string descriptions = Utils.GetDescTransacao(this.UidTransacao);
                this._DescTransacao = descriptions.Extract("[", "]");
                this._ClasseNome = descriptions.Extract(",[", "]");
            }

            switch (valueOption)
            {
                case 1:
                    value = this._ClasseNome;
                    break;

                case 2:
                    value = this._DescTransacao;
                    break;
            }
            return value;
        }
    }
}
