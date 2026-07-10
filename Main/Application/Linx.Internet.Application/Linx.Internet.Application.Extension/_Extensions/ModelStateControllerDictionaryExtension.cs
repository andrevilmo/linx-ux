//// -----------------------------------------------------------------------
//// <copyright file="ModelStateControllerDictionaryExtension.cs" company="Linx Sistemas">
//// Copyright (c) Linx Sistemas. All rights reserved.
//// </copyright>
//// -----------------------------------------------------------------------

//namespace Linx.Internet.Application
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Configuration;
//    using System.Linq;
//    using System.Text;
//    using System.Web;
//    using System.Web.Mvc;

//    /// <summary>
//    /// Extension ModelStateControllerDictionaryExtension
//    /// </summary>
//    public static class ModelStateControllerDictionaryExtension
//    {
//        /// <summary>
//        /// Adiciona o model state da API para a controller, sem chamadas HTTP
//        /// </summary>
//        /// <param name="reference">Model state da controller</param>
//        /// <param name="apiModelState">Model state da API</param>
//        public static void AddModelStateErrorAPI(this ModelStateDictionary reference, System.Web.Http.ModelBinding.ModelStateDictionary apiModelState)
//        {
//            foreach (KeyValuePair<string, System.Web.Http.ModelBinding.ModelState> keyValue in apiModelState)
//            {
//                foreach (System.Web.Http.ModelBinding.ModelError error in keyValue.Value.Errors)
//                {
//                    reference.AddModelError(keyValue.Key, error.ErrorMessage);
//                }
//            }
//        }
//    }
//}
