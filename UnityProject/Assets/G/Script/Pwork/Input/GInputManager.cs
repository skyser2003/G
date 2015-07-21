using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GInputManager : MonoBehaviour {

	private static GInputManager instance;
	public static GInputManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(GInputManager)) as GInputManager;
			}

			return instance;
		}
	}
	
	protected void LateUpdate()
	{
		//for(int iter = 0; iter < InputObjectList.Count; iter++)
		//{
		//	GameObjectBase curobject = InputObjectList[iter];
		//	curobject.ClearInput ();
		//}
	}

	public List<GameObjectBase> InputObjectList = new List<GameObjectBase>();
	public void AddInputObject(GameObjectBase _object)
	{
		InputObjectList.Add(_object);
	}

	public void AddInput(GInputData _data)
	{
		for(int iter = 0; iter < InputObjectList.Count; iter++)
		{
			GameObjectBase curobject = InputObjectList[iter];
			if(curobject.ID == _data.Id)
			{
				curobject.AddInput(_data.InputType);
			}
		}
	}
}

public struct GInputData
{
	public int Id;
	public GInputType InputType;

	public GInputData(int _id, GInputType _type)
	{
		Id = _id;
		InputType = _type;
	}

	public override string ToString ()
	{
		return "GInputData id: " + Id + " type: " + InputType.ToString();
	}
}

public enum GInputType
{
	NONE,
	MOVE_LEFT_DOWN,
	MOVE_LEFT_PRESSED,
	MOVE_LEFT_RELEASE,
	MOVE_RIGHT_DOWN,
	MOVE_RIGHT_PRESSED,
	MOVE_RIGHT_RELEASE,
	JUMP_DOWN,
	JUMP_PRESS,
	JUMP_RELEASE,

	KEY_1_DOWN,
	KEY_1_PRESSED,
	KEY_1_RELEASE,
	KEY_2_DOWN,
	KEY_2_PRESSED,
	KEY_2_RELEASE,
	KEY_3_DOWN,
	KEY_3_PRESSED,
	KEY_3_RELEASE,
	KEY_4_DOWN,
	KEY_4_PRESSED,
	KEY_4_RELEASE,
	KEY_5_DOWN,
	KEY_5_PRESSED,
	KEY_5_RELEASE,

}
