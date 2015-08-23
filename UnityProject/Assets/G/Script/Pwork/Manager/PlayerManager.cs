using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	private static PlayerManager instance;
	public static PlayerManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
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

	protected void Initiate()
	{
		Debug.Log("Create");
		PlayerGameObject = (Instantiate (PlayerObject) as GameObject).GetComponent<GameObjectBase>();
	}

	public Object PlayerObject;
	public GameObjectBase PlayerGameObject;

	public void CreatePlayerInitPos(Vector3 _playerpos)
	{
		PlayerGameObject.transform.position = _playerpos;
	}

}
