using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction
{
	LEFT = 0,
	BOT = 1,
	RIGHT = 2,
	TOP = 3,
}

public enum PartType
{
	MAIN = 0,
	SIDE = 1,
}

public class GDungeonPart : MonoBehaviour
{
	public static float MustOpenLength = 4;

	public List<GDungeonObjectPlacer> ObjectPlacerList = new List<GDungeonObjectPlacer>();
	public List<Direction> InDirectionList = new List<Direction>();
	public List<Direction> OutDirectionList = new List<Direction>();
	public float Width = 30f;
	public float Height = 30f;

	void OnDrawGizmos()
	{
		Color c = Color.red;
		c.a = 0.1f;
		Gizmos.color = c;
		Gizmos.DrawCube(transform.position, new Vector3(30f,30f));

		Color opencolor = Color.green;
		opencolor.a = 0.1f;
		Gizmos.color = opencolor;

		Vector3 pos = transform.position;
		pos = transform.position + Vector3.right * (Width / 2f - MustOpenLength / 2f);
		Gizmos.DrawCube(pos, new Vector3(MustOpenLength, MustOpenLength));

		pos = transform.position - Vector3.right * (Width / 2f - MustOpenLength / 2f);
		Gizmos.DrawCube(pos, new Vector3(MustOpenLength, MustOpenLength));

		pos = transform.position + Vector3.up * (Height / 2f - MustOpenLength / 2f);
		Gizmos.DrawCube(pos, new Vector3(MustOpenLength, MustOpenLength));

		pos = transform.position - Vector3.up * (Height / 2f - MustOpenLength / 2f);
		Gizmos.DrawCube(pos, new Vector3(MustOpenLength, MustOpenLength));

	}

	public virtual void Create(PartType _type, int _curlength, int _totallength)
	{
		for(int iter = 0; iter < ObjectPlacerList.Count; iter++)
		{
			GDungeonObjectPlacer placer = ObjectPlacerList[iter];
			placer.SpawnRandomObject();
		}	
	}
}