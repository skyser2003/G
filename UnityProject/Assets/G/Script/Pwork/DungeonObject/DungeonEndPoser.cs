using UnityEngine;
using System.Collections;

public class DungeonEndPoser : MonoBehaviour {

	public void Activate()
	{
		DungeonStageManager.Instance.DungeonClear ();
	}

}
