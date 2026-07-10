using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.OlapProxy.Service.Models
{
    public class OlapProxyRequest
    {
        public OlapProxyRequest()
        { }

        public OlapProxyRequest(string currentBrand, string allowedBrands, string jEntitySearch)
        {
            this.CurrentBrand = currentBrand;
            this.AllowedBrands = allowedBrands;
            this.JEntitySearch = jEntitySearch;
        }

        public string CurrentBrand { get; set; }

        public string AllowedBrands { get; set; }

        public string JEntitySearch { get; set; }

        private List<string> _currentBrandCollection = null;
        public List<string> CurrentBrandCollection
        {
            get
            {
                if (_currentBrandCollection == null && !string.IsNullOrEmpty(this.CurrentBrand))
                    _currentBrandCollection = this.CurrentBrand.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                return _currentBrandCollection;
            }
        }

        private List<string> _allowedBrandsCollection = null;
        public List<string> AllowedBrandsCollection
        {
            get
            {
                if (_allowedBrandsCollection == null && !string.IsNullOrEmpty(this.AllowedBrands))
                    _allowedBrandsCollection = this.AllowedBrands.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                return _allowedBrandsCollection;
            }
        }       
    }
}