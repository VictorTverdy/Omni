using UnityEngine;
using System;
namespace Omni.Game.SceneTraining
{
    [Serializable]
    public struct ProgressData
    {
        public bool[] Chapters;
        public bool[] TrainingSituations;
        public bool[] DrilingSituations;
        public bool[] ValveOperations;
        public bool[] ExplosivesBunkerSafety;
    }


    public class ProgressInfo : MonoBehaviour
    {
        public static ProgressInfo Instance;

        public ProgressData Progress;

        // Use this for initialization
        void Awake()
        {
            if (Instance != null)
                DestroyImmediate(this);
            else
                Instance = this;

            LoadProgress();
        }

        void OnDisable()
        {
            string save = JsonUtility.ToJson(Progress);
            PlayerPrefs.SetString("Progress", save);
        }

        public void SetEndTrainingSituation(eTrainingState _end)
        {
            int numSituation =(int)_end;
            numSituation--;
            Progress.TrainingSituations[numSituation] = true;
            CheckEndTrainingChapter();
        }

        void CheckEndTrainingChapter()
        {
            for (int i = 0; i < Progress.TrainingSituations.Length; i++)
            {
                if (!Progress.TrainingSituations[i])
                    return;
            }  
            Progress.Chapters[0] = true;
        }

        void LoadProgress()
        {
            string load = PlayerPrefs.GetString("Progress");
            Progress = new ProgressData();
            if (string.IsNullOrEmpty(load))
            {
                int lengtChapters = Enum.GetNames(typeof(eSelectedChapters)).Length;
                int lengtSituations = Enum.GetNames(typeof(eTrainingState)).Length;
                lengtSituations -= 2;
                Progress.Chapters = new bool[lengtChapters];
                Progress.TrainingSituations = new bool[lengtSituations];
            }
            else
            {
                ProgressData loadProgress = JsonUtility.FromJson<ProgressData>(load);
                Progress = loadProgress;
            }
        }
    }
}
