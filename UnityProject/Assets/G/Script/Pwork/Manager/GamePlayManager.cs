using UnityEngine;
using System.Collections;

public class GamePlayManager : MonoBehaviour {
	private static GamePlayManager instance;
	public static GamePlayManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(GamePlayManager)) as GamePlayManager;
				instance.Initiate();
			}
			
			return instance;
		}
	}
	
	void Awake()
	{
		if (instance = null)
		{
			instance = this;
			instance.Initiate();
		}
	}
	
	void OnDestroy()
	{
		instance = this;
	}
	
	protected void Initiate()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	public int DungeonClearCount = 0;
	public int DungeonToBossCount = 3;
	public void DungeonClearEvent()
	{
		DungeonClearCount++;
	}

	public void MoveToNextDungeon()
	{
		if (DungeonClearCount >= DungeonToBossCount) {
			Application.LoadLevel (Constant.Scene_Boss_Lich);
		} else {
			Application.LoadLevel(Constant.Scene_Dungeon_Scene);
		}

	}

}
