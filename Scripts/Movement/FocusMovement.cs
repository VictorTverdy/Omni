using UnityEngine;
using UnityEngine.Events;
using VolumetricFogAndMist;


namespace Omni.Movement
{
    public class FocusMovement : MonoBehaviour
    {
		[SerializeField] private Transform m_startPosition;
		[SerializeField] private Transform m_endPosition;

		[SerializeField]private float m_speed = 1.0F;
		[SerializeField]private bool m_autoStart = false;

		[SerializeField]private  VolumetricFog vFog;
        private float m_startTime;
        private float m_journeyLength;
        private bool m_isMoving;

        private float m_fracJourney = 0;
        [Space(10)]
		[SerializeField]private UnityEvent onArrive;
		[SerializeField]private UnityEvent on70PercentTravelDone;
        private bool on70PercentArrive = false;

		void Start(){
			if (m_autoStart) {                
			    InitMovement (m_startPosition, m_endPosition);
			}
		}
        public void InitMovement(Transform startPos, Transform endPos)
        {
            //tt: for debug in this page!
            //GameValuesConfig.SetAndSaveCloudsAnimation(true); 
            if (GameValues.CloudsAnimation)
            {
                m_fracJourney = 0;
                m_startPosition = startPos;
                m_endPosition = endPos;

                m_startTime = Time.time;
                m_journeyLength = Vector3.Distance(m_startPosition.position, m_endPosition.position);
                m_isMoving = true;
            }
            else {
                transform.position = m_endPosition.position;
                arrive();
            }
        }

        void Update()
        {
            if (!m_isMoving) return;
            if(m_fracJourney > 1)
            {
                arrive();
            }
            if(m_fracJourney >= 0.7f && on70PercentArrive == false) {
                on70PercentArrive=true;
                on70PercentTravelDone.Invoke();
            }
            float distCovered = (Time.time - m_startTime) * m_speed;
            m_fracJourney = distCovered / m_journeyLength;
            transform.position = Vector3.Lerp(m_startPosition.position, m_endPosition.position, m_fracJourney);
        }

        private void arrive()
        {
            m_isMoving = false;
            m_fracJourney = 0;
            on70PercentArrive = false;
            transform.position = m_endPosition.position;
			onArrive.Invoke ();

            //This three functions below are not consecuent with Entity Component System, they should be called with onArrive.Invoke().
            //for the moment, I'm not quite shure of how many percentage of this project should be done with ECS, so I will left they for now... T.
            GameValuesConfig.SetShowLocationPanels(true, gameObject);
			if (vFog) vFog.enabled = false;
			if(GetComponent<CameraZoomInOutMouseWheel>())GetComponent<CameraZoomInOutMouseWheel>().SetEnable(true);
        }
    }

}
