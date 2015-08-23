using UnityEngine;
using System.Collections;

public class BossStageManager : MonoBehaviour {

	private static BossStageManager instance;
	public static BossStageManager Instance
	{
		get{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(BossStageManager)) as BossStageManager;
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
	}
}
