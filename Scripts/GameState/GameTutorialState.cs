using Omni.Game;
using Omni.Utilities;
using Omni.CameraCtrl;
using UnityEngine;

namespace Omni.GameState
{
	public class GameTutorialState : OmniBaseGameState
	{
		// Use this for initialization
		public override void Start()
		{
            base.Start();

			this.ActiveScene("TrainingScene");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Suspend() {
            GameValues.lastScene = EnumScenes.Tutorial;base.Suspend();
        }
    }
}