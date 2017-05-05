using UnityEngine;
namespace Omni.Game.SceneTraining
{
    public enum eTrainingState
    {
        None = -1,//deber�a ser None = -1,
        Normal =0,
        OilSpill = 1,
        Fire =2,       
        Evacuation =3,
        Explosives = 4
    }   


    public class TrainingState : MonoBehaviour
    {
        public eItems[] SequencingItems;
        public eTrainingState currentState;
        public string SituationName;
       
        // Use this for initialization
        public virtual void Start()
        {

        }        
           
    }
}
