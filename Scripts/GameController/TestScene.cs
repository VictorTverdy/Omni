#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Omni.Game
{
    [CustomEditor(typeof(GameController))]
    public class TestScene : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameController myScript = (GameController)target;
            if (GUILayout.Button("Run Scene"))
            {
                myScript.TestScene();
            }
        }

    }
}
#endif