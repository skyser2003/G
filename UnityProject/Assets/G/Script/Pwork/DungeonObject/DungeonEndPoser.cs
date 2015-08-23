using UnityEngine;
using System.Collections;

public class DungeonEndPoser : MonoBehaviour {

	public void Activate()
	{
		DungeonStageManager.Instance.DungeonClear ();
	}


	void OnTriggerEnter2D(Collider2D _col)
	{
		if(_col.gameObject.layer == LayerMask.NameToLayer(Constant.Layer_Player))
		{
			if(DungeonStageManager.Instance != null)
			{
				DungeonStageManager.Instance.DungeonClear();
			}
		}
	}
}
