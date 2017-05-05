using UnityEngine;
using System.Collections;
using Omni.Manager;
using UnityStandardAssets.Characters.FirstPerson;
using Omni.Events;
using Omni.Utilities.EventHandlers;

namespace Omni.Game.SceneTraining
{
    public class VRMovement : MonoBehaviour
    {
        [SerializeField]
        private MouseLook m_MouseLook;

        public Camera m_Camera;     
        public GameObject Extinguisher;
        public GameObject GasMask;
        public GameObject levelLayout;

        private RaycastInfo raycastInfo;
        private Vector3 startPos = new Vector3(-44, 0, -17);
        private Vector3 startAngl =new Vector3(0, -290, 0);
        private const float speedMove = 8f;
        private const float speedMoveAuto = 7f;
        private const float maxVelocityChange = 10.0f;
        private Transform myTransform;
        private Rigidbody myRigibody;
        private bool canMove = true;

        private void OnEnable()
        {           
            InputManager.OnClicked += Moving;
        }

        private void OnDisable()
        { 
            InputManager.OnClicked -= Moving;
        }

        private void Start()
        {
            myRigibody = GetComponent<Rigidbody>();
            raycastInfo = GetComponent<RaycastInfo>();
            MoveToStartPos();
            myTransform = transform;
            m_MouseLook.Init(transform, m_Camera.transform);
        }

        private void Update()
        {
            m_MouseLook.LookRotation(myTransform, m_Camera.transform); 
        }

        private void FixedUpdate()
        {
            m_MouseLook.UpdateCursorLock();
            if (canMove  && !raycastInfo.GetIsVive())
            {
                RigibobyMove(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));                  
            }
        }     

        public void SetCanMove(bool _value)
        {
            canMove = _value;
            if (myRigibody == null)
                myRigibody = GetComponent<Rigidbody>();
            if (!canMove)
            {
                myRigibody.isKinematic = true;
            }
            else
            {
                myRigibody.isKinematic = false; 
            }
        }

        public void ActivateLevelLayout()
        { 
            if (levelLayout.activeInHierarchy)
                levelLayout.SetActive(false);
            else
                levelLayout.SetActive(true);
        }

        public void StartMoveTo(Vector3 _pos)
        {
            StopCoroutine("MoveTo");
            StartCoroutine("MoveTo", _pos);
        }

        public void OnClickESC()
        {          
            TrainingEvent evt = new TrainingEvent(TrainingEvent.auto_menu);
            EventManager.instance.dispatchEvent(evt);
        }      

        public void ShowMenu()
        {
            TrainingEvent evt = new TrainingEvent(TrainingEvent.open_menu);
            EventManager.instance.dispatchEvent(evt);
        }

        public void HideMenu()
        {
            TrainingEvent evt = new TrainingEvent(TrainingEvent.close_menu);
            EventManager.instance.dispatchEvent(evt);
        }

        public void SetIsVive(bool _value)
        {
            if(raycastInfo == null)
                raycastInfo = GetComponent<RaycastInfo>();
            raycastInfo.SetIsVive(_value);                    
        }

        public void ActivateGasMask() 
        {
            GasMask.SetActive(true);
        }
               
        public void MovingVRJoystick(float _valueY, float _valueX)
        {
            if (canMove && raycastInfo.GetIsVive())
            {
                RigibobyMove(new Vector3(_valueY, 0, _valueX));         
            }
        }    

        public void TryInteract(IToDo _interact)
        {
            if (_interact != null)
            {
                _interact.Action();
            }
        }

        public void MoveToStartPos()
        {
            SetCanMove ( true);
            if (raycastInfo == null)
                raycastInfo = GetComponent<RaycastInfo>();
            raycastInfo.ZeroRadarUse();
            GasMask.SetActive(false);
            Extinguisher.SetActive(false);
            transform.localPosition = startPos; 
            transform.localEulerAngles = startAngl;
            if (!raycastInfo.GetIsVive())
                m_Camera.transform.localPosition = new Vector3(0, 1.7f, 0);
            else
                m_Camera.transform.localPosition = Vector3.zero;

            StopCoroutine("MoveTo");
        }

        public void SetParentCameraExtinguisher()
        {
            Extinguisher.SetActive(true);
            if (!raycastInfo.GetIsVive()) 
            {
                Extinguisher.transform.SetParent(m_Camera.transform);
                Extinguisher.transform.localEulerAngles = new Vector3(0f, -90, 82);
                Extinguisher.transform.localPosition = new Vector3(0.03f, -0.03f, 0f);
            }
            else
            {
                Extinguisher.transform.SetParent(raycastInfo.HandRight.transform);
                Extinguisher.transform.localEulerAngles = Vector3.zero;
                Extinguisher.transform.localPosition = Vector3.zero;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "PointMove")
                TryInteract(other.transform.parent.GetComponentInParent<IToDo>());
        }

        private void Moving(eInputEvents _event, float _value)
        {
            if (_event == eInputEvents.Esc)
            {
                OnClickESC();
            }
            if (_event == eInputEvents.H)
            {
                ShowMenu();
                SetCanMove(false);
            }
            if (_event == eInputEvents.N)
            {
                HideMenu();
                SetCanMove(true);
            }
            if (_event == eInputEvents.LevelLayout)
            {
                ActivateLevelLayout();
            }
        }

        private IEnumerator MoveTo(Vector3 _pos)
        {
            _pos.y = myTransform.position.y;
            for (;;)
            {
                myTransform.position = Vector3.MoveTowards(myTransform.position, _pos, speedMoveAuto * Time.deltaTime);
                if (Vector3.Distance(myTransform.position, _pos) < 1f)
                    break;
                yield return new WaitForEndOfFrame();
            }
        }

        private void RigibobyMove(Vector3 targetVelocity)
        {
            if (raycastInfo.GetIsVive())
            {
                targetVelocity = new Vector3(targetVelocity.z, 0, targetVelocity.x);
                targetVelocity = m_Camera.transform.TransformDirection(targetVelocity);
            }
            else
            {
                targetVelocity = myTransform.TransformDirection(targetVelocity);
            }
            
            targetVelocity *= speedMove;

            Vector3 velocity = myRigibody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0f;
            
            myRigibody.AddForce(velocityChange, ForceMode.VelocityChange);
            myRigibody.AddForce(new Vector3(0, -maxVelocityChange * myRigibody.mass, 0));
        }
    }
}
