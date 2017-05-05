using UnityEngine;

using Omni.GameState;
using Omni.Utilities.EventHandlers;
using Omni.Events;

public class HeightLevelHUDController : MonoBehaviour {

	public WellInfoPanel m_wellInfoAnimator;
	public Animator m_filterScreenAnimator;

    private bool m_showInfoFilters;

	public void Start()
	{       
        EventManager.instance.addEventListener (HeightLevelEvent.ON_SHOW_WELL_INFO, this.gameObject, "OnShowWellInfoCallback");
		EventManager.instance.addEventListener (FadeEffectEvent.ON_FADE_IN_ANIMATION_FINISHED, this.gameObject, "OnFadeInEffectFinished");
	}

	public void GotoPadLocation()
	{        
        GameValuesConfig.SetShowLocationPanels(false, gameObject);
        GameStateManager.Instance.PushState(new GamePlayState());
    }

	public void ShowInfoFilters()
	{
		m_showInfoFilters = !m_showInfoFilters;
		m_filterScreenAnimator.SetBool ("ShowPanel", m_showInfoFilters);
	}

	public void CloseWellInfoPopup()
	{
		HUDEvent evt = new HUDEvent (HUDEvent.ON_HIDE_INFO_BARS);
		evt.arguments ["canUse"] = true;
		EventManager.instance.dispatchEvent(evt);

        m_wellInfoAnimator.InitAnimator (false);
	}

	public void BackToWorldLevel()
	{		
		GameStateManager.Instance.PopState ();
		WorldLevelEvent worldEvent = new WorldLevelEvent (WorldLevelEvent.ON_BACK_TO_WORLD);
		EventManager.instance.dispatchEvent (worldEvent);
	}

	private void OnShowWellInfoCallback()
	{
		HUDEvent evt = new HUDEvent (HUDEvent.ON_HIDE_INFO_BARS);
		evt.arguments ["canUse"] = false;
		EventManager.instance.dispatchEvent(evt);

        m_wellInfoAnimator.InitAnimator(true);
    }

	private void OnFadeInEffectFinished(FadeEffectEvent evt)
	{
		WorldLevelEvent worldEvent = new WorldLevelEvent (WorldLevelEvent.ON_BACK_TO_WORLD);
		EventManager.instance.dispatchEvent (worldEvent);
	}
}
