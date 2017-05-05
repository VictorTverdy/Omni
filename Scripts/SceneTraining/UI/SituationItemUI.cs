using UnityEngine;
using UnityEngine.UI;
namespace Omni.Game.SceneTraining
{
    public class SituationItemUI : MonoBehaviour
    {
        public eTrainingState State;
        private GameObject imageComleted;
        private Image imageBack,mainIcon;
        private Text textDescription,textChapter;
        private Color myBlue = new   Color(0,0.54f,1);
        private SituationPanelUI situationPanelUI;

        // Use this for initialization
        void Awake()
        {          
          
            situationPanelUI = FindObjectOfType<SituationPanelUI>();
            GetComponent<Button>().onClick.AddListener(OnMyClick);
            imageBack = transform.Find("ImageBack").GetComponent<Image>();
            Transform ImageIcon = transform.Find("ImageIcon");
            if (ImageIcon  != null)
                 mainIcon = ImageIcon.GetComponent<Image>();
            Transform _compl = transform.Find("Completed");
            if (_compl != null)
            {
                imageComleted = _compl.gameObject;
                imageComleted.SetActive(false);
            }               
            Transform _text = transform.Find("Text");
            if (_text != null)
                textDescription = _text.GetComponent<Text>();
            Transform _chapter = transform.Find("Image (1)");
            if (_chapter != null)
                textChapter = _chapter.Find("Text").GetComponent<Text>();
        }

        public void ActivateCompleted()
        {
            if (imageComleted != null)
                imageComleted.SetActive(true);
        }

        public void OnMyClick()
        {
            if (name != "START")
                situationPanelUI.ActivateItem((int)State);
        }

        public void Activate()
        {            
            imageBack.color = Color.white;
            if (mainIcon != null)
                mainIcon.color = myBlue;
            if (textDescription != null)
                textDescription.color = myBlue;
            if (textChapter != null)
                textChapter.color = Color.white;
        }

        public void Deactivate()
        {
            if (imageBack != null)
            {
                if(mainIcon != null)
                   mainIcon.color = Color.white;
                imageBack.color = myBlue;
                if (textDescription != null)
                    textDescription.color = Color.white;
                if (textChapter != null)
                    textChapter.color = myBlue;
            }
        }
    }
}
