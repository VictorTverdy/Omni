using Omni.CameraCtrl;
using Omni.GameState;
using UnityEngine;
using Omni.BaseLibs;
using Omni.Utilities;
using System.Collections.Generic;
using Omni.Utilities.EventHandlers;
using Omni.Events;

public class WellWorld : MonoBehaviour {

    public int ID;
	public Sprite m_sprite;
	public Color m_blueColor;
	public Color m_greenColor;
	public CanvasRenderer NamePanel;
	public GameObject DotsContainer;
    
	private GenericList<WellDotInfo> m_wellDotInfoList;
	private CameraWorldInputControl cameraWorldInputControl;

	private BoxCollider myCollider;
	private List<WellCategory> m_allWellCategories;

    // Use this for initialization
    void Start()
    {		
		m_wellDotInfoList = new GenericList<WellDotInfo> ();
        cameraWorldInputControl = FindObjectOfType<CameraWorldInputControl>();
		NamePanel.gameObject.SetActive(false);

		myCollider = this.GetComponent<BoxCollider> ();

		m_allWellCategories = new List<WellCategory> ();
		m_allWellCategories.AddRange((WellCategory[]) System.Enum.GetValues(typeof(WellCategory)));

		GenerateDots ();

		EventManager.instance.addEventListener (WorldLevelEvent.ON_APPLY_FILTERS_TO_WELL, this.gameObject, "OnApplyFilter");
    }

    private void OnMouseDown()
    {
        if (cameraWorldInputControl.GetCanInteract())
			GameStateManager.Instance.PushState(new HeightLevelState ());
    }

    private void OnMouseEnter()
    {
        if (cameraWorldInputControl.GetCanInteract())
			NamePanel.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (cameraWorldInputControl.GetCanInteract())
			NamePanel.gameObject.SetActive(false);
    }

	private void GenerateDots()
	{
		Vector3 colliderSize = myCollider.size;

		float colliderTop = -colliderSize.y / 2;
		float colliderBottom = colliderSize.y / 2;

		float colliderLeft = -colliderSize.x / 2;
		float colliderRight = colliderSize.x / 2;

		int numberOfDots = UnityEngine.Random.Range (10, 20);
		for (int i = 0; i < numberOfDots; i++) {
			GameObject newDot = new GameObject ();

			WellDotInfo wellDotInfo = newDot.AddComponent<WellDotInfo> ();

			wellDotInfo.m_wellCategory = GeneralUtilities.RandomEnum<WellCategory> ();

			newDot.transform.parent = DotsContainer.transform;
			newDot.transform.localPosition = Vector3.zero;
			newDot.transform.localRotation = Quaternion.Euler(Vector3.zero);

			float posX = UnityEngine.Random.Range (colliderLeft, colliderRight);
			float posY = UnityEngine.Random.Range (colliderBottom, colliderTop);

			Vector3 randPosition = new Vector3 (posX, posY, 0);

			newDot.transform.localPosition = randPosition;
			newDot.transform.localScale = new Vector3 (0.15f, 0.15f, 0.15f);

			SpriteRenderer spriteRenderer = newDot.AddComponent<SpriteRenderer> ();
			spriteRenderer.sprite = m_sprite;
			spriteRenderer.color = UnityEngine.Random.Range (0.0f, 1.0f ) >= 0.5f ? m_greenColor : m_blueColor;

			m_wellDotInfoList.Add (wellDotInfo);
		}
	}

	public void OnApplyFilter(WorldLevelEvent evt)
	{
		bool inUSe = (bool)evt.arguments ["isOn"];
		WellCategory selectedCategory = (WellCategory)evt.arguments ["wellCategory"];

		if (inUSe) {
			m_allWellCategories.Add (selectedCategory);
		} else {
			m_allWellCategories.RemoveAt(m_allWellCategories.IndexOf (selectedCategory));
		}

		for (int i = 0; i < m_wellDotInfoList.Count; i++) {
			m_wellDotInfoList [i].ApplyFilter (m_allWellCategories);
		}
	}
}
