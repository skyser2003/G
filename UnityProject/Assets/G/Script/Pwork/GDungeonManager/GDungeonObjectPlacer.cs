using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GDungeonObjectPlacer : MonoBehaviour {

	public List<GDungeonObjectPlacerData> ObjectList = new List<GDungeonObjectPlacerData>();

	public void SpawnRandomObject()
	{
		//place object
		int randomindex = Random.Range(0, ObjectList.Count);

		Object selected = ObjectList[randomindex].Prefab;

		if(selected != null)
		{
			GameObject go = Instantiate(selected, transform.position + ObjectList[randomindex].DeltaPos, Quaternion.identity) as GameObject;
			//go.transform.position = transform.position + ObjectList[randomindex].DeltaPos;
			//Debug.Log("WTF!??!??!!?1?!>?!?: " + go.transform.position);
			Debug.Log("Spawn object");
		}
	}

	public void OnDrawGizmosSelected ()
	{
		Color color = Color.yellow;
		color.a = 0.1f;
		Gizmos.color = color;
		for(int iter = 0; iter < ObjectList.Count; iter++)
		{
			GDungeonObjectPlacerData data = ObjectList[iter];

			Gizmos.DrawCube(transform.position + data.DeltaPos, Vector3.one);
		}
	}
}

[System.Serializable]
public struct GDungeonObjectPlacerData
{
	public Object Prefab;
	public Vector3 DeltaPos;
}