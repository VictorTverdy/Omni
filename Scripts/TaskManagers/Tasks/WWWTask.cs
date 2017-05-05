using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


using Omni.DataProviders;

namespace Omni.TaskManagers.Tasks
{
	public class WWWTask : BaseTask
	{	
		private Dictionary<string, string> m_params;
		private Dictionary<string, byte[]> m_binaryParams;

		public WWWTask(string id, string url, bool isLocal, Dictionary<string, string> parameters, Dictionary<string, byte[]> binaryParameters = null) : base(id, url)
		{
			m_dataProvider = DataProvider.GetProviderInstance(isLocal ? DataProvider.DataProviderType.LocalHttpDataProvider : DataProvider.DataProviderType.HttpDataProvider);
			m_url = url;
			m_params = parameters;
			m_binaryParams = binaryParameters;
		}
		
		public override void Start() 
		{
			if(m_binaryParams != null)
			{
				m_dataProvider.SendRequest(m_url, m_params, m_binaryParams);
			}
			else
			{
				m_dataProvider.SendRequest(m_url, m_params);
			}
			base.Start();
		}
	}
}