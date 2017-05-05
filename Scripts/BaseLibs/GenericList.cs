using System.Collections;
using UnityEngine;

public class GenericList<T>: System.Collections.Generic.List<T>{

	public T Back{

		get{ return this.Back; }
	}	

	public void PushBack(T t){

		Add(t);
	}

	public void PopBack(){

		if(0 < Count){
			
			RemoveAt(Count - 1);
		}
	}

	public void PushFront(T t){

		Insert(0, t);
	}
}