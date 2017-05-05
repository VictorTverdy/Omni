using System;

namespace Omni.DataProviders.ResponseTypes
{
	public class BaseResponse
	{
		public bool Error {
			get;
			set;
		}
		
		public string Status {
			get;
			set;
		}	
		
		public int OperationTag {
			get;
			set;
		}
		
		public string RawData {
			get;
			set;
		}
		
	}
}


