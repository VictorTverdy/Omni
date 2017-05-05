//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Omni.DataProviders
{
	public abstract class DataProvider: IDataProvider
	{
		protected string m_url;
		protected WWW m_wwwRequest;

		public enum DataProviderType
		{
			HttpDataProvider,
			LocalHttpDataProvider,
			AssetBundleDataProvider,
			LocalDataProvider
		}

		public static IDataProvider GetProviderInstance(DataProviderType dataProviderType)
		{
			IDataProvider dataProvider = null;
			switch(dataProviderType)
			{
				case DataProviderType.HttpDataProvider:
					dataProvider = new HttpDataProvider();
					break;
				case DataProviderType.LocalHttpDataProvider:
					dataProvider = new LocalHttpDataProvider();
					break;
				case DataProviderType.LocalDataProvider:
					dataProvider = new LocalDataProvider();
					break;
			}
			return	dataProvider;
		}
		
		public DataProvider() { } 

		public virtual ProviderStatus GetReady()
		{
			if(m_wwwRequest != null)
			{
				if(m_wwwRequest.isDone)
				{
					if(m_wwwRequest.error != null)
					{
						Debug.LogError("Error Downloading the resource " + m_wwwRequest.url); 
						return ProviderStatus.ERROR; 
					}
					else
					{
						return ProviderStatus.COMPLETED;
					}
				}
				else
				{
					Progress = m_wwwRequest.progress;
					return ProviderStatus.IN_PROGRESS;
				}
			}
			else
			{
				return ProviderStatus.UNDEFINED; 
			}
		}
		
		protected abstract void ConfigureProvider(Dictionary<string, string> parameters, Dictionary<string, byte[]> binaryParameters = null);

		public abstract void SendRequest(string url, Dictionary<string, string> parameters, Dictionary<string, byte[]> binaryParameters = null);
		
		public abstract T GetData<T>(ResponseType responseType) where T : class;

		public float Progress
		{
			get;
			protected set;
		}

		public void Dispose()
		{
			if(m_wwwRequest != null)
			{
				m_wwwRequest.Dispose();
			}
		}
	}
}