using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour 
{
	private State m_currentState;
	private GenericList<State> m_stack = new GenericList<State>();

	public State CurrentState
	{
		get {return m_currentState;}
	}

	public virtual void PopState()
	{
		if(null != m_currentState)
		{	
			m_currentState.Exit();
		}
		
		m_stack.PopBack();

		if(0 < m_stack.Count)
		{
			m_currentState = (State)m_stack[m_stack.Count - 1];
			m_currentState.Resume();
		}
		else
		{
			m_currentState = null;
		}
	}

	public virtual void PushState(State newState)
    {
        if (null != m_currentState)
		{
			m_currentState.Suspend();
		}
		
		m_currentState = newState;

		m_stack.PushBack(m_currentState);
		
		m_currentState.Owner = transform;
		m_currentState.MachineContainer = this;
		m_currentState.Enter();
	}
	
	public virtual void SwapToState(State newState)
    {
        if (null != m_currentState)
		{
			m_currentState.Exit();
			m_currentState = null;
		}	
		
		m_currentState = newState;

		m_stack.PopBack();
		m_stack.PushBack(m_currentState);
		
		m_currentState.Owner = transform;
		m_currentState.MachineContainer = this;
		m_currentState.Enter();
	}

	// Use this for initialization
	public virtual void Start () { }
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if(null != m_currentState)
		{
			if(!m_currentState.StartExecuted)	
			{
				m_currentState.StartExecuted = true;
				m_currentState.Start();
			}

			m_currentState.Update();
		}
	}

	// Update in a fixed time basis
	public virtual void FixedUpdate () 
	{
		if(null != m_currentState)
		{
			if(!m_currentState.StartExecuted)	
			{
				m_currentState.StartExecuted = true;
				m_currentState.Start();
			}

			m_currentState.FixedUpdate();
		}
	}

	// Update at the end of the current frame
	public virtual void LateUpdate () 
	{
		if(null != m_currentState)
		{
			m_currentState.LateUpdate();
		}
	}

	public int StackSize
	{
		get{ return m_stack.Count;}
	}

	public bool IsInState(string type)
	{
		return m_currentState.ToString() == type;	
	}
}
