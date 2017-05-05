using UnityEngine;
using System.Collections;

public class State : System.Object 
{
	protected Transform m_tOwner;

	public Transform Owner
	{
		get {return m_tOwner;}
		set {m_tOwner = value;}
	}

	public StateMachine MachineContainer { get; set; }

	public bool StartExecuted { get; set; }

	// executed only once before the first Update
	public virtual void Start(){ }
	
	// Use this for initialization
	public virtual void Enter () { }
	
	// Update is called once per frame
	public virtual void Update () { }

	// Update is called once per frame
	public virtual void FixedUpdate () { }

	// Update is called once per frame
	public virtual void LateUpdate () { }		

	public virtual void Exit(){ }

	public virtual void Suspend(){ }

	public virtual void Resume(){ }
}
