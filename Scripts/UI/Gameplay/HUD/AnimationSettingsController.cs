
using UnityEngine;
using UnityEngine.UI;
using Omni.Utilities;

public class AnimationSettingsController : MonoBehaviour {

    public Animator m_animator;
    public Toggle m_toggleFogAnimationButton;
    public Toggle m_toggleFadeAnimationButton;
    public Toggle m_toggleCameraRotationButton;

    private bool m_showPanel;

    public void Start()
    {
        m_showPanel = false;
 
        m_toggleFogAnimationButton.isOn = GameValuesConfig.CloudsAnimation;
        m_toggleFadeAnimationButton.isOn = GameValuesConfig.FadeAnimation;
        m_toggleCameraRotationButton.isOn = GameValuesConfig.RotationCameraAnimation;
    }

    public void ShowPanel()
    {
        m_showPanel = !m_showPanel;
        if (m_showPanel)
        {
            m_animator.SetBool("MoveIn", true);
        }
        else
        {
            m_animator.SetBool("MoveIn", false);
        }
    }

    public void OnFogAnimationHasChanged()
    {
        GameValuesConfig.SetAndSaveCloudsAnimation(m_toggleFogAnimationButton.isOn, gameObject);
    }

    public void OnFadeAnimationHasChanged()
    {
        GameValuesConfig.SetAndSaveFadeAnimation(m_toggleFadeAnimationButton.isOn, gameObject);
    }

    public void OnCameraRotationHasChanged()
    {
        GameValuesConfig.SetAndSaveRotationCameraAnimation(m_toggleCameraRotationButton.isOn, gameObject);
    }
}
