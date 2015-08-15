using UnityEngine;
using System.Collections;

public class DungeonStageManager : MonoBehaviour {

	private static DungeonStageManager instance;
	public static DungeonStageManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(DungeonStageManager)) as DungeonStageManager;
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
		GDungeonManager.Instance.CreateDungeon ();

		//find player
		Transform playerpos = FindObjectOfType (typeof(PlayerSpawnPoser)) as Transform;
		PlayerManager.Instance.SetPlayerInitPos (playerpos.position);
	}


	public void DungeonClear()
	{
		GamePlayManager.Instance.DungeonClearEvent ();
		GamePlayManager.Instance.MoveToNextDungeon ();
	}

}
