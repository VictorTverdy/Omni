using Omni.Game;
using Omni.Utilities;
using Omni.CameraCtrl;

namespace Omni.GameState
{
	public class WellPathState : OmniBaseGameState
	{
		public override void Start()
		{
			base.Start();
			this.ActiveScene("WellPathScene");
		}
    }
}