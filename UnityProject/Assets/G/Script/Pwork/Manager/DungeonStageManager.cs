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
		if (instance == null)
		{
			instance = this;
			instance.Initiate();
		}
	}
	
	void OnDestroy()
	{
		instance = null;
	}

	public CameraSystem CameraSystem;
	public GPlayerInputUI InputSystem;

	protected void Initiate()
	{
		Debug.Log("instantiate dungeonmanager");
		GDungeonManager.Instance.Create ();

		//find player
		Transform playerpos = (FindObjectOfType (typeof(PlayerSpawnPoser)) as PlayerSpawnPoser).transform;
		PlayerManager.Instance.CreatePlayerInitPos (playerpos.position);

		//create player
		CameraSystem.TargetTransform = PlayerManager.Instance.PlayerGameObject.transform;
		InputSystem.Player = PlayerManager.Instance.PlayerGameObject;
	}


	public void DungeonClear()
	{
		GamePlayManager.Instance.DungeonClearEvent ();
		GamePlayManager.Instance.MoveToNextDungeon ();
	}

}
