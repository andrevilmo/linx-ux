using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Framework.ControleSistema.BM;

namespace Linx.Framework.BV.Usuario
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class UsuarioDomainService
	{
        [Invoke(HasSideEffects = true)]
        public Int64 GetUserId(Guid uidUsuario)
        {
            return this.GetTcsUsuarioNoAssociations().Where(i => i.UidUsuario == uidUsuario).Select(i => i.IdUsuario).FirstOrDefault();
        }
    }
}
