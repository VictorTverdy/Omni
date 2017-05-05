#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WellWorld))]
public class TestFilter : Editor {

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        WellWorld myScript = (WellWorld)target;
		if(GUILayout.Button("Appyl Filter"))
        {
			//myScript.OnApplyFilter();
        }
    }

}
#endif
