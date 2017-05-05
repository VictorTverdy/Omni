using UnityEngine;

namespace Omni.Manager
{
    public enum eInputEvents
    {
        axisY,
        axisX,
        cameraZoom,
        mouseOver,
        rightClickDown,
        rightClickUp,
        triggerOverRight,
        triggerOverLeft,
        Esc,
        H,
        N,
        LevelLayout
    }

    public class InputManager : MonoBehaviourSingletonPersistent<InputManager>
    {
        public delegate void ClickAction(eInputEvents _event, float _value);
        public static event ClickAction OnClicked;

        public override void Awake()
        {
            base.Awake();
        }

        void Update()
        {
            if (OnClicked != null)
            {
                OnClicked(eInputEvents.axisY, Input.GetAxis("Vertical"));
                OnClicked(eInputEvents.axisX, Input.GetAxis("Horizontal")); 
                OnClicked(eInputEvents.cameraZoom, Input.GetAxis("Mouse ScrollWheel"));  
                if (Input.GetMouseButtonDown(0))                
                    OnClicked(eInputEvents.mouseOver,0); 
                if (Input.GetMouseButtonDown(1)) 
                    OnClicked(eInputEvents.rightClickDown, 0);
                if (Input.GetMouseButtonUp(1))
                    OnClicked(eInputEvents.rightClickUp, 0);
                if (Input.GetKeyDown(KeyCode.Escape))
                    OnClicked(eInputEvents.Esc, 0);
                if (Input.GetKeyDown(KeyCode.Q))
                    OnClicked(eInputEvents.LevelLayout, 0);
                if (Input.GetKeyDown(KeyCode.H))
                    OnClicked(eInputEvents.H, 0);
                if (Input.GetKeyDown(KeyCode.N))
                    OnClicked(eInputEvents.N, 0);
            }
        }
    }

   

}
