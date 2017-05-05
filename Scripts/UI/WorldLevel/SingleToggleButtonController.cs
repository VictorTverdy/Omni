using Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Omni.Utilities;
using Omni.Events;
using Omni.Utilities.EventHandlers;

[RequireComponent(typeof(Button))]
public class SingleToggleButtonController : MonoBehaviour
{		
	[SerializeField] private GameObject IsOnContent;
	[SerializeField] private GameObject IsOffContent;
	[SerializeField] private WellCategory m_wellCategory;

	[Header("Label Colors")] 
	[SerializeField] private Color EnabledColor;
	[SerializeField] private Color DisabledCololr;
    

	private bool _isOn;
	private Text Label;
	private Button Button;

    private void Start()
    {
        Button = GetComponent<Button>();
        Label = GetComponentInChildren<Text>();
        
		SetOn ();
		_isOn = true;
		Button.onClick.AddListener(() => Set(!_isOn));
    }

    private void Set(bool state)
    {
		_isOn = state;
        if (state)
            SetOn();
        else
            SetOff();

		WorldLevelEvent evt = new WorldLevelEvent (WorldLevelEvent.ON_APPLY_FILTERS_TO_WELL);
		evt.arguments ["wellCategory"] = m_wellCategory;
		evt.arguments ["isOn"] = _isOn;
		EventManager.instance.dispatchEvent (evt);

    }

    private void SetOn()
    {        
        IsOffContent.SetActive(false);
        IsOnContent.SetActive(true);
        if (Label) Label.color = EnabledColor;
    }

    private void SetOff()
    {
        IsOnContent.SetActive(false);
        IsOffContent.SetActive(true);
        if (Label) Label.color = DisabledCololr;
    }
}