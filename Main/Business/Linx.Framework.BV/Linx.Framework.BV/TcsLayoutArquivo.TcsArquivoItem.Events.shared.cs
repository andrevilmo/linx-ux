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
using System.Xml.Serialization;

namespace Linx.TCS0101.BO.TcsLayoutArquivo
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsArquivoItem
	{
        /// Execute before lookup on client side.
        public EntitySearch BeforeGetLookUpArquivoItemPaiQuery()
        {
            EntitySearch esPesquisa = new EntitySearch();

            esPesquisa.Expressions.Add(new EntitySearchExpression("Field", "IdArquivoFk"));
            esPesquisa.Expressions.Add(new EntitySearchExpression("Operator", "=="));
            esPesquisa.Expressions.Add(new EntitySearchExpression("Value", this.TcsArquivo.IdArquivo));

            esPesquisa.Expressions.Add(new EntitySearchExpression("Condition", "&&"));

            esPesquisa.Expressions.Add(new EntitySearchExpression("Field", "IdArquivoItemPai"));
            esPesquisa.Expressions.Add(new EntitySearchExpression("Operator", "!="));
            esPesquisa.Expressions.Add(new EntitySearchExpression("Value", this.IdArquivoItem));

            return esPesquisa;
        }
    }
}
