using System.Collections;
using UnityEngine;

using Omni.GameState;
using Omni.UI.ScreensControllers;

namespace Omni.UI.LoadingScreen
{
	public class LoadingScreenController : MonoBehaviour, IUISceneController {

		public CanvasRenderer MenuPanel;
		public CanvasRenderer LoadingPanel;

		public void Start()
		{
			ShowMenu (false);
		}

		public void ChangeToMenuState()
		{
			StartCoroutine (YieldTheUIInLoadingForTest());
		}

		private IEnumerator YieldTheUIInLoadingForTest()
		{
			yield return new WaitForSeconds (2);
			ShowMenu (true);
		}

		public void StartTutorial()
		{
			GameStateManager.Instance.SwapToState(new GameTutorialState());
		}

		public void StartApp()
		{
            GameStateManager.Instance.SwapToState(new WorldLevelState());   
        }

		public void ResetUI()
		{
			ShowMenu (false);
		}

		private void ShowMenu(bool showMenu)
		{
			MenuPanel.gameObject.SetActive (showMenu);
			LoadingPanel.gameObject.SetActive (!showMenu);
		}
	}
}