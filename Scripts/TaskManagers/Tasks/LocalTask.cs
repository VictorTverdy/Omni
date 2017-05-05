using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


using Omni.DataProviders;

namespace Omni.TaskManagers.Tasks
{
	public class LocalTask : BaseTask
	{	
		private Dictionary<string, string> m_params;
		private Dictionary<string, byte[]> m_binaryParams;

		public LocalTask(string id, string url) : base(id, url)
		{
			m_dataProvider = DataProvider.GetProviderInstance(DataProvider.DataProviderType.LocalDataProvider);
			m_url = url;
		}
		
		public override void Start() 
		{
			m_dataProvider.SendRequest(m_url, null);
			base.Start();
		}
	}
}