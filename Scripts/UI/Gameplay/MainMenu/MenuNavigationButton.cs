using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigationButton : MonoBehaviour {

	public Text m_btnText;

	
	public void SetTitle(string title){
		m_btnText.text = title;
	}
}
