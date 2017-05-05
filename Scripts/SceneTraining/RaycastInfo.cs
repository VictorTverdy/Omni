using Omni.Manager;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Omni.Game.SceneTraining
{
    public class RaycastInfo : MonoBehaviour
    {
        
        public Camera m_Camera;
        public GameObject CameraRadarHazard;
        public GameObject CameraViveRadar;
        public Transform HandRight;

        private bool IsVive;
        private int useRadar;
        private const int limitEasy = 5;
        private const int limitMedium = 3;
        private const int limitHard = 1;
        private const float longRadar = 3;
        private bool canShouwRadar = true;
        private VRMovement vRMovement;
        private Animator currentButtonEnter;

        private void OnEnable()
        {
            InputManager.OnClicked += Moving;
        }

        private void OnDisable()
        {
            InputManager.OnClicked -= Moving;
        }

        private void Awake()
        {
            CameraRadarHazard.SetActive(false);
            CameraViveRadar.SetActive(false);     
            vRMovement = GetComponent<VRMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastUI();
        }

        public void SetIsVive(bool _value)
        {
            IsVive = _value;
            if (_value)
            {
                m_Camera.transform.localPosition = Vector3.zero;
            }
            else
            {
                m_Camera.transform.localPosition = new Vector3(0, 1.7f, 0);
            }
        }

        public bool GetIsVive()
        {
            return IsVive;
        }

        public void ZeroRadarUse()
        {
            CameraRadarHazard.SetActive(false);
            CameraViveRadar.SetActive(false); 
            useRadar = 0;

            eHazardLevel _level = eHazardLevel.medium;
            if (TrainingStateManager.Instance != null)
                _level = TrainingStateManager.Instance.GetCurrentLevel();
            switch (_level)
            {
                case eHazardLevel.easy:
                  //  HazardInfo.Instance.LimitRadar.text = useRadar.ToString() + " / " + limitEasy.ToString();                  
                    HazardInfo.Instance.LimitRadar.text = (limitEasy-useRadar).ToString();                  
                    break;
                case eHazardLevel.medium:
                  //  HazardInfo.Instance.LimitRadar.text = useRadar.ToString() + " / " + limitMedium.ToString();                   
                    HazardInfo.Instance.LimitRadar.text = (limitMedium-useRadar).ToString();                   
                    break;
                case eHazardLevel.hard:
                  //  HazardInfo.Instance.LimitRadar.text = useRadar.ToString() + " / " + limitHard.ToString();                   
                    HazardInfo.Instance.LimitRadar.text = (limitHard-useRadar).ToString();                   
                    break;
            }
            // HazardInfo.Instance.LimitRadar.text = useRadar.ToString();
        }        

        public void RaycastHandOver()
        {
            Ray ray = new Ray(HandRight.transform.position, HandRight.transform.forward * 100);
            RaycastHitInfo(ray);
        }

        public void ShowRadarVive()
        {
            if (canShouwRadar)
                StartCoroutine("AnimatedShowRadar", CameraViveRadar.transform);
        }

        private void RaycastUI()
        {
            if (IsVive)
            {
                Ray rayHand = new Ray(HandRight.transform.position, HandRight.transform.forward * 100);
                RaycastInfoUI(rayHand);
            }
            else
            {
                Ray rayCamera = m_Camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                RaycastInfoUI(rayCamera);
            }
        }

        private void RaycastInfoUI(Ray ray)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {               
                if (hit.distance < 5)
                { 
                    if (currentButtonEnter == null)
                    {
                        if (hit.collider.GetComponent<Button>() == null)
                            return;
                        currentButtonEnter = hit.collider.GetComponent<Animator>();
                        if (currentButtonEnter != null)
                            currentButtonEnter.Play("Highlighted");
                    }
                    else
                    {
                        if (hit.transform.gameObject != currentButtonEnter.gameObject)
                        {
                            currentButtonEnter.Play("Normal");
                            if (hit.collider.GetComponent<Button>() == null)
                                return;
                            currentButtonEnter = hit.collider.GetComponent<Animator>();
                            if (currentButtonEnter != null)
                                currentButtonEnter.Play("Highlighted");
                        }
                    }
                }
            }
            else
            {
                if (currentButtonEnter != null)
                {
                    currentButtonEnter.Play("Normal");
                    currentButtonEnter = null;
                }              
            }
        }

        private void RaycastHitInfo(Ray ray)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "PointMove")
                {
                    vRMovement.StartMoveTo(hit.collider.transform.position);
                }
                else
                {
                    if (hit.distance < 8f)
                    {
                        Button _button = hit.collider.GetComponent<Button>();
                        if (_button != null)
                            StartCoroutine("InvokeButtonCall", _button);
                        TryInteract(hit.collider.GetComponentInParent<IToDo>());
                    }
                }
            }
        }

        private IEnumerator InvokeButtonCall(Button _buttonPress)
        {
            if (_buttonPress != null)
            {
                Animator _anim = _buttonPress.GetComponent<Animator>();
                if (_anim != null)
                { 
                    _anim.Play("Pressed");
                    yield return new WaitForSeconds(0.1f);
                    _anim.Play("Highlighted");
                    yield return new WaitForSeconds(0.1f);
                    _anim.Play("Normal");
                }
                _buttonPress.onClick.Invoke();
            }
        }    

        private void TryInteract(IToDo _interact)
        {
            if (_interact != null)
            {
                _interact.Action();
            }
        }

        private void RaycastCamera()
        {
            Ray rayCamera = m_Camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHitInfo(rayCamera);
        }

        private void ShowRadar()
        {
            if (canShouwRadar )
            {
                StartCoroutine("AnimatedShowRadar", CameraRadarHazard.transform);
            }
        }      

        private IEnumerator AnimatedShowRadar(Transform _radar)
        {
            canShouwRadar = false;
            eHazardLevel _level = TrainingStateManager.Instance.GetCurrentLevel();
            bool isContinue = true;
            int limit = -1;
            switch (_level)
            {
                case eHazardLevel.easy:
                    limit = limitEasy;
                    break;
                case eHazardLevel.medium:
                    limit = limitMedium;
                    break;
                case eHazardLevel.hard:
                    limit = limitHard;
                    break;
            }
            if (useRadar > limit - 1)
                isContinue = false;
            if (isContinue)
            {
                useRadar++;
                //HazardInfo.Instance.LimitRadar.text = useRadar.ToString() + " / " + limit;
                HazardInfo.Instance.LimitRadar.text = (limit-useRadar).ToString();
                _radar.gameObject.SetActive(true);
                _radar.transform.localScale = new Vector3(0.05f, .03f, 1);
                yield return new WaitForSeconds(0.1f);
                for (float x = 0; x <= 1; x += 0.08f)
                {
                    _radar.transform.localScale = new Vector3(x, .03f, 1);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(0.1f);
                for (float y = 0; y <= 1; y += 0.1f)
                {
                    _radar.transform.localScale = new Vector3(1, y, 1);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(longRadar);
                for (float y = 1; y >= 0; y -= 0.1f)
                {
                    _radar.transform.localScale = new Vector3(y, y, 1);
                    yield return new WaitForSeconds(0.01f);                  
                }               
            }
            _radar.gameObject.SetActive(false);
            canShouwRadar = true;
        }      

        private void Moving(eInputEvents _event, float _value)
        {
            if (_event == eInputEvents.mouseOver)
            {
                RaycastCamera();
            }
            if (_event == eInputEvents.rightClickUp)
            {
                ShowRadar();              
            }
            if (_event == eInputEvents.rightClickDown)
            {
               
            }
        }
    }
}
