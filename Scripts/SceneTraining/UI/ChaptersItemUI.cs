using UnityEngine;
using UnityEngine.UI;
namespace Omni.Game.SceneTraining
{
    public class ChaptersItemUI : MonoBehaviour
    {

        public eSelectedChapters State;
        private TrainigChaptersUI trainigChapters;
        private GameObject imageComleted, imageOtline;

        // Use this for initialization
        void Awake()
        {
            imageOtline = transform.Find("ImageOutline").gameObject;
            imageOtline.SetActive(false);
            imageComleted = transform.Find("Completed").gameObject;
            imageComleted.SetActive(false);
            trainigChapters = FindObjectOfType<TrainigChaptersUI>();
            transform.GetComponentInChildren<Button>().onClick.AddListener(OnMyClick);
        }

        public void OnMyClick()
        {
            trainigChapters.ActivateChapter(State);
        }

        public void ActivateOtline(bool _value)
        {
            if (imageOtline != null)
                imageOtline.SetActive(_value);
        }

        public void ActivateCompleted()
        {
            if (imageComleted != null)
                imageComleted.SetActive(true);
        }

    }
}
