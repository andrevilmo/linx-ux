using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;

namespace Linx.Tools
{
    public class LinxEndPointBehavior : IEndpointBehavior
    {
        private String _user;
        private String _company;
        private String _token;
        private String _applicationId;
        private String _accessGroup;

        public LinxEndPointBehavior(String user, String company, String token, string applicationId, string accessGroup)
        {
            this._user = user;
            this._company = company;
            this._token = token;
            this._applicationId = applicationId;
            this._accessGroup = accessGroup;
        }

        public void AddBindingParameters(ServiceEndpoint endpoInt32, BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoInt32, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new LinxMessageInspector(this._user, this._company, this._token, this._applicationId, this._accessGroup));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoInt32, System.ServiceModel.Dispatcher.EndpointDispatcher endpoInt32Dispatcher) { }

        public void Validate(ServiceEndpoint endpoInt32) { }
    }

    public class LinxMessageInspector : IClientMessageInspector
    {
        private String _user;
        private String _company;
        private String _token;
        private String _applicationId;
        private String _accessGroup;

        public LinxMessageInspector(String user, String company, String token, string applicationId, string accessGroup)
        {
            this._user = user;
            this._company = company;
            this._token = token;
            this._applicationId = applicationId;
            this._accessGroup = accessGroup;
        }

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState) { }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
        {
            if (!request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
                request.Properties.Add(HttpRequestMessageProperty.Name, new HttpRequestMessageProperty());

            HttpRequestMessageProperty property = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];

            property.Headers["CurrentUser"] = this._user;
            property.Headers["CurrentCompany"] = this._company;
            property.Headers["AuthorizationToken"] = this._token;
            property.Headers["AccessGroup"] = this._accessGroup;
            property.Headers["Application"] = this._applicationId;
            return null;
        }
    }
}
