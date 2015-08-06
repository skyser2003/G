using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction
{
	LEFT_BOT = 0,
	MID_BOT = 1,
	RIGHT_BOT = 2,
	LEFT_MID = 3,
	MID_MID = 4,
	RIGHT_MID = 5,
	LEFT_TOP = 6,
	MID_TOP = 7,
	RIGHT_TOP = 8,
}

public class GDungeonPart : MonoBehaviour
{
	public List<GDungeonObjectPlacer> ObjectPlacerList = new List<GDungeonObjectPlacer>();
	public List<int> OpenIndex = new List<int>();

}