using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Omni.Events;
using Omni.Utilities.EventHandlers;
using Omni.Utilities;

public class WirelineInfoPanelController : MonoBehaviour {
    	
    public Text SpeedText;
    public Text DepthText;
    public Text TensionText;    
    public Text HeadTensionText;
    
    [MinMaxRange(50f, 100f)]
    [SerializeField] private MinMaxRange SpeedRange;

    [MinMaxRange(50f, 100f)]
    [SerializeField]
    private MinMaxRange DepthRange;

    [MinMaxRange(50f, 100f)]
    [SerializeField]
    private MinMaxRange TensionRange;

    [MinMaxRange(50f, 100f)]
    [SerializeField]
    private MinMaxRange HeadTensionRange;

    private bool m_coroutineIsAlreadyRunning;


    public void Start()
    {
        EventManager.instance.addEventListener(AssetUIEvent.ON_CHANGE_TO_INSIDE_VIEW, this.gameObject, "OnEnterToFieldEvent");
    }

    public void OnEnable()
    {
        StopCoroutine("ChangeValues");
    }

    private void OnEnterToFieldEvent(AssetUIEvent evt)
    {

        InsideFieldLocation location = (InsideFieldLocation)evt.arguments["insideFieldLocation"];

        if (location == InsideFieldLocation.Wireline || location == InsideFieldLocation.SelectedInsideAsset)
        {
            if (!m_coroutineIsAlreadyRunning)
            {
                m_coroutineIsAlreadyRunning = true;
                StartCoroutine("ChangeValues");
            }
        }
        else
        {
            StopCoroutine("ChangeValues");
        }
    }

 
    private IEnumerator ChangeValues()
    {
        yield return new WaitForSeconds(1.0f);

        SpeedText.text = Random.Range(SpeedRange.rangeStart, SpeedRange.rangeEnd).ToString();
        DepthText.text = Random.Range(DepthRange.rangeStart, DepthRange.rangeEnd).ToString();
        TensionText.text = Random.Range(TensionRange.rangeStart, TensionRange.rangeEnd).ToString();
        HeadTensionText.text = Random.Range(HeadTensionRange.rangeStart, HeadTensionRange.rangeEnd).ToString();

        StartCoroutine("ChangeValues");
    }
}
