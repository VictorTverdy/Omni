using System;
using Omni.DataProviders;

namespace Omni.TaskManagers.Tasks
{
	public class BaseTask
	{
		public delegate void TaskEventHandler(BaseTask sender);

		public event TaskEventHandler OnComplete; 
		public event TaskEventHandler OnError;
		public event TaskEventHandler OnProgress;

		protected IDataProvider m_dataProvider = null;
		protected string m_url;

		public BaseTask (string id, string url)
		{
			TaskId = id;
			TaskProgress = 0.0f;
			m_url = url;
		}
		#region Getter & Setters
		public bool Started { get; set; }

		public string Url
		{
			get { return m_url; }
		}

		public string TaskId
		{
			get;
			protected set;
		}
		
		public float TaskProgress
		{
			get;
			protected set;
		}
		#endregion

		public IDataProvider GetDataProvider()
		{
			return m_dataProvider;
		}

		public virtual bool IsCompleted()
		{
			if(m_dataProvider.GetReady() == ProviderStatus.IN_PROGRESS || m_dataProvider.GetReady() == ProviderStatus.UNDEFINED)
			{
				return false;
			}
			return true;
		}

		public virtual void Update() 
		{
			if(Started)
			{
				switch(m_dataProvider.GetReady())
				{
					case ProviderStatus.COMPLETED:
						RaiseOnComplete();
						break;
					case ProviderStatus.ERROR:
						RaiseOnError();
						break;
					case ProviderStatus.IN_PROGRESS:
						TaskProgress = m_dataProvider.Progress;
						RaiseOnProgress();
						break;
				}
			}
		}
		
		public virtual void Start() 
		{
			Started = true;
		}

		protected void RaiseOnComplete()
		{
			if(OnComplete != null)
			{
				OnComplete(this);
			}
		}

		protected void RaiseOnError()
		{
			if(OnError != null)
			{
				OnError(this);
			}
		}

		protected void RaiseOnProgress()
		{
			if(OnProgress != null)
			{
				OnProgress(this);
			}
		}

		public virtual void Dispose()
		{
			OnComplete = null;
			OnError = null;

			m_dataProvider.Dispose();
		}
	}
}

