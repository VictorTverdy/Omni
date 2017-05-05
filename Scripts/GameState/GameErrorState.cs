using UnityEngine;

using System.Collections;


namespace Omni.GameState
{
	public class GameErrorState : State
	{
		// Use this for initialization
		public override void Start ()
		{
			Debug.Log("Game Error occurred, Setting game Error State");	
		}
	}
}
