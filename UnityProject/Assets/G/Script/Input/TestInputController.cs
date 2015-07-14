using UnityEngine;
using System.Collections;

public class TestInputController : MonoBehaviour {

	public MoveObjectBase Test;

	void Update()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			Test.Move(true);
		}else if(Input.GetKey(KeyCode.RightArrow))
		{
			Test.Move(false);
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			Test.Jump();
		}

	}
}
