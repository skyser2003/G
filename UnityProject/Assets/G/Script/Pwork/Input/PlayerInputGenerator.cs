using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputGenerator : MonoBehaviour {

	public GameObjectBase Player;

	protected bool ForceAddInput = false;
	protected List<GInputData> SavedInputList = new List<GInputData>();

	void Start()
	{

	}

	void Update()
	{
		SavedInputList.Clear();
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			GInputData data = new GInputData(Player.ID, GInputType.MOVE_LEFT_PRESSED);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			GInputData data = new GInputData(Player.ID, GInputType.MOVE_RIGHT_PRESSED);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			GInputData data = new GInputData(Player.ID, GInputType.JUMP_DOWN);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}

		if(Input.GetKeyUp(KeyCode.Space))
		{
			GInputData data = new GInputData(Player.ID, GInputType.JUMP_RELEASE);
			SavedInputList.Add(data);
			Debug.Log("release jump input");
		}

		if(Input.GetKeyDown(KeyCode.A))
		{
			GInputData data = new GInputData(Player.ID, GInputType.KEY_1_DOWN);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}else if(Input.GetKey(KeyCode.A))
		{
			GInputData data = new GInputData(Player.ID, GInputType.KEY_1_PRESSED);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}else if(Input.GetKeyUp(KeyCode.A))
		{
			GInputData data = new GInputData(Player.ID, GInputType.KEY_1_RELEASE);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}
		Process();
		ForceAddInput = true;
	}

	void FixedUpdate()
	{
		if(!ForceAddInput)
		{
			Process();
		}else
		{
			ForceAddInput = false;
		}
	}

	protected virtual void Process()
	{
		for(int iter = 0; iter < SavedInputList.Count; iter++)
		{
			GInputManager.Instance.AddInput(SavedInputList[iter]);
		}
	}
}
