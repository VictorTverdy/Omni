using System;
using UnityEngine;
using System.Collections;

namespace Omni.Utilities.EventHandlers
{
	public class EventManager : MonoBehaviour 
	{		
		// singleton instance
		public static EventManager instance;
		
		// settings
		public bool allowSingleton = true; // EventManager class will transfer between scene changes.
		public bool allowWarningOutputs = true;
		public bool allowDebugOutputs = true;
		public bool allowAutoCleanUp = true;
		
		private static bool _created = false;	
		private Hashtable _listeners = new Hashtable();	
		
		// setup singleton if allowed
		private void Awake() {
			if (!_created && allowSingleton) {
				DontDestroyOnLoad(this);
				instance = this;
				_created = true;
				Setup();
			} else {
				if (allowSingleton) {
					if (EventManager.instance.allowWarningOutputs) {
						Debug.LogWarning("Only a single instance of " + this.name + " should exists!");
					}
					Destroy(gameObject);
				} else {
					instance = this;
					Setup();
				}
			}
		}
		
		// clear events on quit
		private void OnApplicationQuit() {
			_listeners.Clear();
		}
		

		// PUBLIC *******************************
		
		// Add event listener
		public bool addEventListener(string eventType, GameObject listener, string function) {
			if (listener == null || eventType == null) {
				if (allowWarningOutputs) {
					Debug.LogWarning("Event Manager: AddListener failed due to no listener or event name specified.");
				}
				return false;
			}
			recordEvent(eventType);
			return recordListener(eventType, listener, function);
		}
		
		// Remove event listener
		public bool removeEventListener(string eventType, GameObject listener) {
			if (!checkForEvent(eventType)) return false;
			
			ArrayList listenerList = _listeners[eventType] as ArrayList;
			foreach (EventListener callback in listenerList) {
				if (callback.name == listener.GetInstanceID().ToString()) {
					listenerList.Remove(callback);
					return true;
				}
			}
			return false;
		}
		
		// Remove all event listeners
		public void removeAllEventListeners(GameObject listener) {
			ArrayList removeList; // create remove list to not break the enumerator
			ArrayList listenerList;
			foreach (DictionaryEntry listenerListObj in _listeners) {
				listenerList = listenerListObj.Value as ArrayList;
				removeList = new ArrayList();
				// find and add to remove list
				foreach (EventListener callback in listenerList) {
					if (callback.listener != null) {
						if (callback.name == listener.GetInstanceID().ToString()) {
							removeList.Add(callback);
						}
					}
				}
				// remove from list
				foreach (EventListener callback in removeList) {
					listenerList.Remove(callback);
				}
			}
		}
		
		// Dispatch an event
		public bool dispatchEvent(CustomEvent evt) {
			string eventType = evt.type;
			if (!checkForEvent(eventType)) {
				if (allowWarningOutputs) {
					Debug.LogWarning("Event Manager: Event \"" + eventType + "\" triggered has no listeners!");
				}
				return false;
			}
			
			ArrayList listenerList = _listeners[eventType] as ArrayList;
			if (allowDebugOutputs) {
				Debug.Log("Event Manager: Event " + eventType + " dispatched to " + listenerList.Count + ((listenerList.Count == 1) ? " listener." : " listeners."));
			}
			for (int i = listenerList.Count - 1; i >= 0 ; i--) {
				EventListener callback = (EventListener)listenerList [i];
				if (callback.listener && callback.listener.activeSelf) {
					callback.listener.SendMessage(callback.function, evt, SendMessageOptions.DontRequireReceiver);
				}
			}
			return false;
		}
		
		// PRIVATE *******************************
		
		private void Setup() {
			// TO DO: Self create GameObject if not already created
		}
		
		// see if event already exists
		private bool checkForEvent(string eventType) {
			if (_listeners.ContainsKey(eventType)) return true;
			return false;
		}
		
		// record event, if it doesn't already exists
		private bool recordEvent(string eventType) {
			if (!checkForEvent(eventType)) {
				_listeners.Add(eventType, new ArrayList());
			}
			return true;
		}
		
		// delete event, if not already removed
		private bool deleteEvent(string eventType) {
			if (!checkForEvent(eventType)) return false;
			_listeners.Remove(eventType);
			return true;
		}
		
		// check if listener exists
		private bool checkForListener(string eventType, GameObject listener) {
			if (!checkForEvent(eventType)) {
				recordEvent(eventType);
			}
			
			ArrayList listenerList = _listeners[eventType] as ArrayList;
			foreach (EventListener callback in listenerList) {
				if (callback.name == listener.GetInstanceID().ToString()) return true;
			}
			return false;
		}
		
		// record listener, if not already recorded
		private bool recordListener(string eventType, GameObject listener, string function) {
			if (!checkForListener(eventType, listener)) {
				ArrayList listenerList = _listeners[eventType] as ArrayList;
				EventListener callback = new EventListener();
				callback.name = listener.GetInstanceID().ToString();
				callback.listener = listener;
				callback.function = function;
				listenerList.Add(callback);
				return true;
			} else {
				if (allowWarningOutputs) {
					Debug.LogWarning("Event Manager: Listener: " + listener.name + " is already in list for event: " + eventType);
				}
				return false;
			}
		}	
	}

	// internal event listener model
	internal class EventListener 
	{
		public string name;
		public GameObject listener;
		public string function;	
	}
}