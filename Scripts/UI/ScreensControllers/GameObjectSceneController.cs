using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Omni.GameState;
using Omni.BaseLibs;
using System;
using UnityEngine.SceneManagement;

namespace Omni.UI.ScreensControllers
{
	public class GameObjectSceneController 
	{
		private string m_currentScenePath;
		private string m_currentPrefabPath;
		private GameObject m_currentPrefabObject;
		private Dictionary<string, GameObject> m_childObjects;

		public GameObjectSceneController()
		{
			m_currentPrefabPath = "";
			m_currentPrefabObject = null;
			m_childObjects = new Dictionary<string, GameObject>();
		}

		public GameObject CurrentSceneObject
		{
			get { return m_currentPrefabObject; }
		}

		public void CleanScene()
		{
			if(m_currentPrefabObject != null)
			{
				GameObject.Destroy(m_currentPrefabObject);
			}

			if (!String.IsNullOrEmpty (m_currentScenePath)) {
				SceneManager.UnloadSceneAsync (m_currentScenePath);
				m_currentScenePath = null;
			}

			m_currentPrefabPath = null;
			m_currentPrefabObject = null;

			RemoveAllChildrenInScene();
		}

		public void EnableObjectsInScene(bool enable)
		{
			if(m_currentPrefabObject != null)
			{
				m_currentPrefabObject.SetActive(enable);
			}

			foreach(KeyValuePair<string, GameObject> child in m_childObjects)
			{
				child.Value.SetActive(enable);
			}
		}

		public AsyncOperation CreateScene(string scenePath)
		{
			m_currentScenePath = scenePath;
			return SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
		}

		public GameObject CreatePrefabAsScene(string scenePath)
		{
			if(m_currentPrefabPath != scenePath)
			{
				if(scenePath != null)
				{
					m_currentPrefabObject = GeneralUtilities.InstantiatePrefab(scenePath, null);
					m_currentPrefabPath = scenePath;
				}
			}
			return m_currentPrefabObject;
		}

		public void RemoveAllChildrenInScene ()
		{
			foreach(KeyValuePair<string, GameObject> child in m_childObjects)
			{
				//Remove them from scene
				GameObject.Destroy(child.Value);
			}
			m_childObjects.Clear();
		}
		
		public bool RegisterGameObject(string name, GameObject element)
		{
			if(m_childObjects.ContainsKey(name) || m_childObjects.ContainsValue(element))
			{
				return false;
			}
			
			m_childObjects.Add(name, element);
			return true;
		}
		
		public GameObject GetGameObject(string name)
		{
			if(m_childObjects.ContainsKey(name))
			{
				return m_childObjects[name];
			}
			
			return null;
		}
		
		public bool RemoveGameObject(GameObject element, bool destroyOnRemove = true)
		{
			if(m_childObjects.ContainsValue(element))
			{
				foreach (KeyValuePair<string, GameObject> item in m_childObjects) 
				{
					if(item.Value == element)
					{
						return RemoveGameObject(item.Key, destroyOnRemove);
					}
				}
			}
			return false;
		}
		
		public bool RemoveGameObject(string name, bool destroyOnRemove = true)
		{
			if(m_childObjects.ContainsKey(name))
			{
				if(destroyOnRemove)
				{
					GameObject.Destroy(m_childObjects[name]);
				}
				m_childObjects.Remove(name);
				return true;
			}
			return false;
		}
	}
}