using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GPlayerInputButton : MonoBehaviour {

	public bool Pressed = false;
	public bool Hold = false;
	public bool Release = false;

	void Update()
	{

	}

	public void ResetInput()
	{
		Pressed = false;
		Release = false;
	}

	public void PointerEnter()
	{
		if(!Hold)
		{
			Pressed = true;
		}
		Hold = true;
	}

	public void PointerExit()
	{
		if(Hold)
		{
			//Debug.Log("set release");
			Release = true;
		}
		Hold = false;
	}
}
