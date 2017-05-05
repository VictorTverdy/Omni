using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Omni.DataProviders;

namespace Omni.TaskManagers
{   
	public delegate void TasksCompletedCallback();
	
    /// <summary>
    /// A class that handles async calls and callbacks
    /// </summary>
    /// <remarks>Author: Danko Kozar</remarks>
    public class CallbackQueue<T>
    {
		#region Delegate definition
	
	    /// <summary>
	    /// Callback signature
	    /// </summary>
	    public delegate void Callback(T response);
		
		/// <summary>
	    /// Callback signature error
	    /// </summary>
		public delegate void CallbackError(T response);
	
	    /// <summary>
	    /// The signature of function for checking status
	    /// </summary>
	    public delegate ProviderStatus StatusChecker(T request);
		
	    #endregion

        #region Members
		
		/// <summary>
		/// Start task manager
		/// </summary>
		protected bool m_startTaskManager;
		
		/// <summary>
		/// Callback when all task completed
		/// </summary>
		protected TasksCompletedCallback m_tasksCompletedCallback;
		
		/// <summary>
		/// All active task.
		/// </summary>
		protected readonly Dictionary<T, Callback> m_activeTasks;
		
		/// <summary>
		/// Finished task.
		/// </summary>
		protected readonly Dictionary<T, Callback> m_finishedTasks;
		
		/// <summary>
		/// All error task.
		/// </summary>
		protected readonly Dictionary<T, CallbackError> m_errorTasks;
		
		/// <summary>
		/// All failed task.
		/// </summary>
		protected readonly Dictionary<T, CallbackError> m_failedTasks;
		
		/// <summary>
		/// List of delegates that points to the progress of each task.
		/// </summary>
		protected Dictionary<T, StatusChecker> m_progressCheckers;
       
        #endregion

        #region Constructor

        public CallbackQueue()
        {
			m_startTaskManager = false;
            m_activeTasks = new Dictionary<T, Callback>();		
            m_finishedTasks = new Dictionary<T, Callback>();
			m_errorTasks = new Dictionary<T, CallbackError>();
			m_failedTasks = new Dictionary<T, CallbackError>();
			m_progressCheckers = new Dictionary<T, StatusChecker>();
        }

        #endregion
		
		#region GETTERS & SETTERS
		
		/// <summary>
        /// Gets currently active requests
        /// </summary>
        public Dictionary<T, Callback> ActiveTask
        {
            get
            {
                return m_activeTasks;
            }
        }
		
		#endregion
		
        #region Methods
		
		/// <summary>
        /// Stops and clears current requests
        /// </summary>
        public void Reset()
        {
            m_activeTasks.Clear();
            m_finishedTasks.Clear();
			m_errorTasks.Clear();
        }
		
		public void StartTaskManager()
		{
			m_startTaskManager = true;
		}
		
        public virtual void Update()
        {   		
			if(m_startTaskManager)
			{
				AverageTaskProgress();
				
				CheckFuntionEndedTaskCondition();
				
	            /**
	             * Remove finished requests from active dictionary
	             * */
	            foreach (T request in m_finishedTasks.Keys)
	            {
	                m_activeTasks.Remove(request);					
					m_progressCheckers.Remove(request);  
					m_errorTasks.Remove(request);
	            }
				
			  /**
	             * Remove error requests from active dictionary
	             * */
	            foreach (T request in m_failedTasks.Keys)
	            {
	                m_activeTasks.Remove(request);
					m_progressCheckers.Remove(request); 
					m_errorTasks.Remove(request);
	            }
	
	            /**
	             * Fire callbacks for finished requests
	             * */
	            foreach (T request in m_finishedTasks.Keys)
	            {
	                m_finishedTasks[request](request);
	            }
				
				/**
	             * Fire callbacks for error requests
	             * */
	            foreach (T request in m_failedTasks.Keys)
	            {
	                m_failedTasks[request](request);
	            }
								
				ThereArePendingTask();
				
	            /**
	             * Clear finished dictionary
	             * */
	            m_finishedTasks.Clear();
			}
        }

        protected T AddCompleteCallbackToQueue(T request, Callback callback)
        {
            m_activeTasks.Add(request, callback);
            return request;
        }
		
		protected T AddProgressCallbackToQueue(T request, StatusChecker callbackProgress)
        {
            m_progressCheckers.Add(request, callbackProgress);
            return request;
        }
		
		protected T AddErrorCallbackToQueue(T request, CallbackError callbackError)
        {
            m_errorTasks.Add(request, callbackError);
            return request;
        }
		
	    /// <summary>
        /// Look up for finished requests
        /// </summary>
		protected virtual void CheckFuntionEndedTaskCondition()
		{
            foreach (T request in m_progressCheckers.Keys)
            {
                if (m_progressCheckers[request](request) == ProviderStatus.COMPLETED)
                {				
					m_finishedTasks.Add(request, m_activeTasks[request]);
				}
				else if(m_progressCheckers[request](request) == ProviderStatus.ERROR)
				{
					m_failedTasks.Add(request, m_errorTasks[request]);
				}
				else if(m_progressCheckers[request](request) == ProviderStatus.IN_PROGRESS)
				{
					continue;
				}
				else if(m_progressCheckers[request](request) == ProviderStatus.UNDEFINED)
				{
					m_failedTasks.Add(request, m_errorTasks[request]);
				}
            }
		}
		
		/// <summary>
        /// Checks if there are not any task pending
        /// </summary>
		protected virtual void ThereArePendingTask()
		{
           if(m_activeTasks != null && m_activeTasks.Count <= 0)
			{
				m_tasksCompletedCallback();
				m_startTaskManager = false;				
				CleanAllReferencesOfManager();
			}
		}	
		
		protected virtual void CleanAllReferencesOfManager()
		{
			m_progressCheckers.Clear();
			m_tasksCompletedCallback = null;
		}
		
		protected virtual void AverageTaskProgress(){}	
        #endregion
    }
}