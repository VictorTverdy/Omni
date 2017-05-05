using UnityEngine;

using Omni.Asset;
using Omni.Events;
using Omni.Utilities.EventHandlers;

namespace Omni.CameraCtrl
{
	public class CameraInteractionController : MonoBehaviour {

		private Camera m_camera;
		private Renderer m_selectedPadRenderer;

        // Use this for initialization
        void Awake()
        {
            m_camera = this.GetComponent<Camera>();
        }
		
		void Update () {
			Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit))
			{
				if (Input.GetMouseButton (0)) 
				{                  
                    if (hit.transform.CompareTag ("InteractiveField")) {
                        //int id = hit.transform.GetComponent<WellBarController>().Id;
                        WellInfoPanel.Instanse.InitInfo(0);
                        GameValuesConfig.SetCurrentWellType(hit.transform.GetComponent<WellBarController>().wellType, gameObject);
                        HeightLevelEvent evt = new HeightLevelEvent (HeightLevelEvent.ON_SHOW_WELL_INFO);
						EventManager.instance.dispatchEvent (evt);
					}
				}

				else if (hit.transform.CompareTag ("InteractivePad")) {

					Renderer renderer1 = hit.transform.gameObject.GetComponent<Renderer>();

					if (m_selectedPadRenderer == null) {
						m_selectedPadRenderer = renderer1;

					} else {
						if (m_selectedPadRenderer != renderer1) {
							m_selectedPadRenderer.material.color = Color.white;
							m_selectedPadRenderer = renderer1;
						}
					}
					m_selectedPadRenderer.material.color = Color.yellow;

				} else {					
					if (m_selectedPadRenderer != null) {
						m_selectedPadRenderer.material.color = Color.white;
						m_selectedPadRenderer = null;
					}
				}
			}
		}
	}
}
