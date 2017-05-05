using Omni.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Omni.CameraCtrl
{
	public class WellBarController : MonoBehaviour {

        public int Id;
        public GameObject MainPanel;
        public GameObject BarOutline;
		public Transform CameraTransform;
        public List<Renderer> BarRenderer;
        public Color highlightColor;
        public float offset;
        public Color[] initialEmission;
        public EnumWellType wellType;
    
        void Update()
        {
        }

        void OnMouseEnter()
		{
            WellInfoPanelDisplay.Instance.ShowPanel(wellType, GetComponent<WellInfo>());
            foreach(Renderer rend in BarRenderer)
            {
                rend.material.SetColor("_EmissionColor", highlightColor);
                rend.material.EnableKeyword("_EMISSION");
            }
            BarOutline.SetActive(true);
		}

        void OnMouseExit()
        {
            WellInfoPanelDisplay.Instance.HidePanel();
            //return;
            foreach (Renderer rend in BarRenderer)
            {
                rend.material.SetColor("_EmissionColor", rend.material.color / 3);
                rend.material.EnableKeyword("_EMISSION");
            }
            BarOutline.SetActive(false);
        }
    }
}
