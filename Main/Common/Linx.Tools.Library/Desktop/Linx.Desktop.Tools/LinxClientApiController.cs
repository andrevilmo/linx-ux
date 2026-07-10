// -----------------------------------------------------------------------
// <copyright file="LinxClientApiController.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LinxClientApiController
    {
        protected string busAddress;
        protected HttpClient _client;
        public LinxClientApiController(string serviceBusAddress)
        {
            this.busAddress = serviceBusAddress + (serviceBusAddress.Right(1) == "/" ? String.Empty : "/");
            _client = new HttpClient(new WebRequestHandler() { AllowAutoRedirect = false, UseProxy = false }) { BaseAddress = new Uri(this.busAddress) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void AuthenticateUser(string userName, string password, string applicationId)
        {
            WebClientHelper.AuthenticateUser(busAddress, userName, password, applicationId);
            _client.DefaultRequestHeaders.Add("CurrentUser", WebClientHelper.UserInfo.CurrentUser);
            _client.DefaultRequestHeaders.Add("AuthorizationToken", WebClientHelper.UserInfo.AuthorizationToken);
            _client.DefaultRequestHeaders.Add("CurrentCompany", WebClientHelper.UserInfo.CurrentCompany);
            _client.DefaultRequestHeaders.Add("Application", WebClientHelper.UserInfo.Application);
            _client.DefaultRequestHeaders.Add("AccessGroup", WebClientHelper.UserInfo.AccessGroup);
            _client.DefaultRequestHeaders.Add("EconomicGroup", WebClientHelper.UserInfo.EconomicGroup);
            _client.DefaultRequestHeaders.Add("Environment", WebClientHelper.UserInfo.Environment);
        }

    }
}
