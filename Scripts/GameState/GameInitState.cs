using Omni.Entities;
using Omni.Utilities;
using Omni.Singletons;
using Omni.DataProviders;
using Omni.UI.LoadingScreen;
using Omni.TaskManagers.Tasks;
using UnityEngine;
using Omni.TaskManagers;
using Omni.Events;
using Omni.Utilities.EventHandlers;

namespace Omni.GameState
{
    public class GameInitState : OmniBaseGameState
	{
		private LoadingScreenController m_loadingScreenController;

		private bool m_startScreenMenu;
		private bool m_taskAreCompleted;

		// Use this for initialization
		public override void Start()
		{
			base.Start();
            
            LocalTask serviceTask = new LocalTask("ConfigFile", "Config/ConfigJson");
			serviceTask.OnComplete += OnConfigComplete;
			serviceTask.OnError += HandleOnError;             
			m_taskManager.AddTask(serviceTask);

			WWWTask wwwTask = new WWWTask ("LogoImage", Config.LOGO_IMAGE_AT_LOCAL_DISK, true, null);
			wwwTask.OnComplete += OnLogoImageCompleted;
			wwwTask.OnError += HandleOnErrorImage;
			m_taskManager.AddTask(wwwTask);

			m_taskManager.OnCompleteAllTasks += OnTaskCompleted;
			m_taskManager.StartTaskManager();
            
			ActiveScene("InitScene");
		}

		public override void Update()
		{
			base.Update ();

			if (!m_startScreenMenu && m_sceneIsLoaded && m_taskAreCompleted) {
				m_startScreenMenu = true;
				m_loadingScreenController.ChangeToMenuState ();
			}
		}

		public override void PreconfigureStateScene()
		{	
			m_loadingScreenController = m_sceenGameObject.GetComponentInChildren<LoadingScreenController>();
		}

		private void OnTaskCompleted(TaskManager sender)
		{			         
			m_taskAreCompleted = true;
		}

		private void OnConfigComplete(BaseTask sender)
		{
            AssetListInfo assetList = sender.GetDataProvider().GetData< AssetListInfo> (ResponseType.Json);
			UserDataSettings.Instance.AssetList = assetList.AssetLists;  
		}

		private void OnLogoImageCompleted(BaseTask sender)
		{
			Sprite sprite = sender.GetDataProvider().GetData<Sprite> (ResponseType.Jpg);
			UserDataSettings.Instance.ClientLogo = sprite;

			HUDEvent evt = new HUDEvent (HUDEvent.ON_LOGO_IMAGE_WAS_LOADED);
			evt.arguments ["imageLoaded"] = true;

			EventManager.instance.dispatchEvent (evt);
		}

		private void HandleOnError (BaseTask sender)
		{
			Debug.LogError("ERROR " + sender.TaskId);
			MachineContainer.SwapToState(new GameErrorState());
		}

		private void HandleOnErrorImage (BaseTask sender)
		{
			Debug.LogError ("THERE IS NOT CLIENT IMAGE");

			HUDEvent evt = new HUDEvent (HUDEvent.ON_LOGO_IMAGE_WAS_LOADED);
			evt.arguments ["imageLoaded"] = false;

			EventManager.instance.dispatchEvent (evt);
		}

        public override void Exit()
        {
            GameValues.lastScene = EnumScenes.Intro;
			m_loadingScreenController.ResetUI();
            base.Exit();
        }

        public override void Suspend()
        {
            GameValues.lastScene = EnumScenes.Intro;
            base.Suspend();
        }
	}
}
