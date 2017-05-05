using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class DrillingRigUpState : MonoBehaviour
    {
        private VRMovement player;
        private Vector3 posStart = new Vector3(-18.9f,2.25f,7.2f);
        private Vector3 posLook = new Vector3(0, -130f, 0);

        void OnEnable()
        {
            Invoke("FadeScreen", 1);
        }

        void OnDisable()
        {
            CancelInvoke("FadeScreen");
        }

        private void FadeScreen()
        {           
            CanvasVRPlayer.Instance.SetActivateFade();
            Invoke("StartSituation",2f);
        }

        private void StartSituation()
        {
            ManagerDrillingItems.Instance.ItemsStartPosition(false);
            if (player == null)
                player = FindObjectOfType<VRMovement>();
            player.transform.position = posStart;
            player.transform.eulerAngles = posLook;          
        }
    }
}
