using UnityEngine;
using System.Collections;

public class PlayerInputGenerator : MonoBehaviour {

	public GameObjectBase Player;

	void Update()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			GInputData data = new GInputData(Player.ID, GInputType.MOVE_LEFT_PRESSED);
			GInputManager.Instance.AddInput(data);
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			GInputData data = new GInputData(Player.ID, GInputType.MOVE_RIGHT_PRESSED);
			GInputManager.Instance.AddInput(data);
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			GInputData data = new GInputData(Player.ID, GInputType.JUMP_DOWN);
			GInputManager.Instance.AddInput(data);
		}

		if(Input.GetKeyDown(KeyCode.A))
		{
			GInputData data = new GInputData(Player.ID, GInputType.KEY_1_DOWN);
			GInputManager.Instance.AddInput(data);
		}

		if(Input.GetKeyUp(KeyCode.A))
		{
			GInputData data = new GInputData(Player.ID, GInputType.KEY_1_RELEASE);
			GInputManager.Instance.AddInput(data);
		}
	}
}
