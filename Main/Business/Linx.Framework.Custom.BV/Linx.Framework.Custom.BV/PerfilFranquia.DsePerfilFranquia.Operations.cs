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
using Linx.Business.Tools;

namespace Linx.Framework.Custom.BV.PerfilFranquia
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class PerfilFranquiaDomainService
    {
        [Invoke(HasSideEffects = true)]
        public List<long> GetPerfilList(Int64 idUsuario)
        {
            PerfilFranquiaDomainService ds = new PerfilFranquiaDomainService();
            return ds.GetTcsUsuarioPerfilNoAssociations().Where(i => i.IdUsuario == idUsuario).Select(i => i.IdPerfil).ToList();
        }

    }
}
