using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public class InputDeviceVive : MonoBehaviour
    {
        public VRMovement MyVRMovement;
        public GameObject Laser;

        private Transform laserTransform;      
        private Vector3 hitPoint;
        private float distance = 100;
        private RaycastInfo raycastInfo;
        private SteamVR_TrackedObject trackedObj;
        private SteamVR_Controller.Device Controller
        {
            get { return SteamVR_Controller.Input((int)trackedObj.index); }
        }

        void Start()
        {
            raycastInfo = MyVRMovement.GetComponent<RaycastInfo>();
               laserTransform = Laser.transform;
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        }

        void OnEnable()
        {
            MyVRMovement.SetIsVive(true);
        }

        void OnDisable()
        {          
            MyVRMovement.SetIsVive(false);
        }

        // Update is called once per frame
        void Update()
        {
            MyVRMovement.MovingVRJoystick(Controller.GetAxis().y, Controller.GetAxis().x);

            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                MyVRMovement.ActivateLevelLayout();

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
                MyVRMovement.OnClickESC();

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
                raycastInfo.ShowRadarVive();

            if (Controller.GetHairTriggerUp())
                raycastInfo.RaycastHandOver();

            if (Controller.GetHairTrigger())
            {
                RaycastHit hit;
                if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, distance))
                {
                    hitPoint = hit.point;
                    ShowLaser(hit);
                }
                else
                {
                    ShowLaser();
                }
            }
            else
            {
                Laser.SetActive(false);
            }
        }

        private void ShowLaser() 
        {
            Laser.SetActive(true);            
            Vector3 forwardPos = trackedObj.transform.forward * distance;
            laserTransform.position = Vector3.Lerp(trackedObj.transform.position, forwardPos, .5f);
            laserTransform.LookAt(forwardPos);
            laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, distance);
        }

        private void ShowLaser(RaycastHit hit)
        {
            Laser.SetActive(true);            
            laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);           
            laserTransform.LookAt(hitPoint);           
            laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,hit.distance);
        }

    }
}
