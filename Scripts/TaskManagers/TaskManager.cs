using System;
using UnityEngine;
using System.Collections.Generic;

using Omni.TaskManagers.Tasks;

namespace Omni.TaskManagers
{	
	public class TaskManager //: CallbackQueue<BaseTask>
	{
		public delegate void ManagerEventHandler(TaskManager sender);
		
		public event ManagerEventHandler OnCompleteAllTasks; 

		private List<BaseTask> m_taskList;
		//This will prevent allocating a list each update
		private List<BaseTask> m_removalList;
		//Prevent the modification of the list within an update cycle when adding tasks
		private List<BaseTask> m_newTaskList;
		protected bool m_taskManagerStarted;

		public TaskManager()
		{
			m_taskManagerStarted = false;
			m_taskList = new List<BaseTask>();
			m_newTaskList = new List<BaseTask>();
			m_removalList = new List<BaseTask>();
		}

		public void AddTask(BaseTask newTask)
		{			
			m_newTaskList.Add(newTask);
		}

		public void StartTaskManager()
		{
			m_taskManagerStarted = true;
			m_taskList.AddRange(m_newTaskList);
			m_newTaskList.Clear();

			foreach (BaseTask task in m_taskList) 
			{
				task.Start();
			}
		}

		public void SuspendTaskManager()
		{
			m_taskManagerStarted = false;
		}

		public void ResumeTaskManager()
		{
			m_taskManagerStarted = true;
		}


		public void Update()
		{
			if(m_taskManagerStarted && m_taskList.Count > 0 || m_newTaskList.Count > 0)
			{
				m_taskList.AddRange(m_newTaskList);
				m_newTaskList.Clear();

				//Update All Tasks
				for (int i = 0; i < m_taskList.Count; i++)
				{
					BaseTask task = m_taskList[i];
					if(task.Started)
					{
						//This will fire all events for completed or error
						task.Update();
						if(task.IsCompleted())
						{
							m_removalList.Add(task);
						}
					}
					else
					{
						task.Start();
					}
				}

				//Calculate Progress
				AverageTaskProgress();

				//Remove completed Tasks
				foreach (BaseTask task in m_removalList)
				{
					m_taskList.Remove(task);
					task.Dispose();
				}

				m_removalList.Clear();

				//If all tasks are completed
				if(m_taskManagerStarted && m_taskList.Count == 0 && m_newTaskList.Count == 0 )
				{
					if(OnCompleteAllTasks != null)
					{
						OnCompleteAllTasks(this);
					}
				}
			}
		}
		
		protected void AverageTaskProgress()
		{
			if(m_taskList != null && m_taskList.Count > 0)
			{
				float progress = 0.0f;				
				foreach (BaseTask task in m_taskList) 
				{
					progress += task.TaskProgress;
				}
				
				GeneralProgress = progress / m_taskList.Count;
			}
			else
			{
				GeneralProgress = 0.0f;
			}
		}

		public void Dispose()
		{
			foreach (BaseTask task in m_taskList)
			{
				task.Dispose();
			}

			m_taskList.Clear();
			m_removalList.Clear();
			m_newTaskList.Clear();

			OnCompleteAllTasks = null;
		}

		public float GeneralProgress
		{
			get;
			protected set;
		}

		public float PendingTasks
		{	
			get{ return m_taskList.Count; }
		}
	}
}