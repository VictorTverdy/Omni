using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Omni.Game.SceneTraining
{
    public class CanvasVRPlayer : MonoBehaviour
    {
        public static CanvasVRPlayer Instance;

        public Image fade;
        public FireBoxInfo FireBoxInfo;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(this);
        }

        public void SetActivateFade()
        {
            StartCoroutine("Fade");
        }

        private IEnumerator Fade()
        {
            Color _color = fade.color;
            for (float _a = 0; _a <= 1; _a += 0.02f)
            {
                _color.a = _a;
                fade.color = _color;
                yield return new WaitForSeconds(0.01f);
            }
            _color.a = 1;
            fade.color = _color;
            yield return new WaitForSeconds(1f);

            for (float _a = 1; _a >= 0; _a -= 0.01f)
            {
                _color.a = _a;
                fade.color = _color;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
