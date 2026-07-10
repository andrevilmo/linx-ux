using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Linx.Framework.ControleSistema.BM
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TCS_LAYOUT
	{
        // Execute business tasks or return an error list for cancelling the process.
        private IEnumerable<string> ValidateEntity(ControleSistemaContext context, System.Data.Entity.EntityState state)
        {
            if (state == System.Data.Entity.EntityState.Added)
                this.UID_OBJETO_CONTEUDO = Guid.NewGuid();

            return default(IEnumerable<string>);
        }
    }
}
