using UnityEngine;

using Omni.Events;
using Omni.Utilities.EventHandlers;

public class FadeEffectController : MonoBehaviour {
	
	[SerializeField] private Animator m_fadeInOut = null;

	private int m_callStepOF;

	private bool m_startFadeOutAnim;
	private bool m_isTransitionState;
	private bool m_fadeOutAnimIsPlaying;
	private bool m_fadeInAnimHasFinished;

	//StartFadeAnimation when the user needs to change a screen
	public void ScreenTransition(int callStepOF)
	{
		m_callStepOF = callStepOF;
		m_isTransitionState = false;

		m_fadeInOut.SetBool ("makeTransition", true);
	}

	//Start FadeIn Animation when the user needs to change state
	public void StateTransition(int callStepOF)
	{
		m_callStepOF = callStepOF;
		m_isTransitionState = true;
		m_startFadeOutAnim = false;
		m_fadeOutAnimIsPlaying = false;
		m_fadeInAnimHasFinished = false;
		m_fadeInOut.SetBool ("fadein", true);
	}

	public void StartFadeOutStateTransition()
	{
		m_startFadeOutAnim = true;
	}

	public void Update()
	{
		if (m_fadeInOut.GetBool ("fadein")) {			
			if (!m_fadeOutAnimIsPlaying && m_fadeInAnimHasFinished && m_startFadeOutAnim) {
				m_fadeOutAnimIsPlaying = true;
				m_fadeInOut.SetBool ("fadeout", true);
			}
		}
	}

	public void OnFadeInAnimationIsFinished(){
		if (m_isTransitionState) {
			m_fadeInAnimHasFinished = true;
		} 

		FadeEffectEvent evt = new FadeEffectEvent (FadeEffectEvent.ON_FADE_IN_ANIMATION_FINISHED);
		evt.arguments ["callStepOF"] = m_callStepOF;
		evt.arguments ["isCallFromTransitionState"] = m_isTransitionState;
		EventManager.instance.dispatchEvent (evt);
	}

	public void OnFadeAnimationIsFinished()
	{
		if (m_isTransitionState) {
			m_fadeInOut.SetBool ("fadein", false);
			m_fadeInOut.SetBool ("fadeout", false);

			FadeEffectEvent evt = new FadeEffectEvent (FadeEffectEvent.ON_FADE_ANIMATION_STATE_COMPLETED);
			EventManager.instance.dispatchEvent (evt);

		} else {		
			m_fadeInOut.SetBool ("makeTransition", false);

			FadeEffectEvent evt = new FadeEffectEvent (FadeEffectEvent.ON_FADE_ANIMATION_COMPLETED);
			evt.arguments ["callStepOF"] = m_callStepOF;
			EventManager.instance.dispatchEvent (evt);
		}
	}
}
