using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GDungeonManager : MonoBehaviour {

	private static GDungeonManager instance;
	public static GDungeonManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(GDungeonManager)) as GDungeonManager;
			}

			return instance;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject CurCreatedDungeon;

	public List<GDungeonPart> DungeonPartList = new List<GDungeonPart>();
	public int DungeonWidht;
	public int DungeonHeight;
	public void CreateDungeon()
	{
		//make path of 16
		//start is left_bot and end is right_bot
		//set main path first.
		//then create sub paths.
		//
		// height - 1
		// .
		// width + 1 ....
		// 0  1  2  ... width


		int startindex = 0;
		int endindex = 0;

		List<int> MainPath = new List<int>();
	}
}
