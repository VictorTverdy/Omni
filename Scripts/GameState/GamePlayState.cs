using Omni.Game;
using Omni.Utilities;
using Omni.UI.Gameplay.HUD;

namespace Omni.GameState
{
    public class GamePlayState : OmniBaseGameState
	{	
        private GameController m_gameController;
		private MainHUDController m_mainHudController;

		// Use this for initialization
		public override void Start()
		{
            base.Start();

			ActiveScene ("LocationScene");
		}

		public override void PreconfigureStateScene()
		{	
			m_gameController = m_sceenGameObject.GetComponent<GameController>();
			m_gameController.SetupGame ();
		}

		public override void RunStateScene()
		{
			m_gameController.RunGame ();
		} 

        public override void Suspend()
        {
            GameValues.lastScene = EnumScenes.Game;base.Suspend();
        }
    }
}