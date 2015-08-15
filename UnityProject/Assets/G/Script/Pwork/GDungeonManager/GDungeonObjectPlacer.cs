using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GDungeonObjectPlacer : MonoBehaviour {

	public List<GDungeonObjectPlacerData> ObjectList = new List<GDungeonObjectPlacerData>();

	public void SpawnRandomObject()
	{
		//place object

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