using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectManager : MonoBehaviour {

	private static GameObjectManager instance;
	public static GameObjectManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(GameObjectManager)) as GameObjectManager;
			}
			
			return instance;
		}
	}

	void FixedUpdate()
	{
		for(int iter = 0; iter < GameObjectList.Count; iter++)
		{
			GameObjectBase curobject = GameObjectList[iter];
			curobject.Process(Time.deltaTime);
		}
	}

	public List<GameObjectBase> GameObjectList = new List<GameObjectBase>();
	public void AddToObjectList(GameObjectBase _base)
	{
		GameObjectList.Add(_base);
	}
}
