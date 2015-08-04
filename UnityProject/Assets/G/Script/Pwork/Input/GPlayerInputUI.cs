using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GPlayerInputUI : MonoBehaviour {

	public GameObjectBase Player;

	public GPlayerInputButton Left;
	public GPlayerInputButton Right;
	public GPlayerInputButton Dodge;
	public GPlayerInputButton Jump;
	public GPlayerInputButton Attack;

	protected List<GInputData> SavedInputList = new List<GInputData>();
	void Update()
	{
		SavedInputList.Clear();
		if(Left.Hold)
		{
			GInputData data = new GInputData(Player.ID, GInputType.MOVE_LEFT_PRESSED);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}
		
		if(Right.Hold)
		{
			GInputData data = new GInputData(Player.ID, GInputType.MOVE_RIGHT_PRESSED);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}
		
		if(Jump.Pressed)
		{
			GInputData data = new GInputData(Player.ID, GInputType.JUMP_DOWN);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}

		if(Jump.Release)
		{
			GInputData data = new GInputData(Player.ID, GInputType.JUMP_RELEASE);
			SavedInputList.Add(data);
		}
		
		if(Attack.Pressed)
		{
			GInputData data = new GInputData(Player.ID, GInputType.KEY_1_DOWN);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}else if(Attack.Release)
		{
			GInputData data = new GInputData(Player.ID, GInputType.KEY_1_RELEASE);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}else if(Attack.Hold)
		{
			GInputData data = new GInputData(Player.ID, GInputType.KEY_1_PRESSED);
			SavedInputList.Add(data);
			//GInputManager.Instance.AddInput(data);
		}

		Left.ResetInput();
		Right.ResetInput();
		Jump.ResetInput();
		Attack.ResetInput();
		Dodge.ResetInput();
	}
	
	void FixedUpdate()
	{
		for(int iter = 0; iter < SavedInputList.Count; iter++)
		{
			GInputManager.Instance.AddInput(SavedInputList[iter]);
		}
	}
}
