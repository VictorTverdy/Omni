using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Omni.BaseLibs;
using Omni.Utilities;
using Omni.TaskManagers;
using Omni.TaskManagers.Tasks;
using Omni.UI.ScreensControllers;
using UnityEngine.SceneManagement;

namespace Omni.GameState
{
	public class OmniBaseGameState : State
	{	
		protected bool m_sceneIsLoaded;
		protected TaskManager m_taskManager;
		protected AsyncOperation m_asyncOperation;

		protected string m_scenePath;
		protected GameObject m_sceenGameObject;
		protected GameObjectSceneController m_gameObjectSceneController;

		// Use this for initialization
		public override void Start()
		{
			m_sceneIsLoaded = false;
			m_taskManager = new TaskManager();
			m_gameObjectSceneController = new GameObjectSceneController();
		}

		public GameObjectSceneController GameObjectSceneController
		{
			get { return m_gameObjectSceneController; }
		}
		
		// Update is called once per frame
		public override void Update()
		{
			if(m_taskManager != null)
			{
				m_taskManager.Update();
			}

			if (!m_sceneIsLoaded && m_asyncOperation.isDone) {
				m_sceneIsLoaded = true;

				Scene scene = SceneManager.GetSceneByName(m_scenePath);
                SceneManager.SetActiveScene(scene);
				m_sceenGameObject = scene.GetRootGameObjects () [0];

				PreconfigureStateScene ();
				(MachineContainer as GameStateManager).SceneIsLoadedStartFadeOut();				
			}
		}
		
		public override void Exit ()
		{
			base.Exit();

			m_taskManager.Dispose();
			m_gameObjectSceneController.CleanScene();
		}

		public override void Suspend()
		{ 
			m_taskManager.SuspendTaskManager();

			m_sceenGameObject.SetActive (false);

			m_gameObjectSceneController.EnableObjectsInScene (false);
		}

		public override void Resume()
		{
			m_taskManager.ResumeTaskManager();

			m_sceenGameObject.SetActive (true);
			m_gameObjectSceneController.EnableObjectsInScene (true);
		}

		public virtual GameObject RegisterGameObject(string gameObjectPath)
		{
			GameObject regiterObject = null;
			if(gameObjectPath != null)
			{
				regiterObject = GeneralUtilities.InstantiatePrefab(gameObjectPath, null);
				m_gameObjectSceneController.RegisterGameObject(regiterObject.name, regiterObject);
			}
			return regiterObject;
		}

		public virtual void RegisterGameObject(string gameObjectName, GameObject gameObject)
		{
			m_gameObjectSceneController.RegisterGameObject(gameObjectName, gameObject);
		}

		public virtual void RemoveObject(GameObject gameObject)
		{
			m_gameObjectSceneController.RemoveGameObject(gameObject);
		}

		protected void ActiveScene(string scenePath)
		{
			m_scenePath = scenePath;
			m_asyncOperation = m_gameObjectSceneController.CreateScene(scenePath);
		}

		public virtual void PreconfigureStateScene(){}

		public virtual void RunStateScene(){}

	}
}