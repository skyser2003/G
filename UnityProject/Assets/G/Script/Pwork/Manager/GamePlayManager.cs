using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		if (instance == null)
		{
			instance = this;
			instance.Initiate();
		}

		if(instance != this)
		{
			Destroy(gameObject);
		}
	}
	
	void OnDestroy()
	{
		if(instance == this)
		{
			instance = null;
		}
	}
	
	protected void Initiate()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	public void SceneChange()
	{
		MyPlayer = null;
		PlayerList = new List<GameObjectBase>();
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

	public GameObjectBase MyPlayer;
	public List<GameObjectBase> PlayerList = new List<GameObjectBase>();

	public void SetMyPlayer(GameObjectBase _myplayer)
	{
		MyPlayer = _myplayer;
		AddPlayer(_myplayer);
	}

	public void AddPlayer(GameObjectBase _go)
	{
		if(!PlayerList.Contains(_go))
		{
			PlayerList.Add(_go);
		}
	}
}
