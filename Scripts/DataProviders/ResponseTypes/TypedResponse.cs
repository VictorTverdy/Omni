using System;


namespace Omni.DataProviders.ResponseTypes
{
	public class TypedResponse<T> : BaseResponse
	{
		public T Data {
			get;
			set;
		}
	}
}