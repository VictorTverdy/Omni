using Omni.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Omni.Utilities.EventHandlers;
using Omni.Events;

public class WellInfoPanelDisplay : MonoBehaviourSingleton<WellInfoPanelDisplay> {
    
	[SerializeField]private WellInfo drillPanel;
	[SerializeField]private WellInfo defaultPanel;    
	[SerializeField]private WellInfo workoverPanel;    
    [SerializeField]private WellInfo abandonedPanel;

    [SerializeField]private RectTransform canvasRT;

	private bool canShow;

    void Awake() {
		canShow = false;

		drillPanel.gameObject.SetActive(false);
        defaultPanel.gameObject.SetActive(false);
        workoverPanel.gameObject.SetActive(false);        
        abandonedPanel.gameObject.SetActive(false);

		EventManager.instance.addEventListener (HUDEvent.ON_HIDE_INFO_BARS, this.gameObject, "OnHideInfoPanel");
    }   

	public void ListenerEnableShowPanel(){
		canShow = true;		
	}

	private void OnHideInfoPanel(HUDEvent evt)
	{
		bool hideInfoPanel = (bool)evt.arguments ["canUse"];

		canShow = hideInfoPanel;

		if (!canShow) {
			HidePanel ();
		}
	}

    public void ShowPanel(EnumWellType wellType, WellInfo wellInfo) {

		if (!canShow)
			return;
		
        switch(wellType) {
            case EnumWellType.None:
            break;
            case EnumWellType.Default:
                setPosition(defaultPanel.gameObject, wellInfo.gameObject);
                setText(defaultPanel, wellInfo);
                defaultPanel.gameObject.SetActive(true);
            break;
            case EnumWellType.Drilling:
                setPosition(drillPanel.gameObject, wellInfo.gameObject);
                setText(drillPanel, wellInfo);
                drillPanel.gameObject.SetActive(true);
            break;
            case EnumWellType.WorkOver:
                setPosition(workoverPanel.gameObject, wellInfo.gameObject);
                setText(workoverPanel, wellInfo);
                workoverPanel.gameObject.SetActive(true);
            break;
            case EnumWellType.Abandoned:
                setPosition(abandonedPanel.gameObject, wellInfo.gameObject);
                setText(abandonedPanel, wellInfo);
                abandonedPanel.gameObject.SetActive(true);
            break;            
        }
    }

    private void setPosition(GameObject panel, GameObject bar) {
         Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, bar.transform.position);
         panel.GetComponent<RectTransform>().anchoredPosition = screenPoint - canvasRT.sizeDelta / 2f;       
              
    }

    public void HidePanel() {
		drillPanel.gameObject.SetActive(false);
        defaultPanel.gameObject.SetActive(false);        
        abandonedPanel.gameObject.SetActive(false);
        workoverPanel.gameObject.SetActive(false);
    }

    private void setText(WellInfo well, WellInfo newWell) {

        string WellNumber = string.Format("{0:0000}", 1000 * UnityEngine.Random.value);

        if (well.gasNum)well.gasNum.text = string.Format("{0:0,0}", newWell.gas);
        if(well.oilNum) well.oilNum.text = string.Format("{0:0,0}", newWell.oil);
        if (well.waterNum) well.waterNum.text = string.Format("{0:0,0}", newWell.water);
        if (well.wellNam) well.wellNam.text = string.Format("WELL HB{0}", WellNumber);
		if(well.wellDepth) well.wellDepth.text = decimal.Parse(newWell.depth).ToString("N") + " mts";
    }
}
