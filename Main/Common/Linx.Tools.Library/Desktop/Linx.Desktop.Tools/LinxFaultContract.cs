using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Linx
{
	[DataContract]
	public class LinxFaultContract
	{
		public LinxFaultContract()
		{
			InnerMessages = new List<string>();
			Parameters = new List<string>();
		}

		private string _Message, _ErrorType;
		private List<string> _InnerMessages;
		private List<string> _Parameters;

		[DataMemberAttribute]
		public string Message
		{
			get { return this._Message; }
			set { this._Message = value; }
		}
		[DataMemberAttribute]
		public string ErrorType
		{
			get { return this._ErrorType; }
			set { this._ErrorType = value; }
		}
		[DataMemberAttribute]
		public List<string> InnerMessages
		{
			get { return this._InnerMessages; }
			set { this._InnerMessages = value; }
		}
		[DataMemberAttribute]
		public List<string> Parameters
		{
			get { return this._Parameters; }
			set { this._Parameters = value; }
		}
	}
}
