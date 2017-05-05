using UnityEngine;

using Omni.Utilities;
using Omni.CameraCtrl;
using Omni.Events;
using Omni.Utilities.EventHandlers;
using UnityEngine.SceneManagement;

namespace Omni.GameState
{
	public class HeightLevelState : OmniBaseGameState
	{
		// Use this for initialization
		public override void Start()
		{
            base.Start();
			this.ActiveScene("WellBarsVisualization");
		}

		public override void Resume()
		{
			base.Resume ();

			HUDEvent evt = new HUDEvent (HUDEvent.ON_HIDE_INFO_BARS);
			evt.arguments ["canUse"] = true;
			EventManager.instance.dispatchEvent(evt);
		}
                
        public override void Suspend()
        {
            GameValues.lastScene = EnumScenes.Wellbars;base.Suspend();
        }

    }
}