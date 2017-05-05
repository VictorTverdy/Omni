using UnityEngine;

using Omni.Utilities;
using Omni.CameraCtrl;

namespace Omni.GameState
{
    public class WorldLevelState : OmniBaseGameState
    {
        // Use this for initialization
        public override void Start()
        {
            base.Start();
            
			ActiveScene("WorldMapScene");
        }

        public override void RunStateScene() {
        }
        public override void Suspend()
        {
            GameValues.lastScene = EnumScenes.WorldMap;base.Suspend();
        }
    }
}
