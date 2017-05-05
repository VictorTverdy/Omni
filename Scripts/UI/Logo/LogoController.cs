using UnityEngine;
using UnityEngine.UI;

using Omni.Events;
using Omni.Utilities;
using Omni.Singletons;
using Omni.Utilities.EventHandlers;

namespace Omni.UI.Logo
{
	public class LogoController : MonoBehaviour {

		public Color OmniColor = Color.white;
		public Color LogoColor = Color.white;

		public Image OmniLogo;
		public Image LogoEnterprise;

		private Sprite m_defaultTexture;

		// Use this for initialization
		void Start () 
		{
         //   Debug.Log("-->LogoController Start");
			EventManager.instance.addEventListener (HUDEvent.ON_LOGO_IMAGE_WAS_LOADED, this.gameObject, "OnImageLoaded");

			//Default iamge
			m_defaultTexture = Resources.Load<Sprite>(Config.LOGO_IMAGE_PATH + Config.LOGO_IMAGE_ENTERPRISE_PATH);

			Sprite sprite = UserDataSettings.Instance.ClientLogo;
			if (sprite == null)
				sprite = m_defaultTexture;
			
			LogoEnterprise.sprite = sprite;

			LogoEnterprise.color = LogoColor;
			OmniLogo.color = OmniColor;
           // Debug.Log("-->LogoController Start End");
		}


		private void OnImageLoaded(HUDEvent evt)
		{
           // Debug.Log("-->LogoController OnImageLoaded");
			bool imageWasLoaded = (bool)evt.arguments ["imageLoaded"];

			if (imageWasLoaded) {
				LogoEnterprise.sprite = UserDataSettings.Instance.ClientLogo;
			} else {
				LogoEnterprise.sprite = m_defaultTexture;
			}
		}
	}
}
