using UnityEngine;
using Omni.Utilities;
using Omni.Utilities.EventHandlers;
using Omni.Events;
using Omni.UI.ScreensControllers;

namespace Omni.GameState
{
    public class GameStateManager : StateMachine
    {
        private State m_newState;
        private int m_callStepOF;
        private bool m_isCallFromTransitionState;

        #region Singleton
        private static GameStateManager instance;

        public static GameStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject _manager = GameObject.Find("GameStateManager");
                    if (_manager != null)
                        instance = _manager.GetComponent<GameStateManager>();
                }

                return instance;
            }
        }

        #endregion

        public void OnApplicationQuit()
        {
            instance = null;
        }

        public void Awake()
        {
            if (instance != null)
                DestroyImmediate(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }

        #region Getters & Setters

        #endregion

        // Use this for initialization
        public override void Start()
        {
            Config.Configure();
            EventManager.instance.addEventListener(FadeEffectEvent.ON_FADE_IN_ANIMATION_FINISHED, this.gameObject, "OnFadeInEffectFinished");
            EventManager.instance.addEventListener(FadeEffectEvent.ON_FADE_ANIMATION_STATE_COMPLETED, this.gameObject, "OnFadeOutEffectFinished");

            //The First state in the Game Must be Loading
            base.SwapToState(new GameInitState()); 
        }

        public override void SwapToState(State newState)
        {
            SetCursorLock(true);
            m_newState = newState;

            UIController.Instance.FadeEffectController.StateTransition(1);
        }

        public override void PopState()
        {
            SetCursorLock(true);
            UIController.Instance.FadeEffectController.StateTransition(2);
        }

        public override void PushState(State newState)
        {
            SetCursorLock(true);
            m_newState = newState;

            UIController.Instance.FadeEffectController.StateTransition(3);
        }

        public void SceneIsLoadedStartFadeOut()
        {
            UIController.Instance.FadeEffectController.StartFadeOutStateTransition();
        }

        private void OnFadeInEffectFinished(FadeEffectEvent evt)
        {
            bool isCallFromTransitionState = false;
            if (evt.arguments.ContainsKey("isCallFromTransitionState"))
            {
                isCallFromTransitionState = (bool)evt.arguments["isCallFromTransitionState"];
            }            

            if (isCallFromTransitionState)
            {
                m_callStepOF = (int)evt.arguments["callStepOF"];

                switch (m_callStepOF)
                {
                    case 1:
                        base.SwapToState(m_newState);
                        break;
                    case 2:
                        UIController.Instance.FadeEffectController.StartFadeOutStateTransition();
                        base.PopState();
                        break;
                    case 3:
                        base.PushState(m_newState);
                        break;
                }
            }
        }

        private void OnFadeOutEffectFinished(FadeEffectEvent evt)
        {
            SetCursorLock(false);
            if (m_callStepOF != 2)
            {
                (this.CurrentState as OmniBaseGameState).RunStateScene();
            }
        }

        private void SetCursorLock(bool lockCursor)
        {
            Cursor.visible = !lockCursor;
            Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
