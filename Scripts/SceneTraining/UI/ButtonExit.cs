using Omni.GameState;
using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class ButtonExit : MonoBehaviour,IToDo
    {
        private bool isOnce;

        public void Action()
        {
            if (!isOnce)
            {
                isOnce = true; 
				GameStateManager.Instance.SwapToState(new GameInitState());
            }
        }       
    }
}
