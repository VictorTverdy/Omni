using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Omni.Singletons;
using Omni.Entities;
using Omni.GameState;

public class WellInfoPanel : MonoBehaviour {

    public static WellInfoPanel Instanse;

    public Transform ParentContent;

    private List<Text> textInfo = new List<Text>();
    private Animator myAnimator;
   
    // Use this for initialization
    void Start()
    {
        Instanse = this;
        myAnimator = GetComponent<Animator>();
        FieldInfo[] Fields = typeof(AssetList).GetFields();
        foreach (var value in Fields)
        {
            if (value.Name == "Status" || value.Name == "GameObjectName" || value.Name == "posX" || value.Name == "posY" || value.Name == "posZ")
                continue;
            GameObject prefabItem = Resources.Load<GameObject>("Prefabs/UI/Well/ItemWellInfoPanel"); 
            GameObject obj = Instantiate(prefabItem, ParentContent) as GameObject;
            obj.transform.localScale = Vector3.one;
            obj.transform.FindChild("Text").GetComponent<Text>().text = value.Name;
            Text _textValue = obj.transform.FindChild("TextValue").GetComponent<Text>();
            _textValue.name = value.Name;
            textInfo.Add(_textValue);
        }
    }

    public void InitInfo(int id)
    {      
        AssetList _assetList = UserDataSettings.Instance.AssetList[id];       
        Type curType = _assetList.GetType();
        FieldInfo[] Fields = curType.GetFields();
        for (int i = 0; i < Fields.Length; i++)
        {
            if (Fields[i].Name == "Status" || Fields[i].Name == "GameObjectName" || Fields[i].Name == "posX" || Fields[i].Name == "posY"||
                Fields[i].Name == "posZ")
                continue;

            textInfo.Find(x => x.name == Fields[i].Name).text = Fields[i].GetValue(_assetList).ToString();
        }        
    }

    public void InitAnimator(bool value)
    {
        myAnimator.SetBool("ShowPopup", value);
    }

	public void GotoSelectedWell()
	{
		this.gameObject.transform.parent.gameObject.SetActive(false);
		GameStateManager.Instance.PushState(new GamePlayState());
	}
}
