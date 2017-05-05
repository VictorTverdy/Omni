using System;
using System.Collections.Generic;

namespace Omni.Entities
{
	[Serializable]
	public class GenericObjectUser
	{
		public  GenericObjectUser()
		{
		}

		public bool success;
		public string error;
		public object result;
		public string unAuthorizedRequest ;
	}
}