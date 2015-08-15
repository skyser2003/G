using UnityEngine;
using System.Collections;

public class Constant{

	public static readonly string Scene_Town = "MainTownScene";
	public static readonly string Scene_Boss_Lich = "Boss_Lich_Map";
	public static readonly string Scene_Dungeon_Scene = "DungeonScene";

	public static readonly string GroundCheckCollider = "GroundPlatform";
	public static readonly string SideCollider = "SideCollider";
	public static readonly string JumpCheckCollider = "JumpCheckCollider";
}

public enum GObjectGroup
{
	MY_PLAYER = 1,
	OPP_PLAYER = 2,
	ENEMY = 4,
	ENEMY_BOSS = 5,
	OBJECT = 6,
	TRAP = 7,
}
