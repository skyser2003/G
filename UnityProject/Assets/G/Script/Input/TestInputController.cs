using UnityEngine;
using System.Collections;

public class TestInputController : MonoBehaviour {

	public MoveObject Test;

	void Update()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			Test.Move(true);
		}else if(Input.GetKey(KeyCode.RightArrow))
		{
			Test.Move(false);
		}

		if(Input.GetKey(KeyCode.Space))
		{
			Test.Jump();
		}

	}
}
