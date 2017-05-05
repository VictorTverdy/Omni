using Omni.Game;
using Omni.Utilities;
using Omni.CameraCtrl;

namespace Omni.GameState
{
	public class DrillingToolState : OmniBaseGameState
	{
        // Use this for initialization
		public override void Start()
		{
            base.Start();
			this.ActiveScene("DrillingToolScene");
        }
    }
}