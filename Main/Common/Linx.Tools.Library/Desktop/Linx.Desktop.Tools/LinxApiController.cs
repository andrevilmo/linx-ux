using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System;

namespace Linx.Tools
{    
    public partial class LinxApiController<T> : ApiController
    {
        private static string fileMatch = String.Empty;
        private static Dictionary<string, T> _repositories;
        public static Dictionary<string, T> Repositories
        {
            get
            {
                if (_repositories == null)
                    _repositories = Linx.Tools.RepositoryHelper<T>.GetInstances(fileMatch);

                return _repositories;
            }
        }

        protected readonly T repository;
        public LinxApiController(string assemblyMatch, string defaultRepositoryName)
        {
            fileMatch = assemblyMatch;
            string repositoryName = System.Web.HttpContext.Current.Request.Headers["RepositoryName"];
            if (repositoryName.IsNullOrEmpty()) repositoryName = defaultRepositoryName;

            if (Repositories.ContainsKey(repositoryName))
                this.repository = Repositories[repositoryName];
            else
                throw new Exception(String.Format("The RepositoryName=[{0}] was not found!", repositoryName));
        }
    }
}
