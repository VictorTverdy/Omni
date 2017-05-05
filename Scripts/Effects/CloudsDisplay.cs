using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricFogAndMist;

namespace Omni.Effects
{
    public class CloudsDisplay : MonoBehaviourSingleton<CloudsDisplay>
    {
        [SerializeField]private bool debug = true;

        public void Show(GameObject go)
        {
            VolumetricFog.instance.enabled = true;
            debugMessage("***Clouds show from " + go.name);
        }
        public void Hide(GameObject go)
        {
            VolumetricFog.instance.enabled = false;
            debugMessage("***Clouds hide from " + go.name);
        }

        private void debugMessage(string s)
        {
            if(debug)
            Debug.Log(s);
        }
    }

}
