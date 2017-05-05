using UnityEngine;
using System.Collections.Generic;

using Omni.Utilities;

namespace Omni.UI.ScreensControllers
{
	public class UIController : MonoBehaviourSingletonPersistent<UIController>
	{		
		private UIListEnum m_currentUIListEnum;

		[SerializeField] private List<GameObject> m_uiGameObjectList;
		[SerializeField] private FadeEffectController m_fadeEffectController;

		public FadeEffectController FadeEffectController
		{
			get { return m_fadeEffectController;}
		}

		public override void Awake()
		{
			base.Awake ();

			for (int i = 0; i < m_uiGameObjectList.Count; i++) 
			{
				ActiveUI(i, false);
			}
		}

		public void OnDestroy() 
		{ 
			for (int i = m_uiGameObjectList.Count - 1; i >= 0; i--) 
			{
				m_uiGameObjectList.RemoveAt(i);
			}
			m_uiGameObjectList.Clear();
		}

		public void OnlyActiveUI(UIListEnum uiIndex)
		{
			for (int i = 0; i < m_uiGameObjectList.Count; i++) 
			{
				ActiveUI(i, false);
			}

			m_currentUIListEnum = uiIndex;
			int uiIndexVal = (int)uiIndex;
			ActiveUI (uiIndexVal, true);
		}
					
		public void ActiveUI(UIListEnum uiIndex, bool active)
		{
			m_currentUIListEnum = uiIndex;
			int uiIndexVal = (int)uiIndex;
			ActiveUI (uiIndexVal, active);
		}

		public GameObject GetCurretGameObjetUI()
		{
			int uiIndexVal = (int)m_currentUIListEnum;
			return m_uiGameObjectList[uiIndexVal];
		}

		private void ActiveUI(int index, bool active)
		{
			if (index >= 0) {
				m_uiGameObjectList [index].SetActive (active);
			}
		}
	}
}